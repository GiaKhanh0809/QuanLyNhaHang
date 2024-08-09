using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaH.DTO
{
    public class MonAn
    {
        public int IDmon { get; set; }
        public string TenMonAn { get; set; }
        public string ImageMon { get; set; }
        public double GiaMon { get; set; }
        public int IDCategory { get; set; }
        //public MonAn(int id , string ten , string img , double gia, int idcate)
        //{
        //    this.IDmon = id;
        //    this.TenMonAn = ten;
        //    this.ImageMon = img;
        //    this.GiaMon = gia;
        //    this.IDCategory = idcate;
        //}
    }
}
