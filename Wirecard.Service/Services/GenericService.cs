﻿using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Core.Repositories;
using Wirecard.Core.Services;
using Wirecard.Core.UnitOfWork;

namespace Wirecard.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericRepository<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepository.AddAsync(newEntity);

            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var datas = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(datas, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var data = await _genericRepository.GetByIdAsync(id);
            if (data == null)
            {
                return Response<TDto>.Fail("Id not found", 404, true);
            }

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(data), 200);

        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var isRecordExist = await _genericRepository.GetByIdAsync(id);
            if (isRecordExist == null)
            {
                return Response<NoDataDto>.Fail("Record not found", 404, true);
            }

            _genericRepository.Remove(isRecordExist);
            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204);

        }

        public async Task<Response<NoDataDto>> Update(TDto entity, int id)
        {
            var isRecordExist = await _genericRepository.GetByIdAsync(id);
            if (isRecordExist == null)
            {
                return Response<NoDataDto>.Fail("Record not found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _genericRepository.Update(updateEntity);
            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(
                ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()),
                200
                );

        }
    }
}
