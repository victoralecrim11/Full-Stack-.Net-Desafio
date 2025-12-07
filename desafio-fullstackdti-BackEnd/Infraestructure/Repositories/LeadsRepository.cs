using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _ctx;
        public LeadRepository(AppDbContext ctx) { _ctx = ctx; }

        public async Task AddAsync(Lead lead)
        {
            _ctx.Leads.Add(lead);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Lead> GetByIdAsync(Guid id)
        {
            return await _ctx.Leads.FindAsync(id);
        }

        public async Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status)
        {
            return await _ctx.Leads.Where(l => l.Status == status)
                           .OrderByDescending(l => l.CreatedAt)
                           .ToListAsync();
        }

        public async Task UpdateAsync(Lead lead)
        {
            _ctx.Leads.Update(lead);
            await _ctx.SaveChangesAsync();
        }
    }
}
