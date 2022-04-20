using System;
using System.Data.SqlClient;

namespace PestScan.Common.ExtensionMethods
{
    public static class SqlDataReaderExtensions
    {
        public static T Read<T>(this SqlDataReader reader, string columnName)
        {
            object columnValue = reader[columnName];

            if (columnValue == null || columnValue == DBNull.Value)
                return default;

            return reader.GetFieldValue<T>(reader.GetOrdinal(columnName));
        }
    }
}