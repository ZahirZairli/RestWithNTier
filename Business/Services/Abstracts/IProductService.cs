using Core.Utilities.Results;
using Entities.Dtos;

namespace Business.Services.Abstracts;

public interface IProductService
{
    Task<IDataResult<List<GetProductDto>>> GetAll();
    Task<IDataResult<GetProductDto>> GetById(int id);
    Task<IDataResult<GetProductDto>> GetByName(string name);
    Task<IResult> AddAsync(CreateProductDto dto);
    Task<IResult> UpdateAsync(UpdateProductDto dto,params string[] notUpdatedProperties);
    Task<IResult> DeleteAsync(int id);

}
