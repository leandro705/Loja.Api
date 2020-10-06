using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.CrossCutting.Enumerators;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class EstabelecimentoService : IEstabelecimentoService
    {             
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;
        private readonly IMapper _mapper;

        public EstabelecimentoService(IEstabelecimentoRepository estabelecimentoRepository, IMapper mapper)
        {
            _estabelecimentoRepository = estabelecimentoRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<EstabelecimentoDto>>> ObterTodos(string url)
        {
            var estabelecimentos = await _estabelecimentoRepository.ObterTodos(url);

            if (!estabelecimentos.Any())
                return await Task.FromResult(ResultDto<IEnumerable<EstabelecimentoDto>>.Validation("Estabelecimentos não encontrado na base de dados!"));

            var estabelecimentoDto = _mapper.Map<IEnumerable<Estabelecimento>, IEnumerable<EstabelecimentoDto>>(estabelecimentos);          

            return await Task.FromResult(ResultDto<IEnumerable<EstabelecimentoDto>>.Success(estabelecimentoDto));
        }

        public async Task<ResultDto<EstabelecimentoDto>> ObterPorId(int estabelecimentoId)
        {
            var estabelecimento = await _estabelecimentoRepository.ObterPorId(estabelecimentoId);

            if (estabelecimento == null)
                return await Task.FromResult(ResultDto<EstabelecimentoDto>.Validation("Estabelecimento não encontrado na base de dados!"));

            var estabelecimentoDto = _mapper.Map<Estabelecimento, EstabelecimentoDto>(estabelecimento);

            return await Task.FromResult(ResultDto<EstabelecimentoDto>.Success(estabelecimentoDto));
        }

        public async Task<ResultDto<EstabelecimentoDto>> Create(EstabelecimentoDto estabelecimentoDto)
        {
            var estabelecimento = _mapper.Map<Estabelecimento>(estabelecimentoDto);            
            estabelecimento.SituacaoId = (int)ESituacao.ATIVO;
            estabelecimento.DataCadastro = DateTime.Now;
            await _estabelecimentoRepository.Create(estabelecimento);
            return await Task.FromResult(ResultDto<EstabelecimentoDto>.Success(_mapper.Map<EstabelecimentoDto>(estabelecimento)));
        }

        public async Task<ResultDto<bool>> Update(EstabelecimentoDto estabelecimentoDto)
        {
            var estabelecimento = await _estabelecimentoRepository.ObterPorId(estabelecimentoDto.EstabelecimentoId);
            estabelecimento.AtualizarEstabelecimento(estabelecimentoDto);
            await _estabelecimentoRepository.Update(estabelecimento);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<bool>> Delete(int estabelecimentoId)
        {
            var estabelecimento = await _estabelecimentoRepository.ObterPorId(estabelecimentoId);
            estabelecimento.DesabilitarEstabelecimento();
            await _estabelecimentoRepository.Update(estabelecimento);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }
    }
}
