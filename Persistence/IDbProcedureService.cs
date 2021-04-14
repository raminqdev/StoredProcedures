using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Persistence.Generator.Helpers;

namespace Persistence
{
    public partial interface IDbProcedureService: Persistence.Generator.Helpers.IBaseDbProcedure
    {

        #region AddStorages_Products
        SqlCommand AddStorages_Products_Command(System.Data.DataTable storages, System.Data.DataTable products);
        ProcedureResult AddStorages_Products(System.Data.DataTable storages, System.Data.DataTable products);
        Task<ProcedureResult> AddStorages_ProductsAsync(System.Data.DataTable storages, System.Data.DataTable products);
        #endregion

        #region AddStorages_Suppliers_Products
        SqlCommand AddStorages_Suppliers_Products_Command(System.Data.DataTable storages, System.Data.DataTable suppliers, System.Data.DataTable products);
        ProcedureResult AddStorages_Suppliers_Products(System.Data.DataTable storages, System.Data.DataTable suppliers, System.Data.DataTable products);
        Task<ProcedureResult> AddStorages_Suppliers_ProductsAsync(System.Data.DataTable storages, System.Data.DataTable suppliers, System.Data.DataTable products);
        #endregion

        #region CreateOrUpdateProduct
        SqlCommand CreateOrUpdateProduct_Command(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId, Guid? supplierId);
        ProcedureResult CreateOrUpdateProduct(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId, Guid? supplierId);
        Task<ProcedureResult> CreateOrUpdateProductAsync(Guid? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, Guid? storageId, Guid? supplierId);
        #endregion

        #region GetAllProducts
        SqlCommand GetAllProducts_Command();
        ProcedureResult GetAllProducts();
        Task<ProcedureResult> GetAllProductsAsync();
        #endregion

        #region GetAllStorages
        SqlCommand GetAllStorages_Command();
        ProcedureResult GetAllStorages();
        Task<ProcedureResult> GetAllStoragesAsync();
        #endregion
    }
}
