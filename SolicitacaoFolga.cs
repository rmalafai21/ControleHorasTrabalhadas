using System;
using ControleHorasTrabalhadas.Enums;

namespace ControleHorasTrabalhadas.Models
{
    public class SolicitacaoFolga
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public StatusSolicitacao Status { get; set; } // Pendente, Aprovada, Rejeitada
        public string Observacoes { get; set; }

        public SolicitacaoFolga(DateTime dataInicio, DateTime dataFim, string observacoes = "")
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            Status = StatusSolicitacao.Pendente; // Começa como pendente
            Observacoes = observacoes;
        }
    }
}
