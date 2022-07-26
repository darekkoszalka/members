using System;
using ClosedXML.Excel;
using MembersBlazorNet6.Data.Models.MemberModels;
using Microsoft.JSInterop;

namespace MembersBlazorNet6.Services.ExportToExcel
{
    public class ExcelService : IExcelService
    {
        private readonly IJSRuntime _jsRuntime;
        public ExcelService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task<bool> PaymentsToExcel(IList<MemberPayment> payments, Member member)
        {
            if (payments != null && member != null)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet =
                        workbook.Worksheets.Add($"{member.FullName}_Składki");
                        worksheet.Cell(1, 1).Value = "Data";
                        worksheet.Cell(1, 2).Value = "Składka";

                        for (int i = 1; i <= 2; i++)
                        {
                            worksheet.Cell(1, i).Style.Font.Bold = true;
                        }

                        int index = 1;
                        foreach (var item in payments)
                        {
                            worksheet.Cell(index + 1, 1).Value = item.PaymentDate.Value.ToString("dd.MM.yyyy");
                            worksheet.Cell(index + 1, 2).Value = item.Payment;
                            index++;
                        }

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            var fileName = $"{member.FullName}_Składki.xlsx";
                            await _jsRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(content));
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            { return false; }
        }
    }
}

