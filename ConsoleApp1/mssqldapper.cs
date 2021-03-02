using Dapper;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    public class mssqldapper
    {
        public void Conn()
        {
            var sqlConn = $@"Data Source=192.168.70.52;Network Library=DBMSSOCN;Initial Catalog=arpro;User Id=sa;Password = 111aaaBBB; ";
            using (SqlConnection conn = new SqlConnection(sqlConn))
            {
                var list = conn.Query("select 1");
            }
        }
    }
}