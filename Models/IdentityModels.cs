using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Arkiva.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            
            return userIdentity;
        }

        public virtual ICollection<Dokument> Dokumenta { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Dokument> Dokumenta { get; set; }
        public DbSet<FusheIndeksimi> FushaIndeksimi { get; set; }
        public DbSet<LlojDokumenti> LlojeDokumenti { get; set; }
        public DbSet<Inspektim> Inspektime { get; set; }
        public DbSet<Subjekt> Subjekte { get; set; }
        public DbSet<LlojDokumentiInspektim> LlojDokumentiInspektime { get; set; }
        public DbSet<FusheIndeksimiDokument> FusheIndeksimiDokumente { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }
}