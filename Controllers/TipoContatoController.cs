using ApiAso.Model.App;
using ApiAso.Repositorio.Interface.Cadastro;
using Microsoft.AspNetCore.Mvc;

namespace ApiAso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoContatoController : ControllerBase
    {
        private readonly ITipoContatoRepositorio _tipoContatoRepositorio;

        public TipoContatoController(ITipoContatoRepositorio tipoContatoRepositorio)
        {
            _tipoContatoRepositorio = tipoContatoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<_TipoContatoModel>>> Obter()
        {
            var liTipoContato = await _tipoContatoRepositorio.Obter();
            if (liTipoContato is null || liTipoContato[0].idTipoContato.Equals(0))
                return BadRequest(liTipoContato);

            return Ok(liTipoContato);
        }
    }
}
