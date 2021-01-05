using Cqrs.Domain.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cqrs.Infra.Data.Base
{
    public abstract class BaseRepository<Entity> : IBaseRepository<Entity>
    {
        public virtual string TableName { get; set; }
        public virtual string InsertSql { get; set; }

        private readonly string connectString;

        private string Fields;
        private string FieldsInsert;
        private string ParamentersInsert;

        protected BaseRepository(string connectString)
        {
            this.connectString = connectString;
            Inicialize();
        }

        public async Task<int> Create(Entity entity)
        {
            string sql = @$"INSERT INTO [AlertPrice] ({FieldsInsert}) 
                                                        VALUES ({ParamentersInsert});
                            SELECT CAST(SCOPE_IDENTITY() as int)";
            using var connection = new SqlConnection(connectString);

            var id = await connection.QueryAsync<int>(sql, entity);
            return await Task.Run(() => id.Single());
        }

        public async Task<IEnumerable<Entity>> GetAll()
        {
            string sql = string.Concat("SELECT ", Fields, " FROM ", TableName,";");
            using var connection = new SqlConnection(connectString);

            var customer = await connection.QueryAsync<Entity>(sql);
            return customer;
        }

        private void Inicialize()
        {
            var propsNames = typeof(Entity).GetProperties().Select(x => x.Name);
            Fields = string.Join(",", propsNames.Select(x => $"[{x}]"));

            propsNames = propsNames.Where(x => !"Id".Equals(x));

            FieldsInsert = string.Join(",", propsNames.Select(x => $"[{x}]"));
            ParamentersInsert = string.Join(",", propsNames.Select(x => $"@{x}"));
        }
    }
}
