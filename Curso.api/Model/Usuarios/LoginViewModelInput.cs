using System.ComponentModel.DataAnnotations;

namespace Curso.api.Model.Usuarios
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "A Senha é obrigarória")]
        public string Senha { get; set; }
    }
}
