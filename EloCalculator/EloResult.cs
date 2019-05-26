using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EloCalculator
{
    public class EloResult
    {
        private List<EloIndividualResult> _results = new List<EloIndividualResult>();

        internal EloResult()
        { }

        public EloIndividualResult GetResult(EloPlayerIdentifier identifier)
        {
            var result = _results.FirstOrDefault(x => x.Identifier == identifier);

            if (result == null)
                throw new InvalidOperationException($"No player found in results with identifier {identifier}");

            return result;
        }

        public int GetRatingDifference(EloPlayerIdentifier identifier)
            => GetResult(identifier).RatingDifference;

        internal void AddIndividualResult(EloIndividualResult result)
            => _results.Add(result);

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var result in _results.OrderByDescending(x => x.RatingDifference))
            {
                var outcome = result.RatingDifference > 0 ? "gained" : "lost";
                sb.AppendLine($"Player ({result.Identifier}) with rating {result.RatingBefore} {outcome} {result.RatingDifference} ELO.");
            }

            return sb.ToString();
        }
    }
}