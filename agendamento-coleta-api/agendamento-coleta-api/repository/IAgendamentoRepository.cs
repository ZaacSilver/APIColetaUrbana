using agendamento_coleta_api.dbcontext;
using agendamento_coleta_api.model;
using Microsoft.EntityFrameworkCore;

namespace agendamento_coleta_api.repository
{
    public interface IAgendamentoRepository
    {
        Task<IEnumerable<Agendamento>> GetAllAsync(int page, int pageSize);
        Task<Agendamento> GetByIdAsync(long id);
        Task<Agendamento> AddAsync(Agendamento agendamento);
        Task<Agendamento> UpdateAsync(Agendamento agendamento);
        Task DeleteAsync(long id);
    }

    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AppDbContext _context;

        public AgendamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Agendamento>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Agendamentos
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }

        public async Task<Agendamento> GetByIdAsync(long id)
        {
            return await _context.Agendamentos.FindAsync(id);
        }

        public async Task<Agendamento> AddAsync(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return agendamento;
        }

        public async Task<Agendamento> UpdateAsync(Agendamento agendamento)
        {
            _context.Entry(agendamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return agendamento;
        }

        public async Task DeleteAsync(long id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
                await _context.SaveChangesAsync();
            }
        }
    }

}
