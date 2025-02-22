﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Security.Infrastructure.DataModel.Context.Configurations;
using System;
using Security.Domain.Entities;
using domain = Security.Domain.Entities;

namespace Security.Infrastructure.DataModel.Context
{
    public partial class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<domain.Action> Actions { get; set; }
        public virtual DbSet<App> Apps { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Master> Masters { get; set; }
        public virtual DbSet<MasterDet> MasterDets { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuAction> MenuActions { get; set; }
        public virtual DbSet<MenuRole> MenuRoles { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DELL;Initial Catalog=BD_EXAMEN;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.ApplyConfiguration(new Configurations.ActionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.AppConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MasterConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MasterDetConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MenuConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MenuActionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MenuRoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.EmployeeConfiguration());
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
