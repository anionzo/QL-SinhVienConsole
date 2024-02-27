using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_SinhVienConsole.Service
{
    public static class HoTro
    {

        public static bool KiemTraDouble(string input)
        {
            if (input == null) return false;
            if (input.Length == 0) return false;
            if(!double.TryParse(input, out var result))
                return false;
            return true;
        }
        public static bool KiemTraTu0Den10(string input)
        {
            if (!double.TryParse(input, out var result))
            {
                return false;
            }
            if(result>= 0 && result <=10)
                return true;
            else return false;
        }
    }
}
