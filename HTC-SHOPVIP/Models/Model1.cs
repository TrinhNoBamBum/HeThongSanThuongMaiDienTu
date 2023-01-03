using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HTC_SHOPVIP.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Blog_anh> Blog_anh { get; set; }
        public virtual DbSet<Blog_Danhgia> Blog_Danhgia { get; set; }
        public virtual DbSet<Blog_Theloai> Blog_Theloai { get; set; }
        public virtual DbSet<ChucNang> ChucNangs { get; set; }
        public virtual DbSet<CTDonHang> CTDonHangs { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }
        public virtual DbSet<Khachhang> Khachhangs { get; set; }
        public virtual DbSet<khuyenmai> khuyenmais { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<Sanpham> Sanphams { get; set; }
        public virtual DbSet<Slide_anh> Slide_anh { get; set; }
        public virtual DbSet<SP_anh> SP_anh { get; set; }
        public virtual DbSet<SP_Danhgia> SP_Danhgia { get; set; }
        public virtual DbSet<SP_Theloai> SP_Theloai { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<KM_SP> KM_SPs { get; set; }
        public virtual DbSet<AnhSPLienKet> AnhSPLienKets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasOptional(e => e.Blog_anh)
                .WithRequired(e => e.Blog);

            modelBuilder.Entity<Blog>()
                .HasMany(e => e.Blog_Danhgia)
                .WithRequired(e => e.Blog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChucNang>()
                .HasMany(e => e.PhanQuyens)
                .WithRequired(e => e.ChucNang)
                .HasForeignKey(e => e.idChucNang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.CTDonHangs)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangSanXuat>()
                .Property(e => e.TenHang)
                .IsFixedLength();

            modelBuilder.Entity<Khachhang>()
                .Property(e => e.username)
                .IsFixedLength();

            modelBuilder.Entity<Khachhang>()
                .Property(e => e.matkhau)
                .IsFixedLength();

            modelBuilder.Entity<Khachhang>()
                .Property(e => e.sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Khachhang>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<Khachhang>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.Khachhang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Khachhang>()
                .HasMany(e => e.SP_Danhgia)
                .WithRequired(e => e.Khachhang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<khuyenmai>()
                .HasMany(e => e.Khachhangs)
                .WithMany(e => e.khuyenmais)
                .Map(m => m.ToTable("KM_KH").MapLeftKey("MaKM").MapRightKey("MaKH"));

            //modelBuilder.Entity<khuyenmai>()
            //    .HasMany(e => e.Sanphams)
            //    .WithMany(e => e.khuyenmais)
            //    .Map(m => m.ToTable("KM_SP").MapLeftKey("MaKM").MapRightKey("MaSP"));

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.username)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.matkhau)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhanQuyens)
                .WithRequired(e => e.NhanVien)
                .HasForeignKey(e => e.idNhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.trongluong)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.Memory)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.Cam1)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.Cam2)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.Ram)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.Pin)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.Manhinh)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.Chip)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .Property(e => e.NamRaMat)
                .IsFixedLength();

            modelBuilder.Entity<Sanpham>()
                .HasMany(e => e.CTDonHangs)
                .WithRequired(e => e.Sanpham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sanpham>()
                .HasMany(e => e.SP_anh)
                .WithRequired(e => e.Sanpham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sanpham>()
                .HasMany(e => e.SP_Danhgia)
                .WithRequired(e => e.Sanpham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SP_anh>()
                .Property(e => e.Linkanh)
                .IsFixedLength();

            modelBuilder.Entity<SP_Theloai>()
                .HasMany(e => e.Sanphams)
                .WithRequired(e => e.SP_Theloai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.tk)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.mk)
                .IsFixedLength();
        }
    }
}
