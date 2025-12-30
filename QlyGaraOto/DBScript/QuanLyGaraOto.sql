CREATE DATABASE QUANLYGARA --Đã sửa chữa lại những lỗi sai (2)
--Nhập đơn giá nhập, thì cập nhật trong bảng phụ tùng, cập nhật tồn kho và đơn giá nhập, 
--đồng thời cập nhật giá bán theo tỉ lệ lợi nhuận.
USE QUANLYGARA

-- 1.NGUOIDUNG
CREATE TABLE NGUOIDUNG 
(
	MaNguoiDung char(7) primary key not null,
	TenNguoiDung nvarchar(30) not null,
	NgaySinh datetime,
	TenDangNhap varchar(30) not null unique,
	MatKhauBam varchar(250) not null,
	MaNhomND char(7) not null 
)

-- 2.NHOMNGUOIDUNG
CREATE TABLE NHOMNGUOIDUNG 
(
	MaNhomND char(7) primary key not null, 
	TenNhomNguoiDung nvarchar(30) not null unique,
)

-- 3.PHANQUYEN
CREATE TABLE PHANQUYEN 
(
	MaChucNang char(7) not null, 
	MaNhomND char(7) not null, 
	primary key (MaChucNang, MaNhomND),
)

-- 4.CHUCNANG
CREATE TABLE CHUCNANG 
(
	MaChucNang char(7) primary key not null,
	TenChucNang nvarchar(50) not null unique,
	TenManHinh nvarchar(50) not null,
)

-- 5.XE
CREATE TABLE XE 
(
	BienSo char(20) primary key not null,
	HieuXe varchar(30) not null, 
	TenChuXe nvarchar(50) not null, 
	DiaChi nvarchar(200),
	SDT char(20),
	Email varchar(50),
	GhiChu nvarchar(1000),
	NgayTiepNhan datetime default getdate(),
	TongNo money 
)


-- 6.HIEUXE
CREATE TABLE HIEUXE
(
	HieuXe varchar(30) primary key not null,
)


-- 7.NOIDUNGSUACHUA
CREATE TABLE NOIDUNGSUACHUA
(
	MaNoiDung char(7) primary key not null,
	MoTa nvarchar(1000),
	TienCong money 
)

-- 8.PHIEUTHUTIEN
CREATE TABLE PHIEUTHUTIEN
(
	MaPhieuThu char(7) primary key not null,
	BienSo char(20) not null,
	NgayThu datetime default getDate(),
	SoTienThu money
)
-- 9.DOANHSO
CREATE TABLE DOANHSO
(
	MaBCDS char(7) primary key,
	Thang int not null,
	Nam int not null,
	TongDoanhSo money not null,
)

-- 10.CT_DOANHSO
CREATE TABLE CT_DOANHSO
(
	MaBCDS char(7) not null, 
	HieuXe varchar(30) not null, 
	primary key (MaBCDS, HieuXe),
	SoLuotSua int, 
	ThanhTien money,
	TiLe float, 
)

-- 11.PHIEUSUACHUA
CREATE TABLE PHIEUSUACHUA
(
	MaPhieuSC char(7) primary key,
	BienSo char(20) not null,
	NgaySuaChua datetime default getdate(),
	TongTien money default 0, 
)

-- 12.CT_PHIEUSUACHUA
CREATE TABLE CT_PHIEUSUACHUA
(
	MaPhieuSC char(7) not null, 
	MaPhuTung char(7) not null, 
	MaNoiDung char(7) not null, 
	SoLuong int,
	DonGia money,
	ThanhTien money,
	primary key (MaPhieuSC, MaNoiDung, MaPhuTung),
	TienCong money
) 

-- 13.PHUTUNG
CREATE TABLE PHUTUNG
(
	MaPhuTung char(7) primary key,
	TenPhuTung nvarchar(50) not null,
	DonGiaNhap money,
	DonGiaBan money, 
	SoLuongTon int, 
)

-- 14.PHIEUNHAPPHUTUNG
CREATE TABLE PHIEUNHAPPHUTUNG
(
	MaPhieuNhap char(7) primary key,
	NgayNhap datetime default getdate(),
	TongTien money
)


-- 15.CT_PHIEUNHAPPHUTUNG
CREATE TABLE CT_PHIEUNHAPPHUTUNG 
(
	MaPhieuNhap char(7) not null,
	MaPhuTung char(7) not null, 
	primary key (MaPhieuNhap, MaPhuTung),
	SoLuongNhap int, 
	DonGiaNhap money, 
	ThanhTien money
)

