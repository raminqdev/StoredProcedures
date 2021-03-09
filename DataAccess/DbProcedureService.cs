using AspNetCore.Lib.Attributes;
using AspNetCore.Lib.Enums;
using Generator.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace DataAccess
{
    [TypeLifeTime(TypeLifetime.Singleton)]
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
                throw new AspNetCore.Lib.Configurations.AppException($"Error on execute stored procedure '{command.CommandText}'.");
            }
        }
        protected override void OnExecuteCommand(SqlCommand command)
        {
            int returnValue = base.GetReturnValue(command);
            if (returnValue < 0)
                throw new AspNetCore.Lib.Configurations.AppException($"Error on execute stored procedure '{command.CommandText}'.");
        }

        #region lectAllStorages
        public SqlCommand lectAllStorages_Command()
        {
            var cmd = new SqlCommand("dbo.SelectAllStorages");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }
        public ProcedureResult lectAllStorages()
        => Execute(lectAllStorages_Command());
        public Task<ProcedureResult> lectAllStoragesAsync()
        => ExecuteAsync(lectAllStorages_Command());
        #endregion
    }
}
