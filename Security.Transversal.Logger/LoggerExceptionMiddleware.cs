using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Security.Transversal.Common;
using Security.Transversal.Common.Enum;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Transversal.Logger
{
    public class LoggerExceptionMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly bool _isRequestResponseLoggingEnabled;
        public LoggerExceptionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _logger = Log.Logger;
            _isRequestResponseLoggingEnabled = configuration.GetValue<bool>("EnableRequestResponseLogger", false);
        }

        public async Task Invoke(HttpContext httpContext /* other scoped dependencies */)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                if (_isRequestResponseLoggingEnabled)
                {
                    _logger.Information(await GetInformationREQUEST(httpContext));

                    // Temporarily replace the HttpResponseStream, which is a write-only stream, with a MemoryStream to capture it's value in-flight.
                    var originalResponseBody = httpContext.Response.Body;
                    using var newResponseBody = new MemoryStream();
                    httpContext.Response.Body = newResponseBody;

                    // Call the next middleware in the pipeline
                    await _next(httpContext);

                    newResponseBody.Seek(0, SeekOrigin.Begin);

                    _logger.Information(await GetInformationRESPONSE(httpContext));

                    newResponseBody.Seek(0, SeekOrigin.Begin);
                    await newResponseBody.CopyToAsync(originalResponseBody);
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                var completeMessage = GetCompleteExceptionMessage(ex);
                _logger.Error($"{completeMessage}\n{ex.StackTrace}");

                httpContext.Response.ContentType = "application/json";

                await using var writer = new StreamWriter(httpContext.Response.Body);
                var jsonSerilizer = new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                if (bool.Parse(_configuration.GetSection("ExceptionSettings:ShowCustomMessage").Value))
                    completeMessage = _configuration.GetSection("ExceptionSettings:CustomMessage").Value;
                jsonSerilizer.Serialize(writer, new Response { Code = (int)CodeResponseEnum.ErrorCritical, Message = completeMessage });

                await writer.FlushAsync().ConfigureAwait(false);
            }

            _logger.Information($"EXECUTION TIME: \"{httpContext.Request.Path.Value}\" ==> {watch.ElapsedMilliseconds} ms");
        }

        private string GetCompleteExceptionMessage(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;
            var errorMessage = $"{ex.Message}\n{GetCompleteExceptionMessage(ex.InnerException)}";

            return errorMessage;
        }

        #region Get Information Request and Response Endpoints

        private async Task<string> GetInformationREQUEST(HttpContext httpContext) {
            return $"HTTP REQUEST:\n" +
                            $"\tMethod: {httpContext.Request.Method}\n" +
                            $"\tPath: {httpContext.Request.Path}\n" +
                            $"\tQueryString: {httpContext.Request.QueryString}\n" +
                            $"\tHeaders: {GetFormatHeaders(httpContext.Request.Headers)}\n" +
                            $"\tSchema: {httpContext.Request.Scheme}\n" +
                            $"\tHost: {httpContext.Request.Host}\n" +
                            $"\tBody: { await ReadBodyFromRequest(httpContext.Request)}";
        }

        private async Task<string> GetInformationRESPONSE(HttpContext httpContext)
        {
            var responseBodyText = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();
            return $"HTTP RESPONSE:\n" +
                        $"\tStatusCode: {httpContext.Response.StatusCode}\n" +
                        $"\tContentType: {httpContext.Response.ContentType}\n" +
                        $"\tHeaders: {GetFormatHeaders(httpContext.Response.Headers)}\n" +
                        $"\tBody: {responseBodyText}";
        }

        private static string GetFormatHeaders(IHeaderDictionary headers) => string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));

        private static async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            // Ensure the request's body can be read multiple times (for the next middlewares in the pipeline).
            request.EnableBuffering();

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            // Reset the request's body stream position for next middleware in the pipeline.
            request.Body.Position = 0;
            return requestBody;
        }
        #endregion

    }
}
