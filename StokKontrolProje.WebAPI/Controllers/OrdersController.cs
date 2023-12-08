using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Entities.Entities.Enums;
using StokKontrolProje.Services.Abstract;
using System.Data;

namespace StokKontrolProje.WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IGenericService<Order> orderService;
		private readonly IGenericService<OrderDetail> odService;
		private readonly IGenericService<Product> productService;

		public OrdersController(IGenericService<Order> orderService, IGenericService<OrderDetail> odService, IGenericService<Product> productService)
		{
			this.orderService = orderService;
			this.odService = odService;
			this.productService = productService;
		}

		[HttpGet, Authorize(Roles = "User, Admin")]
		public IActionResult GetAllOrders()
		{
			return Ok(orderService.GetAll(x => x.OrderDetails, y => y.User));
		}
		[HttpGet, Authorize(Roles = "User, Admin")]
		public IActionResult GetActiveOrders()
		{
			return Ok(orderService.GetActive());
		}
		[HttpGet("{Id}"), Authorize(Roles = "Admin")]
		public IActionResult GetByIdOrders(int Id)
		{
			return Ok(orderService.GetById(Id, x => x.OrderDetails, y => y.User));
		}
		[HttpGet("{Id}"), Authorize(Roles = "User, Admin")]
		public IActionResult GetByIdOrderDetail(int Id)
		{
			return Ok(odService.GetAll(x => x.OrderId == Id, y => y.Product));
		}
		[HttpGet]
		public IActionResult GetPendingOrders()
		{
			return Ok(orderService.GetDefault(x => x.Status == Status.Pending).ToList());
		}
		[HttpGet]
		public IActionResult GetConfirmedOrders()
		{
			return Ok(orderService.GetDefault(x => x.Status == Status.Confirmed).ToList());
		}
		[HttpGet]
		public IActionResult GetCancelledOrders()
		{
			return Ok(orderService.GetDefault(x => x.Status == Status.Cancelled).ToList());
		}
		[HttpPost]
		public IActionResult AddOrder(int userId, [FromQuery] int[] productIds, [FromQuery] short[] quantities)
		{
			Order newOrder = new Order();
			newOrder.UserId = userId;
			newOrder.Status = Status.Pending;
			newOrder.IsActive = true;
			orderService.Add(newOrder);
			for (int i = 0; i < productIds.Length; i++)
			{
				OrderDetail newDetail = new OrderDetail();
				newDetail.OrderId = newOrder.Id;
				newDetail.ProductId = productIds[i];
				newDetail.Quantity = quantities[i];
				newDetail.UnitPrice = productService.GetById(productIds[i]).UnitPrice;
				newDetail.IsActive = true;
				odService.Add(newDetail);
			}
			return Ok(newOrder);
		}
		[HttpGet("{Id}")]
		public IActionResult ConfirmOrder(int Id)
		{
			Order order = orderService.GetById(Id);
			if (order is null) return NotFound();
			else
			{
				List<OrderDetail> details = odService.GetDefault(x => x.OrderId == order.Id).ToList();

				foreach (OrderDetail item in details)
				{
					Product productInOrder = productService.GetById(item.ProductId);
					if (productInOrder.Stock >= item.Quantity)
					{
						productInOrder.Stock -= item.Quantity;
						productService.Update(productInOrder);
					}
					else
						return BadRequest();

				}
				order.Status = Status.Confirmed;
				order.IsActive = false;
				orderService.Update(order);
				return Ok(order);
			}
		}
		[HttpGet("{Id}")]
		public IActionResult CancelOrder(int Id)
		{
			Order cancelledOrder = orderService.GetById(Id);
			if (cancelledOrder is null) return NotFound();
			else
			{
				cancelledOrder.Status = Status.Cancelled;
				cancelledOrder.IsActive = false;
				orderService.Update(cancelledOrder);
				return Ok(cancelledOrder);
			}
		}
	}
}
