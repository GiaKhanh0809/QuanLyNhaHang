using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaH.DTO
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int DishID { get; set; }
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public decimal PriceDish { get; set; }

        // Các thuộc tính khác có thể thêm tùy theo yêu cầu của ứng dụng

        public OrderDetail() { }

        public OrderDetail(int orderID, int dishID, string dishName, int quantity, decimal priceDish)
        {
            OrderID = orderID;
            DishID = dishID;
            DishName = dishName;
            Quantity = quantity;
            PriceDish = priceDish;
        }
    }

}
