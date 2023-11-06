namespace Backend.Businessobj;

using Backend.Services;
using BCrypt.Net;
using System.Net.Mail;
using Backend.Entities;

public interface IUserBO
{
    int changePassword(string username, string password);
}
public class UserBO : IUserBO{
    private IMailService _mailService;
    private IUserService _userService;

    public UserBO(IMailService mailService, IUserService userService){
        _mailService = mailService;
        _userService = userService;
    }  


    public int changePassword(string username, string password){
        string email= "";
        string name = "";
        string passwordHash = BCrypt.HashPassword(password);
        User user = _userService.GetByUsername(username);
    
        user.PasswordHash = passwordHash;
        _userService.UpdatePassword(user);

        MailData mailData= new MailData();
        mailData.ToEmailId = email;
        mailData.EmailSubject = "Password Update Notification";
        mailData.EmailBody = "Dear "+name+"," +
        "<br><br>We are writing to inform you that your password has been successfully updated in our system."+
        "<br>If you did not make this change, please contact our support team immediately."+
        "<br><br>Thank you for using our services."+
        "<br><br>Sincerely,<br><b>Admin</b>"; 
        _mailService.SendMail(mailData);

        return 1;
    }

}
