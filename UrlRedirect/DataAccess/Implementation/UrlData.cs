using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UrlRedirect.DataAccess.Interface;
using UrlRedirect.Models;
using VideoFilesLibrary.Helpers;

namespace UrlRedirect.DataAccess.Implementation {
    public class UrlData :IUrlData {
        public UrlViewModel AssingSubDomainUrl(string valUrlToRedirect) {
            UrlViewModel vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter>();
                vParameterList.Add(new SqlParameter("@UrlToRedirect", valUrlToRedirect));
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Update_ByUrlToRedirect");
                List<UrlViewModel> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count == 1) {
                    vResult = vData.FirstOrDefault();
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public UrlViewModel GetUrlToRedirect(string valSubDomainUrl) {
            UrlViewModel vResult = null;
            DataTable vDatainTable = new DataTable();
            SQLToolsLibrary vSqlTools = new SQLToolsLibrary();
            try {
                List<SqlParameter> vParameterList = new List<SqlParameter>();
                vParameterList.Add(new SqlParameter("@UrlSubDomain", valSubDomainUrl));
                vDatainTable = vSqlTools.ExcecuteSelectWithStoreProcedure(vParameterList, "sp_Select_Url_ByUrlSubDomain");
                List<UrlViewModel> vData = DataTableToList(vDatainTable);
                if (vData != null && vData.Count == 1) {
                    vResult = vData.FirstOrDefault();
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }

        public List<UrlViewModel> DataTableToList(DataTable table) {

            List<UrlViewModel> vResult = new List<UrlViewModel>();
            try {
                foreach (DataRow row in table.Rows) {
                    UrlViewModel vUser = new UrlViewModel {
                        Id = Guid.Parse(Convert.ToString(row["STRID"])),
                        UrlSubDomain = Convert.ToString(row["STRURLSUBDOMAIN"]),
                        UrlToRedirect = Convert.ToString(row["STRURLTOREDIRECT"])
                    };
                    vResult.Add(vUser);
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = new List<UrlViewModel>();
            }
            return vResult;
        }
    }
}
