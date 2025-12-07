using Xunit;
using Moq;
using Domain.Entities;
using Domain.Repositories;
using Application.Interfaces;
using Application.Services;

namespace Tests
{
    public class LeadManagementTests
    {
        // -----------------------------------------
        //  Helper para criar Lead válido
        // -----------------------------------------
        private Lead CriarLead(decimal preco)
        {
            return new Lead(
                Guid.NewGuid(),
                "Joao",
                "Silva",
                "1199999",
                "teste@teste.com",
                "Centro",
                "Categoria",
                "Descrição",
                preco,
                DateTime.UtcNow
            );
        }

        // -----------------------------------------
        //  Aceitar lead com desconto
        // -----------------------------------------
        [Fact]
        public async Task AcceptLead_AppliesDiscount_WhenPriceAbove500()
        {
            var lead = CriarLead(600m);

            var repoMock = new Mock<ILeadRepository>();
            repoMock.Setup(r => r.GetByIdAsync(lead.Id)).ReturnsAsync(lead!);

            var emailMock = new Mock<IEmailService>();

            var service = new LeadService(repoMock.Object, emailMock.Object);

            await service.AcceptLeadAsync(lead.Id);

            Assert.Equal(LeadStatus.Accepted, lead.Status);
            Assert.Equal(540m, lead.Price); // 600 * 0.9

            repoMock.Verify(r => r.UpdateAsync(It.IsAny<Lead>()), Times.Once);
            emailMock.Verify(
                e => e.SendSaleNotificationAsync("vendas@test.com", It.IsAny<string>(), It.IsAny<string>()),
                Times.Once
            );
        }

        // -----------------------------------------
        //  Aceitar lead sem desconto
        // -----------------------------------------
        [Fact]
        public async Task AcceptLead_DoesNotApplyDiscount_WhenPriceBelow500()
        {
            var lead = CriarLead(300m);

            var repoMock = new Mock<ILeadRepository>();
            repoMock.Setup(r => r.GetByIdAsync(lead.Id)).ReturnsAsync(lead);

            var emailMock = new Mock<IEmailService>();

            var service = new LeadService(repoMock.Object, emailMock.Object);

            await service.AcceptLeadAsync(lead.Id);

            Assert.Equal(LeadStatus.Accepted, lead.Status);
            Assert.Equal(300m, lead.Price);

            repoMock.Verify(r => r.UpdateAsync(lead), Times.Once);
            emailMock.Verify(
                e => e.SendSaleNotificationAsync("vendas@test.com", It.IsAny<string>(), It.IsAny<string>()),
                Times.Once
            );
        }

        // -----------------------------------------
        //  Recusar lead
        // -----------------------------------------
        [Fact]
        public async Task DeclineLead_ChangesStatusToDeclined()
        {
            var lead = CriarLead(100m);

            var repoMock = new Mock<ILeadRepository>();
            repoMock.Setup(r => r.GetByIdAsync(lead.Id)).ReturnsAsync(lead);

            var emailMock = new Mock<IEmailService>();

            var service = new LeadService(repoMock.Object, emailMock.Object);

            await service.DeclineLeadAsync(lead.Id);

            Assert.Equal(LeadStatus.Declined, lead.Status);
            repoMock.Verify(r => r.UpdateAsync(lead), Times.Once);
        }

        // -----------------------------------------
        //  Lead não encontrado
        // -----------------------------------------
        [Fact]
        public async Task AcceptLead_ShouldThrowException_WhenLeadNotFound()
        {
            var repoMock = new Mock<ILeadRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Lead?)null);

            var emailMock = new Mock<IEmailService>();
            var service = new LeadService(repoMock.Object, emailMock.Object);
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.AcceptLeadAsync(Guid.NewGuid()));
        }

        // -----------------------------------------
        //  Recusar lead inexistente
        // -----------------------------------------
        [Fact]
        public async Task DeclineLead_ShouldThrowException_WhenLeadNotFound()
        {
            var repoMock = new Mock<ILeadRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Lead?)null);

            var emailMock = new Mock<IEmailService>();
            var service = new LeadService(repoMock.Object, emailMock.Object);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
    service.DeclineLeadAsync(Guid.NewGuid()));
        }
    }
}

