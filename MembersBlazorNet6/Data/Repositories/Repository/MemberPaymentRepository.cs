using System;
using MembersBlazorNet6.Data.Models.MemberModels;
using MembersBlazorNet6.Data.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MembersBlazorNet6.Data.Repositories.Repository
{
    public class MemberPaymentRepository : IMemberPaymentRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public MemberPaymentRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> CreatePayment(MemberPayment payment)
        {
            if (payment != null)
            {
                try
                {
                    using var _context = _contextFactory.CreateDbContext();
                    _context.MemberPayment.Add(payment);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeletePayment(int? paymentId)
        {
            if (paymentId != null)
            {
                try
                {
                    using var _context = _contextFactory.CreateDbContext();
                    var paymentToDelete = await _context.MemberPayment.FindAsync(paymentId);
                    if (paymentToDelete != null)
                    {
                        _context.MemberPayment.Remove(paymentToDelete);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<IList<MemberPayment>> GetPaymentsByMemberId(int? memberId)
        {
            if (memberId != null)
            {
                try
                {
                    using var _context = _contextFactory.CreateDbContext();
                    var paymentList = await _context.MemberPayment
                        .Where(p => p.MemberId == memberId)
                        .ToListAsync();
                    return paymentList;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<IList<MemberPayment>> GetPaymentByMemberIdAndYear(int? memberId, int? year)
        {
            if (memberId != null && year != null)
            {
                try
                {
                    using var _context = _contextFactory.CreateDbContext();
                    var paymentList = await _context.MemberPayment
                        .Where(p => p.MemberId == memberId && p.PaymentDate.Value.Year == year)
                        .OrderBy(p => p.PaymentDate)
                        .ToListAsync();
                    return paymentList;
                }
                catch
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdatePayment(MemberPayment payment)
        {
            if (payment != null)
            {
                try
                {
                    using var _context = _contextFactory.CreateDbContext();
                    _context.MemberPayment.Update(payment);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

