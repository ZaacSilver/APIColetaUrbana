using agendamento_coleta_api.model;
using agendamento_coleta_api.service;
using Microsoft.AspNetCore.Mvc;

namespace agendamento_coleta_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _service;

        public AgendamentoController(IAgendamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAll(int page = 1, int pageSize = 10)
        {
            var agendamentos = await _service.GetAllAsync(page, pageSize);
            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetById(long id)
        {
            var agendamento = await _service.GetByIdAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            return Ok(agendamento);
        }

        [HttpPost]
        public async Task<ActionResult<Agendamento>> Create(Agendamento agendamento)
        {
            var createdAgendamento = await _service.AddAsync(agendamento);
            return CreatedAtAction(nameof(GetById), new { id = createdAgendamento.Id }, createdAgendamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return BadRequest();
            }

            var updatedAgendamento = await _service.UpdateAsync(agendamento);
            return Ok(updatedAgendamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
