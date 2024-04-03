using System.IO;
using System.Net;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace GUI.Helpers;

public class PdfHelper
{
 
    public async Task DownloadAndMergePdf(List<string> pdfUrls, string outputFilePath)
    {
        var mergedPdf = new PdfDocument();

        using (var webClient = new WebClient())
        {
            foreach (var pdfUrl in pdfUrls)
            {
                var tempFile = Path.GetTempFileName();

                await webClient.DownloadFileTaskAsync(new Uri(pdfUrl), tempFile);

                var pdfDocument = PdfReader.Open(tempFile, PdfDocumentOpenMode.Import);

                foreach (var page in pdfDocument.Pages)
                {
                    mergedPdf.AddPage(page);
                }

                File.Delete(tempFile);
            }
        }

        mergedPdf.Save(outputFilePath);
        PdfReader.TestPdfFile(outputFilePath);
    }

}