-- 16.CT_BAOCAOTON
CREATE TABLE CT_BAOCAOTON
(
	Thang int not null, 
	Nam int not null,
	MaPhuTung char(7) not null, 
	primary key (Thang, Nam, MaPhuTung), 
	TonDau int, 
	PhatSinh int,
	TonCuoi int 
)

-- 17. QUY DINH 
CREATE TABLE QUYDINH(
	SoXeSuaChuaToiDa int primary key,
	TiLeLai float
)

ALTER TABLE XE ADD CHECK(TongNo >= 0)
ALTER TABLE NOIDUNGSUACHUA ADD CHECK(TienCong > 0)
ALTER TABLE PHIEUTHUTIEN ADD CHECK(SoTienThu > 0)
ALTER TABLE DOANHSO ADD CHECK(Thang between 1 and 12)
ALTER TABLE DOANHSO ADD CHECK(Nam > 0)
ALTER TABLE DOANHSO ADD CHECK(TongDoanhSo > 0)
ALTER TABLE CT_DOANHSO ADD CHECK(SoLuotSua >= 0)
ALTER TABLE CT_DOANHSO ADD CHECK(ThanhTien >=0)
ALTER TABLE CT_DOANHSO ADD CHECK(TiLe between 0 and 100)
ALTER TABLE PHIEUSUACHUA ADD CHECK(TongTien >= 0)
ALTER TABLE CT_PHIEUSUACHUA ADD CHECK(TienCong >= 0)
ALTER TABLE CT_PHIEUSUACHUA ADD CHECK(SoLuong >= 0)
ALTER TABLE CT_PHIEUSUACHUA ADD CHECK(DonGia >= 0)
ALTER TABLE CT_PHIEUSUACHUA ADD CHECK(ThanhTien >= 0)
ALTER TABLE PHUTUNG ADD CHECK(DonGiaNhap >= 0)
ALTER TABLE PHUTUNG ADD CHECK(DonGiaBan >= 0)
ALTER TABLE PHIEUNHAPPHUTUNG ADD CHECK(TongTien >= 0)
ALTER TABLE CT_PHIEUNHAPPHUTUNG ADD CHECK(SoLuongNhap >= 0)
ALTER TABLE CT_PHIEUNHAPPHUTUNG ADD CHECK(DonGiaNhap >= 0)
ALTER TABLE CT_PHIEUNHAPPHUTUNG ADD CHECK(ThanhTien >= 0)
ALTER TABLE CT_BAOCAOTON ADD CHECK(Thang between 1 and 12)
ALTER TABLE CT_BAOCAOTON ADD CHECK(Nam > 0)
ALTER TABLE CT_BAOCAOTON ADD CHECK(TonDau >= 0)
ALTER TABLE CT_BAOCAOTON ADD CHECK(PhatSinh >= 0)
ALTER TABLE CT_BAOCAOTON ADD CHECK(TonCuoi >= 0)

-- NGUOIDUNG → NHOMNGUOIDUNG
ALTER TABLE NGUOIDUNG
ADD CONSTRAINT FK_NGUOIDUNG_NHOMNGUOIDUNG
FOREIGN KEY (MaNhomND) REFERENCES NHOMNGUOIDUNG(MaNhomND);

-- PHANQUYEN → NHOMNGUOIDUNG
ALTER TABLE PHANQUYEN
ADD CONSTRAINT FK_PHANQUYEN_NHOMNGUOIDUNG
FOREIGN KEY (MaNhomND) REFERENCES NHOMNGUOIDUNG(MaNhomND);

-- PHANQUYEN → CHUCNANG
ALTER TABLE PHANQUYEN
ADD CONSTRAINT FK_PHANQUYEN_CHUCNANG
FOREIGN KEY (MaChucNang) REFERENCES CHUCNANG(MaChucNang);

-- XE → HIEUXE
ALTER TABLE XE
ADD CONSTRAINT FK_XE_HIEUXE
FOREIGN KEY (HieuXe) REFERENCES HIEUXE(HieuXe);

-- PHIEUSUACHUA → XE
ALTER TABLE PHIEUSUACHUA
ADD CONSTRAINT FK_PHIEUSUACHUA_XE
FOREIGN KEY (BienSo) REFERENCES XE(BienSo);

-- CT_PHIEUSUACHUA → PHIEUSUACHUA
ALTER TABLE CT_PHIEUSUACHUA
ADD CONSTRAINT FK_CT_PHIEUSUACHUA_PHIEUSUACHUA
FOREIGN KEY (MaPhieuSC) REFERENCES PHIEUSUACHUA(MaPhieuSC);

