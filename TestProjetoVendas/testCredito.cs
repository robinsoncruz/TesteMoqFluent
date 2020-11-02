using System;
using ProjetoConsoleVendas;
using Moq;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace TestProjetoVendas
{
    public class TestCredito
    {
        private Mock<IConsultaSerasa> mock;
        private const string cpf_invalido = "452A";
        private const string cpf_erro_comunicacao = "76217486300";
        private const string cpf_sem_pendencias = "60487583750";
        private const string cpf_inadimplente = "60487583750";

        public TestCredito()
        {

            mock = new Mock<IConsultaSerasa>(MockBehavior.Strict);
            mock.Setup(s => s.ConsultarPendenciasFinanceiras(cpf_invalido))
                .Returns(() => null);
            mock.Setup(s => s.ConsultarPendenciasFinanceiras(cpf_erro_comunicacao))
                .Throws(new Exception("Erro de Comunicação"));
            mock.Setup(s => s.ConsultarPendenciasFinanceiras(cpf_sem_pendencias))
                .Returns(() => new List<Restricao>());

            Restricao restricao = new Restricao();
            restricao.CodigoEmpresa = 1010;
            restricao.Cpf = cpf_inadimplente;
            restricao.ValorPendencia = 25050d;
            
            List<Restricao> listarestricoes = new List<Restricao>();
            listarestricoes.Add(restricao);

            mock.Setup(s => s.ConsultarPendenciasFinanceiras(cpf_inadimplente))
                .Returns(() => listarestricoes);
        }

        private StatusCredito ObterStatusCredito(string cpf)
        {
            AnaliseCredito analise = new AnaliseCredito(mock.Object);
            return analise.ConsultarCPF(cpf);
        }

        [Fact]
        public void TestarCPFInvalidoSemFluent()
        {
            StatusCredito status = ObterStatusCredito(cpf_invalido);
            Assert.Equal(StatusCredito.ParametroInvalido, status);
        }

        [Fact]
        public void TestarCPFInvalido()
        {
            StatusCredito status = ObterStatusCredito(cpf_invalido);
            status.Should().Be(StatusCredito.ParametroInvalido,
            "Resultado incorreto para um CPF sem pendências");
        }
    }
}


