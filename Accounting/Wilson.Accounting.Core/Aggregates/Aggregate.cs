using System;

namespace Wilson.Accounting.Core.Aggregates
{
    public abstract class Aggregate<TEntity> : IAggregate<TEntity> where TEntity : class
    {
        public TEntity Entity { get; private set; }

        public void Load(TEntity source)
        {
            this.Entity = source;
        }

        public abstract bool Validate();
    }
}
