using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecom.Models;
using ecom.Business;
using System.Linq.Dynamic.Core;

namespace ecom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductsService _ProductService;

        public ProductsController(ProductContext context)
        {
            _ProductService = new ProductsService(context);
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string orderby = "Id")
        {
            var products = await _ProductService.GetProducts(orderby);
            return products.ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _ProductService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/name
        [HttpGet("SearchByName/{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
        {
            var products = await _ProductService.GetProductsByName(name);
            return products.ToList();
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                var result = await _ProductService.UpdateProduct(id, product);
                switch (result)
                {
                    case 400:
                        return BadRequest("Invalid Product");
                    case 404:
                        return NotFound();
                    default:
                        return NoContent();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var createdProduct = await _ProductService.CreateProduct(product);
            if (createdProduct == null)
            {
                return BadRequest("Invalid Product");
            }

            return CreatedAtAction("GetProduct", new { id = createdProduct.Id }, createdProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var status = await _ProductService.DeleteProduct(id);
            if (status == 404)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
