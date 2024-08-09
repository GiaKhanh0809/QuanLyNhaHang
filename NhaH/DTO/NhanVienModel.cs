using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaH.DTO
{
    public class NhanVienModel
    {
        public int ID { get; set; }
        public string TenNV { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime? NgayVaoLam { get; set; }
        public decimal Luong { get; set; }
        public string phone { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public int IDvaitro { get; set; }
    }
}
