using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iPractice.Domain.Entities;
using iPractice.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess
{
    /// <summary>
    /// Represents a repository for managing psychologists.
    /// </summary>
    public class PsychologistRepository : IPsychologistRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PsychologistRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public PsychologistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously finds a psychologist by their identifier.
        /// </summary>
        /// <param name="psychologistId">The identifier of the psychologist to find.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the found psychologist.</returns>
        public Task<Psychologist> FindAsync(long psychologistId)
        {
            return _context.Psychologists
                .Include(p => p.Availabilities)
                .Include(p => p.Bookings)
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(p => p.Id == psychologistId);
        }

        /// <summary>
        /// Asynchronously updates a psychologist.
        /// </summary>
        /// <param name="psychologist">The psychologist to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task UpdateAsync(Psychologist psychologist)
        {
            _context.Psychologists.Update(psychologist);
            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously finds all psychologists associated with a client.
        /// </summary>
        /// <param name="clientId">The identifier of the client.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of psychologists associated with the client.</returns>
        public Task<List<Psychologist>> FindAllByClientId(long clientId)
        {
            return _context.Psychologists
                .Include(p => p.Availabilities)
                .Include(p => p.Bookings)
                .Include(p => p.Clients)
                .Where(p => p.Clients.Any(c => c.Id == clientId))
                .ToListAsync();
        }
    }
}
