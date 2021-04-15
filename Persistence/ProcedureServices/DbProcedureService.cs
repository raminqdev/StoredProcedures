using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Persistence.Generator.Helpers;
using AspNetCore.Lib;
using AspNetCore.Lib.Attributes;
using AspNetCore.Lib.Enums;
using Microsoft.Data.SqlClient;

namespace Persistence.ProcedureServices
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

        #region AddStorages_Products
        public SqlCommand AddStorages_Products_Command(System.Data.DataTable storages, System.Data.DataTable products)
        {
            var cmd = new SqlCommand("dbo.spAddStorages_Products");
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("Storages", storages == null ? DBNull.Value : (object)storages);
			cmd.Parameters.AddWithValue("Products", products == null ? DBNull.Value : (object)products);
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult AddStorages_Products(System.Data.DataTable storages, System.Data.DataTable products)
            => Execute(AddStorages_Products_Command(storages, products));

        public async Task<ProcedureResult> AddStorages_ProductsAsync(System.Data.DataTable storages, System.Data.DataTable products)
            => await ExecuteAsync(AddStorages_Products_Command(storages, products));
        #endregion

        #region AddStorages_Suppliers_Products
        public SqlCommand AddStorages_Suppliers_Products_Command(System.Data.DataTable storages, System.Data.DataTable suppliers, System.Data.DataTable products)
        {
            var cmd = new SqlCommand("dbo.spAddStorages_Suppliers_Products");
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("Storages", storages == null ? DBNull.Value : (object)storages);
			cmd.Parameters.AddWithValue("Suppliers", suppliers == null ? DBNull.Value : (object)suppliers);
			cmd.Parameters.AddWithValue("Products", products == null ? DBNull.Value : (object)products);
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult AddStorages_Suppliers_Products(System.Data.DataTable storages, System.Data.DataTable suppliers, System.Data.DataTable products)
            => Execute(AddStorages_Suppliers_Products_Command(storages, suppliers, products));

        public async Task<ProcedureResult> AddStorages_Suppliers_ProductsAsync(System.Data.DataTable storages, System.Data.DataTable suppliers, System.Data.DataTable products)
            => await ExecuteAsync(AddStorages_Suppliers_Products_Command(storages, suppliers, products));
        #endregion

        #region CreateOrUpdateProduct
        public SqlCommand CreateOrUpdateProduct_Command(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId, Guid? supplierId)
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
			cmd.Parameters.AddWithValue("SupplierId", supplierId == null ? DBNull.Value : (object)supplierId);
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult CreateOrUpdateProduct(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId, Guid? supplierId)
            => Execute(CreateOrUpdateProduct_Command(id, name, code, quantity, unitePrice, description, enabled, storageId, supplierId));

        public async Task<ProcedureResult> CreateOrUpdateProductAsync(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId, Guid? supplierId)
            => await ExecuteAsync(CreateOrUpdateProduct_Command(id, name, code, quantity, unitePrice, description, enabled, storageId, supplierId));
        #endregion

        #region CreateOrUpdateStorage
        public SqlCommand CreateOrUpdateStorage_Command(Guid? id, string name, string phone, string city, string address, bool? enabled)
        {
            var cmd = new SqlCommand("dbo.spCreateOrUpdateStorage");
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("Id", id == null ? DBNull.Value : (object)id);
			cmd.Parameters.AddWithValue("Name", name == null ? DBNull.Value : (object)name);
			cmd.Parameters.AddWithValue("Phone", phone == null ? DBNull.Value : (object)phone);
			cmd.Parameters.AddWithValue("City", city == null ? DBNull.Value : (object)city);
			cmd.Parameters.AddWithValue("Address", address == null ? DBNull.Value : (object)address);
			cmd.Parameters.AddWithValue("Enabled", enabled == null ? DBNull.Value : (object)enabled);
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult CreateOrUpdateStorage(Guid? id, string name, string phone, string city, string address, bool? enabled)
            => Execute(CreateOrUpdateStorage_Command(id, name, phone, city, address, enabled));

        public async Task<ProcedureResult> CreateOrUpdateStorageAsync(Guid? id, string name, string phone, string city, string address, bool? enabled)
            => await ExecuteAsync(CreateOrUpdateStorage_Command(id, name, phone, city, address, enabled));
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

        #region GetProductReport
        public SqlCommand GetProductReport_Command(int? maxQuantity, int? minQuantity, bool? enabled, decimal? maxPrice, decimal? minPrice, Guid? storageId, Guid? supplierId)
        {
            var cmd = new SqlCommand("dbo.spGetProductReport");
            cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("MaxQuantity", maxQuantity == null ? DBNull.Value : (object)maxQuantity);
			cmd.Parameters.AddWithValue("MinQuantity", minQuantity == null ? DBNull.Value : (object)minQuantity);
			cmd.Parameters.AddWithValue("Enabled", enabled == null ? DBNull.Value : (object)enabled);
			cmd.Parameters.AddWithValue("MaxPrice", maxPrice == null ? DBNull.Value : (object)maxPrice);
			cmd.Parameters.AddWithValue("MinPrice", minPrice == null ? DBNull.Value : (object)minPrice);
			cmd.Parameters.AddWithValue("StorageId", storageId == null ? DBNull.Value : (object)storageId);
			cmd.Parameters.AddWithValue("SupplierId", supplierId == null ? DBNull.Value : (object)supplierId);
			cmd.Parameters.Add(new SqlParameter { ParameterName = "ReturnValue", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });
            return cmd;
        }

        public ProcedureResult GetProductReport(int? maxQuantity, int? minQuantity, bool? enabled, decimal? maxPrice, decimal? minPrice, Guid? storageId, Guid? supplierId)
            => Execute(GetProductReport_Command(maxQuantity, minQuantity, enabled, maxPrice, minPrice, storageId, supplierId));

        public async Task<ProcedureResult> GetProductReportAsync(int? maxQuantity, int? minQuantity, bool? enabled, decimal? maxPrice, decimal? minPrice, Guid? storageId, Guid? supplierId)
            => await ExecuteAsync(GetProductReport_Command(maxQuantity, minQuantity, enabled, maxPrice, minPrice, storageId, supplierId));
        #endregion
    }
}
