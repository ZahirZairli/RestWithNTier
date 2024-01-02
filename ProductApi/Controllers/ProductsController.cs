using Business.Services.Abstracts;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _productService.GetById(id);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpGet("GetByName")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _productService.GetByName(name);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add(CreateProductDto createProductDto)
    {

        var result = await _productService.AddAsync(createProductDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
    {
        string[] notUpdatedColumn = new string[] { "Created" };
        var result = await _productService.UpdateAsync(updateProductDto,notUpdatedColumn);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

}
