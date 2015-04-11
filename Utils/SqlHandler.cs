using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.HopeDiary.Utils
{
    public class SqlHandler
    {
        public static void SqlParaAdd<T>(string sqlParaName, SqlDbType dbType, T value,List<SqlParameter> sqlParas )
        {
            SqlParameter sqlPara =(SqlParameter) new SqlParameter(sqlParaName,dbType);
            sqlPara.Value = value;

            sqlParas.Add(sqlPara);
        }
    }
}
