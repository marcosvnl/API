using System.ComponentModel.DataAnnotations;

namespace Curso.api.Model.Usuarios
{
    public class RegistroViewModelInput
    {
        [Required(ErrorMessage = "O Login é obrigatorio")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O E-mail é obrigatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A Senha é obrigatorio")]
        public string Senha { get; set; }
    }
}
