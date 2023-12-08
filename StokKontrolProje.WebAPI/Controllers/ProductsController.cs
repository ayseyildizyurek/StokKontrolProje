using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Services.Abstract;

namespace StokKontrolProje.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IGenericService<Product> service;

		public ProductsController(IGenericService<Product> service)
		{
			this.service = service;
		}
		[HttpGet, Authorize(Roles ="User, Admin")]
		public IActionResult GetAllProducts()
		{

			return Ok(service.GetAll(x => x.Category, y => y.Supplier));
		}
		[HttpGet]
		public IActionResult GetActiveProducts()
		{
			return Ok(service.GetActive(x => x.Category, y => y.Supplier));

		}
		[HttpGet("{id}")]
		public IActionResult GetByIdProducts(int id)
		{
			return Ok(service.GetById(id));
		}
		[HttpPost]
		public IActionResult AddProduct(Product product)
		{
			service.Add(product);
			return CreatedAtAction("GetByIdProducts", new { id = product.Id }, product);
		}
		[HttpPut("{id}")]
		public IActionResult UpdateProduct(int id, Product product)
		{
			if (id != product.Id) return BadRequest();
			try
			{
				if (service.Update(product))
				{
					return Ok(product);
				}
				else
					return NotFound();
			}
			catch (DbUpdateConcurrencyException)
			{

				if (!ProductExist(id))
				{
					return NotFound();
				}
			}
			return NoContent();
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			Product product = service.GetById(id);
			if (product is null) return NotFound();
			try
			{
				service.Remove(product);
				return Ok("Ürün silindi");
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
		[HttpGet("{id}")]
		public IActionResult ActivateProduct(int id)
		{
			Product product = service.GetById(id);
			if (product is null) return NotFound();
			try
			{
				service.Activate(id);
				return Ok(service.GetById(id));
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
		private bool ProductExist(int id)
		{
			return service.Any(x => x.Id == id);
		}
	}
}
