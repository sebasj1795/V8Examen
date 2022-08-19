
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//using Security.Domain.Interfaces.IRepository;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Security.Transversal.Common.ConsumeServiceExternal
{
    public class ServiceExternal
    {
        //private readonly IMasterDetRepository _masterDetailRepository;
        //private readonly ILogger _logger;

        //public ServiceExternal(IMasterDetRepository masterDetailRepository)
        //{
        //    _masterDetailRepository = masterDetailRepository;
        //}
        //public async Task<TResult> PostAsync<T, TResult>(T request, string detailUrlKey, bool isCamelCaseProperty = false)
        //{
        //    string url = await _masterDetailRepository.GetValueByNameMaster(Constants.TableMaster.URL_SERVICES_EXTERNAL_DEMO, detailUrlKey);

        //    var httpClient = await GetHttpClient();
        //    var stringContent = GetStringContent(request, isCamelCaseProperty);
        //    var response = await httpClient.PostAsync(url, stringContent);

        //    if (response.StatusCode != HttpStatusCode.OK)
        //    {
        //        throw new Exception(response.ReasonPhrase);
        //    }

        //    var content = await response.Content.ReadAsStringAsync();

        //   //_logger.Information($"Response => {content}");

        //    var rpt = JsonConvert.DeserializeObject<TResult>(content);

        //    return rpt;
        //}

        //private async Task<HttpClient> GetHttpClient()
        //{
        //    var credentials = await _masterDetailRepository.GetByNameMaster(Constants.TableMaster.CREDENTIALS_SERVICES_EXTERNAL_DEMO);
        //    string User = credentials.Find(x => x.Name == Constants.TableMasterDet.USER).Value;
        //    string Password = credentials.Find(x => x.Name == Constants.TableMasterDet.PASSWORD).Value;
        //    HttpClientHandler clientHandler = new HttpClientHandler
        //    {
        //        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        //    };

        //    HttpClient httpClient = new HttpClient(clientHandler);
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
        //        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{User}:{Password}")));

        //    return httpClient;
        //}

        //private StringContent GetStringContent<T>(T request, bool isCamelCaseProperty)
        //{
        //    string jsonSerializeObject;

        //    if (isCamelCaseProperty)
        //    {
        //        jsonSerializeObject = JsonConvert.SerializeObject(request, new JsonSerializerSettings
        //        {
        //            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //            NullValueHandling = NullValueHandling.Ignore
        //        });
        //    }
        //    else
        //    {
        //        jsonSerializeObject = JsonConvert.SerializeObject(request);
        //    }

        //    //_logger.Information($"Request => {jsonSerializeObject}");
        //    var stringContent = new StringContent(jsonSerializeObject, Encoding.UTF8, "application/json");

        //    return stringContent;
        //}
    }
}
