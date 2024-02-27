using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_SinhVienConsole.DTO
{
    public class SinhVien
    {
        private string _MaSinhVien;
        private string _TenSinhVien;
        private string _GioiTinh;
        private string _Ngaysinh;
        private string _Lop;
        private string _Khoa;
        
        public string MaSinhVien { get => _MaSinhVien; set => _MaSinhVien = value; }
        public string TenSinhVien { get => _TenSinhVien; set => _TenSinhVien = value; }
        public string GioiTinh { get => _GioiTinh; set => _GioiTinh = value; }
        public string Ngaysinh { get => _Ngaysinh; set => _Ngaysinh = value; }
        public string Lop { get => _Lop; set => _Lop = value; }
        public string Khoa { get => _Khoa; set => _Khoa = value; }

        public SinhVien (string maSinhVien, string tenSinhVien, string gioiTinh, string ngaysinh, string lop, string khoa)
        {
            MaSinhVien = maSinhVien;
            TenSinhVien = tenSinhVien;
            GioiTinh = gioiTinh;
            Ngaysinh = ngaysinh;
            Lop = lop;
            Khoa = khoa;
           
        }
        public SinhVien()
        {
            MaSinhVien = "";
            TenSinhVien = "";
            GioiTinh = "";
            Ngaysinh = "";
            Lop = "";
            Khoa = "";
        }
        public SinhVien(SinhVien sinhVien)
        {
            MaSinhVien = sinhVien.MaSinhVien;
            TenSinhVien = sinhVien.TenSinhVien;
            GioiTinh = sinhVien.GioiTinh;
            Ngaysinh = sinhVien.Ngaysinh;
            Lop = sinhVien.Lop;
            Khoa = sinhVien.Khoa;
        }
    }
}
