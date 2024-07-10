using System.Collections;
using iPractice.Domain.Entities;

namespace iPractice.Domain.Interfaces
{
    public interface IPsychologistRepository
    {
        Task<Psychologist?> FindAsync(long psychologistId);

        Task UpdateAsync(Psychologist psychologist);
        Task<List<Psychologist>> FindAllByClientId(long clientId);
    }
}
