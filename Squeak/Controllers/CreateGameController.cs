using Squeak.BusinessLogic;
using Squeak.Models;
using Squeak.Models.CreateGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Squeak.Controllers
{
    public class CreateGameController : ApiController
    {
        [HttpPost]
        [Route("api/CreateGame/CreateSession")]
        public async Task<CreateSessionResponseModel> CreateSession([FromBody]CreateSessionRequestModel model)
        {
            try
            {
                var blCreateGame = new BL_CreateGame();
                return await blCreateGame.CreationSessionAsync(model);

            }
            catch(Exception ex)
            {
                //log error
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [HttpPost]
        [Route("api/CreateGame/JoinSession")]
        public async Task<JoinSessionResponseModel> JoinSession([FromBody]JoinSessionRequestModel model)
        {
            try
            {
                var blCreateGame = new BL_CreateGame();
                return await blCreateGame.JoinSessionAsync(model);

            }
            catch (Exception ex)
            {
                //log error
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

    }
}
