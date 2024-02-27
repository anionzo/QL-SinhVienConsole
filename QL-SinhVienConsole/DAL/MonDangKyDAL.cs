using Newtonsoft.Json;
using QL_SinhVienConsole.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_SinhVienConsole.DAL
{
    public class MonDangKyDAL
    {
        List<MonDangKy> monDangKys = new List<MonDangKy>();
        public MonDangKyDAL() {
            string path = "../../Data/DSMonDangKy.json";
            ReadFileJsonMonDangKy(path);
        }
        public void ReadFileJsonMonDangKy(string path) {
            try
            {
                string json = File.ReadAllText(path);
                List<MonDangKy> monDangKys = JsonConvert.DeserializeObject<List<MonDangKy>>(json);  
                this.monDangKys = monDangKys;
            }catch( Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }

        public void XuatSoMonHocSinhVienDangKy()
        {
            Console.Write("Nhập mssv cần kiêm tra số môn đăng ký: ");
            string input = Console.ReadLine();

            List<MonDangKy> somon = monDangKys.Where(x => x.MaSinhVien.Equals(input))
                .Distinct() // chỉ lấy phần từ riêng biệt trùng tính là 1
                .ToList();
            Console.WriteLine("Sinh viên đăng kí {0} môn học!", somon.Count());
        }
        public void XuatDiemMonHocSinhVien()
        {
            Console.Write("Nhập mssv để xem điểm: ");
            string input = Console.ReadLine();
            List<MonDangKy> somon = monDangKys.Where(x => x.MaSinhVien.Equals(input))
             .ToList();
            if(somon == null)
            {
                Console.WriteLine("Sinh viên {0} không có điểm!");
            }
            else
            {
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (10 / 2)) + "}", "Điểm của Sinh Viên"));
                Console.WriteLine("Mã môn học".PadRight(15) + "Tên môn học".PadRight(22) + "Điểm quá trình".PadRight(20) + "Điểm thành phần".PadRight(20) + "Điểm tổng".PadRight(20) );
                Console.WriteLine(new string('-', Console.WindowWidth)); Console.WriteLine();
                foreach(var  mon in somon)
                {
                    MonHocDAL monHocdal = new MonHocDAL();
                    //double tiLe = monHocdal.GetTiLeDiem(mon.MaMonHoc);
                    double tiLe = monHocdal.GetMonHoc(mon.MaMonHoc).TiLeDiem;
                    string tenMon = monHocdal.GetMonHoc(mon.MaMonHoc).TenMonHoc;
                    double diemtong = (mon.DiemQuaTrinh * tiLe) + (mon.DiemThanhPhan * (1.0 - tiLe));
                    string diemtongket = diemtong > -1 ? diemtong.ToString() : "-";
                    string diemQuaTrinh = mon.DiemQuaTrinh > -1? mon.DiemQuaTrinh.ToString() : "-";
                    string diemThanhPhan = mon.DiemThanhPhan  > -1 ? mon.DiemThanhPhan.ToString() : "-";
                    Console.WriteLine($"{mon.MaMonHoc,-15} {tenMon,-22} {diemQuaTrinh,-20} {diemThanhPhan,-20} {diemtongket,-20}");
                }
            }
        }
        public void NhapDiemMonHoc()
        {
            Console.Write("Nhập mã số môn học nhập điểm: ");
            string mamh = Console.ReadLine();
            List<MonDangKy> mons = this.monDangKys.Where(x => x.MaMonHoc.Equals(mamh,StringComparison.OrdinalIgnoreCase)).ToList();
            if(mons.Count > 0)
            {

                foreach(var m in mons)
                {

                    Console.WriteLine($"{m.MaMonHoc} {m.MaSinhVien} {m.DiemQuaTrinh} {m.DiemThanhPhan}");
                }
            }
            else
            {
                Console.WriteLine("Không có sinh viên đăng ký môn học!");
                return;
            }

            Console.Write("Nhập vào mã sinh viên cần nhập điểm: ");
            string masv = Console.ReadLine();
            MonDangKy mondk = mons.FirstOrDefault(x => x.MaSinhVien.Equals(masv));
            if(mondk != null)
            {
                
            }
        }
    }
}
