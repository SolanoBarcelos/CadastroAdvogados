    using System.Web.Mvc;
    using System.Web.Routing;

    namespace Web.Controllers
    {
        public class AutorizadoAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                bool usuarioAutenticado = true; // Simulação

                if (!usuarioAutenticado)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                        { "controller", "Advogado" },
                        { "action", "Index" }
                        });
                }

                base.OnActionExecuting(filterContext);
            }
        }
    }
