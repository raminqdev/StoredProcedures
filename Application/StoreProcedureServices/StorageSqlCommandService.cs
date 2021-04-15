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
            try
            {
                //sql connection object
                await using (SqlConnection conn = new SqlConnection(_appSettings.ConnectionString))
                {
                    //set stored procedure name
                    string spName = @"dbo.[spCreateOrUpdateStorage]";

                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(spName, conn);

                    //Set SqlParameter and add the parameter to the SqlCommand object
                    cmd.Parameters.Add(new SqlParameter
                        {ParameterName = "@Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = model.Id});
                    cmd.Parameters.Add(new SqlParameter
                        {ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Value = model.Name});
                    cmd.Parameters.Add(new SqlParameter
                        {ParameterName = "@Phone", SqlDbType = SqlDbType.NVarChar, Value = model.Phone});
                    cmd.Parameters.Add(new SqlParameter
                        {ParameterName = "@City", SqlDbType = SqlDbType.NVarChar, Value = model.City});
                    cmd.Parameters.Add(new SqlParameter
                        {ParameterName = "@Address", SqlDbType = SqlDbType.NVarChar, Value = model.Address});
                    cmd.Parameters.Add(new SqlParameter
                        {ParameterName = "@Enabled", SqlDbType = SqlDbType.Bit, Value = model.Enabled});

                    //open connection
                    conn.Open();

                    //set the SqlCommand type to stored procedure and execute
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    //execute the stored procedure                   
                    cmd.ExecuteNonQuery();
                    
                    // //execute
                    // SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    // Console.WriteLine(Environment.NewLine + "Retrieving data from database...");
                    // Console.WriteLine("Retrieved records:");
                    // //check if there are records
                    // if (dr.HasRows)
                    //     while (dr.Read())
                    //     {
                    //         var id = dr.GetGuid(0);
                    //         var name = dr.GetString(1);
                    //         var phone = dr.GetString(2);
                    //         var city = dr.GetString(3);
                    //         var address = dr.GetString(4);
                    //         var enabled = dr.GetBoolean(5);
                    //
                    //         Console.WriteLine("{0},{1},{2},{3},{4},{5}", id.ToString(), name, phone, city, address,
                    //             enabled);
                    //     }
                    // else Console.WriteLine("No data found.");
                    //
                    // //close data reader
                    // dr.Close();

                    //close connection
                    conn.Close();
                }

                return Result.Successful();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex);
                return Result.Failed("Exception");
            }
        }
    }

    public interface IStorageSqlCommandService
    {
        Task<Result> CreateOrUpdate(Storage model);
    }
}