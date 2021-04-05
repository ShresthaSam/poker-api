using PokerApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerApi.Services.Interfaces
{
    public interface IRankAssignmentService
    {
        Task<IEnumerable<Ranking>> AssignRanks(IEnumerable<PokerHandDto> pokerHands);
    }
}
