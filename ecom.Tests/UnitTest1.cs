using System;
using Xunit;
using ecom.Business;
using ecom.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ecom.Tests
{
    public class UnitTest1
    {
        private ProductsService _ProductService;
        private ProductContext _context_test;

        public UnitTest1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            optionsBuilder.UseInMemoryDatabase("ProductList_Test");
            _context_test = new ProductContext(optionsBuilder.Options);

            _ProductService = new ProductsService(_context_test);
        }
        private void SetupTestDb()
        {
            _context_test.Database.EnsureDeleted();

            var p1 = new Product();
            p1.Name = "test1";
            p1.Stock = 0;
            p1.Value = 0;
            var p2 = new Product();
            p2.Name = "test2";
            p2.Stock = 1;
            p2.Value = 1;
            var p3 = new Product();
            p3.Name = "test3";
            p3.Stock = -1;
            p3.Value = -1;

            _context_test.Add(p1);
            _context_test.Add(p2);
            _context_test.Add(p3);

            _context_test.SaveChanges();
            // foreach (Product element in _context_test.Products.ToList())
            // {
            //     Console.Write("id: " + element.Id + " ");
            // }
            // Console.Write("\n ");
        }

        [Fact]
        public void ProductIsValid_Test()
        {
            SetupTestDb();

            Assert.True(_ProductService.ProductIsValid(_context_test.Products.Find(1)),
             "Valid Product1 identified as invalid.");
            Assert.True(_ProductService.ProductIsValid(_context_test.Products.Find(2)),
             "Valid Product2 identified as invalid.");
            Assert.False(_ProductService.ProductIsValid(_context_test.Products.Find(3)),
             "Invalid Product3 identified as valid.");
        }

        [Fact]
        public void DeleteProduct_Test()
        {
            SetupTestDb();

            Assert.True(_ProductService.DeleteProduct(1).Result == 204, "Product not deleted.");
            Assert.True(_ProductService.DeleteProduct(1).Result == 404, "Product not deleted.");
        }

        [Fact]
        public void GetProducts_Test()
        {
            SetupTestDb();
            var products = _ProductService.GetProducts("Id").Result.Count();
            Assert.True(
                products == 3, "Did not return all products. Returned " + products
            );
        }

        [Fact]
        public void GetProductsByName_Test()
        {
            SetupTestDb();
            var products = _ProductService.GetProductsByName("te").Result;
            Assert.True(
                products.Count() == 3, "Did not return all products. Returned " + products.Count()
            );
            Assert.True(
                products.Any(p => p.Name == "test1"), "product 1 not found in search by name.");
            Assert.True(
                products.Any(p => p.Name == "test2"), "product 2 not found in search by name.");
            Assert.True(
                products.Any(p => p.Name == "test3"), "product 3 not found in search by name.");

            var noproducts = _ProductService.GetProductsByName("zz").Result;
            Assert.True(
                products.Count() != 0, "Search returned something when wrong parameter used.");
        }

        [Fact]
        public void UpdateProduct_Test()
        {
            SetupTestDb();
            var p1 = new Product();
            p1.Name = "valid";
            p1.Stock = 5;
            p1.Value = 5;
            var p2 = new Product();
            p2.Name = "invalid";
            p2.Stock = 5;
            p2.Value = -5;

            var status1 = _ProductService.UpdateProduct(1, p1).Result;
            Assert.True(status1 == 204, "Product not updated. " + status1);
            var status2 = _ProductService.UpdateProduct(2, p2).Result;
            Assert.True(status2 == 400, "Product not identified as invalid. " + status2);
            var status3 = _ProductService.UpdateProduct(4, p1).Result;
            Assert.True(status3 == 404, "Product not identified as not existing " + status3);
        }

        [Fact]
        public void CreateProduct_Test()
        {
            SetupTestDb();
            var p1 = new Product();
            p1.Name = "valid";
            p1.Stock = 5;
            p1.Value = 5;

            var created = _ProductService.CreateProduct(p1).Result;
            Assert.True(created != null, "Product not created. ");
        }


    }
}
