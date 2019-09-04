using static System.Environment;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System;
using System.Data.SqlClient;
using System.Configuration;
 
public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log) {
   string OriginUrl = req.Headers.GetValues("DISGUISED-HOST").LastOrDefault(); 
   var vOriginUrl = req.Headers.GetValues("DISGUISED-HOST");
   log.Info("RequestURI org: " + OriginUrl);
    
    //create response
   var response = req.CreateResponse(HttpStatusCode.MovedPermanently);
   var connectionString  = "Server=tcp:sqlredirect.database.windows.net,1433;Initial Catalog=sqldatabaseredirect;Persist Security Info=False;User ID=Administrador;Password=Pa$$w0rdAdmin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        using(var connection = new SqlConnection(connectionString)) {
            //Opens Azure SQL DB connection.
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT STRURLTOREDIRECT from [UrlInformation] where STRURLSUBDOMAIN=@STRURLSUBDOMAIN", connection);
            command.Parameters.AddWithValue("@STRURLSUBDOMAIN",OriginUrl);
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader()) {
                if (reader.Read()) {
                
                    var vUrl =Convert.ToString(reader["STRURLTOREDIRECT"]);
                    if(!string.IsNullOrEmpty(vUrl)){
                        response.Headers.Location = new Uri(vUrl);
                    }else{
                        return req.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
            }
        }
 */
    return response;
}