using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShellAndNecklaceUnitTests.Models {
    internal class DBContextMock:DbContext {
        protected readonly IConfiguration configuration;
        public DBContextMock(IConfiguration configuration) { 
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Picture> Pictures { get; set; }    
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Filetype> Filetypes { get; set; }
    }
}
