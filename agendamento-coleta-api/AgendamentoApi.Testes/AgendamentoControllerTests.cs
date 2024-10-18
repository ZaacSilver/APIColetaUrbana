


using agendamento_coleta_api.Controllers;
using agendamento_coleta_api.service;
using agendamento_coleta_api.model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgendamentoApi.Tests
{
    public class AgendamentoControllerTests
    {
        private readonly Mock<IAgendamentoService> _mockAgendamentoService;
        private readonly AgendamentoController _controller;

        public AgendamentoControllerTests()
        {
            _mockAgendamentoService = new Mock<IAgendamentoService>();
            _controller = new AgendamentoController(_mockAgendamentoService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsStatusCode200()
        {
            var mockAgendamentos = new List<Agendamento>
            {
                new Agendamento { Id = 1, Observacoes = "Teste 1", Localizacao = "Local 1", RaioLocalizacao = 100 },
                new Agendamento { Id = 2, Observacoes = "Teste 2", Localizacao = "Local 2", RaioLocalizacao = 200 }
            };
            _mockAgendamentoService.Setup(service => service.GetAllAsync(1,10)).ReturnsAsync(mockAgendamentos);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsStatusCode200_WhenIdExists()
        {
            var agendamento = new Agendamento { Id = 1, Observacoes = "Teste", Localizacao = "Local", RaioLocalizacao = 100 };
            _mockAgendamentoService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(agendamento);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(agendamento, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsStatusCode404_WhenIdDoesNotExist()
        {
            _mockAgendamentoService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((Agendamento)null);

            var result = await _controller.GetById(1);

            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsStatusCode201()
        {
            var agendamento = new Agendamento { Observacoes = "Teste", Localizacao = "Local", RaioLocalizacao = 100 };
            var createdAgendamento = new Agendamento { Id = 1, Observacoes = "Teste", Localizacao = "Local", RaioLocalizacao = 100 };
            _mockAgendamentoService.Setup(service => service.AddAsync(agendamento)).ReturnsAsync(createdAgendamento);

            var result = await _controller.Create(agendamento);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(createdAgendamento, createdResult.Value);
            Assert.Equal(nameof(_controller.GetById), createdResult.ActionName);
        }

        [Fact]
        public async Task Update_ReturnsStatusCode200_WhenUpdateIsSuccessful()
        {
            var agendamento = new Agendamento { Id = 1, Observacoes = "Teste Atualizado", Localizacao = "Local Atualizado", RaioLocalizacao = 150 };
            _mockAgendamentoService.Setup(service => service.UpdateAsync(agendamento)).ReturnsAsync(agendamento);

            var result = await _controller.Update(1, agendamento);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(agendamento, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsStatusCode400_WhenIdDoesNotMatch()
        {
            var agendamento = new Agendamento { Id = 1, Observacoes = "Teste Atualizado", Localizacao = "Local Atualizado", RaioLocalizacao = 150 };

            var result = await _controller.Update(2, agendamento);

            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsStatusCode204()
        {
            _mockAgendamentoService.Setup(service => service.DeleteAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(1);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }
    }
}