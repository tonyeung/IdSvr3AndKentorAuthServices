using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace webclient
{
    [RoutePrefix("token")]
    public class TokensController : ApiController
    {
        [Route("callback")]
        public async Task<HttpResponseMessage> GetCallBack()
        {
            var code = Request.RequestUri.ParseQueryString()["code"] ?? "";
            var state = Request.RequestUri.ParseQueryString()["state"] ?? "";
            //var tempState = await GetTempStateAsync();

            //if (state.Equals(tempState.Item1, StringComparison.Ordinal))
            //{
            //    ViewBag.State = state + " (valid)";
            //}
            //else
            //{
            //    ViewBag.State = state + " (invalid)";
            //}

            var error = Request.RequestUri.ParseQueryString()["error"] ?? "";


            var client = new TokenClient(
                "http://localhost:13856/connect/token",
                "authCode",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");

            //var code = Request.QueryString["code"];
            //var tempState = await GetTempStateAsync();
            //Request.GetOwinContext().Authentication.SignOut("TempState");

            var response = await client.RequestAuthorizationCodeAsync(
                code,
                "http://localhost:6463/token/callback");

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}