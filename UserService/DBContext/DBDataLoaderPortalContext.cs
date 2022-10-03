using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserService.DBContext
{
    public partial class DBDataLoaderPortalContext : DbContext
    {
        public DBDataLoaderPortalContext()
        {
        }

        public DBDataLoaderPortalContext(DbContextOptions<DBDataLoaderPortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoginMaster> LoginMasters { get; set; } = null!;
        public virtual DbSet<PatientMaster> PatientMasters { get; set; } = null!;
        public virtual DbSet<RoleMaster> RoleMasters { get; set; } = null!;
        public virtual DbSet<UploadFileLog> UploadFileLogs { get; set; } = null!;
        public virtual DbSet<ValidationFailedMaster> ValidationFailedMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DBDataLoaderPortal;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginMaster>(entity =>
            {
                entity.ToTable("LoginMaster");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LoginMasters)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginMaster_RoleMaster");
            });

            modelBuilder.Entity<PatientMaster>(entity =>
            {
                entity.ToTable("PatientMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.District)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.DrugId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Drug_ID");

                entity.Property(e => e.DrugName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Drug_Name");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Email_Id");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.ModifiedBy).HasColumnName("Modified_By");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Modified_Date");

                entity.Property(e => e.PatientName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Patient_Name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Number");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("RoleMaster");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Role_Name");
            });

            modelBuilder.Entity<UploadFileLog>(entity =>
            {
                entity.HasKey(e => e.FileUploadId);

                entity.Property(e => e.FileUploadId).HasColumnName("FileUpload_Id");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate).HasColumnName("Created_Date");

                entity.Property(e => e.FileLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("File_Location");

                entity.Property(e => e.FileName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("File_Name");

                entity.Property(e => e.SavedRecordsCount).HasColumnName("Saved_Records_Count");

                entity.Property(e => e.ServerFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Server_File_Name");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalRecordsCount).HasColumnName("Total_Records_Count");

                entity.Property(e => e.ValidationFailedFileLocation)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Validation_Failed_File_Location");

                entity.Property(e => e.ValidationFailedFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Validation_Failed_File_Name");

                entity.Property(e => e.ValidationFailedRecordsCount).HasColumnName("Validation_Failed_Records_Count");
            });

            modelBuilder.Entity<ValidationFailedMaster>(entity =>
            {
                entity.ToTable("ValidationFailedMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.District)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOB");

                entity.Property(e => e.DrugId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Drug_ID");

                entity.Property(e => e.DrugName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Drug_Name");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Email_Id");

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.PatientName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Patient_Name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Number");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
