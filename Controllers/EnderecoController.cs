using ApiAso.DTO.Cadastro;
using ApiAso.Model.Cadastro;
using ApiAso.Repositorio.Interface.Cadastro;
using Microsoft.AspNetCore.Mvc;

namespace ApiAso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly ILogger<EnderecoController> _logger;

        public EnderecoController(IEnderecoRepositorio enderecoRepositorio, ILogger<EnderecoController> logger)
        {
            _enderecoRepositorio = enderecoRepositorio;
            _logger = logger;
        }

        [HttpGet("Obter")]
        public async Task<ActionResult> Obter([FromQuery] EnderecoDTO filtro)
        {
            _logger.LogInformation("Requisição feita ao obter do endereco");
            var liEndereco = await _enderecoRepositorio.Obter(filtro);
            if (liEndereco is null || liEndereco[0].idTipoEndereco.Equals(0))
                return BadRequest(liEndereco);

            return Ok(liEndereco);
        }
        [HttpPost]
        public async Task<IActionResult> Adicionar(EnderecoModel modelo)
        {
            _logger.LogInformation("Requisição feita ao adicionar do endereco");
            if (modelo is null)
            {
                _logger.LogInformation("O modelo de dados do endereço esta nulo");
                return BadRequest("Endereço invalido");
            }

            var modelEndereco = await _enderecoRepositorio.Adicionar(modelo);
            if (modelEndereco == 0)
            {
                _logger.LogInformation("Não foi possivel adicionar o endereço");
                return BadRequest(modelEndereco);
            }

            return Ok(modelEndereco);
        }
    }
}
