using System;
using System.Collections.Generic;
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
        
        public ProjectCreated(IEnumerable<Project> projects)
        {
            this.DateOccurred = DateTime.Now;
            this.Projects = projects;
        }

        public DateTime DateOccurred { get; set; }

        public Project Project { get; private set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}
