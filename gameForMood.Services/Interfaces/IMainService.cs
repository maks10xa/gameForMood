using gameForMood.Entities;

namespace gameForMood.Services.Interfaces
{
    public interface IMainService
    {
        Task<IEnumerable<Contact>> GetContact(CancellationToken ct);
    }
}
