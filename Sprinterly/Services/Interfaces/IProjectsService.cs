﻿using Sprinterly.Models;

namespace Sprinterly.Services.Interfaces
{
    public interface IProjectsService
    {
        public Task<IEnumerable<Project>> GetProjects(string organization);
    }
}