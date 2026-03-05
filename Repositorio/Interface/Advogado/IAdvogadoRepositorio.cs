using Dominio;
using System.Collections.Generic;

namespace Repositorio.Interface
{
    public interface IAdvogadoRepositorio
    {
        void IncluirAdvogado(Advogado pObjAdvogado);
        void AtualizarAdvogado(Advogado pObjAdvogado);
        void ExcluirAdvogado(int pIntId);
        Advogado ObterAdvogado(int pIntId);
        IEnumerable<Advogado> ListarAdvogados(string pStrBusca = null);
    }
}