-- CT_PHIEUSUACHUA → PHUTUNG
ALTER TABLE CT_PHIEUSUACHUA
ADD CONSTRAINT FK_CT_PHIEUSUACHUA_PHUTUNG
FOREIGN KEY (MaPhuTung) REFERENCES PHUTUNG(MaPhuTung);

-- CT_PHIEUSUACHUA → NOIDUNGSUACHUA
ALTER TABLE CT_PHIEUSUACHUA
ADD CONSTRAINT FK_CT_PHIEUSUACHUA_NOIDUNGSUACHUA
FOREIGN KEY (MaNoiDung) REFERENCES NOIDUNGSUACHUA(MaNoiDung);

-- PHIEUTHUTIEN → XE
ALTER TABLE PHIEUTHUTIEN
ADD CONSTRAINT FK_PHIEUTHUTIEN_XE
FOREIGN KEY (BienSo) REFERENCES XE(BienSo);

-- CT_DOANHSO → DOANHSO
ALTER TABLE CT_DOANHSO
ADD CONSTRAINT FK_CT_DOANHSO_DOANHSO
FOREIGN KEY (MaBCDS) REFERENCES DOANHSO(MaBCDS);

-- CT_DOANHSO → HIEUXE
ALTER TABLE CT_DOANHSO
ADD CONSTRAINT FK_CT_DOANHSO_HIEUXE
FOREIGN KEY (HieuXe) REFERENCES HIEUXE(HieuXe);

-- CT_PHIEUNHAPPHUTUNG → PHIEUNHAPPHUTUNG
ALTER TABLE CT_PHIEUNHAPPHUTUNG
ADD CONSTRAINT FK_CT_PHIEUNHAPPHUTUNG_PHIEUNHAPPHUTUNG
FOREIGN KEY (MaPhieuNhap) REFERENCES PHIEUNHAPPHUTUNG(MaPhieuNhap);

-- CT_PHIEUNHAPPHUTUNG → PHUTUNG
ALTER TABLE CT_PHIEUNHAPPHUTUNG
ADD CONSTRAINT FK_CT_PHIEUNHAPPHUTUNG_PHUTUNG
FOREIGN KEY (MaPhuTung) REFERENCES PHUTUNG(MaPhuTung);

-- CT_BAOCAOTON → PHUTUNG
ALTER TABLE CT_BAOCAOTON
ADD CONSTRAINT FK_CT_BAOCAOTON_PHUTUNG
FOREIGN KEY (MaPhuTung) REFERENCES PHUTUNG(MaPhuTung);

ALTER DATABASE QUANLYGARA SET AUTO_CLOSE OFF;
ALTER DATABASE QUANLYGARA SET AUTO_SHRINK OFF;

WITH DoanhThuTheoHieuXe AS (
    SELECT 
        X.HieuXe,
        SUM(PTT.SoTienThu) AS TongTien
    FROM PHIEUTHUTIEN PTT
    JOIN XE X ON X.BienSo = PTT.BienSo
    WHERE MONTH(PTT.NgayThu) = 12
      AND YEAR(PTT.NgayThu) = 2025
    GROUP BY X.HieuXe
),
LuotSua AS (
    SELECT 
        X.HieuXe,
        COUNT(DISTINCT PSC.MaPhieuSC) AS SoLuotSua
    FROM PHIEUSUACHUA PSC
    JOIN XE X ON X.BienSo = PSC.BienSo
    WHERE MONTH(PSC.NgaySuaChua) = 12
      AND YEAR(PSC.NgaySuaChua) = 2025
    GROUP BY X.HieuXe
),
TongDoanhThu AS (
    SELECT SUM(TongTien) AS TongTienThang
    FROM DoanhThuTheoHieuXe
)
SELECT 
    HX.HieuXe,
    ISNULL(DT.TongTien, 0) AS TongTien,
    ISNULL(LS.SoLuotSua, 0) AS SoLuotSua,
    CASE 
        WHEN TDT.TongTienThang = 0 THEN 0
        ELSE ROUND(ISNULL(DT.TongTien,0) * 100.0 / TDT.TongTienThang, 2)
    END AS TiLePhanTram
FROM HIEUXE HX
LEFT JOIN DoanhThuTheoHieuXe DT ON DT.HieuXe = HX.HieuXe
LEFT JOIN LuotSua LS ON LS.HieuXe = HX.HieuXe
CROSS JOIN TongDoanhThu TDT;
select ISNULL(sum(SoTienThu), 0)
from PHIEUTHUTIEN
where MONTH(NgayThu) = 11 AND YEAR(NgayThu) = 2025

