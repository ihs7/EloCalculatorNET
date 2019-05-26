using System;

namespace EloCalculator
{
	internal class Expected
        {
            public EloPlayer Player { get; }
            public EloPlayer Versus { get; }
            public double ExpectedScore { get; }

            public Expected(EloPlayer player, EloPlayer versus)
            {
                Player = player ?? throw new ArgumentNullException(nameof(player));
                Versus = versus ?? throw new ArgumentNullException(nameof(versus));

                ExpectedScore = 1 / (1.0f + (float)Math.Pow(10.0f, (versus.Rating - player.Rating) / 400.0f));
            }
        }
}