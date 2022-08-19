using iText.Html2pdf;
using System;
using System.IO;
using System.Reflection;

namespace Security.Transversal.Common.Helpers
{
    public static class PdfHelper
    {
        public static MemoryStream ConvertToPdf(string htmlString)
        {
            var fileName = DateTime.Now.ToShortDateString().Replace('/', '_') + "-" +
                           DateTime.Now.ToLongTimeString().Replace(':', '_') + ".pdf";
            var tempPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            HtmlConverter.ConvertToPdf(htmlString, new FileStream(tempPath, FileMode.Create));
            var stream = new MemoryStream(File.ReadAllBytes(tempPath));
            File.Delete(tempPath);
            return stream;
        }
    }
}
