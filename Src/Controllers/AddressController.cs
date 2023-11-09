using Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Controllers;

[ApiController]
[Route("api/address")]
public class AddressController : ControllerBase {
    private readonly IAddressRepository _addressRepository;

    public AddressController (IAddressRepository addressRepository) {
        _addressRepository = addressRepository;
    }
 
}
