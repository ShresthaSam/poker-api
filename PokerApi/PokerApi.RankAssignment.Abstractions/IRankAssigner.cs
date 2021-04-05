using PokerApi.Common.Interfaces;
using PokerApi.Models;
using Serilog;
using System.Collections.Generic;

namespace PokerApi.RankAssignment.Abstractions
{
    /// <summary>
    /// A concrete implementation of Assigner is provided in 
    /// PokerApi.RankAssignment.Simple library which is currently being 
    /// used in the application. Another custom implementation can easily replace
    /// this Assigner using the same or a different library.
    /// The second overload provides additional helper classes for a given
    /// implementation. Use the first overload if your implementation would
    /// rather provide everything on its own.
    /// </summary>
    public interface IRankAssigner
    {
        IEnumerable<Ranking> AssignRanks(IEnumerable<PokerHandDomainModel> pokerHands);
        IEnumerable<Ranking> AssignRanks(
            IEnumerable<PokerHandDomainModel> pokerHands,
            ILogger logger,
            ICardConverter cardConverter,
            ICardSorter cardSorter,
            IPokerHandRecognizer pokerHandRecognizer);
    }
}