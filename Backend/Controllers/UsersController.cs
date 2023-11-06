namespace Backend.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Backend.Authorization;
using Backend.Helpers;
using Backend.Models.Users;
using Backend.Services;
using System.Text.RegularExpressions;
using Backend.Businessobj;


[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase   
{
    private IUserService _userService;
    private IUserBO _userBO;
    private readonly AppSettings _appSettings;
    public UsersController(
        IUserService userService,
        IUserBO userBO,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _userBO = userBO;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);
        return Ok(response);
    }
    [AllowAnonymous]
    [HttpGet("getbyusernameemail/{usernameemail}")]
    public IActionResult GetByUsernameEmail(string usernameemail)
    {
        var response = _userService.GetByUsername(usernameemail);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        _userService.Register(model);
        return Ok(new { message = "Registration successful" });
    }

    [AllowAnonymous]
    [HttpPost("changepassword/{username}/{password}")]
    public IActionResult ChangePassword(string username, string password)
    {
        _userBO.changePassword(username, password);
        return Ok(new { message = "Password Changed successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        if(!string.IsNullOrWhiteSpace(model.Password)){
            //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            if(regex.IsMatch(model.Password)){
                _userService.Update(id, model);
            }
        }else{
            _userService.Update(id, model);
        }

        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}