using System.Collections;
using iPractice.Domain.Entities;

namespace iPractice.Domain.Interfaces
{
    /// <summary>
    /// Represents a repository for managing psychologists.
    /// </summary>
    public interface IPsychologistRepository
    {
        /// <summary>
        /// Asynchronously finds a psychologist by their identifier.
        /// </summary>
        /// <param name="psychologistId">The identifier of the psychologist to find.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the found psychologist.</returns>
        Task<Psychologist?> FindAsync(long psychologistId);

        /// <summary>
        /// Asynchronously updates a psychologist.
        /// </summary>
        /// <param name="psychologist">The psychologist to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Psychologist psychologist);

        /// <summary>
        /// Asynchronously finds all psychologists associated with a client.
        /// </summary>
        /// <param name="clientId">The identifier of the client.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of psychologists associated with the client.</returns>
        Task<List<Psychologist>> FindAllByClientId(long clientId);
    }
}
