using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ILeadRepository
    {
        Task<Lead?> GetByIdAsync(Guid id);
        Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status);
        Task AddAsync(Lead lead);
        Task UpdateAsync(Lead lead);
    }
}
