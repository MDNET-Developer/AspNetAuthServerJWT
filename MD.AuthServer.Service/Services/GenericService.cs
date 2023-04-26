using AutoMapper;
using MD.AuthServer.Core.Repositries;
using MD.AuthServer.Core.Service;
using MD.AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLiblary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MD.AuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            var mappedData = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(mappedData);
            var convertDto = _mapper.Map<TDto>(mappedData);
            return Response<TDto>.Success(convertDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            var mappedData = _mapper.Map<IEnumerable<TDto>>(data);
            return Response<IEnumerable<TDto>>.Success(mappedData, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return Response<TDto>.Fail($"Id = {id} not found", 404, true);
            }
            return Response<TDto>.Success(_mapper.Map<TDto>(data), 200);
        }



        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var exsistId = await _repository.GetByIdAsync(id);
            if (exsistId == null)
            {
                return Response<NoDataDto>.Fail($"ID = {id} not found", 404, true);
            }
            _repository.Remove(exsistId);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(TDto dto, int id)
        {
            var exsistId = await _repository.GetByIdAsync(id);
            if (exsistId == null)
            {
                return Response<NoDataDto>.Fail($"ID = {id} not found", 404, true);
            }
            var mappedData = _mapper.Map<TEntity>(dto);
            _repository.Update(mappedData);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var dataList = _repository.Where(expression);
            var mappedData = _mapper.Map<IEnumerable<TDto>>(await dataList.ToListAsync());
            return Response<IEnumerable<TDto>>.Success(mappedData,200);
        }
    }
}
