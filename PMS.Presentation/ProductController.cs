using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Service.Contracts;
using PMS.Shared.DataTransferObjects;
using PMS.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UMS.ActionFilters;

namespace PMS.Presentation
{

    [Route("api/users/{userId:guid}/products")]
    [ApiController]
    //[ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class RolesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public RolesController(IServiceManager service) => _service = service;
        [HttpGet(Name = "GetProducts")]
        [HttpHead]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetProducts(Guid userId, [FromQuery] ProductParameters prodParameters)
        {
            var pagedResult = await _service.ProductService.GetProductsAsync(userId, prodParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.users);
        }
        [HttpGet("{id:guid}", Name = "GetProductById")]
        public async Task<IActionResult> GetProduct(Guid userId, Guid id)
        {
            var product = await _service.ProductService.GetProductAsync(userId, id, false);
            return Ok(product);
        }
        [HttpPost(Name = "CreateProducts")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProduct(Guid userId,[FromBody] ProductForPostDTO product)
        {
            var createdProduct = await _service.ProductService.CreateProductAsync(userId,product);
            return CreatedAtRoute("GetProductById", new { userId, id = createdProduct.Id }, createdProduct);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid userId,Guid id)
        {
            await _service.ProductService.DeleteProductAsync(userId,id, false);
            return NoContent();
        }
        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePrroduct(Guid userId,Guid id, [FromBody] ProductForUpdateDTO product)
        {
            await _service.ProductService.UpdateProductAsync(userId,id, product, true);
            return NoContent();
        }
        [HttpOptions]
        public IActionResult GetRolesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }


    }

}
