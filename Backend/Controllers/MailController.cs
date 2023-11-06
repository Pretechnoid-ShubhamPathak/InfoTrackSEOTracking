using Microsoft.AspNetCore.Mvc;

[ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        
        //injecting the IMailService into the constructor
        public MailController(IMailService _MailService)
        {
            _mailService = _MailService;
        }

        [HttpPost]
        [Route("SendMail")]
        public bool SendMail(MailData mailData)
        {
            return _mailService.SendMail(mailData);
        }

        [HttpGet]
        [Route("sendotp/{username}/{emailid}")]
        public int SendOTP(string username,string emailid)
        {
            return _mailService.SendOTP(username,emailid);
        }
    }