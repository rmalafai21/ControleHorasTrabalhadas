using System;
using ControleHorasTrabalhadas.Models;
using ControleHorasTrabalhadas.Enums;

namespace ControleHorasTrabalhadas.Services
{
    public class GerenciadorFolga
    {
        public void SolicitarFolga(Funcionario funcionario, DateTime dataInicio, DateTime dataFim, string observacoes = "")
        {
            if (funcionario == null)
            {
                Console.WriteLine("Erro: Funcionário não encontrado.");
                return;
            }
            if (dataInicio > dataFim)
            {
                Console.WriteLine("Erro: Data de início não pode ser maior que a data de fim.");
                return;
            }
            funcionario.SolicitacoesFolga.Add(new SolicitacaoFolga(dataInicio, dataFim, observacoes));
            Console.WriteLine($"Solicitação de folga para {funcionario.Nome} de {dataInicio.ToShortDateString()} a {dataFim.ToShortDateString()} enviada. Status: Pendente.");
        }

        public void AprovarFolga(SolicitacaoFolga solicitacao)
        {
            if (solicitacao == null)
            {
                Console.WriteLine("Erro: Solicitação de folga não encontrada.");
                return;
            }
            solicitacao.Status = StatusSolicitacao.Aprovada;
            Console.WriteLine($"Solicitação de folga de {solicitacao.DataInicio.ToShortDateString()} a {solicitacao.DataFim.ToShortDateString()} APROVADA.");
        }

        public void RejeitarFolga(SolicitacaoFolga solicitacao, string motivo)
        {
            if (solicitacao == null)
            {
                Console.WriteLine("Erro: Solicitação de folga não encontrada.");
                return;
            }
            solicitacao.Status = StatusSolicitacao.Rejeitada;
            solicitacao.Observacoes = motivo;
            Console.WriteLine($"Solicitação de folga de {solicitacao.DataInicio.ToShortDateString()} a {solicitacao.DataFim.ToShortDateString()} REJEITADA. Motivo: {motivo}.");
        }
    }
}
