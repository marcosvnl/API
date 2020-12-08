using Curso.api.Model.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Curso.api.filters
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
               var validaCampoViewmodel = new ValdaCampoViewModelOutput(context.ModelState.SelectMany(sm => sm.Value.Errors.Select(s => s.ErrorMessage)));
               context.Result = new BadRequestObjectResult(validaCampoViewmodel);
            }

            
        }
    }
}
