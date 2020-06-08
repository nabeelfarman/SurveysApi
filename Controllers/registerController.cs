using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;



namespace SurveysApi.Controllers
{
    public class RegisterController : ControllerBase
    {
        // static string connectionString = "Data Source=in-matrix.database.windows.net;Initial Catalog=SurveyApp;Persist Security Info=True;User ID=dev_team;Password=infovative.123 providerName=System.Data.SqlClient";
    static string connectionString = "Server=tcp:in-matrix.database.windows.net,1433;Initial Catalog=SurveyApp;Persist Security Info=False;User ID=dev_team;Password=infovative.123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [HttpGet]
        [Route("api/getIndividualType")]
        [EnableCors("CorePolicy")]
        public IEnumerable<individualType> getIndividualType()
        {

            // try
            // {
                List<individualType> rows = new List<individualType>();
                using (IDbConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                            con.Open();
                    rows = con.Query<individualType>(@"SELECT IndvdlTypeCd, IndvdlTypeName
                                                        FROM     dbo.[Prty.tbl_IndividualType]
                                                        WHERE  (DelFlag = 0)").ToList();
                    
                    

                }
                return rows;
            // }catch(Exception e){
            //     return e;
            // }
        }
    }
}
