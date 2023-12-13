using FluentAssertions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Services;
using Testcontainers.PostgreSql;
using Bunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ShellAndNecklaceIntegrations
{
    public class BlazorIntegrationTestContext : Bunit.TestContext, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer;

        public BlazorIntegrationTestContext()
        {
            _dbContainer = new PostgreSqlBuilder()
                .WithImage("postgres")
                .WithPassword("Strong_password_123!")
                .Build();

            Services.AddDbContextFactory<OneShotShopContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));
            Services.AddScoped<ReviewService>();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            var dbContext = Services.GetRequiredService<OneShotShopContext>();
            await dbContext.Database.MigrateAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }


}
