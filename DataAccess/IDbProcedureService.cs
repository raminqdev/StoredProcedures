using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Generator.Helpers;
namespace DataAccess
{
     public partial interface IDbProcedureService: Generator.Helpers.IBaseDbProcedure
     {
           #region lectAllStorages
           SqlCommand lectAllStorages_Command ();
           ProcedureResult lectAllStorages();
           Task<ProcedureResult> lectAllStoragesAsync();
           #endregion
     }
}
