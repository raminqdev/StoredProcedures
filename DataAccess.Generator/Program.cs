using Generator.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Generator
{
    class Program
    {
        private static string CurrentDirectory { get; set; }

        static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                CurrentDirectory = Directory.GetCurrentDirectory();
            else
                CurrentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            const string connectionString = 
                "Data Source=localhost;Initial Catalog=Inventory.sp;Integrated Security=True;User ID=admin;Password=secret;";

            var procedures = new Handler(connectionString).GetProcedures("dbo");

            GenerateInterfaceFile(procedures);

            GenerateFile(procedures);
        }

        private static void GenerateInterfaceFile(IEnumerable<StoredProcedure> procedures)
        {
            var code = new StringBuilder();

            code.AppendLine("using System;");
            code.AppendLine("using System.Data;");
            code.AppendLine("using System.Data.SqlClient;");
            code.AppendLine("using System.Threading.Tasks;");
            code.AppendLine("using Generator.Helpers;");
            code.AppendLine("");
            code.AppendLine("namespace DataAccess");
            code.AppendLine("{");
            code.AppendLine("    public partial interface IDbProcedureService: Generator.Helpers.IBaseDbProcedure");
            code.AppendLine("    {");

            for (int i = 0; i < procedures.OrderBy(e => e.Name).Count(); i++)
            {
                var proc = procedures.ElementAt(i);
                var arguments = BuildArguments(proc);

                code.AppendLine("");
                code.AppendLine($"        #region {proc.Name.Substring(2)}");
                code.AppendLine($"        SqlCommand {proc.Name.Substring(2)}_Command({arguments});");
                code.AppendLine($"        ProcedureResult {proc.Name.Substring(2)}({arguments});");
                code.AppendLine($"        Task<ProcedureResult> {proc.Name.Substring(2)}Async({arguments});");
                code.AppendLine($"        #endregion");
                code.AppendLine("");
            }

            code.AppendLine("    }");
            code.AppendLine("}");

            var path = Path.Combine(Directory.GetParent(CurrentDirectory).FullName,
                "DataAccess/IDbProcedureService.cs");
            File.WriteAllText(path, code.ToString());
        }

        private static void GenerateFile(IEnumerable<StoredProcedure> procedures)
        {
            var code = new StringBuilder();

            code.AppendLine("using System;");
            code.AppendLine("using System.Data;");
            code.AppendLine("using System.Data.SqlClient;");
            code.AppendLine("using System.Threading.Tasks;");
            code.AppendLine("using Generator.Helpers;");
            code.AppendLine("using AspNetCore.Lib;");
            code.AppendLine("using AspNetCore.Lib.Attributes;");
            code.AppendLine("using AspNetCore.Lib.Enums;");
            code.AppendLine("");
            code.AppendLine("namespace DataAccess");
            code.AppendLine("{");
            //code.AppendLine("    [TypeLifeTime(TypeLifetime.Singleton)]");
            code.AppendLine(
                "    partial class DbProcedureService : Generator.Helpers.BaseDbProcedure, IDbProcedureService");
            code.AppendLine("    {");
            code.AppendLine(
                "        public DbProcedureService(AspNetCore.Lib.Configurations.IAppSettings appSettings)");
            code.AppendLine($"                : base(appSettings.ConnectionString)");
            code.AppendLine("        {");
            code.AppendLine("        }");
            code.AppendLine("");
            code.AppendLine(
                "        protected override void BatchExecute_OnExecuteCommand(SqlCommand command, Action rollback)");
            code.AppendLine("        {");
            code.AppendLine("            int returnValue = base.GetReturnValue(command);");
            code.AppendLine("            if (returnValue < 0)");
            code.AppendLine("            {");
            code.AppendLine("                rollback();");
            code.AppendLine(
                "                throw new AspNetCore.Lib.Configurations.AppException($\"Error on executing stored procedure '{command.CommandText}'.\");");
            code.AppendLine("            }");
            code.AppendLine("        }");
            code.AppendLine("");
            code.AppendLine("        protected override void OnExecuteCommand(SqlCommand command)");
            code.AppendLine("        {");
            code.AppendLine("            int returnValue = base.GetReturnValue(command);");
            code.AppendLine("            if (returnValue < 0)");
            code.AppendLine(
                "                throw new AspNetCore.Lib.Configurations.AppException($\"Error on executing stored procedure '{command.CommandText}'.\");");
            code.AppendLine("        }");

            for (int i = 0; i < procedures.OrderBy(e => e.Name).Count(); i++)
            {
                var proc = procedures.ElementAt(i);
                var arguments = BuildArguments(proc);
                code.AppendLine("");
                code.AppendLine($"        #region {proc.Name.Substring(2)}");
                code.AppendLine($"        public SqlCommand {proc.Name.Substring(2)}_Command({arguments})");
                code.AppendLine($"        {{");
                code.AppendLine($"            var cmd = new SqlCommand(\"{proc.ToString()}\");");
                code.AppendLine($"            cmd.CommandType = CommandType.StoredProcedure;");
                code.AppendLine(BuildCommandParameters(proc));
                code.AppendLine($"            return cmd;");
                code.AppendLine($"        }}");
                code.AppendLine("");
                code.AppendLine($"        public ProcedureResult {proc.Name.Substring(2)}({arguments})");
                code.AppendLine($"            => Execute({proc.Name.Substring(2)}_Command({BuildCallArgs(proc)}));");
                code.AppendLine("");
                code.AppendLine(
                    $"        public async Task<ProcedureResult> {proc.Name.Substring(2)}Async({arguments})");
                code.AppendLine(
                    $"            => await ExecuteAsync({proc.Name.Substring(2)}_Command({BuildCallArgs(proc)}));");
                code.AppendLine($"        #endregion");
            }

            code.AppendLine("    }");
            code.AppendLine("}");
            var path = Path.Combine(Directory.GetParent(CurrentDirectory).FullName, "DataAccess/DbProcedureService.cs");
            File.WriteAllText(path, code.ToString());
        }


        private static string QuotedStr(string s) => "\"" + s + "\"";

        private static string BuildArguments(StoredProcedure proc)
        {
            var text = new StringBuilder();
            for (int i = 0; i < proc.Parameters.Where(e => !e.IsOutput).OrderBy(e => e.Name).Count(); i++)
            {
                var prm = proc.Parameters.ElementAt(i);
                if (prm != null)
                {
                    var s = $"{prm.GetCSharpDataType()} {prm.Name.Substring(1, 1).ToLower()}{prm.Name.Substring(2)}, ";
                    text.Append(s);
                }
            }

            return text.ToString().TrimEnd(' ', ',');
        }

        private static string BuildCallArgs(StoredProcedure proc)
        {
            var text = new StringBuilder();
            for (int i = 0; i < proc.Parameters.Where(e => !e.IsOutput).OrderBy(e => e.Name).Count(); i++)
            {
                var prm = proc.Parameters.ElementAt(i);
                var s = $"{prm.Name.Substring(1, 1).ToLower()}{prm.Name.Substring(2)}, ";
                text.Append(s);
            }

            return text.ToString().TrimEnd(' ', ',');
        }

        private static string BuildCommandParameters(StoredProcedure proc)
        {
            var text = new StringBuilder();

            for (int i = 0; i < proc.Parameters.OrderBy(e => e.Name).Count(); i++)
            {
                var prm = proc.Parameters.ElementAt(i);
                var s = "";
                var argName = $"{prm.Name.Substring(1, 1).ToLower()}{prm.Name.Substring(2)}";

                if (!prm.IsOutput)
                    s =
                        $"\t\t\tcmd.Parameters.AddWithValue({QuotedStr(prm.Name.TrimStart('@'))}, {argName} == null ? DBNull.Value : (object){argName});";
                else
                    s =
                        $"\t\t\tcmd.Parameters.Add(new SqlParameter {{  ParameterName = {QuotedStr(prm.Name.TrimStart('@'))}, Direction= ParameterDirection.Output, Size = int.MaxValue}});";

                text.AppendLine(s);
            }

            text.AppendLine(
                "\t\t\tcmd.Parameters.Add(new SqlParameter { ParameterName = \"ReturnValue\", Direction = ParameterDirection.ReturnValue, Size = int.MaxValue, SqlDbType = SqlDbType.Int });");
            return text.ToString().TrimEnd('\r', '\n');
        }
    }
}