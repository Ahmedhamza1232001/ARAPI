using arapi.Dtos.Product;
using ARAPI.Dtos.Product;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARAPI.Services.ProductService
{
    public class ProductService : IProductService
    {

        private static List<Product> _products = new List<Product>()
        {
            new Product(),
            new Product {Id =12,Name="Chair"}
        };
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var servicesResponse = new ServiceResponse<List<GetProductDto>>();
            Product product = _mapper.Map<Product>(newProduct);
            product.Id = _products.Max(p =>p.Id)+1;
            _products.Add(product);
            servicesResponse.Data=_products.Select(p=> _mapper.Map<GetProductDto>(p)).ToList();
            return servicesResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> response = new ServiceResponse<List<GetProductDto>>();
            try
            {
                Product product = _products.First  (p => p.Id == id);
                _products.Remove (product);
                response.Data = _products.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
                
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
            return new ServiceResponse<List<GetProductDto>> {
                Data = _products.Select(p => _mapper.Map<GetProductDto>(p)).ToList()
            };
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            var servicesResponse = new ServiceResponse<GetProductDto>();
            var product = _products.FirstOrDefault(p=>p.Id ==id);
            servicesResponse.Data = _mapper.Map<GetProductDto>(product);
            return servicesResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> response = new ServiceResponse<GetProductDto>();
            try
            {
                Product product = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                _mapper.Map(updatedProduct, product);
                
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