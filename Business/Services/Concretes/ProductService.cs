using AutoMapper;
using Business.Services.Abstracts;
using Business.Utilities.Constants;
using Core.Utilities.Exceptions;
using Core.Utilities.Results;
using DataAccess.Repositories.Abstracts;
using Entities;
using Entities.Dtos;

namespace Business.Services.Concretes;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IResult> AddAsync(CreateProductDto dto)
    {
        if (await _productRepository.ExistAsync(p=>p.Name == dto.Name))
        {
            throw new AlreadyExistExceptions(ExceptionMessages.AlreadyExist);
        }
        Product product = _mapper.Map<Product>(dto);
        product.ProductCode = product.Name.Substring(1, 2);
        product.Created = DateTime.UtcNow;
        await _productRepository.AddAsync(product);
        int result = await _productRepository.SaveAsync();
        if (result==0)
        {
            return new ErrorResult(Messages.NotAdded);
        }
        return new SuccessResult(Messages.Added);
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        Product product = await _productRepository.GetAsync(x => x.Id == id);
        if (product is null) throw new NotFoundException(ExceptionMessages.NotFound);
       _productRepository.Delete(product);
        int result = await _productRepository.SaveAsync();
        if (result == 0)
        {
            return new ErrorResult(Messages.NotDeleted);
        }
        return new SuccessResult(Messages.Deleted);
    }

    public async Task<IDataResult<List<GetProductDto>>> GetAll()
    {
        var result = await _productRepository.GetAllAsync();
        //if (result.Count == 0) throw new NotFoundException(ExceptionMessages.NotFound);
        if (result.Count == 0)
        {
            return new ErrorDataResult<List<GetProductDto>>(Messages.NotListed);
        };
        return new SuccesDataResult<List<GetProductDto>>(_mapper.Map<List<GetProductDto>>(result),Messages.Listed);
    }

    public async Task<IDataResult<GetProductDto>> GetById(int id)
    {
        Product product = await _productRepository.GetAsync(x => x.Id == id);
        //if (product is null) throw new NotFoundException(ExceptionMessages.NotFound);
        if (product is null)
        {
            return new ErrorDataResult<GetProductDto>(Messages.NotFound);
        };
        return new SuccesDataResult<GetProductDto>(_mapper.Map<GetProductDto>(product),Messages.Found);
    }

    public async Task<IDataResult<GetProductDto>> GetByName(string name)
    {
        Product product = await _productRepository.GetAsync(x => x.Name == name);
        //if (product is null) throw new NotFoundException(ExceptionMessages.NotFound);
        if (product is null)
        {
            return new ErrorDataResult<GetProductDto>(Messages.NotFound);
        };
        return new SuccesDataResult<GetProductDto>(_mapper.Map<GetProductDto>(product),Messages.Found);
    }

    public async Task<IResult> UpdateAsync(UpdateProductDto dto,params string[] notUpdatedProperties)
    {
        if (!await _productRepository.ExistAsync(x => x.Id == dto.Id)) throw new NotFoundException(ExceptionMessages.NotFound);
        Product product = _mapper.Map<Product>(dto);
        product.ProductCode = dto.Name.Substring(1, 2);
        _productRepository.Update(product,notUpdatedProperties);
        int result = await _productRepository.SaveAsync();
        if (result == 0)
        {
            return new ErrorResult(Messages.NotUpdated);
        }
        return new SuccessResult(Messages.Updated);
    }
}
