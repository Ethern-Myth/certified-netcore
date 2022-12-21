using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using backend.models.response;
using backend.models.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Customer, Admin, Super")]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService service;
    public OrderController(IOrderService service) =>
        this.service = service;

    [HttpGet]
    public async Task<IActionResult> GetOrders() =>
       Ok(await Response(await service.getResponse()));

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetOrder(Guid id) =>
        Ok(await Response(await service.getResponse(id, 0)));

    [HttpPost]
    public async Task<IActionResult> SaveOrder(OrderRequest request)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var order = Request(request);
            var collection = await this.service.getCollection(request.CCID);
            order.ItemCount = collection.CustomerProduct.Products.Count;
            order.OrderTotal = collection.CustomerProduct.Subtotal;
            await service.postRequest(order);
            response = await Response(order);
        }
        else
            return Problem();
        return CreatedAtAction(
            actionName: nameof(GetOrder),
            routeValues: new { id = response.OrderID },
            value: response);
    }

    [Authorize(Roles = "Admin, Super")]
    [HttpPut("Update/{id:Guid}")]
    public async Task<IActionResult> UpdateOrder(OrderRequest request, Guid id)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var order = Request(request);
            order.OrderID = id;
            var collection = await this.service.getCollection(request.CCID);
            order.ItemCount = collection.CustomerProduct.Products.Count;
            order.OrderTotal = collection.CustomerProduct.Subtotal;
            order.DateUpdated = DateTimeOffset.UtcNow;
            await service.putRequest(order, id, 0);
            response = await Response(order);
        }
        else
            return Problem();
        return Ok(response);
    }

    [Authorize(Roles = "Admin, Super")]
    [HttpDelete("Delete/{id:Guid}")]
    public async Task<IActionResult> RemoveOrder(Guid id)
    {
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new static Order Request(OrderRequest request) =>
        new Order(
            request.CCID,
            request.IsPaid
        );

    [NonAction]
    private new async Task<OrderResponse> Response(Order order)
    {
        try
        {
            return new OrderResponse();
            // var customerCollection = await service.getCollection(order.CCID);
            // return new OrderResponse(
            //     order.OrderID,
            //     order.OrderDate,
            //     order.OrderTotal,
            //     order.IsPaid,
            //     order.CCID,
            //     new PartialCustomerCollectionResponse(
            //         customerCollection.CPID,
            //         customerCollection.CustomerID,
            //         new PartialCustomerProductResponse(
            //             new CustomerResponse(
            //                 customerCollection.Customer.Name,
            //                 customerCollection.Customer.Surname,
            //                 customerCollection.Customer.Email,
            //                 customerCollection.Customer.Phone
            //             ),
            //             new ProductQuantity(

            //             )
            //             customerCollection.CustomerProduct.Subtotal
            //         ),
            //         customerCollection.IsAvailable
            //     )
            // );
        }
        catch
        {
            return new OrderResponse();
        }
    }

    [NonAction]
    private new async Task<List<OrderResponse>> Response(List<Order> orders)
    {
        try
        {
            // var results = new List<OrderResponse>();
            // foreach (var order in orders)
            // {
            //     var customerCollection = await service.getCollection(order.CCID);
            //     results.Add(new OrderResponse(
            //     order.OrderID,
            //     order.OrderDate,
            //     order.OrderTotal,
            //     order.IsPaid,
            //     order.CCID,
            //     new PartialCustomerCollectionResponse(
            //         customerCollection.CPID,
            //         customerCollection.CustomerID,
            //         new PartialCustomerProductResponse(
            //             new CustomerResponse(
            //                 customerCollection.Customer.Name,
            //                 customerCollection.Customer.Surname,
            //                 customerCollection.Customer.Email,
            //                 customerCollection.Customer.Phone
            //             ),
            //             customerCollection.CustomerProduct.Products,
            //             customerCollection.CustomerProduct.Subtotal
            //         ),
            //         customerCollection.IsAvailable
            //     )
            // ));
            // }
            // return results;
            return new List<OrderResponse>();
        }
        catch
        {
            return new List<OrderResponse>();
        }
    }
}
