using gameForMood.Entities;
using gameForMood.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gameForMood.Services
{
    public class MainService(GameForMoodContext context) : IMainService
    {
        private readonly GameForMoodContext _context = context;

        public async Task<IEnumerable<Contact>> GetContact(CancellationToken ct) => await _context.Contacts.AsNoTracking().ToListAsync(ct);
    }
}
