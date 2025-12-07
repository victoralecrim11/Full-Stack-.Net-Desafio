using System;

namespace Domain.Entities
{
    public class Lead
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Suburb { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public LeadStatus Status { get; private set; }

        protected Lead() { }

        public Lead(Guid id, string firstName, string lastName, string phone, string email,
                    string suburb, string category, string description, decimal price, DateTime createdAt)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName;
            Phone = phone;
            Email = email;
            Suburb = suburb;
            Category = category;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            Status = LeadStatus.New;
        }

        public void Accept()
        {
            if (Status != LeadStatus.New)
                throw new InvalidOperationException("Only new leads can be accepted.");

            if (Price > 500m)
                Price = Math.Round(Price * 0.9m, 2);

            Status = LeadStatus.Accepted;
        }

        public void Decline()
        {
            if (Status != LeadStatus.New)
                throw new InvalidOperationException("Only new leads can be declined.");

            Status = LeadStatus.Declined;
        }
    }
}
