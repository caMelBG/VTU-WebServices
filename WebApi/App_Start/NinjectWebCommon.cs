﻿[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebClient.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebClient.App_Start.NinjectWebCommon), "Stop")]

namespace WebClient.App_Start
{
    using DataAccess;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Models;
    using Models.Converters;
    using Models.Converters.Interface;
    using Models.DtoModels;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Repositories;
    using Repositories.Interfaces;
    using System;
    using System.Data.Entity;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<UniversityContext>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IModelConverter<Student, StudentDto>>().To<StudentConverter>();
            kernel.Bind<IModelConverter<Course, CourseDto>>().To<CourseConverter>();
        }
    }
}