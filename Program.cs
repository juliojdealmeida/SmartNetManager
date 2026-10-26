RedeService rede = new RedeService();

int opcao;

do
{
    Console.WriteLine("\n===== SMARTNET MANAGER =====");
    Console.WriteLine("1 - Reservar IP");
    Console.WriteLine("2 - Adicionar dispositivo à fila");
    Console.WriteLine("3 - Processar fila");
    Console.WriteLine("4 - Dashboard");
    Console.WriteLine("5 - Relatório fila");
    Console.WriteLine("6 - Relatório ativos");
    Console.WriteLine("7 - Firewall Apple");
    Console.WriteLine("8 - Firewall Alto Consumo");
    Console.WriteLine("9 - Desfazer ação");
    Console.WriteLine("0 - Sair");

    Console.Write("\nOpção: ");
    opcao = int.Parse(Console.ReadLine());

    switch (opcao)
    {
        case 1:

            Console.Write("MAC Address: ");
            string mac = Console.ReadLine();

            Console.Write("IP: ");
            string ip = Console.ReadLine();

            rede.ReservarIp(mac, ip);

            break;

        case 2:

            Dispositivo dispositivo = new Dispositivo();

            Console.Write("Nome: ");
            dispositivo.Nome = Console.ReadLine();

            Console.Write("MAC Address: ");
            dispositivo.MacAddress = Console.ReadLine();

            Console.Write("IP: ");
            dispositivo.EnderecoIP = Console.ReadLine();

            Console.Write("Fabricante: ");
            dispositivo.Fabricante = Console.ReadLine();

            Console.Write("Consumo Banda (Mbps): ");
            dispositivo.ConsumoBanda =
                double.Parse(Console.ReadLine());

            dispositivo.DataConexao = DateTime.Now;

            rede.AdicionarNaFila(dispositivo);

            break;

        case 3:

            rede.ProcessarFila();

            break;

        case 4:

            rede.ExibirDashboard();

            break;

        case 5:

            rede.ImprimirRelatorio(
                rede.ObterFilaAutenticacao());

            break;

        case 6:

            rede.ImprimirRelatorio(
                rede.ObterDispositivosAtivos());

            break;

        case 7:

            rede.AplicarFirewall(
                d => d.Fabricante == "Apple");

            break;

        case 8:

            rede.AplicarFirewall(
                d => d.ConsumoBanda > 100);

            break;

        case 9:

            rede.DesfazerUltimaAcao();

            break;

        case 0:

            Console.WriteLine("Encerrando sistema...");
            break;

        default:

            Console.WriteLine("Opção inválida.");
            break;
    }

} while (opcao != 0);