using DTOs;
using Errors;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utils;
using Utils.Validators;

namespace Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase {
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IStorage _storage;
    private readonly CreateAddressValidator _createAddressValidator = new ();
    
    public UserController (IUserRepository userRepository, IAddressRepository addressRepository, IStorage storage) {
        _userRepository = userRepository;
        _addressRepository = addressRepository;
        _storage = storage;
    }

    [HttpPut]
    [Route("{id}/photo")]
    [Authorize]
    public ActionResult ChangePhoto([FromForm] IFormFile image, string id) {
        
        if (!image.ContentType.Contains("image")) {
            return BadRequest("Not a image");
        }
        
        string savedFileName = _storage.upload(image);
        
        User user = _userRepository.GetById(int.Parse(id)) ?? throw new Exception(EntityNotFound.Throw("user"));
        
        user.photo = savedFileName;
        bool isUpdate = _userRepository.Edit(user);

        return isUpdate ? Ok(user) : BadRequest("Failed to save");
    }

    [HttpGet]
    [Route("{id}/photo")]
    [Authorize]
    public ActionResult GetPhoto(string id) {
        User user = _userRepository.GetById(int.Parse(id)) ?? throw new Exception(EntityNotFound.Throw("user"));

        PhotoUrl response = new (_storage.getUrl(user.photo));

        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [Route("address")]
    public ActionResult CreateUserAddress (CreateAddressDTO addressData) {
        
        var validatorResult = _createAddressValidator.Validate(addressData);

        if(!validatorResult.IsValid) {
            return BadRequest(validatorResult.Errors.Select(e => e.ErrorMessage));
        }

        int userId = Token.GetUserId(HttpContext);

        Address address = new(addressData.city, addressData.street, addressData.number, addressData.complement) {
            userId = userId
        };

        _addressRepository.Add(address);
        
        return Ok(address);
    }
}