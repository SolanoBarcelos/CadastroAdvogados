using Dominio;
using Repositorio.Implementacao;
using Repositorio.Interface;
using System;
using System.Linq;
using System.Web.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Autorizado]
    public class AdvogadoController : Controller
    {
        private readonly IAdvogadoRepositorio _advogadoRepositorio;

        public AdvogadoController()
        {
            _advogadoRepositorio = new AdvogadoRepositorio();
        }

        public ActionResult Index(string Busca)
        {
            var listaDominio = _advogadoRepositorio.ListarAdvogados(Busca);

            var listaViewModel = listaDominio.Select(pObj => new AdvogadoViewModel
            {
                Id = pObj.Id,
                Nome = pObj.Nome,
                Senioridade = pObj.Senioridade,
                Logradouro = pObj.Endereco.Logradouro,
                Numero = pObj.Endereco.Numero,
                Bairro = pObj.Endereco.Bairro,
                Estado = pObj.Endereco.Estado,
                Cep = pObj.Endereco.Cep
            }).ToList();

            return View(listaViewModel);
        }

        public ActionResult Cadastro(int? pIntId)
        {
            var viewModel = new AdvogadoViewModel();

            if (pIntId.HasValue && pIntId.Value > 0)
            {
                var advogado = _advogadoRepositorio.ObterAdvogado(pIntId.Value);

                if (advogado != null)
                {
                    viewModel.Id = advogado.Id;
                    viewModel.Nome = advogado.Nome;
                    viewModel.Senioridade = advogado.Senioridade;
                    viewModel.Logradouro = advogado.Endereco.Logradouro;
                    viewModel.Bairro = advogado.Endereco.Bairro;
                    viewModel.Estado = advogado.Endereco.Estado;
                    viewModel.Cep = advogado.Endereco.Cep;
                    viewModel.Numero = advogado.Endereco.Numero;
                    viewModel.Complemento = advogado.Endereco.Complemento;
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar(AdvogadoViewModel pObjAdvogado)
        {
            if (!ModelState.IsValid)
            {
                return View("Cadastro", pObjAdvogado);
            }

            var advogado = new Advogado
            {
                Id = pObjAdvogado.Id,
                Nome = pObjAdvogado.Nome,
                Senioridade = pObjAdvogado.Senioridade,
                Endereco = new Endereco
                {
                    Logradouro = pObjAdvogado.Logradouro,
                    Bairro = pObjAdvogado.Bairro,
                    Estado = pObjAdvogado.Estado,
                    Cep = pObjAdvogado.Cep,
                    Numero = pObjAdvogado.Numero,
                    Complemento = pObjAdvogado.Complemento
                }
            };

            if (advogado.Id > 0)
                _advogadoRepositorio.AtualizarAdvogado(advogado);
            else
                _advogadoRepositorio.IncluirAdvogado(advogado);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Excluir(int pIntId)
        {
            try
            {
                _advogadoRepositorio.ExcluirAdvogado(pIntId);
                return Json(new { sucesso = true });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, erro = ex.Message });
            }
        }
    }
}