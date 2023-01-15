using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductManagement.Domain.ValueObjects
{
    public abstract class ValueObject
    {
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
                return false;

            return left is null || left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
            => !(left == right);

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
                return false;

            var other = obj as ValueObject;
            return GetEqualityComponents().SequenceEqual(other!.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(component => component is null ? 0 : component.GetHashCode())
                .Aggregate((hash, next) => hash ^ next);
        }
    }
}
