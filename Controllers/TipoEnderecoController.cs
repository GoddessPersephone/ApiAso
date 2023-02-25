using ApiAso.Model.App;
using ApiAso.Repositorio.Interface.Cadastro;
using Microsoft.AspNetCore.Mvc;

namespace ApiAso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEnderecoController : ControllerBase
    {
        private readonly ITipoEnderecoRepositorio _tipoEnderecoRepositorio;

        public TipoEnderecoController(ITipoEnderecoRepositorio tipoEnderecoRepositorio)
        {
            _tipoEnderecoRepositorio = tipoEnderecoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<_TipoEnderecoModel>>> Obter()
        {
            var liTipoEndereco = await _tipoEnderecoRepositorio.Obter();
            if (liTipoEndereco is null || liTipoEndereco[0].idTipoEndereco.Equals(0))
                return BadRequest(liTipoEndereco);

            return Ok(liTipoEndereco);
        }
    }
}
