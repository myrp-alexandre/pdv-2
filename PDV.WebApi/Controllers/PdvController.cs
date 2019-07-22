using System;
using Microsoft.AspNetCore.Mvc;
using PDV.Domain.Entities.Requests;
using PDV.Domain.Interfaces.Services;
using PDV.Domain.Entities.Responses;

namespace PDV.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PdvController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public PdvController(ITransacaoService transacaoService)
        {
            this._transacaoService = transacaoService;
        }

        // POST api/pdv/pagar
        [HttpPost("pagar")]
        public IActionResult Pagar([FromBody] PagamentoRequest request)
        {
            var response = new PagamentoResponse { Valido = true };

            try
            {
                response = this._transacaoService.MostrarMensagemTrocoInteligente(request.TotalVenda, request.ValorPago);
                return Ok(response);
            }
            catch (Exception)
            {
                response.Valido = false;
                response.Mensagem = $"Ocorreu um erro inesperado. Por favor tente novamente.";

                return StatusCode(500, response);
            }
        }

    }
}
