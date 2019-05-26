using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EloCalculator
{
    public class EloResult
    {
        private readonly IEnumerable<EloIndividualResult> _results;

        public EloResult(IEnumerable<EloIndividualResult> results)
        {
            _results = results ?? throw new ArgumentNullException(nameof(results));
        }

        public EloIndividualResult GetResult(EloPlayerIdentifier playerIdentifier)
        {
            var result = _results.FirstOrDefault(x => x.PlayerIdentifier == playerIdentifier);

            if (result == null)
                throw new InvalidOperationException($"No player found in results with identifier {playerIdentifier}");

            return result;
        }

        public IEnumerable<EloIndividualResult> GetResults()
            => _results;

        public IEnumerable<EloIndividualResult> GetResults(EloTeamIdentifier teamIdentifier)
            => _results.Where(x => x.TeamIdentifier == teamIdentifier);

        public int GetRatingDifference(EloPlayerIdentifier identifier)
            => GetResult(identifier).RatingDifference;

        public int GetRatingAfter(EloPlayerIdentifier identifier)
            => GetResult(identifier).RatingAfter;

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var result in _results.OrderByDescending(x => x.RatingDifference))
            {
                var outcome = result.RatingDifference > 0 ? "gained" : "lost";
                sb.AppendLine($"Player ({result.PlayerIdentifier}) with rating {result.RatingBefore} {outcome} {result.RatingDifference} ELO.");
            }

            return sb.ToString();
        }
    }
}