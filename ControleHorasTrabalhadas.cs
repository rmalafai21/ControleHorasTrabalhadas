using System;
using System.Linq;
using ControleHorasTrabalhadas.Models;
using ControleHorasTrabalhadas.Services;
using ControleHorasTrabalhadas.Enums;

namespace ControleHorasTrabalhadas
{
    class Program
    {
        static GerenciadorFuncionario _gerenciadorFuncionario = new GerenciadorFuncionario();
        static GerenciadorPonto _gerenciadorPonto = new GerenciadorPonto();
        static GerenciadorFolga _gerenciadorFolga = new GerenciadorFolga();

        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Sistema de Controle de Horas e Folgas!");
            ExibirMenuPrincipal();
        }

        static void ExibirMenuPrincipal()
        {
            while (true)
            {
                Console.WriteLine("\n--- Menu Principal ---");
                Console.WriteLine("1. Gerenciar Funcionários");
                Console.WriteLine("2. Registrar Ponto");
                Console.WriteLine("3. Solicitar/Gerenciar Folgas");
                Console.WriteLine("4. Relatórios");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuFuncionario();
                        break;
                    case "2":
                        MenuRegistroPonto();
                        break;
                    case "3":
                        MenuFolgas();
                        break;
                    case "4":
                        MenuRelatorios();
                        break;
                    case "0":
                        Console.WriteLine("Saindo do sistema. Até mais!");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void MenuFuncionario()
        {
            while (true)
            {
                Console.WriteLine("\n--- Gerenciar Funcionários ---");
                Console.WriteLine("1. Adicionar Funcionário");
                Console.WriteLine("2. Listar Funcionários");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();
                        Console.Write("Cargo: ");
                        string cargo = Console.ReadLine();
                        Console.Write("Salário por Hora: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal salarioHora))
                        {
                            _gerenciadorFuncionario.AdicionarFuncionario(nome, cargo, salarioHora);
                        }
                        else
                        {
                            Console.WriteLine("Salário inválido.");
                        }
                        break;
                    case "2":
                        var funcionarios = _gerenciadorFuncionario.ListarFuncionarios();
                        if (funcionarios.Any())
                        {
                            Console.WriteLine("\n--- Lista de Funcionários ---");
                            foreach (var f in funcionarios)
                            {
                                Console.WriteLine($"ID: {f.Id}, Nome: {f.Nome}, Cargo: {f.Cargo}, Salário/Hora: {f.SalarioHora:C}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum funcionário cadastrado.");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void MenuRegistroPonto()
        {
            Console.WriteLine("\n--- Registrar Ponto ---");
            Console.Write("Digite o ID do funcionário: ");
            if (int.TryParse(Console.ReadLine(), out int funcionarioId))
            {
                var funcionario = _gerenciadorFuncionario.BuscarFuncionarioPorId(funcionarioId);
                if (funcionario != null)
                {
                    Console.WriteLine("1. Entrada");
                    Console.WriteLine("2. Saída");
                    Console.Write("Tipo de registro: ");
                    string tipoOpcao = Console.ReadLine();

                    if (tipoOpcao == "1")
                    {
                        _gerenciadorPonto.RegistrarPonto(funcionario, TipoRegistroPonto.Entrada);
                    }
                    else if (tipoOpcao == "2")
                    {
                        _gerenciadorPonto.RegistrarPonto(funcionario, TipoRegistroPonto.Saida);
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("Funcionário não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        static void MenuFolgas()
        {
            while (true)
            {
                Console.WriteLine("\n--- Gerenciar Folgas ---");
                Console.WriteLine("1. Solicitar Folga");
                Console.WriteLine("2. Aprovar/Rejeitar Folgas Pendentes");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Digite o ID do funcionário: ");
                        if (int.TryParse(Console.ReadLine(), out int funcionarioIdSolicitacao))
                        {
                            var funcionario = _gerenciadorFuncionario.BuscarFuncionarioPorId(funcionarioIdSolicitacao);
                            if (funcionario != null)
                            {
                                Console.Write("Data de Início (dd/mm/aaaa): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime dataInicio))
                                {
                                    Console.Write("Data de Fim (dd/mm/aaaa): ");
                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dataFim))
                                    {
                                        Console.Write("Observações (opcional): ");
                                        string obs = Console.ReadLine();
                                        _gerenciadorFolga.SolicitarFolga(funcionario, dataInicio, dataFim, obs);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Data de Fim inválida.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Data de Início inválida.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Funcionário não encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    case "2":
                        GerenciarFolgasPendentes();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void GerenciarFolgasPendentes()
        {
            Console.WriteLine("\n--- Folgas Pendentes ---");
            bool encontrouPendente = false;
            foreach (var func in _gerenciadorFuncionario.ListarFuncionarios())
            {
                foreach (var solicitacao in func.SolicitacoesFolga.Where(s => s.Status == StatusSolicitacao.Pendente))
                {
                    encontrouPendente = true;
                    Console.WriteLine($"\nFuncionário: {func.Nome}");
                    Console.WriteLine($"  ID da Folga: {func.SolicitacoesFolga.IndexOf(solicitacao)}"); // Não é o ideal, mas funciona para console
                    Console.WriteLine($"  Período: {solicitacao.DataInicio.ToShortDateString()} - {solicitacao.DataFim.ToShortDateString()}");
                    Console.WriteLine($"  Observações: {solicitacao.Observacoes}");
                    Console.Write("  Aprovar (A) ou Rejeitar (R)? ");
                    string decisao = Console.ReadLine().ToUpper();

                    if (decisao == "A")
                    {
                        _gerenciadorFolga.AprovarFolga(solicitacao);
                    }
                    else if (decisao == "R")
                    {
                        Console.Write("  Motivo da Rejeição: ");
                        string motivo = Console.ReadLine();
                        _gerenciadorFolga.RejeitarFolga(solicitacao, motivo);
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida.");
                    }
                }
            }
            if (!encontrouPendente)
            {
                Console.WriteLine("Nenhuma solicitação de folga pendente.");
            }
        }

        static void MenuRelatorios()
        {
            Console.WriteLine("\n--- Relatórios ---");
            Console.Write("Digite o ID do funcionário para o relatório: ");
            if (int.TryParse(Console.ReadLine(), out int funcionarioIdRelatorio))
            {
                var funcionario = _gerenciadorFuncionario.BuscarFuncionarioPorId(funcionarioIdRelatorio);
                if (funcionario != null)
                {
                    Console.Write("Mês (1-12): ");
                    if (int.TryParse(Console.ReadLine(), out int mes))
                    {
                        Console.Write("Ano: ");
                        if (int.TryParse(Console.ReadLine(), out int ano))
                        {
                            decimal horasTrabalhadas = funcionario.CalcularHorasTrabalhadasMes(mes, ano);
                            Console.WriteLine($"\n--- Relatório para {funcionario.Nome} - {mes}/{ano} ---");
                            Console.WriteLine($"Horas Trabalhadas: {horasTrabalhadas:F2} horas");
                            Console.WriteLine($"Salário Estimado: {horasTrabalhadas * funcionario.SalarioHora:C}");

                            Console.WriteLine("\nRegistros de Ponto:");
                            foreach (var registro in funcionario.RegistrosPonto.Where(r => r.DataHora.Month == mes && r.DataHora.Year == ano).OrderBy(r => r.DataHora))
                            {
                                Console.WriteLine($"- {registro.DataHora}: {registro.Tipo}");
                            }

                            Console.WriteLine("\nSolicitações de Folga:");
                            foreach (var solicitacao in funcionario.SolicitacoesFolga.Where(s => s.DataInicio.Month == mes || s.DataFim.Month == mes).OrderBy(s => s.DataInicio))
                            {
                                Console.WriteLine($"- Período: {solicitacao.DataInicio.ToShortDateString()} a {solicitacao.DataFim.ToShortDateString()} | Status: {solicitacao.Status} | Obs: {solicitacao.Observacoes}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ano inválido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Mês inválido.");
                    }
                }
                else
                {
                    Console.WriteLine("Funcionário não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }
    }
}
