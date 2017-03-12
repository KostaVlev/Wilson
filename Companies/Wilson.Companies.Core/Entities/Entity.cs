using System;

namespace Wilson.Companies.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; } = new Guid();
    }
}
