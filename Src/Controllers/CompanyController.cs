using DTOs;
using Errors;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Utils;
using Utils.Validators;

namespace Controllers {
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly CreateCompanyValidator _createCompanyValidator = new ();
        private readonly CreateAddressValidator _createAddressValidator = new ();
    
        public CompanyController (ICompanyRepository companyRepository, IUserRepository userRepository, IAddressRepository addressRepository) {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateCompany(CreateCompanyDTO companyData) {
            var validatorResult = _createCompanyValidator.Validate(companyData);
            if (!validatorResult.IsValid) {
                return BadRequest(validatorResult.Errors.Select(e => e.ErrorMessage));
            }

            int userId = Token.GetUserId(HttpContext);
            User user = _userRepository.GetById(userId) ?? throw new Exception(EntityNotFound.Throw("user"));

            Company company = new (companyData.name, companyData.description, companyData.categories) {
                userId = user.id
            };

            company = _companyRepository.Add(company);

            return Ok(company);
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public ActionResult GetById(int id) {
            Company company = _companyRepository.GetById(id) ?? throw new Exception(EntityNotFound.Throw("company"));
            return Ok(company);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllByUser() {
            int userId = Token.GetUserId(HttpContext);
            List<Company> companies = _companyRepository.GetAll(company => company.userId == userId);
            return Ok(companies);
        }

        [HttpPost]
        [Authorize]
        [Route("{id}/address")]
        public ActionResult CreateAddress (CreateAddressDTO addressData, int id) {
            Company? company = _companyRepository.GetById(id);

            if (company == null) return NotFound(EntityNotFound.Throw("company"));

            var validatorResult = _createAddressValidator.Validate(addressData);
            if (!validatorResult.IsValid) {
                return BadRequest(validatorResult.Errors.Select(e => e.ErrorMessage));
            }

            Address address = new (addressData.city, addressData.street, addressData.number, addressData.complement) {
                companyId = company.id
            };

            _addressRepository.Add(address);

            return Ok(address);
        }
    }
}