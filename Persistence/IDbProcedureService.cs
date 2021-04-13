using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Persistence.Generator.Helpers;

namespace Persistence
{
    public partial interface IDbProcedureService: Persistence.Generator.Helpers.IBaseDbProcedure
    {

        #region AddStorages
        SqlCommand AddStorages_Command(System.Data.DataTable storages, System.Data.DataTable products);
        ProcedureResult AddStorages(System.Data.DataTable storages, System.Data.DataTable products);
        Task<ProcedureResult> AddStoragesAsync(System.Data.DataTable storages, System.Data.DataTable products);
        #endregion

        #region CreateOrUpdateProduct
        SqlCommand CreateOrUpdateProduct_Command(int? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, int? storageId);
        ProcedureResult CreateOrUpdateProduct(int? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, int? storageId);
        Task<ProcedureResult> CreateOrUpdateProductAsync(int? id, string name, string code, int? quantity, decimal? unitePrice, string description, bool? enabled, int? storageId);
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
