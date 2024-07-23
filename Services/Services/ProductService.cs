using AutoMapper;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateProductAsync(ProductForCreate productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                await _context.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductForView>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                IEnumerable<ProductForView> view = _mapper.Map<IEnumerable<ProductForView>>(products);
                return view;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<ProductForView> GetProductByIdAsync(Guid id)
        {
            try
            {
                var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
                ProductForView view = _mapper.Map<ProductForView>(product);
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception("Don't have this Product");
            }
        }

        public async Task<bool> UpdateProductAsync(ProductForUpdate productDto, Guid id)
        {
            try
            {
                var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
                _mapper.Map(productDto, product);
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
