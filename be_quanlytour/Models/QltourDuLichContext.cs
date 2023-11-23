using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace be_quanlytour.Models;

public partial class QltourDuLichContext : DbContext
{
    public QltourDuLichContext()
    {
    }

    public QltourDuLichContext(DbContextOptions<QltourDuLichContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaiViet> BaiViets { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingK> BookingKs { get; set; }

    public virtual DbSet<BookingTour> BookingTours { get; set; }

    public virtual DbSet<DanhGia> DanhGia { get; set; }

    public virtual DbSet<DiaDiem> DiaDiems { get; set; }

    public virtual DbSet<DiemDen> DiemDens { get; set; }

    public virtual DbSet<DoiTac> DoiTacs { get; set; }

    public virtual DbSet<HanhKhach> HanhKhaches { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhachSan> KhachSans { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LichTrinh> LichTrinhs { get; set; }

    public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

    public virtual DbSet<MucHoiDap> MucHoiDaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhuongTien> PhuongTiens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TaiKhoanNv> TaiKhoanNvs { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourKhuyenMai> TourKhuyenMais { get; set; }

    public virtual DbSet<TourPhuongTien> TourPhuongTiens { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaiViet>(entity =>
        {
            entity.HasKey(e => e.IdBaiViet).HasName("PK__BaiViet__42161C7A7AB6431A");

            entity.ToTable("BaiViet");

            entity.Property(e => e.AnhBia).IsUnicode(false);
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayDang).HasColumnType("date");
            entity.Property(e => e.TieuDe).HasMaxLength(200);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.BaiViets)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaiViet__MaNV__76969D2E");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.IdBooking).HasName("PK__Booking__7271F57675365868");

            entity.ToTable("Booking");

            entity.Property(e => e.IdBooking)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.MaVoucher)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ThoiDiemBook).HasColumnType("date");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__MaKH__68487DD7");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__Booking__MaNV__6754599E");

            entity.HasOne(d => d.MaVoucherNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MaVoucher)
                .HasConstraintName("FK__Booking__MaVouch__693CA210");
        });

        modelBuilder.Entity<BookingK>(entity =>
        {
            entity.HasKey(e => e.IdBookingKs).HasName("PK__BookingK__7158ED4C299614AD");

            entity.ToTable("BookingKS");

            entity.Property(e => e.IdBookingKs).HasColumnName("IdBookingKS");
            entity.Property(e => e.IdBooking)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NgayNhan).HasColumnType("datetime");
            entity.Property(e => e.NgayTra).HasColumnType("datetime");

            entity.HasOne(d => d.IdBookingNavigation).WithMany(p => p.BookingKs)
                .HasForeignKey(d => d.IdBooking)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingKS__IdBoo__6D0D32F4");

            entity.HasOne(d => d.IdLoaiPhongNavigation).WithMany(p => p.BookingKs)
                .HasForeignKey(d => d.IdLoaiPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingKS__IdLoa__6C190EBB");
        });

        modelBuilder.Entity<BookingTour>(entity =>
        {
            entity.HasKey(e => e.IdBookingTour).HasName("PK__BookingT__1AE13BB43C982AB8");

            entity.ToTable("BookingTour");

            entity.Property(e => e.IdBooking)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaTour)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdBookingNavigation).WithMany(p => p.BookingTours)
                .HasForeignKey(d => d.IdBooking)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingTo__IdBoo__6B24EA82");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.BookingTours)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingTo__MaTou__6A30C649");
        });

        modelBuilder.Entity<DanhGia>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGia__AA9515BFF1C2DDA8");

            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.MaTour)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ThoiDiem).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__MaKH__797309D9");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__MaTour__787EE5A0");
        });

        modelBuilder.Entity<DiaDiem>(entity =>
        {
            entity.HasKey(e => e.IdDiaDiem).HasName("PK__DiaDiem__5EA8273AF32D58EB");

            entity.ToTable("DiaDiem");

            entity.Property(e => e.TenDiaDiem).HasMaxLength(50);
        });

        modelBuilder.Entity<DiemDen>(entity =>
        {
            entity.HasKey(e => e.IdDiemDen).HasName("PK__DiemDen__D267BA447F2E5E3C");

            entity.ToTable("DiemDen");

            entity.Property(e => e.IdDiemDen)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenDiemDen).HasMaxLength(100);

            entity.HasOne(d => d.IdDiaDiemNavigation).WithMany(p => p.DiemDens)
                .HasForeignKey(d => d.IdDiaDiem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiemDen__IdDiaDi__6477ECF3");
        });

        modelBuilder.Entity<DoiTac>(entity =>
        {
            entity.HasKey(e => e.IdDoiTac).HasName("PK__DoiTac__F260D42074147D2B");

            entity.ToTable("DoiTac");

            entity.Property(e => e.IdDoiTac)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoaiDt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoDienThoaiDT");
            entity.Property(e => e.Ten).HasMaxLength(100);
        });

        modelBuilder.Entity<HanhKhach>(entity =>
        {
            entity.HasKey(e => e.IdHanhKhach).HasName("PK__HanhKhac__A798BF08127886A1");

            entity.ToTable("HanhKhach");

            entity.Property(e => e.LoaiHanhKhach).HasMaxLength(30);
            entity.Property(e => e.TenHanhKhach).HasMaxLength(50);

            entity.HasOne(d => d.IdBookingTourNavigation).WithMany(p => p.HanhKhaches)
                .HasForeignKey(d => d.IdBookingTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HanhKhach__IdBoo__73BA3083");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E332F3BE7");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaPassport)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoCccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoCCCD");
            entity.Property(e => e.SoDienThoaiKh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoDienThoaiKH");
        });

        modelBuilder.Entity<KhachSan>(entity =>
        {
            entity.HasKey(e => e.IdKhachSan).HasName("PK__KhachSan__0F22D8A12E4364AD");

            entity.ToTable("KhachSan");

            entity.Property(e => e.IdKhachSan)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.IdDoiTac)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Ten).HasMaxLength(100);

            entity.HasOne(d => d.IdDoiTacNavigation).WithMany(p => p.KhachSans)
                .HasForeignKey(d => d.IdDoiTac)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhachSan__IdDoiT__656C112C");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF15225AEBEF");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.MaKm).HasColumnName("MaKM");
            entity.Property(e => e.PhanTramKm).HasColumnName("PhanTramKM");
            entity.Property(e => e.TenKm)
                .HasMaxLength(100)
                .HasColumnName("TenKM");
            entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");
        });

        modelBuilder.Entity<LichTrinh>(entity =>
        {
            entity.HasKey(e => new { e.IdDiemDen, e.MaTour }).HasName("PK__LichTrin__2682EF395134BFE1");

            entity.ToTable("LichTrinh");

            entity.Property(e => e.IdDiemDen)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaTour)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Ngay).HasColumnType("date");

            entity.HasOne(d => d.IdDiemDenNavigation).WithMany(p => p.LichTrinhs)
                .HasForeignKey(d => d.IdDiemDen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichTrinh__IdDie__70DDC3D8");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.LichTrinhs)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichTrinh__MaTou__71D1E811");
        });

        modelBuilder.Entity<LoaiPhong>(entity =>
        {
            entity.HasKey(e => e.IdLoaiPhong).HasName("PK__LoaiPhon__E8A0463AAD4E404D");

            entity.ToTable("LoaiPhong");

            entity.Property(e => e.IdKhachSan)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenLoai).HasMaxLength(100);

            entity.HasOne(d => d.IdKhachSanNavigation).WithMany(p => p.LoaiPhongs)
                .HasForeignKey(d => d.IdKhachSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoaiPhong__IdKha__619B8048");
        });

        modelBuilder.Entity<MucHoiDap>(entity =>
        {
            entity.HasKey(e => e.IdHoiDap).HasName("PK__MucHoiDa__85D875B6BE8D4FED");

            entity.ToTable("MucHoiDap");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.MucHoiDaps)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MucHoiDap__MaNV__778AC167");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A9E4E22D8");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.ChucVu).HasMaxLength(30);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.SoCccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoCCCD");
            entity.Property(e => e.SoDienThoaiNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoDienThoaiNV");
        });

        modelBuilder.Entity<PhuongTien>(entity =>
        {
            entity.HasKey(e => e.IdPhuongTien).HasName("PK__PhuongTi__3262AF135981B2F4");

            entity.ToTable("PhuongTien");

            entity.Property(e => e.IdDoiTac)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TenPhuongTien).HasMaxLength(50);

            entity.HasOne(d => d.IdDoiTacNavigation).WithMany(p => p.PhuongTiens)
                .HasForeignKey(d => d.IdDoiTac)
                .HasConstraintName("FK__PhuongTie__IdDoi__72C60C4A");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.IdTaiKhoan).HasName("PK__TaiKhoan__9A53D3DD2B3EC305");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.IdTaiKhoan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaKH");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan__MaKH__628FA481");
        });

        modelBuilder.Entity<TaiKhoanNv>(entity =>
        {
            entity.HasKey(e => e.IdTaiKhoan).HasName("PK__TaiKhoan__9A53D3DD6256B4D0");

            entity.ToTable("TaiKhoanNV");

            entity.Property(e => e.IdTaiKhoan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TaiKhoanNvs)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoanNV__MaNV__6383C8BA");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.MaTour).HasName("PK__Tour__4E5557DEA87B959F");

            entity.ToTable("Tour");

            entity.Property(e => e.MaTour)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AnhBia)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.GioTapTrung).HasColumnType("datetime");
            entity.Property(e => e.NgayKhoiHanh).HasColumnType("date");
            entity.Property(e => e.NoiKhoiHanh).HasMaxLength(100);
            entity.Property(e => e.TenTour).HasMaxLength(100);
        });

        modelBuilder.Entity<TourKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tour_Khu__3214EC27F8582461");

            entity.ToTable("Tour_KhuyenMai");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaKm).HasColumnName("MaKM");
            entity.Property(e => e.MaTour)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.TourKhuyenMais)
                .HasForeignKey(d => d.MaKm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Khuye__MaKM__6EF57B66");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.TourKhuyenMais)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Khuy__MaTou__6FE99F9F");
        });

        modelBuilder.Entity<TourPhuongTien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tour_Phu__3214EC27EFBE1C67");

            entity.ToTable("Tour_PhuongTien");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaTour)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdPhuongTienNavigation).WithMany(p => p.TourPhuongTiens)
                .HasForeignKey(d => d.IdPhuongTien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Phuo__IdPhu__75A278F5");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.TourPhuongTiens)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour_Phuo__MaTou__74AE54BC");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.MaVoucher).HasName("PK__Voucher__0AAC5B116412EB11");

            entity.ToTable("Voucher");

            entity.Property(e => e.MaVoucher)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdDoiTac)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenVoucher).HasMaxLength(100);
            entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");

            entity.HasOne(d => d.IdDoiTacNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.IdDoiTac)
                .HasConstraintName("FK__Voucher__IdDoiTa__66603565");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
