using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShellAndNecklaceAPI.Data;

public partial class OneShotShopContext : DbContext
{
    public OneShotShopContext()
    {
    }

    public OneShotShopContext(DbContextOptions<OneShotShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Filetype> Filetypes { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Purchaseorder> Purchaseorders { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //  => optionsBuilder.UseNpgsql("OneShotShop");
   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
      //=> optionsBuilder.UseNpgsql("Host=database-1.cisqkskacvfb.us-west-2.rds.amazonaws.com;Port=5432;Username=dallinphelps_25;Database=db_dallinphelps_25;Password=713102526190");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account", "OneShotShop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(48)
                .HasColumnName("password");
            entity.Property(e => e.Address)
                .HasMaxLength(128)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(48)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .HasColumnName("username");
            entity.Property(e => e.Verified).HasColumnName("verified");
            entity.Property(e => e.Closed).HasColumnName("closed");
        });

        modelBuilder.Entity<Filetype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("filetype_pkey");

            entity.ToTable("filetype", "OneShotShop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fileextension)
                .HasMaxLength(6)
                .HasColumnName("fileextension");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_pkey");

            entity.ToTable("item", "OneShotShop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.Itemname)
                .HasMaxLength(32)
                .HasColumnName("itemname");
            entity.Property(e => e.Pictureid).HasColumnName("pictureid");
            entity.Property(e => e.Pricebase)
                .HasColumnType("money")
                .HasColumnName("pricebase");
            entity.Property(e => e.Statusid).HasColumnName("statusid");

            entity.HasOne(d => d.Picture).WithMany(p => p.Items)
                .HasForeignKey(d => d.Pictureid)
                .HasConstraintName("item_pictureid_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Items)
                .HasForeignKey(d => d.Statusid)
                .HasConstraintName("item_statusid_fkey");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderitem_pkey");

            entity.ToTable("orderitem", "OneShotShop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Pricepaid)
                .HasColumnType("money")
                .HasColumnName("pricepaid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Itemid)
                .HasConstraintName("orderitem_itemid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderitem_orderid_fkey");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("picture_pkey");

            entity.ToTable("picture", "OneShotShop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Filetypeid).HasColumnName("filetypeid");
            entity.Property(e => e.Imagename)
                .HasMaxLength(100)
                .HasColumnName("imagename");

            entity.HasOne(d => d.Filetype).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.Filetypeid)
                .HasConstraintName("picture_filetypeid_fkey");
        });

        modelBuilder.Entity<Purchaseorder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchaseorder_pkey");

            entity.ToTable("purchaseorder", "OneShotShop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Email)
                .HasMaxLength(48)
                .HasColumnName("email");
            entity.Property(e => e.Notes)
                .HasMaxLength(256)
                .HasColumnName("notes");
            entity.Property(e => e.Orderdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("orderdate");

            entity.HasOne(d => d.Account).WithMany(p => p.Purchaseorders)
                .HasForeignKey(d => d.Accountid)
                .HasConstraintName("purchaseorder_accountid_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("review_pkey");

            entity.ToTable("review", "OneShotShop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Reviewbody)
                .HasMaxLength(256)
                .HasColumnName("reviewbody");
            entity.Property(e => e.Reviewdate).HasColumnName("reviewdate");

            entity.HasOne(d => d.Account).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Accountid)
                .HasConstraintName("review_accountid_fkey");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status", "OneShotShop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status1)
                .HasMaxLength(16)
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
