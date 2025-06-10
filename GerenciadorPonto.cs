using System;
using ControleHorasTrabalhadas.Models;
using ControleHorasTrabalhadas.Enums;

namespace ControleHorasTrabalhadas.Services
{
    public class GerenciadorPonto
    {
        public void RegistrarPonto(Funcionario funcionario, TipoRegistroPonto tipo)
        {
            if (funcionario == null)
            {
                Console.WriteLine("Erro: Funcionário não encontrado.");
                return;
            }
            funcionario.RegistrosPonto.Add(new RegistroPonto(DateTime.Now, tipo));
            Console.WriteLine($"Ponto de {tipo} registrado para {funcionario.Nome} em {DateTime.Now}.");
        }
    }
}
