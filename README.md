# MyShop
##Thành viên thực hiện:
Ngô Thành Nhân: github.com/KleinNotana
Lưu Chấn Huy: github.com/chanhuy142

## Các chức năng của ứng dụng
### Màn hình đăng nhập
- Cho nhập username và password để đi vào màn hình chính. Có chức năng lưu username và password ở local để lần sau người dùng không cần đăng nhập lại. Password cần được mã hóa.
- Cho phép cấu hình thêm thông tin như server, tên database kết nối.
### Màn hình dashboard
Cung cấp tổng quan về hệ thống đang quản lí:
- Có tổng cộng bao nhiêu sản phẩm đang bán
- Có tổng cộng bao nhiêu đơn hàng mới trong tuần 
- Liệt kê top 5 sản phẩm đang sắp hết hàng (số lượng < 5)
### Quản lí hàng hóa
- mport dữ liệu gốc ban đầu (loại sản phẩm, danh sách các sản phẩm) từ tập tin Excel hoặc Access. Chú ý không sử dụng Excel hoặc Access làm CSDL chính.
- Thao tác với **Loại sản phẩm**: Xem danh sách, Thêm, Xóa, Cập nhật
- Thao tác với **Sản phẩm**
    - Xem danh sách theo Loại sản phẩm
        - Có phân trang
        - Sắp xếp theo tiêu chí
    - Xem chi tiết một sản phẩm
        - Xóa, cập nhật sản phẩm
    - Thêm mới một sản phẩm
- Cho phép tìm kiếm sản phẩm theo tên
- Cho phép lọc lại sản phẩm theo khoảng giá
### Quản lí các đơn hàng
- Tạo ra các đơn hàng
- Cho phép xóa một đơn hàng, cập nhật một đơn hàng
- Cho phép xem danh sách các đơn hàng có phân trang, xem chi tiết một đơn hàng
- Tìm kiếm các đơn hàng từ ngày đến ngày
### Báo cáo thống kê 
- Báo cáo doanh thu và lợi nhuận theo ngày đến ngày, theo tuần, theo tháng, theo năm (vẽ biểu đồ)
- Xem các sản phẩm và số lượng bán theo ngày đến ngày, theo tuần, theo tháng, theo năm (vẽ biểu đồ)
### Cấu hình
- Cho phép hiệu chỉnh số lượng sản phẩm mỗi trang
- Cho phép khi chạy chương trình lên thì mở lại màn hình cuối mà lần trước tắt
