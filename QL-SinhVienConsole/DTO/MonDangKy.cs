using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_SinhVienConsole.DTO
{
    public class MonDangKy
    {
        private string _MaSinhVien;
        private string _MaMonHoc;
        private double _DiemQuaTrinh;
        private double _DiemThanhPhan;
        //private double _DiemTong;


        public string MaSinhVien { get => _MaSinhVien; set => _MaSinhVien = value; }
        public string MaMonHoc { get => _MaMonHoc; set => _MaMonHoc = value; }
        public double DiemQuaTrinh { get => _DiemQuaTrinh; set => _DiemQuaTrinh = value; }
        public double DiemThanhPhan { get => _DiemThanhPhan; set => _DiemThanhPhan = value; }
        //public double DiemTong { get => _DiemTong; set => _DiemTong = value; }

        public MonDangKy(string maSinhVien, string maMonHoc, double diemQuaTrinh, double diemThanhPhan/*, double diemTong*/)
        {
            MaSinhVien = maSinhVien;
            MaMonHoc = maMonHoc;
            DiemQuaTrinh = diemQuaTrinh;
            DiemThanhPhan = diemThanhPhan;
            //DiemTong = diemTong;
        }

        public MonDangKy()
        {
        }
        
        public MonDangKy(MonDangKy monDangKy)
        {
            MaSinhVien = monDangKy.MaSinhVien;
            MaMonHoc = monDangKy.MaMonHoc;
            DiemQuaTrinh = monDangKy.DiemQuaTrinh;
            DiemThanhPhan = monDangKy.DiemThanhPhan;
            //DiemTong = monDangKy.DiemTong;
        }
    }
}
