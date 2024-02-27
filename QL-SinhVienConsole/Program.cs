using QL_SinhVienConsole.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_SinhVienConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Menu();
         
        }
        public static void Menu()
        {

            int chucNang = -1;
            SinhVienDAL sv = new SinhVienDAL();
            MonHocDAL mh = new MonHocDAL();
            MonDangKyDAL  mdk=new MonDangKyDAL();
            Console.WriteLine("0.Thoát khỏi chương trình!");
            Console.WriteLine("1.Xem danh sách sinh viên");
            Console.WriteLine("2.Xem chi tiết sinh viên");
            Console.WriteLine("3.Xem số môn học sinh viên đăng ký");
            Console.WriteLine("4.Xem điểm môn học của sinh viên");
            Console.WriteLine("5.Nhập điểm của sinh viên");
            Console.WriteLine("6.Xem kết quả trượt đỗ của sinh viên.");
            Console.WriteLine(new string('-', 15));

            do
            {
                Console.Write("Nhập vào chức năng thực hiện: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out chucNang))
                {
                    Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ.");
                    continue; 
                }
                switch (chucNang)
                {
                    case 1:
                        // Thực hiện chức năng xem danh sách sinh viên
                        sv.XuatDSSinhVien(sv.GetListSinhVien());
                        Console.WriteLine(new string('-', 15));
                        break;
                    case 2:
                        // Thực hiện chức năng xem chi tiết sinh viên
                        sv.XuatChiTietSinhVien();
                        Console.WriteLine(new string('-', 15));
                        break;
                    case 3:
                        // Thực hiện chức năng xem số môn học sinh viên đăng ký
                        mdk.XuatSoMonHocSinhVienDangKy();
                        Console.WriteLine(new string('-', 15));
                        break;
                    case 4:
                        // Thực hiện chức năng xem điểm môn học của sinh viên
                        mdk.XuatDiemMonHocSinhVien();
                        Console.WriteLine(new string('-', 15));
                        break;
                    case 5:
                        // Thực hiện chức năng nhập điểm của sinh viên
                        mh.XuatDSMonHoc();
                        mdk.NhapDiemMonHoc();
                        break;
                    case 6:
                        // Thực hiện chức năng xem kết quả trượt đỗ của sinh viên
                        // XemKetQuaTruotDoCuaSinhVien();
                        break;
                    case 0:
                        // Thoát khỏi ứng dụng
                        Console.WriteLine("Chương trình đã thoát.");
                        break;
                    default:
                        // Nếu lựa chọn không hợp lệ, thông báo cho người dùng và yêu cầu nhập lại
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
                        break;
                }
            } while (chucNang != 0);
        }
    }
}
