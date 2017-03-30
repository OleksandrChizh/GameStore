using System.IO;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GameStore.WebUI.PaymentStrategy
{
    public class BankPayment : IPaymentStrategy
    {
        private readonly string[] _paragraphs;

        public BankPayment(string[] paragraphs)
        {
            _paragraphs = paragraphs;
        }

        public ActionResult GetActionResult()
        {
            var workStream = new MemoryStream();
            var document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            document.Open();
            foreach (string paragraph in _paragraphs)
            {
                document.Add(new Paragraph(paragraph));
            }

            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }
    }
}