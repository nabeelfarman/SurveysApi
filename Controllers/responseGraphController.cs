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
        public class ReponseGraphController : ControllerBase
    {
        // static string connectionString = "Data Source=in-matrix.database.windows.net;Initial Catalog=SurveyApp;Persist Security Info=True;User ID=dev_team;Password=infovative.123 providerName=System.Data.SqlClient";
        static string connectionString = "Server=tcp:in-matrix.database.windows.net,1433;Initial Catalog=SurveyApp;Persist Security Info=False;User ID=dev_team;Password=infovative.123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}