using agendamento_coleta_api.model;
using agendamento_coleta_api.repository;

namespace agendamento_coleta_api.service
{
    public interface IAgendamentoService
    {
        Task<IEnumerable<Agendamento>> GetAllAsync(int page, int pageSize);
        Task<Agendamento> GetByIdAsync(long id);
        Task<Agendamento> AddAsync(Agendamento agendamento);
        Task<Agendamento> UpdateAsync(Agendamento agendamento);
        Task DeleteAsync(long id);
    }

    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _repository;

        public AgendamentoService(IAgendamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Agendamento>> GetAllAsync(int page, int pageSize)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<Agendamento> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Agendamento> AddAsync(Agendamento agendamento)
        {
            return await _repository.AddAsync(agendamento);
        }

        public async Task<Agendamento> UpdateAsync(Agendamento agendamento)
        {
            return await _repository.UpdateAsync(agendamento);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
