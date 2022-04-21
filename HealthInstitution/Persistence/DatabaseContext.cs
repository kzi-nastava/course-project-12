﻿using HealthInstitution.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace HealthInstitution.Persistence
{
    public class DatabaseContext : DbContext
    {

        public string DbPath { get; }

        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        // Medicine
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        // Room related things
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentTransfer> EquipmentTransfers { get; set; }
        public DbSet<RoomRenovation> RoomRenovations { get; set; }

        // Patient related things
        public DbSet<Activity> Activities { get; set; }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }


        // Appointment related things
        public DbSet<Anamnesis> Anamnesis { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "health.db");
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "health.db");
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = System.IO.Path.Join(path, "health.db");

            //DbPath = System.IO.Path.Join("Persistence\\Database\\", "health.db");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseLazyLoadingProxies().UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
