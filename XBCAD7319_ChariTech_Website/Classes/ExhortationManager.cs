using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace XBCAD7319_ChariTech_Website.Classes
{
    public class ExhortationManager
    {



        public DataTable GetExhortationsByChurchID(int churchID)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["AzureSqlConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ExhortationID, Title, AudioFilePath FROM Exhortation WHERE ChurchID = @ChurchID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChurchID", churchID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable exhortations = new DataTable();
                    da.Fill(exhortations);  // Fill the DataTable with exhortations data
                    return exhortations;
                }
            }
        }




    }
}