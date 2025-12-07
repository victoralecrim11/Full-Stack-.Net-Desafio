using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class LeadService
    {
        private readonly ILeadRepository _repo;
        private readonly IEmailService _email;

        public LeadService(ILeadRepository repo, IEmailService email)
        {
            _repo = repo;
            _email = email;
        }

        public async Task<IEnumerable<LeadDto>> GetNewLeadsAsync()
        {
            var leads = await _repo.GetByStatusAsync(LeadStatus.New);
            return leads.Select(Map);
        }

        public async Task<IEnumerable<LeadDto>> GetAcceptedLeadsAsync()
        {
            var leads = await _repo.GetByStatusAsync(LeadStatus.Accepted);
            return leads.Select(Map);
        }

        public async Task AcceptLeadAsync(Guid id)
        {
            var lead = await _repo.GetByIdAsync(id);
            if (lead == null) throw new KeyNotFoundException("Lead not found");

            lead.Accept();
            await _repo.UpdateAsync(lead);

            await _email.SendSaleNotificationAsync("vendas@test.com", "Lead accepted",
                $"Lead {lead.Id} accepted. Final price: {lead.Price}");
        }

        public async Task DeclineLeadAsync(Guid id)
        {
            var lead = await _repo.GetByIdAsync(id);
            if (lead == null) throw new KeyNotFoundException("Lead not found");

            lead.Decline();
            await _repo.UpdateAsync(lead);
        }

        private LeadDto Map(Lead l) => new LeadDto
        {
            Id = l.Id,
            FirstName = l.FirstName,
            LastName = l.LastName,
            Phone = l.Phone,
            Email = l.Email,
            Suburb = l.Suburb,
            Category = l.Category,
            Description = l.Description,
            Price = l.Price,
            CreatedAt = l.CreatedAt
        };
    }
}
