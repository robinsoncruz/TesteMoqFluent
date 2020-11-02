using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoConsoleVendas
{
    public class AnaliseCredito
    {
        private readonly IConsultaSerasa _consultaserasa;

        public AnaliseCredito(IConsultaSerasa consulta)
        {
            _consultaserasa = consulta;
        }

        public StatusCredito ConsultarCPF(string cpf)
        {
            var pendencias = _consultaserasa.ConsultarPendenciasFinanceiras(cpf);

            if (pendencias == null)
                return StatusCredito.ParametroInvalido;
            else if (pendencias.Count == 0)
                return StatusCredito.SemPendencias;
            else
                return StatusCredito.Inadimplente;

        }





    }
}
