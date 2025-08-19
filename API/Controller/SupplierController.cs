using BL.Abstract;
using DTO.Models;
using EL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IResourceLocalizer _localizer;
        private readonly ISupplierService _supplierService;

        public SupplierController(
            IResourceLocalizer localizer, 
            ISupplierService supplierService)
        {
            _localizer = localizer;
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var suppliers = _supplierService.GetList();
            return Ok(suppliers);
        }

        [HttpPost]
        public IActionResult Create(AddModel.Supplier model)
        {
            Supplier newSupplier = new Supplier
            {
                Name = model.Name,
                ContactName = model.ContactName,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address,
            };

            _supplierService.Add(newSupplier);

            return Ok(new
            {
                Message = _localizer.Localize("SupplierCreated"),
            });
        }
    }
}
