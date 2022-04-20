﻿using HealthInstitution.Persistence;
using HealthInstitution.Services.Implementation;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.ViewModel;
using Ninject.Modules;

namespace HealthInstitution.Ninject
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ICrudService<>)).To(typeof(CrudService<>));
            Bind(typeof(IUserService<>)).To(typeof(UserService<>));
            //Bind(typeof(IPatientService)).To(typeof(PatientService));

            //Bind(typeof(IDialogService)).To(typeof(DialogService));

            Bind<DatabaseContext>().To<DatabaseContext>().InSingletonScope();

            Bind<LoginViewModel>().To<LoginViewModel>();
            Bind<MainViewModel>().To<MainViewModel>();
            //Bind<RegisterViewModel>().To<RegisterViewModel>();
        }
    }
}
