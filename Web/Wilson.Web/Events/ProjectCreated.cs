using System;
using Wilson.Projects.Core.Entities;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events
{
    public class ProjectCreated : IDomainEvent
    {
        public ProjectCreated(Project project)
        {
            this.Project = project;
            this.DateOccurred = DateTime.Now;
        }

        public Project Project { get; private set; }

        public DateTime DateOccurred { get; private set; }
    }
}
