using System;
using System.Collections.Generic;
using System.Linq;

namespace EloCalculator
{
    public partial class EloMatch
    {
        private IList<EloPlayer> _players = new List<EloPlayer>();
        private int _kFactor = 15;

        public EloMatch()
        { }

        public EloMatch(EloPlayer player1, EloPlayer player2)
        {
            _players.Add(player1);
            _players.Add(player2);
        }

        public EloMatch(IEnumerable<EloPlayer> players)
        {
            _players = players.ToList() ?? throw new ArgumentNullException(nameof(players));
        }

        public EloMatch(params EloPlayer[] players)
        {
            _players = players.ToList() ?? throw new ArgumentNullException(nameof(players));
        }

        public void AddPlayer(EloPlayer player)
        {
            _players.Add(player);
        }

        public EloPlayerIdentifier AddPlayer(int rating, int place)
        {
            var player = new EloPlayer(rating, place);
            _players.Add(player);
            return player.Identifier;
        }

        public void SetKFactor(int kFactor)
            => _kFactor = kFactor;

        public EloResult Calculate(int kFactor = 15)
        {
            var result = new EloResult();

            foreach (var player in _players)
            {
                double eloDiff = 0;

                foreach (var opponent in _players.Where(x => x.Identifier != player.Identifier))
                {
                    var expected = new Expected(player, opponent);
                    float s;

                    if (player.Place < opponent.Place)
                        s = 1.0F;
                    else if (player.Place == opponent.Place)
                        s = 0.5F;
                    else
                        s = 0.0F;

                    eloDiff += Math.Round(_kFactor * (s - expected.ExpectedScore));
                }

                result.AddIndividualResult(new EloIndividualResult(player.Identifier, player.Rating, player.Rating + (int)eloDiff));
            }

            return result;
        }
    }
}