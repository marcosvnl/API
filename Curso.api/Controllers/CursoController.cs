using Curso.api.Model.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize] // forçar atenticação para a uso da API
    public class CursoController : ControllerBase
    {
        /// <summary>
        /// Este serviço permite cadastrar cursos para o usuário autenticado.
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            //var CodigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Esse serviço permite obter todos os cursos ativos do uisuário.
        /// </summary>
        /// <returns>retorna status OK e dados do curso do usuário</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var cursos = new List<CursoViewmodelOutput>();
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            cursos.Add(new CursoViewmodelOutput()
            {
                Login = codigoUsuario.ToString(),
                Descricao = "Teste API",
                Nome = "Marcos Vinicius"

            });
            return Ok(cursos);
        }
    } 
}
