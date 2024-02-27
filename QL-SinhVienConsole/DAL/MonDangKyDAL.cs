using Newtonsoft.Json;
using QL_SinhVienConsole.DTO;
using QL_SinhVienConsole.Service;
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
        string path;
        public MonDangKyDAL() {
             path = "../../Data/DSMonDangKy.json";
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
                    string diemQuaTrinh = m.DiemQuaTrinh > -1 ? m.DiemQuaTrinh.ToString() : "-";
                    string diemThanhPhan = m.DiemThanhPhan > -1 ? m.DiemThanhPhan.ToString() : "-";
                    Console.WriteLine($"{m.MaMonHoc} {m.MaSinhVien} {diemQuaTrinh} {diemThanhPhan}");
                }
            }
            else
            {
                Console.WriteLine("Không có sinh viên đăng ký môn học!");
                return;
            }

            Console.WriteLine("Nhập 0 để thoát khỏi thêm điểm");
            Console.Write("Nhập vào mã sinh viên cần nhập điểm: ");
            string masv = Console.ReadLine();
            MonDangKy mondk = mons.FirstOrDefault(x => x.MaSinhVien.Equals(masv));
            if(mondk != null)
            {
                MonDangKy mon = NhapDiemCuaMonHoc(mondk);
                NhapDiemCuaMotSinhVien(mon);
            }
            else
            {
                Console.WriteLine("Không tìm được sinh viên trong môn học!");
            }
        }
        public MonDangKy NhapDiemCuaMonHoc(MonDangKy monDangKy)
        {
            double diemQT, diemTP;
            Console.Write("Nhập vào điểm quá trình: ");
            string input1 = Console.ReadLine();
            while (HoTro.KiemTraDouble(input1) != true || HoTro.KiemTraTu0Den10(input1) != true)
            {
                Console.WriteLine("Vui lòng nhập vào một số thực hoặc số từ 0-10");
                Console.Write("Nhập lại điểm quá trình: ");
                input1 = Console.ReadLine();
            }

            Console.Write("Nhập vào điểm thành phần: ");
            string input2 = Console.ReadLine();
            while (HoTro.KiemTraDouble(input2) != true || HoTro.KiemTraTu0Den10(input2) != true)
            {
                Console.WriteLine("Vui lòng nhập vào một số thực hoặc số từ 0-10");
                Console.Write("Nhập lại điểm thành phần: ");
                input2 = Console.ReadLine();
            }
            diemQT = int.Parse(input1);
            diemTP = int.Parse(input2);
            monDangKy.DiemQuaTrinh = diemQT;
            monDangKy.DiemThanhPhan = diemTP;
            return monDangKy;
        }
        public void NhapDiemCuaMotSinhVien(MonDangKy monDangKy)
        {
            List<MonDangKy> monDK = monDangKys;
            foreach(var m in monDK)
            {
                if(m.MaMonHoc.Equals(monDangKy.MaMonHoc) && (m.MaSinhVien.Equals(monDangKy.MaSinhVien)))
                {
                    m.DiemQuaTrinh = monDangKy.DiemQuaTrinh;
                    m.DiemThanhPhan = monDangKy.DiemThanhPhan;
                    break;
                }
            }
            string jsonUpdated = JsonConvert.SerializeObject(monDK);
            File.WriteAllText(path, jsonUpdated);
        }
    }
}
