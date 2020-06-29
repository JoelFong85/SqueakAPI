using DataAccess;
using Squeak.Models.CreateGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Squeak.BusinessLogic
{
    public class BL_CreateGame
    {
        SqueakEntities db = new SqueakEntities();

        // add constructors in future if needed to read header auth tokens


        #region CreateSession
        public async Task<CreateSessionResponseModel> CreationSessionAsync(CreateSessionRequestModel model)
        {
            return await Task.Run(() => CreateSession(model));
        }

        public CreateSessionResponseModel CreateSession(CreateSessionRequestModel model)
        {
            ApplicationFactory.CurrentLogger.Info($@"BL_CreateGame > CreateSession start. 
                                                    Player Name: {model.Name} | 
                                                    Device Id: {model.DeviceId} | 
                                                    App Id: {model.AppId}
                                                    ");

            //validation - fluent validation
            CreateSessionResponseModel result = new CreateSessionResponseModel()
            {
                SessionPin = "0"
            };

            try
            {
                var sessionPin = GenerateRandomNo().ToString();
                var currentTime = DateTime.Now;

                T_Game_Sessions newGame = new T_Game_Sessions()
                {
                    SessionPin = sessionPin,
                    CreateTime = currentTime,
                    SessionIsActive = true
                };

                db.T_Game_Sessions.Add(newGame);
                db.SaveChanges();

                T_Game_Session_Players gameLeader = new T_Game_Session_Players()
                {
                    SessionId = newGame.SessionId,
                    PlayerName = model.Name,
                    JoinTime = currentTime,
                    DeviceId = model.DeviceId,
                    RequestAppId = model.AppId,
                };

                db.T_Game_Session_Players.Add(gameLeader);
                db.SaveChanges();

                result.SessionPin = sessionPin;

                ApplicationFactory.CurrentLogger.Info($"BL_CreateGame > CreateSession success. {result.SessionPin}");
            }
            catch (Exception ex)
            {
                ApplicationFactory.CurrentLogger.Error($"BL_CreateGame > CreateSession error. {ex.Message}");
            }

            return result;
        }

        private int GenerateRandomNo()
        {
            int min = 1;
            int max = 999999;
            Random _rdm = new Random();
            int randomNo = _rdm.Next(min, max);

            var gameSessionExists = db.T_Game_Sessions.Any(m => m.SessionPin == randomNo.ToString() && m.SessionIsActive);

            if (gameSessionExists)
                GenerateRandomNo();

            return randomNo;
        }

        #endregion

        #region JoinSession
        public async Task<JoinSessionResponseModel> JoinSessionAsync(JoinSessionRequestModel model)
        {
            return await Task.Run(() => JoinSession(model));
        }

        public JoinSessionResponseModel JoinSession(JoinSessionRequestModel model)
        {
            //log input

            //validation - fluent validation
            JoinSessionResponseModel result = new JoinSessionResponseModel();

            try
            {
                var session = db.T_Game_Sessions.SingleOrDefault(m => m.SessionPin == model.SessionPin && m.SessionIsActive);

                if (session.T_Game_Session_Players.Count() > 5)
                {
                    result.Message = "There are too many players in the game.";
                    throw new Exception(result.Message);
                }

                bool playerNameExists = session.T_Game_Session_Players.Any(m => m.PlayerName == model.Name);
                if (playerNameExists)
                {
                    result.Message = "Player name exists. Please choose another name.";
                    throw new Exception(result.Message);
                }

                if (model.Name.Length > 10)
                {
                    result.Message = "Player name has maximum length of 10 characters.";
                    throw new Exception(result.Message);
                }

                T_Game_Session_Players newPlayer = new T_Game_Session_Players()
                {
                    SessionId = session.SessionId,
                    PlayerName = model.Name,
                    JoinTime = DateTime.Now,
                    DeviceId = model.DeviceId,
                    RequestAppId = model.AppId,
                };

                db.T_Game_Session_Players.Add(newPlayer);
                db.SaveChanges();

                result.Successful = true;
            }
            catch (Exception ex)
            {
                //log error
                result.Successful = false;
            }

            //log success

            return result;
        }
        #endregion
    }
}