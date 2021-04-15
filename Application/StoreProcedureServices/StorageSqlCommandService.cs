using System;
using System.Data;
using System.Threading.Tasks;
using AspNetCore.Lib.Configurations;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Persistence.EFModels;

namespace Application.StoreProcedureServices
{
    public class StorageSqlCommandService : IStorageSqlCommandService
    {
        private readonly IAppSettings _appSettings;
        private readonly ILogger _logger;

        public StorageSqlCommandService(IAppSettings appSettings, ILogger logger)
        {
            _appSettings = appSettings;
            _logger = logger;
        }


        public async Task<Result> CreateOrUpdate(Storage model)
        {
            var command = new SqlCommand("dbo.spCreateOrUpdateStorage")
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter
                {ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = model.Id});
            command.Parameters.Add(new SqlParameter
                {ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = model.Name});
            command.Parameters.Add(new SqlParameter
                {ParameterName = "@Phone", SqlDbType = SqlDbType.NVarChar, Value = model.Phone});
            command.Parameters.Add(new SqlParameter
                {ParameterName = "@City", SqlDbType = SqlDbType.NVarChar, Value = model.City});
            command.Parameters.Add(new SqlParameter
                {ParameterName = "@Address", SqlDbType = SqlDbType.NVarChar, Value = model.Address});
            command.Parameters.Add(new SqlParameter
                {ParameterName = "@Enabled", SqlDbType = SqlDbType.Bit, Value = model.Enabled});

            var dataSet = new DataSet();
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                command.Connection = connection;
                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        do
                        {
                            using (var table = dataSet.Tables.Add())
                            {
                                table.Load(reader);
                            }
                        } while (!reader.IsClosed);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Exception(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return Result.Successful();
        }
    }

    public interface IStorageSqlCommandService
    {
        Task<Result> CreateOrUpdate(Storage model);
    }
}