using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace ProductManagement.Products
{
    public class ProductAppService_Tests : ProductManagementApplicationTestBase
    {
        private readonly IProductAppService _productAppService;
        public ProductAppService_Tests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
        }

        [Fact]
        public async Task Should_Get_Product_List()
        {
            //Act
            var output = await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            //Assert
            output.TotalCount.ShouldBe(3);
            output.Items.ShouldContain(x => x.Name.Contains("Acme Monochrome Laser Printer"));
        }

        [Fact]
        public async Task Should_Get_Category_List()
        {
            //Act
            var output = await _productAppService.GetCategoriesAsync();

            //Assert
            output.Items.Count.ShouldBe(2);
            output.Items.ShouldContain(x => x.Name.Contains("Monitors"));
        }

        [Fact]
        public async Task Should_Create_Product()
        {
            var categories = await _productAppService.GetCategoriesAsync();

            var input = new CreateUpdateProductDto
            {
                CategoryId = categories.Items.FirstOrDefault().Id,
                Name = "Product for test",
                IsFreeCargo = true,
                Price = 1000,
                ReleaseDate = DateTime.Now,
                StockState = ProductStockState.InStock
            };

            //Act
            await _productAppService.CreateAsync(input);

            //Assert
            var products = await _productAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            products.Items.ShouldContain(x => x.Name.Contains("Product for test"));
        }
    }
}
