using System;

namespace EloCalculator
{
    public class EloPlayer
    {
        public EloPlayerIdentifier Identifier { get; } = new EloPlayerIdentifier(Guid.NewGuid());
        public int Rating { get; private set; }
        public int Place { get; private set; }

        public EloPlayer(int rating, int place)
        {
            Rating = rating;
            Place = place;
        }

        public EloPlayer(int rating, bool won)
        {
            Rating = rating;
            Place = won ? 0 : 1;
        }
    }
}