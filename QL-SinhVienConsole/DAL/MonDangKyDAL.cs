using Newtonsoft.Json;
using QL_SinhVienConsole.DTO;
using QL_SinhVienConsole.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QL_SinhVienConsole.DAL
{
    public class MonDangKyDAL
    {
        List<MonDangKy> _dsMonDangKy = new List<MonDangKy>();
        string path;
        public MonDangKyDAL() {
            //path = "../../Data/DSMonDangKy.json";
            //ReadFileJsonMonDangKy(path);

            path = "../../Data/DSMonDangKy.xml";
            ReadFileXmlMonDangKy(path);
        }
        public void ReadFileJsonMonDangKy(string path) {
            try
            {
                string json = File.ReadAllText(path);
                List<MonDangKy> monDangKys = JsonConvert.DeserializeObject<List<MonDangKy>>(json);  
                this._dsMonDangKy = monDangKys;
            }catch( Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        public void ReadFileXmlMonDangKy(string path)
        {
            try
            {
                List<MonDangKy> monDangKys = new List<MonDangKy>();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                XmlNodeList nodeList = xmlDocument.DocumentElement.SelectNodes("/MonDangKys/MonDangKy");
                foreach(XmlNode node in nodeList)
                {
                    MonDangKy mondk = new MonDangKy
                    {
                        MaMonHoc = node.SelectSingleNode("MaMonHoc").InnerText,
                        MaSinhVien = node.SelectSingleNode("MaSinhVien").InnerText,
                        DiemQuaTrinh = Double.Parse(node.SelectSingleNode("DiemQuaTrinh").InnerText, System.Globalization.CultureInfo.InvariantCulture),
                        DiemThanhPhan = Double.Parse(node.SelectSingleNode("DiemThanhPhan").InnerText, System.Globalization.CultureInfo.InvariantCulture)
                        
                    };
                    monDangKys.Add(mondk);
                }
                this._dsMonDangKy = monDangKys;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        public void XuatSoMonHocSinhVienDangKy()
        {
            Console.Write("Nhập mssv cần kiêm tra số môn đăng ký: ");
            string input = Console.ReadLine();

            List<MonDangKy> dsMonDangKy = _dsMonDangKy.Where(x => x.MaSinhVien.Equals(input))
                .Distinct() // chỉ lấy phần từ riêng biệt trùng tính là 1
                .ToList();
            Console.WriteLine("Sinh viên đăng kí {0} môn học!", dsMonDangKy.Count());
        }
        public void XuatDiemMonHocSinhVien()
        {
            Console.Write("Nhập mssv để xem điểm: ");
            string input = Console.ReadLine();
            List<MonDangKy> soMonHocDK = _dsMonDangKy.Where(x => x.MaSinhVien.Equals(input))
             .ToList();
            if(soMonHocDK == null)
            {
                Console.WriteLine("Sinh viên {0} không có điểm!");
            }
            else
            {
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (10 / 2)) + "}", "Điểm của Sinh Viên"));
                Console.WriteLine("Mã môn học".PadRight(15) + "Tên môn học".PadRight(22) + "Điểm quá trình".PadRight(20) + "Điểm thành phần".PadRight(20) + "Điểm tổng".PadRight(20) );
                Console.WriteLine(new string('-', Console.WindowWidth)); Console.WriteLine();
                foreach(var  mon in soMonHocDK)
                {
                    MonHocDAL monHocdal = new MonHocDAL();
                    //double tiLe = monHocdal.GetTiLeDiem(mon.MaMonHoc);
                    double tiLeDiem = monHocdal.GetMonHoc(mon.MaMonHoc).TiLeDiem;
                    string tenMon = monHocdal.GetMonHoc(mon.MaMonHoc).TenMonHoc;
                    double diemtong = (mon.DiemQuaTrinh * tiLeDiem) + (mon.DiemThanhPhan * (1.0 - tiLeDiem));
                    string diemtongket = diemtong > -1 ? diemtong.ToString() : "-";
                    string diemQuaTrinh = mon.DiemQuaTrinh > -1? mon.DiemQuaTrinh.ToString() : "-";
                    string diemThanhPhan = mon.DiemThanhPhan  > -1 ? mon.DiemThanhPhan.ToString() : "-";
                    Console.WriteLine($"{mon.MaMonHoc,-15} {tenMon,-22} {diemQuaTrinh,-20} {diemThanhPhan,-20} {diemtongket,-20}");
                }
            }
        }
        //--- 5.
        public void NhapDiemMonHoc()
        {
            Console.Write("Nhập mã số môn học nhập điểm: ");
            string maMonHoc = Console.ReadLine();
            List<MonDangKy> dsMonDangKy = this._dsMonDangKy.Where(x => x.MaMonHoc.Equals(maMonHoc,StringComparison.OrdinalIgnoreCase)).ToList();
            if(dsMonDangKy.Count > 0)
            {
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (10 / 2)) + "}", "Điểm của Môn Học"));
                Console.WriteLine("Mã môn học".PadRight(15) + "Mã sinh viên".PadRight(22) + "Điểm quá trình".PadRight(20) + "Điểm thành phần".PadRight(20));

                Console.WriteLine(new string('-', Console.WindowWidth)); Console.WriteLine();


                foreach (var m in dsMonDangKy)
                {
                    string diemQuaTrinh = m.DiemQuaTrinh > -1 ? m.DiemQuaTrinh.ToString() : "-";
                    string diemThanhPhan = m.DiemThanhPhan > -1 ? m.DiemThanhPhan.ToString() : "-";
                    Console.WriteLine($"{m.MaMonHoc, -15} {m.MaSinhVien,-22} {diemQuaTrinh,-20} {diemThanhPhan,-20}");
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
            MonDangKy monDangKy = dsMonDangKy.FirstOrDefault(x => x.MaSinhVien.Equals(masv));
            if(monDangKy != null)
            {
                MonDangKy mon = NhapDiemCuaMonHoc(monDangKy);
                //NhapDiemCuaMotSinhVien(mon);
                NhapDiemCuaMotSinhVienXML(mon);
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
            diemQT = double.Parse(input1);
            diemTP = double.Parse(input2);
            monDangKy.DiemQuaTrinh = diemQT;
            monDangKy.DiemThanhPhan = diemTP;
            return monDangKy;
        }
        public void NhapDiemCuaMotSinhVien(MonDangKy monDangKy)
        {
            List<MonDangKy> dsMonDangKy = _dsMonDangKy;
            foreach(var m in dsMonDangKy)
            {
                if(m.MaMonHoc.Equals(monDangKy.MaMonHoc) && (m.MaSinhVien.Equals(monDangKy.MaSinhVien)))
                {
                    m.DiemQuaTrinh = monDangKy.DiemQuaTrinh;
                    m.DiemThanhPhan = monDangKy.DiemThanhPhan;
                    break;
                }
            }
            string jsonUpdated = JsonConvert.SerializeObject(dsMonDangKy);
            File.WriteAllText(path, jsonUpdated);
            Console.WriteLine("Nhập điểm thành công!");

        }
        public void NhapDiemCuaMotSinhVienXML(MonDangKy monDangKy)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                XmlNodeList nodeList = xmlDocument.DocumentElement.SelectNodes("/MonDangKys/MonDangKy");
                foreach (XmlNode node in nodeList)
                {
                    MonDangKy monDK = new MonDangKy
                    {
                        MaMonHoc = node.SelectSingleNode("MaMonHoc").InnerText,
                        MaSinhVien = node.SelectSingleNode("MaSinhVien").InnerText,
                        DiemQuaTrinh = Double.Parse(node.SelectSingleNode("DiemQuaTrinh").InnerText, System.Globalization.CultureInfo.InvariantCulture),
                        DiemThanhPhan = Double.Parse(node.SelectSingleNode("DiemThanhPhan").InnerText, System.Globalization.CultureInfo.InvariantCulture)
                    };

                    if(monDK.MaMonHoc.Equals(monDangKy.MaMonHoc) == true && monDK.MaSinhVien.Equals(monDangKy.MaSinhVien) == true)
                    {
                        node.SelectSingleNode("DiemQuaTrinh").InnerText = monDangKy.DiemQuaTrinh.ToString();
                        node.SelectSingleNode("DiemThanhPhan").InnerText = monDangKy.DiemThanhPhan.ToString();
                        break;
                    }
                }
                xmlDocument.Save(path);
                Console.WriteLine("Nhập điểm thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        // 6.
        public void XuatDauRotCuaMonHoc()
        {
            Console.Write("Nhập mã số môn học nhập điểm: ");
            string maMonHhoc = Console.ReadLine();
            List<MonDangKy> dsMonDangKy = this._dsMonDangKy.Where(x => x.MaMonHoc.Equals(maMonHhoc, StringComparison.OrdinalIgnoreCase)).ToList();
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (10 / 2)) + "}", "Đậu rớt của Sinh Viên"));
            Console.WriteLine("Mã môn học".PadRight(12) + "Tên môn học".PadRight(20) + "Tên sinh viên".PadRight(15) + "Điểm quá trình".PadRight(20) + "Điểm thành phần".PadRight(10) + "Điểm tổng".PadRight(20) + "Qua Môn");
            Console.WriteLine(new string('-', Console.WindowWidth)); Console.WriteLine();
            foreach (var mon in dsMonDangKy)
            {
                MonHocDAL monHocDAL = new MonHocDAL();
                SinhVienDAL sinhVienDAL = new SinhVienDAL();
                //double tiLe = monHocdal.GetTiLeDiem(mon.MaMonHoc);
                double tiLeDien = monHocDAL.GetMonHoc(mon.MaMonHoc).TiLeDiem;
                string tenMon = monHocDAL.GetMonHoc(mon.MaMonHoc).TenMonHoc;
                string tensv = sinhVienDAL.GetSinhVien(mon.MaSinhVien).TenSinhVien;
                double diemTong = (mon.DiemQuaTrinh * tiLeDien) + (mon.DiemThanhPhan * (1.0 - tiLeDien));
                string diemTongKet = diemTong > -1 ? diemTong.ToString() : "-";
                string diemQuaTrinh = mon.DiemQuaTrinh > -1 ? mon.DiemQuaTrinh.ToString() : "-";
                string diemThanhPhan = mon.DiemThanhPhan > -1 ? mon.DiemThanhPhan.ToString() : "-";
                string dauRot;
                if (diemTongKet == "-")
                {
                    dauRot ="-";
                }else if(diemTong >= 4) {
                    dauRot = "Đậu";

                }else { dauRot = "Rớt"; }
                Console.WriteLine($"{mon.MaMonHoc,-12} {tenMon,-20}  {tensv,-15} {diemQuaTrinh,-20} {diemThanhPhan,-20} {diemTongKet,-10} {dauRot}");
            }
        }
    }
}
