using Newtonsoft.Json;
using QL_SinhVienConsole.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QL_SinhVienConsole.DAL
{
    public class MonHocDAL
    {
        List<MonHoc> _dsMonHoc = new List<MonHoc>();
        string path;
        public MonHocDAL() { 
             //path = "../../Data/DSMonHoc.json";
            //ReadFileJsonMonHoc(path);

            string path = "../../Data/DSMonHoc.xml";
            ReadFileXmlMonHoc(path);

        }
        public void ReadFileJsonMonHoc(string path)
        {
            try
            {
                string json = File.ReadAllText(path) ;
                List<MonHoc> monHocs = JsonConvert.DeserializeObject<List<MonHoc>>(json);
                this._dsMonHoc = monHocs ;

            }catch(Exception ex) {
            
                Console.WriteLine("Lỗi:" + ex.Message);
            }
        }

        public void ReadFileXmlMonHoc(string path)
        {
            List<MonHoc> monHocs = new List<MonHoc>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/MonHocs/MonHoc");
                foreach (XmlNode node in nodeList)
                {
                    MonHoc monHoc = new MonHoc
                    {
                        MaMonHoc = node.SelectSingleNode("MaMonHoc").InnerText,
                        TenMonHoc = node.SelectSingleNode("TenMonHoc").InnerText,
                        SoTietMonHoc = int.Parse(node.SelectSingleNode("SoTietMonHoc").InnerText),
                        TiLeDiem = Double.Parse(node.SelectSingleNode("TiLeDiem").InnerText, System.Globalization.CultureInfo.InvariantCulture)
                    };
                    monHocs.Add(monHoc);
                }
                this._dsMonHoc = monHocs ;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi:" + ex.Message);
            }
        }
        public double GetTiLeDiem(string maMonHoc)
        {
            double tiLeDiem = this._dsMonHoc.FirstOrDefault(x => x.MaMonHoc.Equals(maMonHoc)).TiLeDiem;
            return tiLeDiem;
        }
        public MonHoc GetMonHoc(string maMonHoc)
        {
            MonHoc mon  = this._dsMonHoc.FirstOrDefault(x => x.MaMonHoc.Equals(maMonHoc));
            return mon;
        }
        public void XuatDSMonHoc() {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (10 / 2)) + "}", "Danh sách Môn học"));
            Console.WriteLine("Mã môn".PadRight(20) + "Tên môn".PadRight(25) + "Số tiết".PadRight(15));
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine();
            foreach (var  monHoc in this._dsMonHoc)
            {
                Console.WriteLine($"{monHoc.MaMonHoc,-20} {monHoc.TenMonHoc,-25} {monHoc.SoTietMonHoc,-25}");
            }
            
        }
    }
}