WITH T AS 
(SELECT CT.MaPhuTung, PT.TenPhuTung, COUNT(CT.SoLuong) AS SoLuong
FROM CT_PHIEUSUACHUA CT JOIN PHUTUNG PT ON PT.MaPhuTung = CT.MaPhuTung
						JOIN PHIEUSUACHUA PSC ON PSC.MaPhieuSC = CT.MaPhieuSC
WHERE MONTH(PSC.NgaySuaChua) = 12 AND YEAR(PSC.NgaySuaChua) = 2025
GROUP BY CT.MaPhuTung, PT.TenPhuTung)
SELECT PT.MaPhuTung, PT.TenPhuTung,(ISNULL(T.SoLuong, 0) + pt.SoLuongTon) AS TonDau ,ISNULL(T.SoLuong, 0) AS PhatSinh, PT.SoLuongTon AS TonCuoi
FROM PHUTUNG PT LEFT JOIN T ON T.MaPhuTung = PT.MaPhuTung

SET DATEFORMAT dmy;
INSERT INTO NHOMNGUOIDUNG (MaNhomND, TenNhomNguoiDung) VALUES ('GR00001', 'Admin');
INSERT INTO NHOMNGUOIDUNG (MaNhomND, TenNhomNguoiDung) VALUES ('GR00002', N'Nhân viên');
INSERT INTO NGUOIDUNG (MaNguoiDung, TenNguoiDung, NgaySinh, TenDangNhap, MatKhauBam, MaNhomND) VALUES
					  ('US00001', N'Hoàng Quốc Tùng', '11/12/2003', 'TungLee', 'JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=', 'GR00001')
INSERT INTO HIEUXE(HieuXe) VALUES('Toyota'),
								 ('Honda'),
								 ('Ford'),
								 ('Nissan'),
								 ('Mitsubishi'),
								 ('Mazda'),
								 ('KIA'),
								 ('Vinfast'),
								 ('Huyndai')
INSERT INTO HIEUXE (HieuXe) VALUES('Suzuki')
INSERT INTO QUYDINH (SoXeSuaChuaToiDa, TiLeLai) VALUES(30, 1.05)

INSERT INTO PHUTUNG (MaPhuTung, TenPhuTung, DonGiaNhap, DonGiaBan, SoLuongTon) VALUES
('PT00001', N'Lọc nhớt động cơ', 80000, 120000, 50),
('PT00002', N'Lọc gió động cơ', 90000, 140000, 40),
('PT00003', N'Lọc gió điều hòa', 100000, 150000, 35),
('PT00004', N'Bugi đánh lửa', 60000, 100000, 60),
('PT00005', N'Dây curoa cam', 250000, 350000, 20),
('PT00006', N'Dây curoa tổng', 220000, 320000, 25),
('PT00007', N'Má phanh trước', 300000, 450000, 30),
('PT00008', N'Má phanh sau', 280000, 420000, 28),
('PT00009', N'Đĩa phanh trước', 500000, 750000, 15),
('PT00010', N'Đĩa phanh sau', 480000, 720000, 15),

('PT00011', N'Dầu nhớt động cơ', 350000, 500000, 100),
('PT00012', N'Dầu hộp số tự động', 600000, 850000, 40),
('PT00013', N'Dầu hộp số sàn', 450000, 650000, 45),
('PT00014', N'Dầu phanh', 120000, 180000, 70),
('PT00015', N'Dầu trợ lực lái', 150000, 220000, 55),
('PT00016', N'Nước làm mát động cơ', 90000, 140000, 80),
('PT00017', N'Ắc quy 45Ah', 900000, 1200000, 18),
('PT00018', N'Ắc quy 60Ah', 1100000, 1450000, 15),
('PT00019', N'Bơm xăng', 850000, 1200000, 12),
('PT00020', N'Bơm nước làm mát', 780000, 1100000, 14),

('PT00021', N'Quạt két nước', 650000, 950000, 16),
('PT00022', N'Két nước làm mát', 1200000, 1700000, 10),
('PT00023', N'Cảm biến oxy', 700000, 1000000, 12),
('PT00024', N'Cảm biến nhiệt độ', 350000, 550000, 20),
('PT00025', N'Cảm biến áp suất lốp', 500000, 750000, 18),
('PT00026', N'Đèn pha trước', 400000, 650000, 25),
('PT00027', N'Đèn hậu', 380000, 600000, 22),
('PT00028', N'Gương chiếu hậu', 550000, 800000, 20),
('PT00029', N'Cần gạt mưa', 120000, 200000, 60),
('PT00030', N'Mô tơ gạt mưa', 450000, 650000, 18),

