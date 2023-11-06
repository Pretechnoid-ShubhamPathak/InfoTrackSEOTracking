using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

public interface IMailService
{
    bool SendMail(MailData mailData);
    int SendOTP(string username,string emailid);
}

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    public MailService(IOptions<MailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }

    public int SendOTP(string username,string emailid)
    {
        Random generator = new Random();
        int otp = generator.Next(0, 1000000);
        String strOtp = otp.ToString("D6");

        MailData mailData = new MailData();
        mailData.EmailToName = username;
        mailData.ToEmailId = emailid;
        mailData.EmailSubject = Constants.OtpMailSubject;
        mailData.EmailBody = "Dear "+username+","+
        "<br><br>To verify your account, please use the following One-Time Password (OTP):"+
        "<br><br>OTP: <strong>"+strOtp+"</strong>"+
        "<br><br>Enter this OTP on our website to proceed with the process. If you didn't request this OTP or have concerns, contact our support team."+
        "<br><br>Thank you."+
        "<br><br><br>Best regards.";

        if(SendMail(mailData)){
            return otp;
        }else{
            return 0;
        }
    }

    public bool SendMail(MailData mailData)
    {
        try
        {
            using (MimeMessage emailMessage = new MimeMessage())
            {
                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.ToEmailId);
                emailMessage.To.Add(emailTo);
                if(!string.IsNullOrEmpty(mailData.CCEmailId))
                    emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                if(!string.IsNullOrEmpty(mailData.BCCEmailId))
                    emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

                emailMessage.Subject = mailData.EmailSubject;
                
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = mailData.EmailBody;

                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                using (SmtpClient mailClient = new SmtpClient())
                {
                    mailClient.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                    mailClient.Send(emailMessage);
                    mailClient.Disconnect(true);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            // Exception Details
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}