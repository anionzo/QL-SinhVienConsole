using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_SinhVienConsole.DTO
{
    public class MonHoc
    {
        private string _MaMonHoc;
        private string _TenMonHoc;
        private int _SoTietMonHoc;
        private double _TiLeDiem; // Điểm cuối kỳ
        public string TenMonHoc { get => _TenMonHoc; set => _TenMonHoc = value; }
        public int SoTietMonHoc { get => _SoTietMonHoc; set => _SoTietMonHoc = value; }
        public string MaMonHoc { get => _MaMonHoc; set => _MaMonHoc = value; }
        public double TiLeDiem { get => _TiLeDiem; set => _TiLeDiem = value; }

        public MonHoc() { }
        public MonHoc( string maMonHoc,string tenMonHoc, int soTietMonHoc, double tiLeDiem)
        {
            MaMonHoc = maMonHoc;
            TenMonHoc = tenMonHoc;
            SoTietMonHoc = soTietMonHoc;
            TiLeDiem = tiLeDiem;
        }
        public MonHoc( MonHoc monHoc)
        {
            MaMonHoc = monHoc.MaMonHoc;
            TenMonHoc = monHoc.TenMonHoc;
            SoTietMonHoc = monHoc.SoTietMonHoc;
            TiLeDiem = monHoc.TiLeDiem;
        }
       
    }
}
