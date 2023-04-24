using SharedLiblary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Core.Service
{
    public interface IGenericService<TEntity,TDto>
        where TEntity : class
        where TDto : class
    {
        Task<Response<TEntity>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TEntity>>> GetAllAsync();
        Task<Response<IEnumerable<TEntity>>> Where(Expression<Func<TEntity, bool>> expression);
        Task<Response<TDto>> AddAsync(TEntity entity);
        Task<Response<NoDataDto>> Remove(TEntity entity);
        Task<Response<NoDataDto>> Update(TEntity entity);

    }
}
