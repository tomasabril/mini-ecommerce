using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ecom.Models;
using System.Linq.Dynamic.Core;

// Class to hold Business logic
namespace ecom.Business
{

    public class ProductsService
    {
        private readonly ProductContext _context;

        public ProductsService(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts(string orderby)
        {
            return await _context.Products.AsQueryable().OrderBy(orderby).ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _context.Products.Where(product => product.Name.Contains(name)).ToListAsync();
        }

        public async Task<int> UpdateProduct(int id, Product product)
        {
            if (!ProductIsValid(product))
            {
                return 400;
            }
            var oldProduct = await _context.Products.FindAsync(id);

            if (oldProduct == null)
            {
                return 404;
            }
            oldProduct.Name = product.Name;
            oldProduct.Stock = product.Stock;
            oldProduct.Value = product.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return 204;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            if (ProductIsValid(product))
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product;
            }
            return null;
        }

        public async Task<int> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return 404;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return 204;
        }


        // helper methods //
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public bool ProductIsValid(Product product)
        {
            if (product == null) { return false; }
            if (product.Value >= 0)
            {
                return true;
            }
            return false;
        }
    }
}