using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iPractice.Domain.Entities;
using iPractice.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iPractice.DataAccess
{
    public class PsychologistRepository : IPsychologistRepository
    {
        private readonly ApplicationDbContext _context;

        public PsychologistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Psychologist> FindAsync(long psychologistId)
        {
            return _context.Psychologists
                .Include(p => p.Availabilities)
                .Include(p => p.Bookings)
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(p => p.Id == psychologistId);
        }

        public Task UpdateAsync(Psychologist psychologist)
        {
            _context.Psychologists.Update(psychologist);
            return _context.SaveChangesAsync();
        }

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
