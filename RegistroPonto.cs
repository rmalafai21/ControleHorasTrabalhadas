using System;
using ControleHorasTrabalhadas.Enums;

namespace ControleHorasTrabalhadas.Models
{
    public class RegistroPonto
    {
        public DateTime DataHora { get; set; }
        public TipoRegistroPonto Tipo { get; set; } // Entrada, Saída

        public RegistroPonto(DateTime dataHora, TipoRegistroPonto tipo)
        {
            DataHora = dataHora;
            Tipo = tipo;
        }
    }
}
