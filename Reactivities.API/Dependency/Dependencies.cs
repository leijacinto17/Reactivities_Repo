using Application.Queries.Activities;
using Application.Queries.Users;
using Application.Services.Activities;
using Autofac;
using Domain.Interfaces;
using Infrastructure;
using Reactivities.Application.Queries.Photos;
using Reactivities.Application.Services.Photos;
using Reactivities.Application.Services.Profiles;

namespace API.Dependency
{
    public class Dependencies : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            #region Activity
            builder.RegisterType<ActivitiesServices>().As<IActivitiesServices>();
            builder.RegisterType<ActivitiesQueryBuilder>().As<IActivitiesQueryBuilder>();
            #endregion

            #region User
            builder.RegisterType<UserQueryBuilder>().As<IUserQueryBuilder>();
            #endregion

            #region Photo
            builder.RegisterType<PhotosServices>().As<IPhotosServices>();
            builder.RegisterType<PhotosQueryBuilder>().As<IPhotosQueryBuilder>();
            #endregion

            #region Profile
            builder.RegisterType<ProfilesServices>().As<IProfilesServices>();
            #endregion
        }
    }
}
