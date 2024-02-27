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
        List<MonHoc> monHocs = new List<MonHoc>();
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
                this.monHocs = monHocs ;

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
                this.monHocs = monHocs ;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi:" + ex.Message);
            }
        }
        public double GetTiLeDiem(string mamonhoc)
        {
            double tile = this.monHocs.FirstOrDefault(x => x.MaMonHoc.Equals(mamonhoc)).TiLeDiem;
            return tile;
        }
        public MonHoc GetMonHoc(string mamonhoc)
        {
            MonHoc mon  = this.monHocs.FirstOrDefault(x => x.MaMonHoc.Equals(mamonhoc));
            return mon;
        }
        public void XuatDSMonHoc() {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (10 / 2)) + "}", "Danh sách Môn học"));
            Console.WriteLine("Mã môn".PadRight(20) + "Tên môn".PadRight(25) + "Số tiết".PadRight(15));
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine();
            foreach (var  monHoc in this.monHocs)
            {
                Console.WriteLine($"{monHoc.MaMonHoc,-20} {monHoc.TenMonHoc,-25} {monHoc.SoTietMonHoc,-25}");
            }
            
        }
    }
}
