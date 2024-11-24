

using System.Reflection;
using Autofac;
using CafeManagement.Application.Dto;
using CafeManagement.Application.Employee.Query;
using CafeManagement.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace CafeManagement.Infrastructure.DependencyInjection;

public class AutofacModule : Module
{
    private readonly IConfiguration _configuration;

    public AutofacModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        // Register DbContext
        builder.Register(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseNpgsql(
                    _configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

                return new ApplicationDbContext(optionsBuilder.Options);
            })
            .As<ApplicationDbContext>()
            .As<IApplicationDbContext>()
            .InstancePerLifetimeScope();

        // Register all MediatR handlers from Application assembly
        var applicationAssembly = typeof(GetEmployeesListQuery).Assembly;

        builder.RegisterAssemblyTypes(applicationAssembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        // Register Validators
        builder.RegisterAssemblyTypes(applicationAssembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}