('PT00031', N'Bộ ly hợp', 1800000, 2500000, 8),
('PT00032', N'Bàn ép ly hợp', 1200000, 1700000, 10),
('PT00033', N'Đĩa ly hợp', 1000000, 1450000, 12),
('PT00034', N'Giảm xóc trước', 900000, 1300000, 14),
('PT00035', N'Giảm xóc sau', 850000, 1250000, 14),
('PT00036', N'Rotuyn lái', 300000, 480000, 25),
('PT00037', N'Thanh cân bằng', 400000, 600000, 20),
('PT00038', N'Ổ bi bánh xe', 350000, 520000, 30),
('PT00039', N'Vòng bi trục', 280000, 430000, 35),
('PT00040', N'Cao su chân máy', 450000, 700000, 18),

('PT00041', N'Cao su chân hộp số', 420000, 680000, 18),
('PT00042', N'Bugi sấy', 150000, 250000, 40),
('PT00043', N'Kim phun nhiên liệu', 900000, 1300000, 10),
('PT00044', N'Bộ lọc nhiên liệu', 200000, 320000, 30),
('PT00045', N'Cổ hút gió', 650000, 950000, 12),
('PT00046', N'Cổ xả', 700000, 1000000, 10),
('PT00047', N'Ống nước làm mát', 180000, 300000, 50),
('PT00048', N'Dây điện động cơ', 600000, 900000, 15),
('PT00049', N'Hộp cầu chì', 500000, 750000, 18),
('PT00050', N'Rơ le điện', 120000, 200000, 60),

('PT00051', N'Công tắc đèn', 150000, 240000, 40),
('PT00052', N'Công tắc kính', 200000, 320000, 35),
('PT00053', N'Mô tơ kính', 450000, 650000, 20),
('PT00054', N'Khóa cửa', 380000, 580000, 25),
('PT00055', N'Bộ khóa trung tâm', 700000, 1050000, 15),
('PT00056', N'Camera lùi', 650000, 950000, 12),
('PT00057', N'Cảm biến lùi', 500000, 750000, 18),
('PT00058', N'Màn hình DVD', 2200000, 3000000, 6),
('PT00059', N'Loa cửa xe', 350000, 550000, 30),
('PT00060', N'Ampli ô tô', 1800000, 2500000, 7),

('PT00061', N'Vô lăng', 1200000, 1700000, 10),
('PT00062', N'Túi khí lái', 3000000, 4200000, 4),
('PT00063', N'Túi khí phụ', 2800000, 4000000, 4),
('PT00064', N'Dây an toàn', 500000, 750000, 20),
('PT00065', N'Ghế trước', 2200000, 3200000, 5),
('PT00066', N'Ghế sau', 2000000, 3000000, 5),
('PT00067', N'Tấm lót sàn', 400000, 650000, 25),
('PT00068', N'Ốp taplo', 850000, 1250000, 10),
('PT00069', N'Ốp cửa', 600000, 900000, 15),
('PT00070', N'Ốp cản trước', 1200000, 1800000, 8),

('PT00071', N'Ốp cản sau', 1100000, 1700000, 8),
('PT00072', N'Nắp capo', 2500000, 3500000, 4),
('PT00073', N'Cửa trước', 3000000, 4200000, 3),
('PT00074', N'Cửa sau', 2800000, 4000000, 3),
('PT00075', N'Cốp sau', 2600000, 3800000, 4),
('PT00076', N'Bản lề cửa', 150000, 250000, 50),
('PT00077', N'Khóa capo', 200000, 320000, 30),
('PT00078', N'Ống xả', 1200000, 1800000, 10),
('PT00079', N'Bộ giảm thanh', 1400000, 2100000, 8),
('PT00080', N'Cảm biến khí thải', 950000, 1400000, 10),

('PT00081', N'Bộ tăng áp', 4500000, 6500000, 3),
('PT00082', N'Van EGR', 850000, 1250000, 8),
('PT00083', N'Bộ điều khiển ECU', 6000000, 8500000, 2),
('PT00084', N'Cảm biến trục cam', 400000, 650000, 15),
('PT00085', N'Cảm biến trục khuỷu', 420000, 680000, 15),
('PT00086', N'Bộ dây cao áp', 350000, 550000, 25),
('PT00087', N'Bộ chia điện', 700000, 1050000, 12),
('PT00088', N'Mô tơ đề', 1200000, 1800000, 8),
('PT00089', N'Máy phát điện', 1800000, 2600000, 6),
('PT00090', N'Puly trục cơ', 650000, 950000, 12),

