using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Generator.Helpers;

namespace DataAccess
{
    public partial interface IDbProcedureService: Generator.Helpers.IBaseDbProcedure
    {

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
