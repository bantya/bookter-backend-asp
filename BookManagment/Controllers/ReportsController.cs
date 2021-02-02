using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using Image = iText.Layout.Element.Image;
using Rectangle = iText.Kernel.Geom.Rectangle;

namespace BookManagment.Controllers
{
    public class ReportsController : Controller
    {
        [HttpPost]
        [Route("order/{token}/report")]
        public ActionResult Get(string token)
        {
            MemoryStream ms = new MemoryStream();

            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.LETTER);

            doc.SetMargins(75, 35, 70, 35);

            string pathLogo = Server.MapPath("~/Content/logo.jpg");
            Image img = new Image(ImageDataFactory.Create(pathLogo));
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(img));
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler());


            Table table = new Table(1).UseAllAvailableWidth();
            Cell cell = new Cell().Add(new Paragraph("INVOICE").SetFontSize(14)).SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER);
            table.AddCell(cell);

            doc.Add(table);

            Table _table = new Table(1).UseAllAvailableWidth();
            _table.AddCell(new Cell().Add(new Paragraph("For,")).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER));
            _table.AddCell(new Cell().Add(new Paragraph("Aditya Gaikwad")).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER));
            _table.AddCell(new Cell().Add(new Paragraph("Akurdi, Pune")).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER));


            doc.Add(_table);

            Style styleCell = new Style().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER);

            _table = new Table(5).UseAllAvailableWidth();
            Cell _cell = new Cell().Add(new Paragraph("#")).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1)).SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            _table.AddHeaderCell(_cell.AddStyle(styleCell));
            _cell = new Cell().Add(new Paragraph("Book Name")).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1)).SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            _table.AddHeaderCell(_cell.AddStyle(styleCell));
            _cell = new Cell().Add(new Paragraph("Quantity")).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1)).SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            _table.AddHeaderCell(_cell.AddStyle(styleCell));
            _cell = new Cell().Add(new Paragraph("Price")).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1)).SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            _table.AddHeaderCell(_cell.AddStyle(styleCell));
            _cell = new Cell().Add(new Paragraph("Total")).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1)).SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            _table.AddHeaderCell(_cell.AddStyle(styleCell));

            Customer customer = (Customer)this.Session["user"];
            List<OrderBooks> books = BussinessManager.getOrderBooks(token, customer.customerid);

            int x = 0;
            double total = 0;

            foreach (OrderBooks book in books)
            {
                x++;
                _cell = new Cell().Add(new Paragraph(x.ToString())).SetBorder(Border.NO_BORDER);
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(book.book_name)).SetBorder(Border.NO_BORDER);
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(book.quantity.ToString()).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER);
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph("₹ " + book.price.ToString()).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER);
                _table.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph("₹ " + (book.price * book.quantity)).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER);
                _table.AddCell(_cell);
                total += book.price * book.quantity;
            }

            doc.Add(_table);

            _table = new Table(1).UseAllAvailableWidth();
            _table.AddCell(new Cell().Add(new Paragraph("Total: " + total)).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1)).SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1)));

            doc.Add(_table);

            doc.Close();

            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        public ActionResult GetBasicPDF()
        {
            MemoryStream ms = new MemoryStream();

            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.LETTER);

            doc.SetMargins(75, 35, 70, 35);

            //string nameFont = Server.MapPath("/path/to/font.ttf");
            //PdfFont font = PdfFontFactory.CreateFont(nameFont);
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.HELVETICA);

            Style styles = new Style()
                .SetFontColor(ColorConstants.BLUE)
                .SetFont(font)
                .SetFontSize(24)
                .SetBackgroundColor(ColorConstants.PINK);

            doc.Add(new Paragraph("Namaste India").AddStyle(styles));

            doc.Close();

            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        [HttpGet]
        public ActionResult Download()
        {
            // read from the string
            //StringReader sr = new StringReader(this.GetData().ToString());

            //// building the PDF document with default page size of A4 Page size.
            //Document pdfDoc = new Document();

            //// parse the HTML string using HTMLWorker of Itextsharp library,
            //iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);

            //var cssFile = System.IO.File.ReadAllText(Server.MapPath("~/Content/doc.css"));
            //var htmlFile = System.IO.File.ReadAllText(Server.MapPath("~/Content/report.html"));

            //using (var memoryStream = new MemoryStream())
            //{
            //    var document = new Document(PageSize.A4, 50, 50, 60, 60);
            //    var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);
            //    document.Open();

            //    using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssFile)))
            //    {
            //        using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlFile)))
            //        {
            //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
            //        }
            //    }

            //    document.Close();

            //    byte[] pdf = memoryStream.ToArray();

            //    Response.Clear();
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("Content-Disposition", "attachment; filename=Invoice.pdf");
            //    Response.Buffer = true;
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.BinaryWrite(pdf);
            //    Response.End();
            //    Response.Close();
            //}

            // use the memory stream to reside the file in-memory. 
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //    pdfDoc.Open();

            //    htmlparser.Parse(sr);
            //    pdfDoc.Close();

            //    byte[] bytes = memoryStream.ToArray();
            //    memoryStream.Close();

            //    Response.Clear();
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("Content-Disposition", "attachment; filename=Invoice.pdf"); 
            //    Response.Buffer = true;
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.BinaryWrite(bytes);
            //    Response.End();
            //    Response.Close();
            //}

            return View();
        }
    }

    public class HeaderEventHandler : IEventHandler
    {
        private Image img;

        public HeaderEventHandler(Image img)
        {
            this.img = img;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas1 = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 75, page.GetPageSize().GetWidth() - 50, 60);
            new Canvas(canvas1, pdfDoc, rootArea).Add(getTable(docEvent));
        }

        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 20f, 80f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            Style styleCell = new Style().SetBorder(Border.NO_BORDER);
            Style styleText = new Style().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

            Cell cell = new Cell().Add(img.SetAutoScale(true));

            tableEvent.AddCell(cell.AddStyle(styleCell).SetTextAlignment(TextAlignment.LEFT));

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            cell = new Cell()
                .Add(new Paragraph("Bookter.in\n").SetFont(bold))
                .Add(new Paragraph("Be good, read good!\n").SetFont(bold))
                .Add(new Paragraph(DateTime.Now.ToShortDateString()))
                .AddStyle(styleText)
                .AddStyle(styleCell);

            tableEvent.AddCell(cell);

            return tableEvent;
        }
    }

    public class FooterEventHandler : IEventHandler
    {
        public void HandleEvent(Event @event)
        {

            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas1 = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new Rectangle(35, 20, page.GetPageSize().GetWidth() - 70, 50);
            new Canvas(canvas1, pdfDoc, rootArea).Add(getTable(docEvent));
        }

        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 92f, 8f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            PdfPage page = docEvent.GetPage();
            int pageNum = docEvent.GetDocument().GetPageNumber(page);

            Style styleCell = new Style().SetPadding(5).SetBorder(Border.NO_BORDER).SetBorderTop(new SolidBorder(ColorConstants.BLACK, 2));

            Cell cell = new Cell().Add(new Paragraph(DateTime.Now.ToLongDateString()));

            tableEvent.AddCell(cell.AddStyle(styleCell).SetTextAlignment(TextAlignment.RIGHT).SetFontColor(ColorConstants.LIGHT_GRAY));

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            cell = new Cell().Add(new Paragraph(pageNum.ToString()));
            cell.AddStyle(styleCell).SetBackgroundColor(ColorConstants.BLACK).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER);

            tableEvent.AddCell(cell);

            return tableEvent;
        }
    }
}
