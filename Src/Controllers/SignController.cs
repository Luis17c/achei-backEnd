using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using DTOs;
using Utils;
using System.Text.Json;
using Utils.Validators;
using Errors;

namespace loginExercise.Controllers;

[ApiController]
[Route("api")]
public class SignController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ICrypt _crypt;
    
    private readonly SignInValidator signInValidator = new ();
    private readonly SignUpValidator signUpValidator = new ();
    
    public SignController(IUserRepository userRepository, ICrypt crypt) {
        _userRepository = userRepository ?? throw new ArgumentNullException();
        _crypt = crypt ?? throw new ArgumentNullException();
    }

    [HttpPost]
    [Route("signUp")]
    public ActionResult<SignResponse> SignUp(UserSignUpDTO newUser) {   
        var validatorResult = signUpValidator.Validate(newUser);

        if (!validatorResult.IsValid) {
            return BadRequest(validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        if (_userRepository.GetByEmail(newUser.email) != null)
            return BadRequest("Email already in use");

        User user = new(newUser.name, newUser.email, newUser.password, null);

        user.password = _crypt.encrypt(user.password ?? throw new Exception("Password is required"));
        
        _userRepository.Add(user);
        user = _userRepository.GetByEmail(user.email) ?? throw new Exception(EntityNotFound.Throw("user"));

        SignResponse response = new(user, Token.Generate(user));
        
        return Ok(response);
    }

    [HttpPost]
    [Route("signIn")]
    public ActionResult<string> SignIn(UserSignInDTO signData) {
        var validatorResult = signInValidator.Validate(signData);

        if (!validatorResult.IsValid) {
            return BadRequest(validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        User? user = _userRepository.GetByEmail(signData.email);

        if (user == null) 
            return BadRequest("Wrong password or email");

        if (!_crypt.compare(signData.password, user.password ?? "")) {
            return BadRequest("Wrong password or email");
        }

        SignResponse response = new(user, Token.Generate(user));

        return Ok(response);
    }

    [HttpPost]
    [Route("facebookAuth/{accessToken}")]
    public ActionResult FacebookAuth(string accessToken) {

        FbUserData fbUserData;

        using var client = new HttpClient();
        fbUserData = JsonSerializer.Deserialize<FbUserData>(client.GetAsync(
            $"https://graph.facebook.com/me?access_token={accessToken}&fields=id,name,email,picture.width(100).height(100)"
        ).Result.Content.ReadAsStringAsync().Result) ?? throw new Exception("Invalid token");

        User? user;

        user = _userRepository.GetByEmail(fbUserData.email);

        if (user == null) {
            user = new (fbUserData.name, fbUserData.email, null, fbUserData.picture.data.url);
            _userRepository.Add(user);
            user = _userRepository.GetByEmail(user.email) ?? throw new Exception(EntityNotFound.Throw("user"));
        }

        SignResponse response = new(user, Token.Generate(user));

        return Ok(response);
    }

    [HttpPost]
    [Route("googleAuth/{accessToken}")]
    public ActionResult GoogleAuth(string idToken) {

        GoogleResponse googleResponse;

        using var client = new HttpClient();
        googleResponse = JsonSerializer.Deserialize<GoogleResponse>(client.GetAsync(
            $"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}"
        ).Result.Content.ReadAsStringAsync().Result) ?? throw new Exception("Invalid token");

        User? user;

        user = _userRepository.GetByEmail(googleResponse.email);

        if (user == null) {
            user = new (googleResponse.name, googleResponse.email, null, googleResponse.picture);
            _userRepository.Add(user);
            user = _userRepository.GetByEmail(user.email) ?? throw new Exception(EntityNotFound.Throw("user"));
        }

        SignResponse response = new(user, Token.Generate(user));

        return Ok(response);
    }
}


