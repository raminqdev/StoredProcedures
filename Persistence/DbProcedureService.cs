using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Persistence.Generator.Helpers;
using AspNetCore.Lib;
using AspNetCore.Lib.Attributes;
using AspNetCore.Lib.Enums;
using Microsoft.Data.SqlClient;

namespace Persistence
{
    [TypeLifeTime(TypeLifetime.Singleton)]
    partial class DbProcedureService : Persistence.Generator.Helpers.BaseDbProcedure, IDbProcedureService
    {
        public DbProcedureService(AspNetCore.Lib.Configurations.IAppSettings appSettings)
                : base(appSettings.ConnectionString)
        {
        }

        protected override void BatchExecute_OnExecuteCommand(SqlCommand command, Action rollback)
        {
            int returnValue = base.GetReturnValue(command);
            if (returnValue < 0)
            {
                rollback();
                throw new AspNetCore.Lib.Configurations.AppException($"Error on executing stored procedure '{command.CommandText}'.");
            }
        }

        protected override void OnExecuteCommand(SqlCommand command)
        {
            int returnValue = base.GetReturnValue(command);
            if (returnValue < 0)
                throw new AspNetCore.Lib.Configurations.AppException($"Error on executing stored procedure '{command.CommandText}'.");
        }

        #region AddStorages
        public SqlCommand AddStorages_Command(System.Data.DataTable storages, System.Data.DataTable products)
        {
            var cmd = new SqlCommand("dbo.spAddStorages");
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("Storages", storages == null ? DBNull.Value : (object)storages);
			cmd.Parameters.AddWithValue("Products", products == null ? DBNull.Value : (object)products);
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult AddStorages(System.Data.DataTable storages, System.Data.DataTable products)
            => Execute(AddStorages_Command(storages, products));

        public async Task<ProcedureResult> AddStoragesAsync(System.Data.DataTable storages, System.Data.DataTable products)
            => await ExecuteAsync(AddStorages_Command(storages, products));
        #endregion

        #region CreateOrUpdateProduct
        public SqlCommand CreateOrUpdateProduct_Command(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId)
        {
            var cmd = new SqlCommand("dbo.spCreateOrUpdateProduct");
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("Id", id == null ? DBNull.Value : (object)id);
			cmd.Parameters.AddWithValue("Name", name == null ? DBNull.Value : (object)name);
			cmd.Parameters.AddWithValue("Code", code == null ? DBNull.Value : (object)code);
			cmd.Parameters.AddWithValue("Quantity", quantity == null ? DBNull.Value : (object)quantity);
			cmd.Parameters.AddWithValue("UnitePrice", unitePrice == null ? DBNull.Value : (object)unitePrice);
			cmd.Parameters.AddWithValue("Description", description == null ? DBNull.Value : (object)description);
			cmd.Parameters.AddWithValue("Enabled", enabled == null ? DBNull.Value : (object)enabled);
			cmd.Parameters.AddWithValue("StorageId", storageId == null ? DBNull.Value : (object)storageId);
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult CreateOrUpdateProduct(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId)
            => Execute(CreateOrUpdateProduct_Command(id, name, code, quantity, unitePrice, description, enabled, storageId));

        public async Task<ProcedureResult> CreateOrUpdateProductAsync(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId)
            => await ExecuteAsync(CreateOrUpdateProduct_Command(id, name, code, quantity, unitePrice, description, enabled, storageId));
        #endregion

        #region GetAllProducts
        public SqlCommand GetAllProducts_Command()
        {
            var cmd = new SqlCommand("dbo.spGetAllProducts");
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult GetAllProducts()
            => Execute(GetAllProducts_Command());

        public async Task<ProcedureResult> GetAllProductsAsync()
            => await ExecuteAsync(GetAllProducts_Command());
        #endregion

        #region GetAllStorages
        public SqlCommand GetAllStorages_Command()
        {
            var cmd = new SqlCommand("dbo.spGetAllStorages");
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult GetAllStorages()
            => Execute(GetAllStorages_Command());

        public async Task<ProcedureResult> GetAllStoragesAsync()
            => await ExecuteAsync(GetAllStorages_Command());
        #endregion
    }
}
