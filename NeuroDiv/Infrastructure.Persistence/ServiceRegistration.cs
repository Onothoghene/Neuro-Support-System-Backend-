using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseInMemoryDatabase("OrderFlowDB"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b =>
                        {
                            b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                            b.UseCompatibilityLevel(120);
                        }));
            }

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

            services.AddTransient<IUserProfileRepositoryAsync, UserProfileRepositoryAsync>();
            services.AddTransient<IOrganizationUsersInviteRepositoryAsync, OrganizationUsersInviteRepositoryAsync>();
            services.AddTransient<IOrganizationUsersRepositoryAsync, OrganizationUsersRepositoryAsync>();
            services.AddTransient<IOrganizationRolesRepositoryAsync, OrganizationRolesRepositoryAsync>();
            services.AddTransient<IOrganizationUserRolesRepositoryAsync, OrganizationUserRolesRepositoryAsync>();
            services.AddTransient<IOrganizationsRepositoryAsync, OrganizationsRepositoryAsync>();
            services.AddTransient<IOrganizationRolesRepositoryAsync, OrganizationRolesRepositoryAsync>();
            services.AddTransient<IEmailChangeRequestRepositoryAsync, EmailChangeRequestRepositoryAsync>();
            services.AddTransient<ITherapistProfileRepositoryAsync, TherapistProfileRepositoryAsync>();
            services.AddTransient<ITherapistSpecializationRepositoryAsync, TherapistSpecializationRepositoryAsync>();
            services.AddTransient<IParentProfileRepositoryAsync, ParentProfileRepositoryAsync>();
            services.AddTransient<IChildProfileRepositoryAsync, ChildProfileRepositoryAsync>();
            services.AddTransient<IGoalCategoryRepositoryAsync, GoalCategoryRepositoryAsync>();
            services.AddTransient<IChildTherapistAssignmentRepositoryAsync, ChildTherapistAssignmentRepositoryAsync>();
            services.AddTransient<ITherapyGoalRepositoryAsync, TherapyGoalRepositoryAsync>();
            services.AddTransient<IChildParentRepositoryAsync, ChildParentRepositoryAsync>();
            services.AddTransient<IDiagnosisTypeRepositoryAsync, DiagnosisTypeRepositoryAsync>();
            services.AddTransient<ISessionDurationRepositoryAsync, SessionDurationRepositoryAsync>();
            services.AddTransient<ISessionRepositoryAsync, SessionRepositoryAsync>();
            services.AddTransient<IChildSessionRecordRepositoryAsync, ChildSessionRecordRepositoryAsync>();
            services.AddTransient<IGoalProgressLogRepositoryAsync, GoalProgressLogRepositoryAsync>();

            services.AddTransient<IContactUsRepositoryAsync, ContactUsRepositoryAsync>();
            services.AddTransient<ICommentRepositoryAsync, CommentRepositoryAsync>();
            services.AddTransient<IFileTempRepositoryAsync, FileTempRepositoryAsync>();
            services.AddTransient<IPaymentRepositoryAsync, PaymentRepositoryAsync>();

            #endregion
        }
    }
}
