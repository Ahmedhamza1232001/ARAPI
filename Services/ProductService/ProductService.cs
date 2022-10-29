using arapi.Data;
using arapi.Dtos.Product;
using ARAPI.Dtos.Product;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARAPI.Services.ProductService
{
    public class ProductService : IProductService
    {

        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ProductService(IMapper mapper , ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var servicesResponse = new ServiceResponse<List<GetProductDto>>();
            Product product = _mapper.Map<Product>(newProduct);
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            servicesResponse.Data= await _context.Products
                .Select(p=> _mapper.Map<GetProductDto>(p))
                .ToListAsync();
            return servicesResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();
            try
            {
                Product product =await _context.Products.FirstAsync  (p => p.Id == id);
                _context.Products.Remove (product);
                await _context.SaveChangesAsync();
                response.Data = _context.Products.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
                
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;

            }



            return response;
        }


        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProuducts()
        {
            var response = new ServiceResponse<List<GetProductDto>>();
            var dbProduct = await _context.Products.ToListAsync();
            response.Data = dbProduct.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
            return response;
            
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            var servicesResponse = new ServiceResponse<GetProductDto>();
            var dbProduct = await _context.Products.FirstOrDefaultAsync(p=>p.Id ==id);
            servicesResponse.Data = _mapper.Map<GetProductDto>(dbProduct);
            return servicesResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                _mapper.Map(updatedProduct, product);
                
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetProductDto>(product);
            }
           catch(Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;

            }



            return response;
        }

       


    }
}