using api.Enums;
using api.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net.Mail;
using System.Security.Principal;
using static System.Net.WebRequestMethods;

namespace api.Contexts
{
    public partial class ApiDbContext: DbContext
    {
        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<AssurProduct> AssurProducts { get; set; }
        public DbSet<CategoryVehicle> CategoryVehicles { get; set; }        
        public DbSet<Suscriber> Suscribers { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public"); // ou ton schéma réel
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssurProduct>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_assurproduct");

                entity.HasIndex(e => e.Id, "assurproduct_pk").IsUnique();

                entity.ToTable("assurproduct");
            });

            modelBuilder.Entity<CategoryVehicle>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_categoryvehicle");

                entity.HasIndex(e => e.Id, "categoryvehicle_pk").IsUnique();

                entity.ToTable("categoryvehicle");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_vehicle");

                entity.HasIndex(e => e.Id, "vehicle_pk").IsUnique();

                entity.HasIndex(e => e.IdCategoryVehicle, "categoryvehicle_fk");

                entity.HasIndex(e => e.IdSuscriber, "suscriber_fk");

                entity.ToTable("vehicle");
            });

            modelBuilder.Entity<Suscriber>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_suscriber");

                entity.HasIndex(e => e.Id, "suscriber_pk").IsUnique();

                entity.ToTable("suscriber");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_subscription");

                entity.HasIndex(e => e.Id, "subscription_pk").IsUnique();

                entity.HasIndex(e => e.IdVehicle, "vehicle_fk");

                entity.HasIndex(e => e.IdSubscriber, "suscriber_fk");

                entity.HasIndex(e => e.IdAssurProduct, "assurproduct_fk");

                entity.ToTable("subscription");
            });

            modelBuilder.Entity<Simulation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_simulation");

                entity.HasIndex(e => e.Id, "simulation_pk").IsUnique();

                entity.HasIndex(e => e.IdVehicle, "vehicle_fk");

                entity.HasIndex(e => e.IdAssurProduct, "assurproduct_fk");

                entity.ToTable("simulation");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
