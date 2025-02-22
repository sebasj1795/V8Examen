﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;

namespace Security.Infrastructure.DataModel.Context.Configurations
{
    public partial class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id);

            entity.Property(e => e.BaseSalary).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.BeginDate).HasColumnType("date");

            entity.Property(e => e.Birthday).HasColumnType("date");

            entity.Property(e => e.Comission).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.CompesationBonus).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.Constributions).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.Division)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.EmployeeCode)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.EmployeeName)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.EmployeeSurname)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.IdentificationNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.Office)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ProductionBonus).HasColumnType("decimal(18, 2)");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Employee> entity);
    }
}
