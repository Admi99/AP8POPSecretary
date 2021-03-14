﻿using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Repositories;
using AP8POSecretary.Domain.Services;
using AP8POSecretary.Infrastructure.Data;
using AP8POSecretary.Infrastructure.Repositories;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Infrastructure.Configuration
{
    public class DependencyBuilder
    {
        private readonly IKernel _kernel;
        public DependencyBuilder(IKernel kernel)
        {
            _kernel = kernel;
        }
        public T Get<T>() => _kernel.Get<T>();

        public void Build()
        {
            _kernel.Bind<DataContextFactory>().ToSelf();

            _kernel.Bind<IDataRepository<Group>>().To<GenericRepository<Group>>();
            _kernel.Bind<IDataService<Group>>().To<DataService<Group>>();

            _kernel.Bind<IDataRepository<Subject>>().To<GenericRepository<Subject>>();
            _kernel.Bind<IDataService<Subject>>().To<DataService<Subject>>();

            _kernel.Bind<IDataRepository<Employee>>().To<GenericRepository<Employee>>();
            _kernel.Bind<IDataService<Employee>>().To<DataService<Employee>>();

            _kernel.Bind<IDataRepository<WorkingLabel>>().To<GenericRepository<WorkingLabel>>();
            _kernel.Bind<IDataService<WorkingLabel>>().To<DataService<WorkingLabel>>();
        }
    }
}
