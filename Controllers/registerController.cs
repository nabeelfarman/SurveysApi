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


        //* Save indivdual data 
        [AllowAnonymous]
        [HttpPost]
        [Route("api/saveIndividual")]
        [EnableCors("CorePolicy")]
        public IActionResult saveIndividual([FromBody] individual obj)
        {

            //* Try Block
            try
            {
                //* Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                //* Database Query, Save Link in Database & Email Link 
                using (IDbConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@IndvdlID", obj.indvdlID);
                    parameters.Add("@IndvdlTypeCd", obj.typeCd);
                    parameters.Add("@IndvdlFirstName", obj.firstName);
                    parameters.Add("@IndvdlLastName", obj.lastName);
                    parameters.Add("@IndvdlERPUsrID", obj.email);
                    parameters.Add("@IndvdlERPPsswrd", obj.password);
                    parameters.Add("@Response", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.party_sp_registeration", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@Response");

                }
                if (sqlResponse != "Record Saved Successfully!")
                {
                    sqlResponse = "Sorry! Your Email Doesn't Exists.";
                }
                else
                {
                    //* for setting email information details.
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("noreply@survey.com");
                        mail.To.Add(obj.email.ToString());
                        mail.Subject = "Survey Account Password";
                        mail.Body = "Hi" + obj.firstName + ", Your survey account password is <h1>" + obj.password + "</h1>";
                        mail.IsBodyHtml = true;

                        //* for setting smtp mail name and port
                        using (SmtpClient smtp = new SmtpClient("mail.tierra.net", 24))
                        // using (SmtpClient smtp = new SmtpClient("mail.gmail.com", 587))
                        {

                            //* for setting sender credentials(email and password) using smtp
                            smtp.Credentials = new System.Net.NetworkCredential("nabeel.farman@ms.infovativesolutions.com",
                                                                                "N@b##l35");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                    sqlResponse = "Mail sent to your current email address!";
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    msg = ex.Message
                });

            }
        }
    }
}