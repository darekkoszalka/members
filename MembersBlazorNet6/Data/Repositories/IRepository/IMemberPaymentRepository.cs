using System;
using MembersBlazorNet6.Data.Models.MemberModels;

namespace MembersBlazorNet6.Data.Repositories.IRepository
{
    public interface IMemberPaymentRepository
    {
        public Task<bool> CreatePayment(MemberPayment payment);

        public Task<bool> UpdatePayment(MemberPayment payment);

        public Task<bool> DeletePayment(int? paymentId);

        public Task<IList<MemberPayment>> GetPaymentsByMemberId(int? memberId);

        public Task<IList<MemberPayment>> GetPaymentByMemberIdAndYear(int? memberId, int? year);
    }
}

