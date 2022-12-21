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
            var collection = await service.getCollection(request.CCID);
            int count = 0;
            foreach (var item in collection.CustomerProduct.Products)
            {
                count += item.Quantity;
            }
            order.ItemCount = count;
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
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateOrder(OrderRequest request, Guid id)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var order = Request(request);
            order.OrderID = id;
            var collection = await this.service.getCollection(request.CCID);
            int count = 0;
            foreach (var item in collection.CustomerProduct.Products)
            {
                count += item.Quantity;
            }
            order.ItemCount = count;
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
    [HttpDelete("{id:Guid}")]
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
            var customerCollection = await service.getCollection(order.CCID);
            var shippingAddress = await service.getShippingAddress(customerCollection.CustomerID);
            var customer = await service.getCustomer(customerCollection.CustomerID);
            var products = new List<ProductQuantityResponse>();
            foreach (var item in customerCollection.CustomerProduct.Products)
            {
                products.Add(new ProductQuantityResponse(
                    item.Quantity,
                    item.ProductTotal,
                    item.Product
                ));
            }
            var orderResponse = new OrderResponse(
                order.OrderID,
                order.OrderDate,
                order.OrderTotal,
                order.IsPaid,
                new ShippingResponse(
                    shippingAddress.ShippingID,
                    new CustomerResponse(
                        customer.Name,
                        customer.Surname,
                        customer.Email,
                        customer.Phone
                    ),
                    shippingAddress.AddressLine1,
                    shippingAddress.AddressLine2,
                    shippingAddress.Suburb,
                    shippingAddress.Town,
                    shippingAddress.Region,
                    shippingAddress.PostalCode,
                    shippingAddress.Country
                ),
                new OnlyProductsResponse(
                    products,
                    customerCollection.CustomerProduct.Subtotal
                )
            );
            return orderResponse;
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
            var results = new List<OrderResponse>();
            var products = new List<ProductQuantityResponse>();
            foreach (var order in orders)
            {
                var customerCollection = await service.getCollection(order.CCID);
                var shippingAddress = await service.getShippingAddress(customerCollection.CustomerID);
                var customer = await service.getCustomer(customerCollection.CustomerID);
                foreach (var item in customerCollection.CustomerProduct.Products)
                {
                    products.Add(new ProductQuantityResponse(
                        item.Quantity,
                        item.ProductTotal,
                        item.Product
                    ));
                }
                var orderResponse = new OrderResponse(
                    order.OrderID,
                    order.OrderDate,
                    order.OrderTotal,
                    order.IsPaid,
                    new ShippingResponse(
                        shippingAddress.ShippingID,
                        new CustomerResponse(
                            customer.Name,
                            customer.Surname,
                            customer.Email,
                            customer.Phone
                        ),
                        shippingAddress.AddressLine1,
                        shippingAddress.AddressLine2,
                        shippingAddress.Suburb,
                        shippingAddress.Town,
                        shippingAddress.Region,
                        shippingAddress.PostalCode,
                        shippingAddress.Country
                    ),
                    new OnlyProductsResponse(
                        products,
                        customerCollection.CustomerProduct.Subtotal
                    )
                );
                results.Add(orderResponse);
            }
            return results;
        }
        catch
        {
            return new List<OrderResponse>();
        }
    }
}
