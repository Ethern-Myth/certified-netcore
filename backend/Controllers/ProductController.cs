using backend.interfaces;
using backend.models.models;
using backend.models.requests;
using backend.models.responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin, Customer")]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService service;
    private string folderPath = Path.Combine("Resources", "Images");

    public ProductController(IProductService service)=>
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
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var product = await RequestProduct(request);
        await service.postRequest(product);
        await SaveImage(request.Image, product.ImageName);
        response = await ServerResponse(product);
        return CreatedAtAction(
            actionName: nameof(GetProduct),
            routeValues: new { id = response.ProductID },
            value: response);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateProduct([FromForm] ProductRequest request, Guid id)
    {
        dynamic response;
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var oldFile = await service.getSingleResponse(id, 0);
        var product = await RequestProduct(request);
        product.ProductID = id;
        product.DateUpdated = DateTimeOffset.UtcNow;
        await service.putRequest(product, id, 0);
        await UpdateImage(request.Image, oldFile.ImageName, product.ImageName);
        response = await ServerResponse(product);
        return Ok(response);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> RemoveProduct(Guid id)
    {
        await DeleteImage(id);
        await service.deleteRequest(id, 0);
        return NoContent();
    }

    [NonAction]
    private new async Task<Product> RequestProduct(ProductRequest request)
    {
        var imageName = await ImagePath(request.Image);
        var imageUrl = await ImageUrl(request.Image);
        return await Task.Run(() => new Product(
            request.Name,
            request.Description,
            request.Brand,
            request.Price,
            request.InStock,
            request.PDTypeID,
            request.ConversionSize,
            request.ConversionID,
            imageName,
            imageUrl
            ));
    }

    [NonAction]
    private new async Task<ProductResponse> ServerResponse(Product product)
    {
        try
        {
            var productType = await service.GetProductTypes(product.PDTypeID);
            var conversion = await service.GetConversion(product.ConversionID);
            return new ProductResponse(
                product.ProductID,
                product.Name,
                product.Desc,
                product.Brand,
                product.Price,
                product.InStock,
                productType,
                product.ConversionSize,
                conversion,
                product.ImageName,
                product.ImageUrl
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

        var results = new List<ProductResponse>();
        try
        {
            foreach (var item in products)
            {
                var productType = await service.GetProductTypes(item.PDTypeID);
                var conversion = await service.GetConversion(item.ConversionID);
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
                    item.ConversionSize,
                    new Conversion(
                        conversion.ConversionID,
                        conversion.Unit
                    ),
                    item.ImageName,
                    item.ImageUrl
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

        string fileName = "";
        if (file.Length > 0)
        {
            fileName = file.FileName;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string extension = Path.GetExtension(file.FileName);
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            FileInfo[] files = dir.GetFiles(fileName, SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                if (item.Exists)
                {
                    fileName = fileName.Replace(extension, "-" + Guid.NewGuid() + extension);
                    break;
                }
            }
        }
        return fileName;
    }

    [NonAction]
    private async Task<string> ImageUrl(IFormFile file)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}/";
        return Path.Combine(baseUrl, folderPath, file.FileName);
    }

    [NonAction]
    private async Task SaveImage(IFormFile file, string imageName)
    {
        try
        {
            if (file.Length > 0)
            {
                var path = Path.Combine(folderPath, imageName);
                using (var fileStream = new FileStream(path, FileMode.Create))
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
    private async Task UpdateImage(IFormFile file, string oldFile, string imageName)
    {
        try
        {
            if (file.Length > 0)
            {
                string fileToDelete = Path.Combine(folderPath, oldFile);
                FileInfo fileInfo = new FileInfo(fileToDelete);
                if (fileInfo.Exists)
                    fileInfo.Delete();
                var path = Path.Combine(folderPath, imageName);
                using (var fileStream = new FileStream(path, FileMode.Create))
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
        var fileToDelete = Path.Combine(folderPath, product.ImageName);
        FileInfo fileInfo = new FileInfo(fileToDelete);
        if (fileInfo.Exists)
            fileInfo.Delete();
    }
}
