using System;
using System.IO;
using System.Threading.Tasks;
using Ensemble_Elegance.Controllers;
using Ensemble_Elegance.Models;
using Ensemble_Elegance.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Moq;
using Xunit;

namespace Ensemble_Elegance.Tests
{
    public class AdminControllerTests
    {
        private readonly AdminController _controller;
        private readonly Mock<IAdminService> _adminService;
        public AdminControllerTests()
        {
            _adminService = new Mock<IAdminService>();
            _controller = new AdminController(_adminService.Object);
        }
        [Fact]
        public void AddNewProduct_View()
        {
            var result = _controller.AddNewproduct();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task AddsNewProductToDb()
        {
            ProductModel product = new ProductModel() { Id = 1, Name = "Product", Description = "blablabla" };
            await _controller.AddNewproduct(product);
            var result = _adminService.Object.GetProductByIdAsync(1);
            Assert.NotNull(result);
        }

    }
}
