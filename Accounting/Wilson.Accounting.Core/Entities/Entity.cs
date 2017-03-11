using System;

namespace Wilson.Accounting.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; } = new Guid();
    }
}