('PT00091', N'Puly máy phát', 600000, 900000, 14),
('PT00092', N'Puly máy lạnh', 580000, 880000, 14),
('PT00093', N'Block máy lạnh', 2500000, 3600000, 5),
('PT00094', N'Dàn lạnh', 1800000, 2600000, 6),
('PT00095', N'Dàn nóng', 2000000, 2900000, 6),
('PT00096', N'Van tiết lưu', 400000, 650000, 18),
('PT00097', N'Ống dẫn gas', 300000, 500000, 30),
('PT00098', N'Quạt gió điều hòa', 750000, 1100000, 10),
('PT00099', N'Bảng điều khiển điều hòa', 1200000, 1800000, 7),
('PT00100', N'Bộ điều khiển trung tâm', 3500000, 5000000, 3);


INSERT INTO NOIDUNGSUACHUA (MaNoiDung, MoTa, TienCong) VALUES
('ND00001', N'Thay nhớt động cơ', 150000),
('ND00002', N'Thay lọc nhớt động cơ', 80000),
('ND00003', N'Thay lọc gió động cơ', 100000),
('ND00004', N'Thay lọc gió điều hòa', 120000),
('ND00005', N'Thay bugi đánh lửa', 200000),
('ND00006', N'Thay dây curoa cam', 450000),
('ND00007', N'Thay dây curoa tổng', 400000),
('ND00008', N'Thay má phanh trước', 300000),
('ND00009', N'Thay má phanh sau', 280000),
('ND00010', N'Thay đĩa phanh trước', 500000),

('ND00011', N'Thay đĩa phanh sau', 480000),
('ND00012', N'Thay dầu hộp số tự động', 350000),
('ND00013', N'Thay dầu hộp số sàn', 300000),
('ND00014', N'Thay dầu phanh', 120000),
('ND00015', N'Thay dầu trợ lực lái', 150000),
('ND00016', N'Thay nước làm mát động cơ', 120000),
('ND00017', N'Thay ắc quy xe', 180000),
('ND00018', N'Thay bơm xăng', 400000),
('ND00019', N'Thay bơm nước làm mát', 420000),
('ND00020', N'Thay quạt két nước', 350000),

('ND00021', N'Thay két nước làm mát', 600000),
('ND00022', N'Thay cảm biến oxy', 300000),
('ND00023', N'Thay cảm biến nhiệt độ', 250000),
('ND00024', N'Thay cảm biến áp suất lốp', 280000),
('ND00025', N'Thay đèn pha trước', 200000),
('ND00026', N'Thay đèn hậu', 180000),
('ND00027', N'Thay gương chiếu hậu', 220000),
('ND00028', N'Thay cần gạt mưa', 80000),
('ND00029', N'Thay mô tơ gạt mưa', 300000),
('ND00030', N'Thay bộ ly hợp', 900000),

('ND00031', N'Thay bàn ép ly hợp', 700000),
('ND00032', N'Thay đĩa ly hợp', 650000),
('ND00033', N'Thay giảm xóc trước', 500000),
('ND00034', N'Thay giảm xóc sau', 480000),
('ND00035', N'Thay rotuyn lái', 300000),
('ND00036', N'Thay thanh cân bằng', 320000),
('ND00037', N'Thay ổ bi bánh xe', 280000),
('ND00038', N'Thay vòng bi trục', 260000),
('ND00039', N'Thay cao su chân máy', 350000),
('ND00040', N'Thay cao su chân hộp số', 350000),

('ND00041', N'Thay kim phun nhiên liệu', 600000),
('ND00042', N'Vệ sinh kim phun nhiên liệu', 300000),
('ND00043', N'Vệ sinh họng ga', 250000),
('ND00044', N'Vệ sinh buồng đốt', 500000),
('ND00045', N'Vệ sinh két nước', 200000),
('ND00046', N'Vệ sinh hệ thống làm mát', 300000),
('ND00047', N'Vệ sinh dàn lạnh điều hòa', 250000),
('ND00048', N'Vệ sinh dàn nóng điều hòa', 250000),
('ND00049', N'Nạp gas điều hòa', 350000),
('ND00050', N'Thay block máy lạnh', 900000),

-- Tăng dần mức độ phức tạp
('ND00051', N'Thay van tiết lưu điều hòa', 450000),
('ND00052', N'Thay ống dẫn gas điều hòa', 300000),
('ND00053', N'Thay quạt gió điều hòa', 420000),
('ND00054', N'Sửa hệ thống điều hòa tổng thể', 800000),
('ND00055', N'Thay mô tơ đề', 600000),
('ND00056', N'Thay máy phát điện', 700000),
('ND00057', N'Sửa hệ thống sạc điện', 500000),
('ND00058', N'Sửa hệ thống khởi động', 500000),
('ND00059', N'Thay dây điện động cơ', 450000),
('ND00060', N'Thay hộp cầu chì', 350000),

