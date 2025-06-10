using System.Collections.Generic;
using System.Linq;
using ControleHorasTrabalhadas.Models;

namespace ControleHorasTrabalhadas.Services
{
    public class GerenciadorFuncionario
    {
        private List<Funcionario> _funcionarios = new List<Funcionario>();
        private int _nextId = 1;

        public void AdicionarFuncionario(string nome, string cargo, decimal salarioHora)
        {
            _funcionarios.Add(new Funcionario(_nextId++, nome, cargo, salarioHora));
            Console.WriteLine($"Funcionário '{nome}' adicionado com sucesso!");
        }

        public Funcionario BuscarFuncionarioPorId(int id)
        {
            return _funcionarios.FirstOrDefault(f => f.Id == id);
        }

        public List<Funcionario> ListarFuncionarios()
        {
            return _funcionarios;
        }
    }
}
