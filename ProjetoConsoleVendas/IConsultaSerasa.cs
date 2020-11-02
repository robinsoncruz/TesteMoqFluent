using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoConsoleVendas
{
    public interface IConsultaSerasa :IConsulta 
    {
        IList<Restricao> ConsultarPendenciasFinanceiras(string cpf);

    }
}
