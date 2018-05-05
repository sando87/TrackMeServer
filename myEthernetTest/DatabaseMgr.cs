using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace myEthernetTest
{
    class DatabaseMgr
    {
        string sql_a1 = "SELECT * FROM user WHERE id='sando' ORDER BY date DESC";
        string sql_a2 = "SELECT * FROM user WHERE auth IN(2,3)";
        string sql_a3 = "SELECT * FROM user WHERE auth BETWEEN 1 AND 3";
        string sql_a4 = "SELECT * FROM user WHERE date LIKE '2018%'";
        string sql_a5 = "SELECT * FROM user WHERE date LIKE '2018__05'";
        string sql_b = "INSERT INTO user VALUES('test', 'test', '20180507', 2)";
        string sql_c = "DELETE user WHERE id='sando'";
        string sql_d = "UPDATE user SET auth=1 WHERE id='sando'";
        static private MySqlConnection mConn = null;
        public static void Open()
        {
            string strConn = "Server=localhost;Database=test;Uid=root;Pwd=root;";
            mConn = new MySqlConnection(strConn);
            mConn.Open();
        }
        public static void Close()
        {
            if(mConn!=null)
                mConn.Close();
        }
        public static bool IsHaveUser(string key)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT * FROM user WHERE id='" + key + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, mConn);
            int ret = adapter.Fill(ds);
            return (ret > 0) ? true : false;
        }
        public static DataRow GetUserInfo(string key)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT * FROM user WHERE id='" + key + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, mConn);
            int ret = adapter.Fill(ds, "USER");
            if (ret == 0)
                return null;

            DataRow dr = ds.Tables["USER"].Rows[0];

            return dr;
        }
    }
}
