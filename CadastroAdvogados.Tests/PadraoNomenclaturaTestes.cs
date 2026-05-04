using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FluentAssertions.Execution; // Adicionado para permitir o bloco customizado de asserção com mensagens detalhadas

namespace CadastroAdvogados.Tests
{
    /// <summary>
    /// ARCHITECTURE UNIT TEST: 
    /// PADRÃO DE NOMENCLATURA (NOTAÇÃO HÚNGARA)
    /// ---------------------------------------------------------------------------
    /// OBJETIVO: Automatizar a validação das convenções de nomenclatura de parâmetros,
    /// eliminando a necessidade de revisão manual.
    /// 
    /// DIRETIZES MICROSOFIT X PADRÃO PROJETO: 
    /// Embora as diretrizes modernas da Microsoft (C# Coding Conventions) 
    /// desaconselhem o uso de Notação Húngara, este projeto adota o padrão.
    /// https://learn.microsoft.com/pt-br/dotnet/standard/design-guidelines/general-naming-conventions
    /// Este teste garante a integridade do padrão em todas as camadas 
    /// (Dominio, Repositorio e Web) via Reflection.
    /// 
    /// FUNCIONAMENTO:
    /// 1. Varrê os assemblies da solução em busca de métodos públicos.
    /// 2. Ignora 'overrides' de frameworks (ex: ASP.NET MVC) para evitar falsos positivos.
    /// 3. Valida prefixos baseados no tipo: pStr (String), pInt (Integer), pBln (Boolean), 
    ///    pDec (Decimal), pDat (DateTime) e pObj (Classes/Objetos).
    /// 
    /// MANUTENÇÃO: 
    /// Caso novos tipos sejam adicionados ou classes de terceiros precisem 
    /// ser ignoradas, deve-se ajustar o método 'ObterPrefixoPorTipo' ou o filtro LINQ de busca.
    /// </summary>

    [TestClass]
    public class ArquiteturaNamingTestes
    {
        [TestMethod]
        public void Parametros_Em_Toda_A_Solucao_Devem_Usar_Notacao_Hungara()
        {
            // 1. Lista de Assemblies para varrer
            var nomesAssemblies = new[] { "Dominio", "Repositorio", "Web" };

            var assembliesParaValidar = new List<Assembly>();
            foreach (var nome in nomesAssemblies)
            {
                try
                {
                    // Carrega os projetos referenciados na solução
                    assembliesParaValidar.Add(Assembly.Load(nome));
                }
                catch
                {
                    /* Ignorar falhas de carregamento pontuais */
                }
            }

            // 2. Coleta métodos de classes públicas
            var metodosParaValidar = assembliesParaValidar
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsPublic && !t.IsInterface)
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                .Where(m => !m.IsSpecialName)
                // Ignora métodos que são overrides (sobrescritas de frameworks)
                .Where(m => m.GetBaseDefinition().DeclaringType == m.DeclaringType);

            // 3. Execução validação
            foreach (var metodo in metodosParaValidar)
            {
                foreach (var param in metodo.GetParameters())
                {
                    string prefixoEsperado = ObterPrefixoPorTipo(param.ParameterType);

                    if (!string.IsNullOrEmpty(prefixoEsperado))
                    {
                        bool respeitaNotacao = param.Name.StartsWith(prefixoEsperado);

                        // Asserção DEVE FICAR COM RECUO ASSIM para correta formatação no Test Explorer
                        Execute.Assertion
                            .ForCondition(respeitaNotacao)
                            .FailWith($@"
[FALHA DE PADRÃO DE NOMENCLATURA]
---------------------------------------------------
-> Projeto:  {metodo.DeclaringType.Assembly.GetName().Name}
-> Classe:   {metodo.DeclaringType.Name}
-> Método:   {metodo.Name}
-> Variável: '{param.Name}'
---------------------------------------------------
Valor Esperado: Começar com '{prefixoEsperado}'
Valor Atual:    '{param.Name}'
Regra Quebrada: Como o tipo é {param.ParameterType.Name}, a Notação Húngara exige o prefixo '{prefixoEsperado}'.");
                    }
                }
            }
        }

        private string ObterPrefixoPorTipo(Type tipo)
        {
            // Mapeamento de tipos para as nomenclaturas em Notação Húngara
            if (tipo == typeof(string)) return "pStr";
            if (tipo == typeof(int) || tipo == typeof(int?)) return "pInt";
            if (tipo == typeof(bool) || tipo == typeof(bool?)) return "pBln";
            if (tipo == typeof(decimal) || tipo == typeof(decimal?)) return "pDec";
            if (tipo == typeof(DateTime) || tipo == typeof(DateTime?)) return "pDat";
            if (tipo.IsClass && tipo != typeof(string)) return "pObj";

            return null;
        }
    }
}