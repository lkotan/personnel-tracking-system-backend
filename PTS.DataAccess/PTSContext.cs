using Microsoft.EntityFrameworkCore;
using PTS.DataAccess.Extensions;
using PTS.Entities;

namespace PTS.DataAccess
{
    public class PTSContext:DbContext
    {
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Assigment> Assigments { get; set; }
        public DbSet<AssigmentTag> AssigmentTags { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PersonnelNotification> PersonnelNotifications { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailParameter> EmailParameters { get; set; }

        public PTSContext(DbContextOptions<PTSContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder.MapConfiguration();
            modelBuilder = modelBuilder.SetDataType();
            //modelBuilder = modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
