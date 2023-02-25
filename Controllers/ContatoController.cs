using ApiAso.DTO.Cadastro;
using ApiAso.Model.Cadastro;
using ApiAso.Repositorio.Interface.Cadastro;
using Microsoft.AspNetCore.Mvc;

namespace ApiAso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepositorio _ContatoRepositorio;
        private readonly ILogger<ContatoController> _logger;

        public ContatoController(IContatoRepositorio ContatoRepositorio, ILogger<ContatoController> logger)
        {
            _ContatoRepositorio = ContatoRepositorio;
            _logger = logger;
        }

        [HttpGet("Obter")]
        public async Task<ActionResult> Obter([FromQuery] ContatoDTO filtro)
        {
            _logger.LogInformation("Requisição feita ao obter do Contato");
            var liContato = await _ContatoRepositorio.Obter(filtro);
            if (liContato is null || liContato[0].idTipoContato.Equals(0))
                return BadRequest(liContato);

            return Ok(liContato);
        }
        [HttpPost]
        public async Task<IActionResult> Adicionar(ContatoModel modelo)
        {
            _logger.LogInformation("Requisição feita ao adicionar do Contato");
            if (modelo is null)
            {
                _logger.LogInformation("O modelo de dados do contato esta nulo");
                return BadRequest("Contato invalido");
            }

            var modelContato = await _ContatoRepositorio.Adicionar(modelo);
            if (modelContato == 0)
            {
                _logger.LogInformation("Não foi possivel adicionar o contato");
                return BadRequest(modelContato);
            }

            return Ok(modelContato);
        }
    }
}
