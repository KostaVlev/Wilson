using System;

namespace Wilson.Projects.Core.Entities
{
    public interface IValueObject<TEntity> : IEquatable<TEntity> where TEntity : class
    {
    }
}
