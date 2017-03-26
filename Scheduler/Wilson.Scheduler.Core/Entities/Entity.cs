using System;

namespace Wilson.Scheduler.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
