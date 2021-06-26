using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPISqlDependencyDemo
{
    public partial class Fake_SAPContext : DbContext
    {
        public Fake_SAPContext()
        {
        }

        public Fake_SAPContext(DbContextOptions<Fake_SAPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AklContentTote> AklContentTotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(Appsettings.SQL_SERVER_CONNECTION_STRING);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AklContentTote>(entity =>
            {
                entity.ToTable("AKL_CONTENT_TOTE");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aufnr)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("aufnr")
                    .IsFixedLength(true);

                entity.Property(e => e.Charg)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("charg")
                    .IsFixedLength(true);

                entity.Property(e => e.Matnr)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("matnr")
                    .IsFixedLength(true);

                entity.Property(e => e.Meins)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("meins")
                    .HasDefaultValueSql("('PC')")
                    .IsFixedLength(true);

                entity.Property(e => e.MengeAufnr)
                    .HasColumnType("decimal(16, 3)")
                    .HasColumnName("menge_aufnr");

                entity.Property(e => e.MengeSource)
                    .HasColumnType("decimal(16, 3)")
                    .HasColumnName("menge_source");

                entity.Property(e => e.MengeTarget)
                    .HasColumnType("decimal(16, 3)")
                    .HasColumnName("menge_target");

                entity.Property(e => e.PmsDate)
                    .HasColumnType("date")
                    .HasColumnName("pms_date");

                entity.Property(e => e.PmsTime).HasColumnName("pms_time");

                entity.Property(e => e.PmsUser)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("pms_user")
                    .IsFixedLength(true);

                entity.Property(e => e.TypeCounting)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("type_counting")
                    .IsFixedLength(true);

                entity.Property(e => e.Vlenr)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vlenr")
                    .IsFixedLength(true);

                entity.Property(e => e.Workstation)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("workstation")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
