using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squeak.Models.CreateGame
{
    public class JoinSessionRequestModel
    {
        public string SessionPin { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }
        public string AppId { get; set; }
    }
}