# Quản lý gara ô tô - Nhập môn công nghệ phần mềm - Nhóm 21 - SE104.Q13 (2025 - 2026)
## Thông tin ứng dụng:
+ Ứng dụng quản lý gara ô tô dành cho các cửa hàng gara ô tô muốn quản lý công việc kinh doanh, tự động hóa nhiều quy trình quản lý, tính toán, xuất file, xem báo cáo thu chi cho cửa hàng.
+ Chức năng chính :
  - Tiếp nhận và bảo trì xe
  - Lập phiếu sửa chữa
  - Lập phiếu thu tiền
  - Lập phiếu nhập vật tư phụ tùng
  - Phân quyền người dùng
  - Quản lý danh sách xe, danh sách nội dung sửa chữa, danh sách vật tư phụ tùng, danh sách các loại phiếu nhập, phiếu thu tiền, phiếu sửa chữa.
  - Hỗ trợ thêm xóa sửa xuất file, tìm kiếm và tra cứu thông tin liên quan với các loại danh sách
  - Xuất báo cáo doanh thu theo tháng trong năm và báo cáo về tồn kho và sử dụng vật tư phụ tùng
  - Chức năng đăng nhập, thay đổi thông tin tài khoản người sử dụng.

## Hướng dẫn chạy ứng dụng:
+ B1: Mở git bash ở một folder rồi tiến hành viết câu lệnh này để clone dự án về máy.
``` Bash
git clone https://github.com/Tuanlee5214/QuanLiGaRaOto.git
```
+ B2: Chạy file QuanLyGaraOto.sql trên ứng dụng SSMS để tạo database cho ứng dụng.
+ B3: Mở file QuanLiGaraOTo.sln để mở code trong VS 2022. 
+ B4: Tiến hành vào folder Service, chọn file DatabaseConnection để chỉnh lại connectString kết nối DB cho phù hợp.
```Bash
Server=TUANLEE\\SQLEXPRESS;Database=QUANLYGARA111;Trusted_Connection=True
```
+ B5: Đổi tên server TUANLEE sang tên server máy bạn cho phù hợp.
+ B6: Tiến hành chạy và thử nghiệm ứng dụng
  - Tài khoản admin : TungLee
  - Mật khẩu : admin123
  - Tài khoản nhân viên :TuanLee
  - Mật khẩu : admin123
 
## Công nghệ sử dụng:
+ Database : SQL Server
+ Ngôn ngữ : C#
+ UI : Winform C#
+ Kiến trúc thick client không có backend, UI vừa chứa logic xử lí giao diện và tương tác với db.

## Người thực hiện:
+ Lê Anh Tuấn
+ MSSV : 23521711
+ Ngành : Kĩ thuật phần mềm UIT.
