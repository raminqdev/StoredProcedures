using System;
using System.Data;
using System.Threading.Tasks;
using AspNetCore.Lib.Configurations;
using AspNetCore.Lib.Models;
using AspNetCore.Lib.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Persistence.EFModels;
using Persistence.Generator.Helpers;

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

        /* 
            In EntityFrameworkCore we have two methods for executing Stored Procedures that:
            1- Query for records from a database - FromSqlRaw()
            2- Execute a command (typically DML) on the database - ExecuteSqlRaw() or the awaitable ExecuteSqlRawAsync()
            https://referbruv.com/blog/posts/working-with-stored-procedures-in-aspnet-core-ef-core
        */


        public async Task<Result<Guid>> CreateOrUpdate(Storage model)
        {
            var command = new SqlCommand("dbo.spCreateOrUpdateStorage")
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter
            { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = model.Id });
            command.Parameters.Add(new SqlParameter
            { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = model.Name });
            command.Parameters.Add(new SqlParameter
            { ParameterName = "@Phone", SqlDbType = SqlDbType.NVarChar, Value = model.Phone });
            command.Parameters.Add(new SqlParameter
            { ParameterName = "@City", SqlDbType = SqlDbType.NVarChar, Value = model.City });
            command.Parameters.Add(new SqlParameter
            { ParameterName = "@Address", SqlDbType = SqlDbType.NVarChar, Value = model.Address });
            command.Parameters.Add(new SqlParameter
            { ParameterName = "@Enabled", SqlDbType = SqlDbType.Bit, Value = model.Enabled });

            Guid Id = model.Id;

            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                try
                {
                    command.Connection = connection;
                    await connection.OpenAsync();

                    var returnObj = command.ExecuteScalar();

                    //you can now access your output param
                    if (returnObj != null)
                    {
                        Guid.TryParse(returnObj.ToString(), out Id);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.Exception(ex, model);
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return Result<Guid>.Successful(Id);
        }

        // public async Task<Result<Guid>> CreateOrUpdate(Storage model)
        // {
        //     var command = new SqlCommand("dbo.spCreateOrUpdateStorage")
        //     {
        //         CommandType = CommandType.StoredProcedure
        //     };
        //     command.Parameters.Add(new SqlParameter
        //     { ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = model.Id });
        //     command.Parameters.Add(new SqlParameter
        //     { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = model.Name });
        //     command.Parameters.Add(new SqlParameter
        //     { ParameterName = "@Phone", SqlDbType = SqlDbType.NVarChar, Value = model.Phone });
        //     command.Parameters.Add(new SqlParameter
        //     { ParameterName = "@City", SqlDbType = SqlDbType.NVarChar, Value = model.City });
        //     command.Parameters.Add(new SqlParameter
        //     { ParameterName = "@Address", SqlDbType = SqlDbType.NVarChar, Value = model.Address });
        //     command.Parameters.Add(new SqlParameter
        //     { ParameterName = "@Enabled", SqlDbType = SqlDbType.Bit, Value = model.Enabled });

        //     var dataSet = new DataSet();
        //     using (var connection = new SqlConnection(_appSettings.ConnectionString))
        //     {
        //         command.Connection = connection;
        //         try
        //         {
        //             await connection.OpenAsync();
        //             using (var reader = await command.ExecuteReaderAsync())
        //             {
        //                 do
        //                 {
        //                     using (var table = dataSet.Tables.Add())
        //                     {
        //                         table.Load(reader);
        //                     }
        //                 } while (!reader.IsClosed);
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             _logger.Exception(ex, model);
        //         }
        //         finally
        //         {
        //             connection.Close();
        //         }
        //     }
        //     var res = new ProcedureResult(command, dataSet);
        //     return res.Succeed ? Result<Guid>.Successful() : Result<Guid>.Failed("Failed");
        // }
    }

    public interface IStorageSqlCommandService
    {
        Task<Result<Guid>> CreateOrUpdate(Storage model);
    }
}