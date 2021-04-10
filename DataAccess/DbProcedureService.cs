using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Generator.Helpers;
using AspNetCore.Lib;
using AspNetCore.Lib.Attributes;
using AspNetCore.Lib.Enums;

namespace DataAccess
{
    partial class DbProcedureService : Generator.Helpers.BaseDbProcedure, IDbProcedureService
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
