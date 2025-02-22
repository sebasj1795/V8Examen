﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;

namespace Security.Infrastructure.DataModel.Context.Configurations
{
    public partial class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> entity)
        {
            entity.ToTable("Module");

            entity.Property(e => e.DateCrea).HasColumnType("datetime");

            entity.Property(e => e.DateUpd).HasColumnType("datetime");

            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.IconCss)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.IconImg)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.App)
                .WithMany(p => p.Modules)
                .HasForeignKey(d => d.IdApp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Module_App");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Module> entity);
    }
}
