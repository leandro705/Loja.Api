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
    public class MunicipioService : IMunicipioService
    {             
        private readonly IMunicipioRepository _municipioRepository;
        private readonly IMapper _mapper;

        public MunicipioService(IMunicipioRepository municipioRepository, IMapper mapper)
        {
            _municipioRepository = municipioRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<MunicipioDto>>> ObterTodosPorEstado(int estadoId)
        {
            var municipios = _municipioRepository.Find(x => x.EstadoId == estadoId);

            if (!municipios.Any())
                return ResultDto<IEnumerable<MunicipioDto>>.Validation("Município não encontrado na base de dados!");

            var municipioDto = _mapper.Map<IEnumerable<Municipio>, IEnumerable<MunicipioDto>>(municipios);          

            return await Task.FromResult(ResultDto<IEnumerable<MunicipioDto>>.Success(municipioDto));
        }
     
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
