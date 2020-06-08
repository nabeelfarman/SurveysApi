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
    public class ValuesController : ControllerBase
    {
        static string connectionString = "Data Source=in-matrix.database.windows.net;Initial Catalog=SurveyApp;Persist Security Info=True;User ID=dev_team;Password=infovative.123 providerName=System.Data.SqlClient";

    }
}
