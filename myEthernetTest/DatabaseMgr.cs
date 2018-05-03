using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace myEthernetTest
{
    class DatabaseMgr
    {
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
        public static bool IsHaveUser(string user)
        {
            return true;
        }
    }
}
