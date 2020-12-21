using Curso.api.Business.Etities;
using Curso.api.Business.Repositories;
using Curso.api.Configurations;
using Curso.api.filters;
using Curso.api.Model;
using Curso.api.Model.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;
        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }
        /// <summary>
        /// Seviço que permite autenticar um usuário cadastrado e ativo
        /// </summary>
        /// <param name="loginViewModelInput">View model do login</param>
        /// <returns>Retona status ok, dados do usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ou autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatorios", Type = typeof(ValdaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            var usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);

            if (usuario == null)
            {
                return BadRequest("Houve um erro ou acessar o banco de dados");
            }

            //if (usuario.Senha != loginViewModelInput.Senha.GerarSenhaCriptografada())
            //{
            //    return BadRequest("Houve um erro ou acessar o banco de dados");
            //}

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };

            var token = _authenticationService.GerarToken(usuarioViewModelOutput);

            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });

        }

        /// <summary>
        /// Seviço que permite cadastrar um usuário cadastrado não existemte.
        /// </summary>
        /// <param name="loginViewModelInput">View model de registro de login</param>
        /// <returns>Retona status ok, dados do usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ou autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatorios", Type = typeof(ValdaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput loginViewModelInput)
        {

            

            //var migracoesPendentes = contexto.Database.GetPendingMigrations();

            //if (migracoesPendentes.Count() > 0)
            //{
            //    contexto.Database.Migrate();
            //}

            var usuario = new Usuario();
            usuario.Login = loginViewModelInput.Login;
            usuario.Senha = loginViewModelInput.Senha;
            usuario.Email = loginViewModelInput.Email;
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", loginViewModelInput);
        }
    }
}
