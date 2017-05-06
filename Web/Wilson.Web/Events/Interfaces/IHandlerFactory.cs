using System;

namespace Wilson.Web.Events.Interfaces
{
    public interface IEventsFactory
    {
        IServiceProvider ServiceProvider { get; set; }

        void Register<T>(Action<T> callback) where T : IDomainEvent;

        void Raise<T>(T args) where T : IDomainEvent;

        void ClearCallbacks();
    }
}
