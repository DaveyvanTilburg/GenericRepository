using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PCSService
{
    public class Repository <T> where T : IEntity, new()
    {
        private readonly string _getStoredProcedureName;

        public Repository(string getStoredProcedureName)
        {
            _getStoredProcedureName = getStoredProcedureName;
        }

        public IEnumerable<T> Select()
        {
            using (SqlConnection cn = CData.GetConnection())
            {
                using (SqlCommand com = new SqlCommand(_getStoredProcedureName, cn))
                {
                    com.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    SqlDataReader reader = com.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new T();
                        item.Set(reader);

                        yield return item;
                    }
                }
            }
        }
    }

    public interface IEntity
    {
        void Set(SqlDataReader reader);
    }
}