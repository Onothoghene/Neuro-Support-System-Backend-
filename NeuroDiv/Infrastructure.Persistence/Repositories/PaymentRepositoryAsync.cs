using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PaymentRepositoryAsync : GenericRepositoryAsync<Payment>, IPaymentRepositoryAsync
    {
        private readonly DbSet<Payment> _payments;

        public PaymentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _payments = dbContext.Set<Payment>();
        }

        //public async Task<Payment> GetPaymentByIdAsync(int id)
        //{
        //    return await _payments.FindAsync(id);
        //}

        public async Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(string orderId)
        {
            Guid orderIdGuid = Guid.Parse(orderId);
            return await _payments.Where(p => p.Id == orderIdGuid)
                                  // .Include(x => x.Order)
                                  .ToListAsync();
        }

        public async Task<Payment> GetPaymentByOrderIdAsync(string orderId)
        {
            Guid orderIdGuid = Guid.Parse(orderId);
            return await _payments.Where(p => p.Id == orderIdGuid).FirstOrDefaultAsync();
        }

    }
}
