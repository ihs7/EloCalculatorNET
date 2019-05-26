using System.Collections.Generic;
using System.Linq;

namespace EloCalculator
{
    public static class EloTeamExtensions
    {
        public static IEnumerable<EloPlayerIdentifier> GetPlayerIdentifiers(this IEnumerable<EloTeam> teams)
            => teams.SelectMany(x => x.Members.Select(m => m.Identifier));

        public static IEnumerable<EloTeamIdentifier> GetTeamIdentifiers(this IEnumerable<EloTeam> teams)
            => teams.Select(x => x.Identifier);
    }
}
