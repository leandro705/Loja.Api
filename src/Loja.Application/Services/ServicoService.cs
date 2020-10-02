using AutoMapper;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repository;
using Loja.Domain.Interfaces.Services;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.CrossCutting.Enumerators;
using System;

namespace Loja.Application.Services
{
    public class ServicoService : IServicoService
    {             
        private readonly IServicoRepository _servicoRepository;
        private readonly IMapper _mapper;

        public ServicoService(IServicoRepository servicoRepository, IMapper mapper)
        {
            _servicoRepository = servicoRepository;
            _mapper = mapper;
        }       
    
        public async Task<ResultDto<IEnumerable<ServicoDto>>> ObterTodos()
        {
            var servicos = await _servicoRepository.ObterTodos();

            if (!servicos.Any())
                return ResultDto<IEnumerable<ServicoDto>>.Validation("Serviços não encontrado na base de dados!");

            var servicoDto = _mapper.Map<IEnumerable<Servico>, IEnumerable<ServicoDto>>(servicos);          

            return await Task.FromResult(ResultDto<IEnumerable<ServicoDto>>.Success(servicoDto));
        }       

        public async Task<ResultDto<ServicoDto>> ObterPorId(int servicoId)
        {
            var servico = await _servicoRepository.ObterPorId(servicoId);

            if (servico == null)
                return await Task.FromResult(ResultDto<ServicoDto>.Validation("Serviço não encontrado na base de dados!"));

            var servicoDto = _mapper.Map<Servico, ServicoDto>(servico);

            return await Task.FromResult(ResultDto<ServicoDto>.Success(servicoDto));
        }

        public async Task<ResultDto<ServicoDto>> Create(ServicoDto servicoDto)
        {
            var servico = _mapper.Map<Servico>(servicoDto);
            servico.SituacaoId = (int)ESituacao.ATIVO;
            servico.DataCadastro = DateTime.Now;
            await _servicoRepository.Create(servico);
            return await Task.FromResult(ResultDto<ServicoDto>.Success(_mapper.Map<ServicoDto>(servico)));
        }

        public async Task<ResultDto<bool>> Update(ServicoDto servicoDto)
        {
            var servico = await _servicoRepository.ObterPorId(servicoDto.ServicoId);
            servico.AtualizarServico(servicoDto);
            await _servicoRepository.Update(servico);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<bool>> Delete(int servicoId)
        {
            var servico = await _servicoRepository.ObterPorId(servicoId);
            servico.DesabilitarServico();
            await _servicoRepository.Update(servico);
            return await Task.FromResult(ResultDto<bool>.Success(true));
        }
    }
}