('ND00061', N'Sửa chập điện hệ thống đèn', 300000),
('ND00062', N'Thay công tắc đèn', 180000),
('ND00063', N'Thay công tắc kính', 220000),
('ND00064', N'Thay mô tơ kính', 400000),
('ND00065', N'Thay khóa cửa', 300000),
('ND00066', N'Sửa khóa cửa', 250000),
('ND00067', N'Thay bộ khóa trung tâm', 500000),
('ND00068', N'Thay camera lùi', 350000),
('ND00069', N'Thay cảm biến lùi', 300000),
('ND00070', N'Thay màn hình DVD', 600000),

('ND00071', N'Thay loa cửa xe', 250000),
('ND00072', N'Thay ampli ô tô', 500000),
('ND00073', N'Sửa hệ thống âm thanh', 400000),
('ND00074', N'Thay vô lăng', 450000),
('ND00075', N'Thay túi khí lái', 1200000),
('ND00076', N'Thay túi khí phụ', 1000000),
('ND00077', N'Thay dây an toàn', 350000),
('ND00078', N'Thay ghế trước', 600000),
('ND00079', N'Thay ghế sau', 550000),
('ND00080', N'Thay tấm lót sàn', 200000),

('ND00081', N'Thay ốp taplo', 500000),
('ND00082', N'Thay ốp cửa', 400000),
('ND00083', N'Thay cản trước', 650000),
('ND00084', N'Thay cản sau', 650000),
('ND00085', N'Thay nắp capo', 800000),
('ND00086', N'Thay cửa trước', 900000),
('ND00087', N'Thay cửa sau', 900000),
('ND00088', N'Thay cốp sau', 850000),
('ND00089', N'Sửa bản lề cửa', 250000),
('ND00090', N'Thay khóa capo', 220000),

('ND00091', N'Thay ống xả', 600000),
('ND00092', N'Thay bộ giảm thanh', 700000),
('ND00093', N'Sửa hệ thống xả', 500000),
('ND00094', N'Thay cảm biến khí thải', 350000),
('ND00095', N'Thay bộ tăng áp', 1200000),
('ND00096', N'Sửa turbo tăng áp', 1500000),
('ND00097', N'Thay van EGR', 500000),
('ND00098', N'Thay ECU động cơ', 1800000),
('ND00099', N'Lập trình ECU', 1000000),
('ND00100', N'Chuẩn đoán lỗi động cơ bằng máy', 250000),

-- Từ 101–200: nâng cao, tổng hợp, đại tu
('ND00101', N'Đại tu động cơ', 5000000),
('ND00102', N'Đại tu hộp số', 4500000),
('ND00103', N'Đại tu hệ thống phanh', 2500000),
('ND00104', N'Đại tu hệ thống treo', 2200000),
('ND00105', N'Đại tu hệ thống lái', 2000000),
('ND00106', N'Đại tu hệ thống điện', 2800000),
('ND00107', N'Đại tu hệ thống điều hòa', 2600000),
('ND00108', N'Đại tu hệ thống nhiên liệu', 2400000),
('ND00109', N'Đại tu hệ thống làm mát', 2200000),
('ND00110', N'Đại tu toàn bộ xe', 8000000),

('ND00111', N'Kiểm tra tổng quát xe', 300000),
('ND00112', N'Kiểm tra động cơ', 250000),
('ND00113', N'Kiểm tra hộp số', 250000),
('ND00114', N'Kiểm tra hệ thống điện', 250000),
('ND00115', N'Kiểm tra hệ thống phanh', 250000),
('ND00116', N'Kiểm tra hệ thống treo', 250000),
('ND00117', N'Kiểm tra hệ thống lái', 250000),
('ND00118', N'Kiểm tra hệ thống điều hòa', 250000),
('ND00119', N'Kiểm tra khí thải', 200000),
('ND00120', N'Kiểm tra tiếng ồn động cơ', 200000),

('ND00121', N'Cân chỉnh thước lái', 300000),
('ND00122', N'Cân mâm bấm chì', 200000),
('ND00123', N'Đảo lốp xe', 150000),
('ND00124', N'Thay lốp xe', 250000),
('ND00125', N'Vá lốp xe', 80000),
('ND00126', N'Sơn cản trước', 700000),
('ND00127', N'Sơn cản sau', 700000),
('ND00128', N'Sơn cửa xe', 800000),
('ND00129', N'Sơn nắp capo', 900000),
('ND00130', N'Sơn toàn bộ xe', 6000000),

