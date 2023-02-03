using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventStreamingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetSessionInfo()
        {
            List<string> sessionInfo = new List<string>();
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVeriables.SessionKeyUserName)))
            {
                HttpContext.Session.SetString(SessionKeyEnum.SessionKeyUserName.ToString(), "Current User");
                HttpContext.Session.SetString(SessionKeyEnum.SessionKeyUserId.ToString(), Guid.NewGuid().ToString());
            }

            var username=HttpContext.Session.GetString(SessionVeriables.SessionKeyUserName);
            var sessionId=HttpContext.Session.GetString(SessionVeriables.SessionKeyUserId);

            sessionInfo.Add(username);
            sessionInfo.Add(sessionId);
            return sessionInfo;
        }
    }
}
