using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APP.BusinessLogic.Interfaces;
using APP.CRUDTask.DTO;
using APP.Entities;
using CRUDTask.ModelBinders;
using CRUDTask.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDTask.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _CompanyService;
        private readonly ILogger<CompanyController> _Logger;
        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _CompanyService = companyService;
            _Logger = logger;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "pagenumber")] int pageNumber = 0, [FromQuery(Name = "pagenumber")] int pageSize = 100)
        {
            try
            {
                var list = (await _CompanyService.GetAllCompanies(pageNumber, pageSize)).Select(e => CreateViewDtoFromEntity(e));
                return Ok(list);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't reterive companies", statusCode: 500);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var company = await _CompanyService.GetCompanyById(id);
                var companyDTO = CreateViewDtoFromEntity(company);
                return Ok(companyDTO);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't reterive company!", statusCode: 500);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]AddCompanyDTO company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] company_photo = null;
                    try
                    {
                        company_photo = await AppUtitlity.ReadImageFileFromRequest(Request.Form.Files.ToList());
                    }
                    catch (Exception e)
                    {
                        _Logger.LogError(e.ToString());
                        return BadRequest(e.Message);
                    }
                    var created_company = await _CompanyService.CreateCompany(company.Code, company_photo, company.Name, company.PhoneNumber);
                    return Ok(CreateViewDtoFromEntity(created_company));
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't create company!", statusCode: 500);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [ModelBinder(BinderType = typeof(JsonModelBinder))] UpdateCompanyDTO company, IList<IFormFile> photos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] company_photo = null;
                    try
                    {
                        company_photo = await AppUtitlity.ReadImageFileFromRequest(Request.Form.Files.ToList());
                    }
                    catch (Exception e)
                    {
                        _Logger.LogError(e.ToString());
                        return BadRequest(e.Message);
                    }
                    var update_company = await _CompanyService.UpdateCompany(id, company.Code, company_photo, company.Name, company.PhoneNumber);
                    return Ok(CreateViewDtoFromEntity(update_company));
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't create company!", statusCode: 500);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _CompanyService.DeleteCompany(id);
                return Ok();
            }
            catch (Exception e)
            {
                _Logger.LogError(e.ToString());
                return Problem("Couldn't delete company!", statusCode: 500);
            }
        }

        private ViewCompanyDTO CreateViewDtoFromEntity(Company company)
        {
            if (company == null)
                return null;
            return new ViewCompanyDTO
            {
                Code = company.Code,
                CompanyPhoto = company.CompanyPhoto,
                Name = company.Name,
                PhoneNumber = company.PhoneNumber,
                ID = company.ID
            };
        }
    }
}
