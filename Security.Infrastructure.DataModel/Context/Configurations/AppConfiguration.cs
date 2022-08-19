﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Security.Infrastructure.DataModel.Context;
using System;
using Security.Domain.Entities;

namespace Security.Infrastructure.DataModel.Context.Configurations
{
    public partial class AppConfiguration : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> entity)
        {
            entity.ToTable("App");

            entity.Property(e => e.Comment)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.DateCrea).HasColumnType("datetime");

            entity.Property(e => e.DateUpd).HasColumnType("datetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(70)
                .IsUnicode(false);

            entity.Property(e => e.NameBd)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NameBD");

            entity.Property(e => e.PasswordServer)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Port)
                .HasMaxLength(4)
                .IsUnicode(false);

            entity.Property(e => e.Server)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.UserServer)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Company)
                .WithMany(p => p.Apps)
                .HasForeignKey(d => d.IdCompany)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_App_Company");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<App> entity);
    }
}
