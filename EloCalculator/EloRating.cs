using EloCalculator.Internal;
using System;
using System.Collections.Generic;

namespace EloCalculator
{
    public class EloRating : ValueObject
    {
        public int Value { get; }

        public EloRating(int value)
        {
            if (value <= 0)
                throw new ArgumentException(nameof(value) + " can not be zero or negative.");

            Value = value;
        }

        public static implicit operator int(EloRating x)
            => x.Value;

        public override string ToString()
            => Value.ToString();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
