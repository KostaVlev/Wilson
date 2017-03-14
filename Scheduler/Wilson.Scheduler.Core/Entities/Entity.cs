using System;

namespace Wilson.Scheduler.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; } = new Guid();
    }
}
