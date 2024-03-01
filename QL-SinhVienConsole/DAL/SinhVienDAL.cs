using QL_SinhVienConsole.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml;

namespace QL_SinhVienConsole.DAL
{
    public class SinhVienDAL
    {
        List<SinhVien> _dsSinhVien = new List<SinhVien>();
        string path;

        public SinhVienDAL() {
            // path = "../../Data/DSSinhVien.json";
            //ReadFileJsonSinhVien(path);

            path = "../../Data/DSSinhVien.xml";
            ReadFileXMLSinhVien(path);


        }
        public void ReadFileJsonSinhVien(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                List<SinhVien> sinhViens = JsonConvert.DeserializeObject<List<SinhVien>>(json);

                this._dsSinhVien = sinhViens;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }

        }

        public void ReadFileXMLSinhVien(string filePath)
        {
            List<SinhVien> dsSinhVien = new List<SinhVien>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/SinhViens/SinhVien");
                foreach (XmlNode node in nodeList)
                {
                    SinhVien sinhVien = new SinhVien();
                    sinhVien.MaSinhVien= node.SelectSingleNode("MaSinhVien").InnerText;
                    sinhVien.TenSinhVien = node.SelectSingleNode("TenSinhVien").InnerText;
                    sinhVien.GioiTinh = node.SelectSingleNode("GioiTinh").InnerText;
                    sinhVien.Ngaysinh = node.SelectSingleNode("Ngaysinh").InnerText;
                    sinhVien.Lop = node.SelectSingleNode("Lop").InnerText;
                    sinhVien.Khoa = node.SelectSingleNode("Khoa").InnerText;
                     dsSinhVien.Add(sinhVien);
                }

                this._dsSinhVien = dsSinhVien;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }
        public List<SinhVien> GetListSinhVien()
        {
            return this._dsSinhVien;
        }
        public SinhVien GetSinhVien(string masv)
        {
            SinhVien sinhVien = this._dsSinhVien.SingleOrDefault(x=> x.MaSinhVien == masv);   
            return sinhVien;
        }

        public void XuatDSSinhVien(List<SinhVien> dsSinhVien)
        {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (10 / 2)) + "}", "Danh Sách Sinh Viên"));
            Console.WriteLine("Mã số".PadRight(20) + "Tên sinh viên".PadRight(25) + "Lớp".PadRight(15) + "Giới tính".PadRight(15));
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine();
            foreach (var sv in dsSinhVien)
            {
                Console.WriteLine($"{sv.MaSinhVien,-20} {sv.TenSinhVien,-25} {sv.Lop,-15} {sv.GioiTinh,-15}");
            }
        }
        public void XuatChiTietSinhVien()
        {
            string masv;
            Console.Write("Vui lòng nhập mssv để hiện chi tiết: ");
            masv = Console.ReadLine();
            SinhVien sinhVien = GetSinhVien(masv);
            if (sinhVien == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên {0} ",masv); return;
            }
            else
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Sinh viên có mã số " + masv);
                Console.WriteLine("Mã số:       {0}", sinhVien.MaSinhVien);
                Console.WriteLine("Họ tên:      {0}", sinhVien.TenSinhVien);
                Console.WriteLine("Lớp:         {0}", sinhVien.Lop);
                Console.WriteLine("Giới tính:   {0}", sinhVien.GioiTinh);
                Console.WriteLine("Khoa:        {0}", sinhVien.Khoa);
                Console.WriteLine("-------------------------------------------");
            }
        }
    }
}
