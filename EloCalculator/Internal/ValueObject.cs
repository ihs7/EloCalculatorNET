using System;
using System.Collections.Generic;
using System.Linq;

namespace EloCalculator.Internal
{
    public abstract class ValueObject
    {
        internal ValueObject()
        { }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                throw new ArgumentException($"Invalid comparison of Value Objects of different types: {GetType()} and {obj.GetType()}");

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return (current * 23) + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject a, ValueObject b)
            => ReferenceEquals(a, b) ? true : (a is null) || (b is null) ? false : a.Equals(b);

        public static bool operator !=(ValueObject a, ValueObject b)
            => !(a == b);
    }
}