('ND00131', N'Đánh bóng xe', 500000),
('ND00132', N'Phủ ceramic xe', 2000000),
('ND00133', N'Dán phim cách nhiệt', 800000),
('ND00134', N'Dán decal xe', 700000),
('ND00135', N'Lắp phụ kiện trang trí', 300000),
('ND00136', N'Lắp cảm biến áp suất lốp', 350000),
('ND00137', N'Lắp camera hành trình', 400000),
('ND00138', N'Lắp màn hình Android', 600000),
('ND00139', N'Lắp khóa thông minh', 500000),
('ND00140', N'Lắp đề nổ từ xa', 550000),

('ND00141', N'Sửa hệ thống ABS', 1200000),
('ND00142', N'Sửa hệ thống ESP', 1300000),
('ND00143', N'Sửa hộp số tự động', 3000000),
('ND00144', N'Sửa hộp số sàn', 2500000),
('ND00145', N'Sửa động cơ tăng áp', 2800000),
('ND00146', N'Sửa động cơ diesel', 2600000),
('ND00147', N'Sửa động cơ xăng', 2400000),
('ND00148', N'Thay trục khuỷu', 3500000),
('ND00149', N'Thay trục cam', 3000000),
('ND00150', N'Thay piston', 3200000),

('ND00151', N'Thay bạc piston', 2800000),
('ND00152', N'Thay xéc măng', 2500000),
('ND00153', N'Thay gioăng quy lát', 2200000),
('ND00154', N'Mài mặt máy', 1800000),
('ND00155', N'Thay bơm cao áp', 2000000),
('ND00156', N'Sửa hệ thống phun xăng điện tử', 1700000),
('ND00157', N'Sửa hệ thống Common Rail', 2200000),
('ND00158', N'Thay rail nhiên liệu', 1900000),
('ND00159', N'Thay cảm biến nhiên liệu', 600000),
('ND00160', N'Sửa rò rỉ nhiên liệu', 500000),

('ND00161', N'Thay hệ thống treo trước', 2500000),
('ND00162', N'Thay hệ thống treo sau', 2400000),
('ND00163', N'Sửa khung gầm xe', 3000000),
('ND00164', N'Nắn chassis xe', 3500000),
('ND00165', N'Sửa xe tai nạn nặng', 7000000),
('ND00166', N'Sửa xe tai nạn nhẹ', 3000000),
('ND00167', N'Thay kính chắn gió', 600000),
('ND00168', N'Thay kính cửa xe', 500000),
('ND00169', N'Thay gioăng cửa', 300000),
('ND00170', N'Sửa chống ồn xe', 800000),

('ND00171', N'Dán cách âm xe', 1500000),
('ND00172', N'Vệ sinh nội thất xe', 400000),
('ND00173', N'Vệ sinh khoang động cơ', 350000),
('ND00174', N'Vệ sinh ghế da', 300000),
('ND00175', N'Vệ sinh ghế nỉ', 280000),
('ND00176', N'Bảo dưỡng định kỳ cấp 1', 500000),
('ND00177', N'Bảo dưỡng định kỳ cấp 2', 800000),
('ND00178', N'Bảo dưỡng định kỳ cấp 3', 1200000),
('ND00179', N'Bảo dưỡng toàn diện', 2000000),
('ND00180', N'Kiểm tra xe trước đăng kiểm', 350000),

('ND00181', N'Sửa hệ thống lái trợ lực điện', 1800000),
('ND00182', N'Sửa hệ thống lái trợ lực dầu', 1600000),
('ND00183', N'Thay bơm trợ lực lái', 900000),
('ND00184', N'Sửa thước lái', 1500000),
('ND00185', N'Thay thước lái', 2200000),
('ND00186', N'Sửa hệ thống cân bằng điện tử', 2000000),
('ND00187', N'Sửa hệ thống kiểm soát lực kéo', 1800000),
('ND00188', N'Sửa hệ thống hỗ trợ phanh', 1700000),
('ND00189', N'Thay module điều khiển', 2500000),
('ND00190', N'Cập nhật phần mềm xe', 1000000),

('ND00191', N'Lập trình chìa khóa xe', 600000),
('ND00192', N'Thay chìa khóa thông minh', 700000),
('ND00193', N'Sửa hệ thống chống trộm', 800000),
('ND00194', N'Sửa hệ thống khóa thông minh', 900000),
('ND00195', N'Sửa hệ thống định vị GPS', 500000),
('ND00196', N'Lắp thiết bị định vị GPS', 600000),
('ND00197', N'Thay module điều hòa', 1200000),
('ND00198', N'Sửa bảng điều khiển trung tâm', 1500000),
('ND00199', N'Thay bảng điều khiển trung tâm', 2000000),
('ND00200', N'Xử lý tổng hợp các lỗi phát sinh khác', 1000000);
