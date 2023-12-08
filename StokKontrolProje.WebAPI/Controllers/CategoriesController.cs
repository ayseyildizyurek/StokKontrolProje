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
	public class CategoriesController : ControllerBase
	{
		private readonly IGenericService<Category> service;

		public CategoriesController(IGenericService<Category> service)
		{
			this.service = service;
		}

		[HttpGet, Authorize(Roles ="User, Admin")]
		public IActionResult GetAllCategories()
		{
			return Ok(service.GetAll());
		}
		[HttpGet, Authorize(Roles = "User, Admin")]
		public IActionResult GetActiveCategories()
		{
			return Ok(service.GetActive());
		}
		[HttpGet("{id}"), Authorize(Roles = "Admin")]
		public IActionResult GetByIdCategories(int id)
		{
			return Ok(service.GetById(id));
		}
		[HttpPost, Authorize(Roles = "Admin")]
		public IActionResult AddCategory(Category category)
		{
			service.Add(category);
			return CreatedAtAction("GetByIdCategories", new { id = category.Id }, category);
		}
		[HttpPut("{id}"), Authorize(Roles = "Admin")]
		public IActionResult UpdateCategory(int id, Category category)
		{
			if (id != category.Id)
			{
				return BadRequest();
			}
			try
			{

				if (service.Update(category))
					return Ok(category);
				else
					return NotFound();

			}
			catch (DbUpdateConcurrencyException)
			{

				if (!CategoryExist(id))
				{
					return NotFound();
				}
			}
			return NoContent();
		}
		[HttpDelete("{id}"), Authorize(Roles = "Admin")]
		public IActionResult DeleteCategory(int id)
		{
			var category = service.GetById(id);
			if (category == null) return NotFound();
			try
			{
				service.Remove(category);
				return Ok("KategoriSilindi");
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}
		[HttpGet("{id}"), Authorize(Roles = "Admin")]
		public IActionResult ActivateCategory(int id)
		{
			var category = service.GetById(id);
			if (category is null) return NotFound();
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
		private bool CategoryExist(int id)
		{
			return service.Any(x => x.Id == id);
		}
	}
}
