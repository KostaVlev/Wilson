using System;

namespace Wilson.Companies.Core.Entities
{
    public interface IValueObject<TEntity> : IEquatable<TEntity> where TEntity : class
    {
    }
}
