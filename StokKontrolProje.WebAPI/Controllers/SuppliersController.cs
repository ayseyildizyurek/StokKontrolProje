using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Services.Abstract;

namespace StokKontrolProje.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SuppliersController : ControllerBase
	{
		private readonly IGenericService<Supplier> service;

		public SuppliersController(IGenericService<Supplier> service)
		{
			this.service = service;
		}
		[HttpGet]
		public IActionResult GetAllSuppliers()
		{
			return Ok(service.GetAll());
		}
		[HttpGet]
		public IActionResult GetActiveSuppliers()
		{
			return Ok(service.GetActive());
		}
		[HttpGet("{id}")]
		public IActionResult GetByIdSupplier(int id)
		{
			return Ok(service.GetById(id));
		}
		[HttpPost]
		public IActionResult AddSupplier(Supplier supplier)
		{
			if (service.Add(supplier))
			{
				return CreatedAtAction("GetByIdSupplier", new { id = supplier.Id }, supplier);
			}
			return BadRequest();
		}
		[HttpPut("{id}")]
		public IActionResult UpdateSupplier(int id, Supplier supplier)
		{
			if (id != supplier.Id)
				return BadRequest();
			try
			{
				if (service.Update(supplier))
					return Ok(supplier);
				else
					return BadRequest();
			}
			catch (Exception)
			{
				return NotFound();
			}
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteSupplier(int id)
		{
			var supplier = service.GetById(id);
			if (supplier is null)
			{
				return NotFound();
			}

			if (service.Remove(supplier))
				return Ok("Tedarikçi Silindi");
			else return BadRequest("Tedarikçi silinemedi");

		}
		[HttpGet("{id}")]
		public IActionResult ActivateSupplier(int id)
		{
			Supplier s = service.GetById(id);
			if (s is null) return NotFound();
			service.Activate(id);
			return Ok(service.GetById(id));
		}
	}
}
