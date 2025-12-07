using Application.DTOs;
using Domain.Entities;

namespace Communication.Mappings
{
    public static class LeadMappings
    {
        public static LeadDto ToDto(this Lead l)
        {
            if (l == null) return null;
            return new LeadDto
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
}
