using System;
using System.Collections.Generic;
using System.Linq;

public class RedeService
{
    // MISSÃO 1 - DHCP
    private Dictionary<string, string> reservasIp =
        new Dictionary<string, string>();

    // MISSÃO 2 - FILA DE AUTENTICAÇÃO
    private Queue<Dispositivo> filaAutenticacao =
        new Queue<Dispositivo>();

    // MISSÃO 3 - DISPOSITIVOS ATIVOS
    private List<Dispositivo> dispositivosAtivos =
        new List<Dispositivo>();

    // MISSÃO 4 - HISTÓRICO
    private Stack<string> historico =
        new Stack<string>();


    // =========================
    // MISSÃO 1
    // =========================

    public void ReservarIp(string mac, string ip)
    {
        if (reservasIp.ContainsKey(mac))
        {
            Console.WriteLine("MAC já cadastrado.");
            return;
        }

        reservasIp.Add(mac, ip);

        Console.WriteLine("IP reservado com sucesso.");
    }


    // =========================
    // MISSÃO 2
    // =========================

    public void AdicionarNaFila(Dispositivo dispositivo)
    {
        filaAutenticacao.Enqueue(dispositivo);

        Console.WriteLine(
            $"{dispositivo.Nome} adicionado à fila.");
    }

    public void ProcessarFila()
    {
        if (filaAutenticacao.Count == 0)
        {
            Console.WriteLine("Fila vazia.");
            return;
        }

        var dispositivo = filaAutenticacao.Dequeue();

        dispositivosAtivos.Add(dispositivo);

        historico.Push(
            $"Autenticado: {dispositivo.Nome}");

        Console.WriteLine(
            $"{dispositivo.Nome} autenticado com sucesso.");
    }


    // =========================
    // MISSÃO 3
    // DASHBOARD
    // =========================

    public void ExibirDashboard()
    {
        Console.WriteLine("\n===== DASHBOARD =====");

        if (dispositivosAtivos.Count == 0)
        {
            Console.WriteLine(
                "Nenhum dispositivo ativo.");
            return;
        }

        var consumoTotal =
            dispositivosAtivos
                .Sum(d => d.ConsumoBanda);

        Console.WriteLine(
            $"Consumo total: {consumoTotal} Mbps");

        Console.WriteLine("\nTOP 3 CONSUMO");

        var top3 =
            dispositivosAtivos
                .OrderByDescending(d => d.ConsumoBanda)
                .Take(3);

        foreach (var dispositivo in top3)
        {
            Console.WriteLine(
                $"{dispositivo.Nome} - {dispositivo.ConsumoBanda} Mbps");
        }

        Console.WriteLine("\nFABRICANTES");

        var fabricantes =
            dispositivosAtivos
                .GroupBy(d => d.Fabricante);

        foreach (var grupo in fabricantes)
        {
            Console.WriteLine(
                $"{grupo.Key}: {grupo.Count()} dispositivo(s)");
        }

        bool anomalia =
            dispositivosAtivos
                .Any(d => d.ConsumoBanda > 500);

        if (anomalia)
        {
            Console.WriteLine(
                "\n⚠ ALERTA: Consumo superior a 500 Mbps");
        }
    }


    // =========================
    // MISSÃO 4
    // =========================

    public void DesfazerUltimaAcao()
    {
        if (historico.Count == 0)
        {
            Console.WriteLine(
                "Nenhuma ação para desfazer.");
            return;
        }

        string ultimaAcao =
            historico.Pop();

        Console.WriteLine(
            $"Ação revertida: {ultimaAcao}");
    }


    // =========================
    // MISSÃO 5
    // =========================

    public void ImprimirRelatorio(
        IEnumerable<Dispositivo> colecao)
    {
        Console.WriteLine("\n===== RELATÓRIO =====");

        foreach (var dispositivo in colecao)
        {
            Console.WriteLine(
                $"Nome: {dispositivo.Nome}");
            Console.WriteLine(
                $"IP: {dispositivo.EnderecoIP}");
            Console.WriteLine(
                $"MAC: {dispositivo.MacAddress}");
            Console.WriteLine(
                $"Fabricante: {dispositivo.Fabricante}");
            Console.WriteLine(
                $"Consumo: {dispositivo.ConsumoBanda} Mbps");
            Console.WriteLine("--------------------------------");
        }
    }


    // =========================
    // MISSÃO 6
    // =========================

    public void AplicarFirewall(
        Func<Dispositivo, bool> regraBloqueio)
    {
        var bloqueados =
            dispositivosAtivos
                .Where(regraBloqueio)
                .ToList();

        foreach (var dispositivo in bloqueados)
        {
            dispositivosAtivos.Remove(dispositivo);
        }

        historico.Push(
            $"Firewall aplicou bloqueio em {bloqueados.Count} dispositivo(s)");

        Console.WriteLine(
            $"{bloqueados.Count} dispositivo(s) bloqueado(s).");
    }


    // =========================
    // MÉTODOS AUXILIARES
    // =========================

    public List<Dispositivo> ObterDispositivosAtivos()
    {
        return dispositivosAtivos;
    }

    public Queue<Dispositivo> ObterFilaAutenticacao()
    {
        return filaAutenticacao;
    }
}