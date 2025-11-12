using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo_2310.Models;

public partial class Database : DbContext
{
    public Database()
    {
    }

    public Database(DbContextOptions<Database> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Equioment> Equioments { get; set; }

    public virtual DbSet<FkOrderEquip> FkOrderEquips { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TypeEquipment> TypeEquipments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connect = "Server=dbsrv\\AG2024;Database=demo_p_NDA;Trusted_Connection=True; TrustServerCertificate=True";
        string connect_home = "Server=uglybastard\\SQLEXPRESS;Database=paul_basa;Trusted_Connection=True; TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(connect_home);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__adress$__3213E83F28C71518");

            entity.ToTable("adress$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adress1)
                .HasMaxLength(255)
                .HasColumnName("adress");
        });

        modelBuilder.Entity<Equioment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__equiomen__3213E83F3B6DDC61");

            entity.ToTable("equioment$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Articul)
                .HasMaxLength(255)
                .HasColumnName("articul");
            entity.Property(e => e.CostRent).HasColumnName("cost_rent");
            entity.Property(e => e.CountFree).HasColumnName("count_free");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.IdProducer).HasColumnName("id_producer");
            entity.Property(e => e.IdProvider).HasColumnName("id_provider");
            entity.Property(e => e.IdTypeEquipment).HasColumnName("id_type_equipment");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.UnitRent)
                .HasMaxLength(255)
                .HasColumnName("unit_rent");

            entity.HasOne(d => d.IdProducerNavigation).WithMany(p => p.Equioments)
                .HasForeignKey(d => d.IdProducer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_equipment_producer");

            entity.HasOne(d => d.IdProviderNavigation).WithMany(p => p.Equioments)
                .HasForeignKey(d => d.IdProvider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_equipment_provider");

            entity.HasOne(d => d.IdTypeEquipmentNavigation).WithMany(p => p.Equioments)
                .HasForeignKey(d => d.IdTypeEquipment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_equipment_type");
        });

        modelBuilder.Entity<FkOrderEquip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fk_order__3213E83FE193D782");

            entity.ToTable("fk_order_equip$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEquip).HasColumnName("id_equip");
            entity.Property(e => e.IdOrder).HasColumnName("id_order");

            entity.HasOne(d => d.IdEquipNavigation).WithMany(p => p.FkOrderEquips)
                .HasForeignKey(d => d.IdEquip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_equip_equipment");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.FkOrderEquips)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_equip_order");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order$__3213E83F485280AB");

            entity.ToTable("order$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.CountRent).HasColumnName("count_rent");
            entity.Property(e => e.DateStart)
                .HasColumnType("datetime")
                .HasColumnName("date_start");
            entity.Property(e => e.IdAdress).HasColumnName("id_adress");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NumberOrder).HasColumnName("number_order");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.HasOne(d => d.IdAdressNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdAdress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_address");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_user");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producer__3213E83FD0D03554");

            entity.ToTable("producer$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Producer1)
                .HasMaxLength(255)
                .HasColumnName("producer");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__provider__3213E83FDA825722");

            entity.ToTable("provider$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Provider1)
                .HasMaxLength(255)
                .HasColumnName("provider");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role$__3213E83F32903655");

            entity.ToTable("role$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role1)
                .HasMaxLength(255)
                .HasColumnName("role");
        });

        modelBuilder.Entity<TypeEquipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__type_equ__3213E83F0A145CF7");

            entity.ToTable("type_equipment$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user$__3213E83FA66FE9BE");

            entity.ToTable("user$");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fio)
                .HasMaxLength(255)
                .HasColumnName("fio");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Pass)
                .HasMaxLength(255)
                .HasColumnName("pass");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
