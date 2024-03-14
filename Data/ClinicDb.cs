using clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace clinic.Data
{
    public class ClinicDb: DbContext
    {
        public ClinicDb(DbContextOptions<ClinicDb>  options):base(options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

 modelBuilder.Entity<Clinic>()
        .HasMany(e => e.Doctors)
        .WithOne(e => e.Clinic)
        .HasForeignKey(e => e.ClinicId)
        .IsRequired();

 modelBuilder.Entity<Schedule>()
        .HasMany(e => e.Fees)
        .WithOne(e => e.Schedule)
        .HasForeignKey(e => e.ScheduleId)
        .IsRequired();

 modelBuilder.Entity<Schedule>()
        .HasMany(e => e.Fees)
        .WithOne(e => e.Schedule)
        .HasForeignKey(e => e.ScheduleId)
        .IsRequired();

modelBuilder.Entity<ScheduleType>()
        .HasOne(e => e.Schedule)
        .WithOne(e => e.ScheduleType)
        .HasForeignKey<Schedule>(e => e.ScheduleTypeId)
        .IsRequired();

modelBuilder.Entity<Booking>()
        .HasOne(e => e.User)
        .WithMany(e => e.Bookings)
        .HasForeignKey(e => e.UserId)
        .IsRequired();



modelBuilder.Entity<Booking>()
        .HasOne(e => e.Schedule)
        .WithOne(e => e.Booking)
        .HasForeignKey<Booking>(e => e.ScheduleId)
        .IsRequired();

    modelBuilder.Entity<UserRole>()
        .HasKey(bc => new { bc.UserId, bc.RoleId });  
    modelBuilder.Entity<UserRole>()
        .HasOne(bc => bc.User)
        .WithMany(b => b.Roles)
        .HasForeignKey(bc => bc.UserId); 

    modelBuilder.Entity<UserRole>()
        .HasOne(bc => bc.Role)
        .WithMany(c => c.Roles)
        .HasForeignKey(bc => bc.RoleId);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
    // optionsBuilder.UseMySql("");
    var serverVersion = new MySqlServerVersion(new Version(10, 4, 28));
    optionsBuilder.UseMySql(
            "Server=localhost;Database=clinicdb;User=root;Password=;",

        serverVersion 
    );



        }
    
    
    
 public   DbSet<Booking> Bookings {get; set;}
    public    DbSet<Clinic> Clinics {get; set;}
   public DbSet<Doctor> Doctors {get; set;}
   public DbSet<Fees> Fees {get; set;}
    public DbSet<Role> Roles {get; set;}
    DbSet<Schedule> Schedules {get; set;}

     public   DbSet<ScheduleType> ScheduleTypes {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<UserRole> UserRoles {get; set;}

    }
}