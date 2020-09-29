using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class EstadoService : IEstadoService
    {             
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMapper _mapper;

        public EstadoService(IEstadoRepository estadoRepository, IMapper mapper)
        {
            _estadoRepository = estadoRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<EstadoDto>>> ObterTodos()
        {
            var estados = _estadoRepository.GetAll();

            if (!estados.Any())
                return ResultDto<IEnumerable<EstadoDto>>.Validation("Estados não encontrado na base de dados!");

            var estadoDto = _mapper.Map<IEnumerable<Estado>, IEnumerable<EstadoDto>>(estados);          

            return await Task.FromResult(ResultDto<IEnumerable<EstadoDto>>.Success(estadoDto));
        }
     
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
