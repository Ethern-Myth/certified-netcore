using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using backend.models.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Customer, Admin, Super")]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService service;

    public ProductController(IProductService service) =>
        this.service = service;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetProducts() =>
        Ok(await ServerResponse(await service.getResponse()));

    [AllowAnonymous]
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetProduct(Guid id) =>
        Ok(await ServerResponse(await service.getSingleResponse(id, 0)));

    [HttpPost, DisableRequestSizeLimit]
    public async Task<IActionResult> SaveProduct([FromForm] ProductRequest request)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var product = await RequestProduct(request);
            await service.postRequest(product);
            await SaveImage(request.Image, product.ProductImgPath);
            response = await ServerResponse(product);
        }
        else
            return Problem();
        return CreatedAtAction(
            actionName: nameof(GetProduct),
            routeValues: new { id = response.ProductID },
            value: response);
    }

    [HttpPut("Update/{id:Guid}")]
    public async Task<IActionResult> UpdateProduct([FromForm] ProductRequest request, Guid id)
    {
        dynamic response;
        if (ModelState.IsValid)
        {
            var oldFile = await service.getSingleResponse(id, 0);
            var product = await RequestProduct(request);
            product.ProductID = id;
            product.DateUpdated = DateTimeOffset.UtcNow;
            await service.putRequest(product, id, 0);
            await UpdateImage(request.Image, oldFile.ProductImgPath, product.ProductImgPath);
            response = await ServerResponse(product);
        }
        else
            return Problem();
        return Ok(response);
    }

    [HttpDelete("Delete/{id:Guid}")]
    public async Task<IActionResult> RemoveProduct(Guid id)
    {
        await DeleteImage(id);
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new async Task<Product> RequestProduct(ProductRequest request)
    {
        var imagePath = await ImagePath(request.Image);
        return await Task.Run(() => new Product(
            request.Name,
            request.Description,
            request.Brand,
            request.Price,
            request.InStock,
            request.PDTypeID,
            imagePath
            ));
    }

    [NonAction]
    private new async Task<ProductResponse> ServerResponse(Product product)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}/";
        try
        {
            var productType = await service.GetProductTypes(product.PDTypeID);
            return new ProductResponse(
                product.ProductID,
                product.Name,
                product.Desc,
                product.Brand,
                product.Price,
                product.InStock,
                productType,
                Path.Combine(baseUrl, product.ProductImgPath)
            );
        }
        catch
        {
            return new ProductResponse();
        }
    }

    [NonAction]
    private new async Task<List<ProductResponse>> ServerResponse(List<Product> products)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}/";
        var results = new List<ProductResponse>();
        try
        {
            foreach (var item in products)
            {
                var productType = await service.GetProductTypes(item.PDTypeID);
                results.Add(
                    new ProductResponse(
                    item.ProductID,
                    item.Name,
                    item.Desc,
                    item.Brand,
                    item.Price,
                    item.InStock,
                    new ProductType(
                        productType.PDTypeID,
                        productType.Category
                    ),
                    Path.Combine(baseUrl, item.ProductImgPath)
                )
            );
            }
            return results;
        }
        catch
        {
            return new List<ProductResponse>();
        }
    }

    [NonAction]
    private async Task<string> ImagePath(IFormFile file)
    {
        string path = "";
        string fileName = "";
        if (file.Length > 0)
        {
            fileName = file.FileName;
            path = Path.Combine("Resources", "Images");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string extension = Path.GetExtension(file.FileName);
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles(fileName, SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                if (item.Exists)
                {
                    fileName = fileName.Replace(extension, "-" + Guid.NewGuid() + extension);
                    break;
                }
            }
            path = Path.Combine(path, fileName);
        }
        return path;
    }

    [NonAction]
    private async Task SaveImage(IFormFile file, string imagePath)
    {
        try
        {
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed: ", ex);
        }
    }

    [NonAction]
    private async Task UpdateImage(IFormFile file, string oldFile, string imagePath)
    {
        try
        {
            if (file.Length > 0)
            {
                FileInfo fileInfo = new FileInfo(oldFile);
                if (fileInfo.Exists)
                    fileInfo.Delete();
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed: ", ex);
        }
    }

    [NonAction]
    private async Task DeleteImage(Guid id)
    {
        var product = await service.getSingleResponse(id, 0);
        var fileToDelete = product.ProductImgPath;
        FileInfo fileInfo = new FileInfo(fileToDelete);
        if (fileInfo.Exists)
            fileInfo.Delete();
    }
}
