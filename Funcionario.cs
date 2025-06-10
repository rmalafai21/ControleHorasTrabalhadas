using System;
using System.Collections.Generic;

namespace ControleHorasTrabalhadas.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public decimal SalarioHora { get; set; }
        public List<RegistroPonto> RegistrosPonto { get; set; } = new List<RegistroPonto>();
        public List<SolicitacaoFolga> SolicitacoesFolga { get; set; } = new List<SolicitacaoFolga>();

        public Funcionario(int id, string nome, string cargo, decimal salarioHora)
        {
            Id = id;
            Nome = nome;
            Cargo = cargo;
            SalarioHora = salarioHora;
        }

        public decimal CalcularHorasTrabalhadasMes(int mes, int ano)
        {
            decimal totalHoras = 0;
            foreach (var registro in RegistrosPonto)
            {
                if (registro.DataHora.Month == mes && registro.DataHora.Year == ano)
                {
                    if (registro.Tipo == Enums.TipoRegistroPonto.Entrada)
                    {
                        var saida = RegistrosPonto.FirstOrDefault(r =>
                            r.DataHora.Date == registro.DataHora.Date &&
                            r.Tipo == Enums.TipoRegistroPonto.Saida &&
                            r.DataHora > registro.DataHora);
                        if (saida != null)
                        {
                            totalHoras += (decimal)(saida.DataHora - registro.DataHora).TotalHours;
                        }
                    }
                }
            }
            return totalHoras;
        }
    }
}

