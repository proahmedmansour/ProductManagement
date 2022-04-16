﻿using ProductManagement.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Products
{
    public class ProductAppService : ProductManagementAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        public ProductAppService(IRepository<Product, Guid> productRepository,
            IRepository<Category, Guid> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _productRepository.WithDetailsAsync(x => x.Category);

            queryable = queryable
                        .Skip(input.SkipCount)
                        .Take(input.MaxResultCount)
                        .OrderBy(_ => input.Sorting ?? nameof(Product.Name));

            var products = await AsyncExecuter.ToListAsync(queryable);
            var count = await _productRepository.GetCountAsync();

            return new PagedResultDto<ProductDto>(count, ObjectMapper.Map<List<Product>, List<ProductDto>>(products));
        }

        public async Task CreateAsync(CreateUpdateProductDto input)
        {
            await _productRepository.InsertAsync(ObjectMapper.Map<CreateUpdateProductDto, Product>(input));
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetListAsync();

            return new ListResultDto<CategoryLookupDto>(ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories));
        }
    }
}
