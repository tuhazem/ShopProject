using ClosedXML.Excel;
using QuestPDF.Infrastructure;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Features.Products;
using System;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopProject.Domain.Entities;

namespace ShopProject.Infrastructure.Implementations
{
    public class ExcelService : IExcelService
    {

        public byte[] GenerateProductsReport(IEnumerable<ProdctShortDTO> products)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Inventory Report");

                worksheet.Cell(1, 1).Value = "Product Name";
                worksheet.Cell(1, 2).Value = "Price ";
                worksheet.Cell(1, 3).Value = "Quantity";

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#3f51b5");
                headerRow.Style.Font.FontColor = XLColor.White;

                int currentRow = 2;
                foreach (var item in products)
                {
                    worksheet.Cell(currentRow, 1).Value = item.Name;
                    worksheet.Cell(currentRow, 2).Value = item.Price;
                    worksheet.Cell(currentRow, 3).Value = item.Stock;

                    if (item.Stock < 5)
                        worksheet.Cell(currentRow, 3).Style.Font.FontColor = XLColor.Red;

                    currentRow++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }}



        public byte[] GeneratePdfInvoice(Order order)
        {
            //For Free License
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("SHOP APP").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                            col.Item().Text($"Date: {order.OrderDate:yyyy-MM-dd}");
                            col.Item().Text($"Invoice #: {order.Id}");
                        });

                        row.ConstantItem(100).Height(50).Placeholder(); 
                    });

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);  
                            columns.RelativeColumn();   
                            columns.ConstantColumn(50); 
                            columns.RelativeColumn();   
                            columns.RelativeColumn();   
                        });

                        // عناوين الجدول
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("#");
                            header.Cell().Element(CellStyle).Text("Product");
                            header.Cell().Element(CellStyle).Text("Qty");
                            header.Cell().Element(CellStyle).Text("Price");
                            header.Cell().Element(CellStyle).Text("Total");

                            static IContainer CellStyle(IContainer container) =>
                                container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                        });

                        int i = 1;
                        foreach (var item in order.OrderItems)
                        {
                            table.Cell().Element(Padding).Text($"{i++}");
                            table.Cell().Element(Padding).Text(item.Product.Name);
                            table.Cell().Element(Padding).Text($"{item.Quantity}");
                            table.Cell().Element(Padding).Text($"{item.UnitPrice} EGP");
                            table.Cell().Element(Padding).Text($"{item.Quantity * item.UnitPrice} EGP");

                            static IContainer Padding(IContainer container) => container.PaddingVertical(5);
                        }
                    });

                    page.Footer().Column(col =>
                    {
                        col.Item().AlignRight().Text($"Total Gain: {order.TotalPrice} EGP").FontSize(16).Bold();
                        col.Item().AlignCenter().Text("Thank you for shopping with us!").FontSize(10).Italic();
                    });
                });
            });

            return document.GeneratePdf();
        }

    }
}
