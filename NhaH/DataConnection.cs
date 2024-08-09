using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaH
{
    public class DataConnection
    {
        string sql;
        public DataConnection()
        {
            sql = "Data Source=DESKTOP-MEUMH4V;Initial Catalog=QL_NhaHang1;Integrated Security=True";
        }
        public SqlConnection sqlstring()
        {
            return new SqlConnection(sql);
        }
    }
}
