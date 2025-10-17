using System;
using System.Collections.Generic;
using EVCharging.Repositories.TrongLH.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EVCharging.Repositories.TrongLH.Context;

public partial class FA25_PRN232_SE1717_G2_EVChargingContext : DbContext
{
    public FA25_PRN232_SE1717_G2_EVChargingContext()
    {
    }

    public FA25_PRN232_SE1717_G2_EVChargingContext(DbContextOptions<FA25_PRN232_SE1717_G2_EVChargingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EnergySupplyTrongLh> EnergySupplyTrongLhs { get; set; }

    public virtual DbSet<StationTrongLh> StationTrongLhs { get; set; }

    public virtual DbSet<SystemUserAccount> SystemUserAccounts { get; set; }

    public static string? GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string? connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnergySupplyTrongLh>(entity =>
        {
            entity.ToTable("EnergySupplyTrongLh");

            entity.Property(e => e.EnergySupplyTrongLhid).HasColumnName("EnergySupplyTrongLHId");
            entity.Property(e => e.AvailableKw).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CapacityKw).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ContractNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EfficiencyRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PeakCapacity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SourceName).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.StationTrongLhid).HasColumnName("StationTrongLHId");
            entity.Property(e => e.SupplyType).HasMaxLength(50);

            entity.HasOne(d => d.StationTrongLh).WithMany(p => p.EnergySupplyTrongLhs)
                .HasForeignKey(d => d.StationTrongLhid)
                .HasConstraintName("FK_EnergySupplyTrongLh_StationTrongLh");
        });

        modelBuilder.Entity<StationTrongLh>(entity =>
        {
            entity.ToTable("StationTrongLh");

            entity.HasIndex(e => e.Code, "IX_StationTrongLh_Code")
                .IsUnique()
                .HasFilter("([Code] IS NOT NULL)");

            entity.Property(e => e.StationTrongLhid).HasColumnName("StationTrongLHId");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OwnerCompany).HasMaxLength(100);
            entity.Property(e => e.Province).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<SystemUserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId);

            entity.ToTable("System.UserAccount");

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
