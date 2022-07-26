using System;
using MembersBlazorNet6.Data.Models.MemberModels;

namespace MembersBlazorNet6.Services.ExportToExcel
{

    public interface IExcelService
    {
        public Task<bool> PaymentsToExcel(IList<MemberPayment> payments, Member member);
    }
}

