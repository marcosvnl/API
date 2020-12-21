using Curso.api.Business.Etities;
using Curso.api.Business.Repositories;
using Curso.api.Model.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize] // forçar atenticação para a uso da API
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

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
            Cursos cursos = new Cursos();
            cursos.Nome = cursoViewModelInput.Nome;
            cursos.Descricao = cursoViewModelInput.Descricao;
            var CodigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            cursos.CodigoUsuario = CodigoUsuario;
            _cursoRepository.Adicionar(cursos);
            _cursoRepository.Commit();
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
            
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario)
                .Select(s => new CursoViewmodelOutput()
                {
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Login =s.Usuario.Login
                });

                return Ok(cursos);
        }
    } 
}
