using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISessionOccurrenceGeneratorService
    {
        Task GenerateUpcomingOccurrencesAsync();
    }
}
