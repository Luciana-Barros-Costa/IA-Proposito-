using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Requisicao> requisicoes = new();
    static List<Requisicao> uteis = new();
    static Random random = new();

    static void Main()
    {
        Console.WriteLine("🤖 IA Propósito (Simulação Console)");
        Console.WriteLine("Digite 'sair' para encerrar.\n");

        while (true)
        {
            Console.Write("Pergunte algo: ");
            string pergunta = Console.ReadLine()?.Trim();

            if (pergunta?.ToLower() == "sair") break;
            if (string.IsNullOrEmpty(pergunta))
            {
                Console.WriteLine("⚠️ Digite uma pergunta válida!\n");
                continue;
            }

            var resposta = GerarResposta();
            Console.WriteLine($"\nIA: {resposta}\n");

            Console.Write("Essa resposta foi útil? (s/n): ");
            bool util = Console.ReadLine()?.ToLower() == "s";

            var registro = new Requisicao
            {
                Pergunta = pergunta,
                Resposta = resposta,
                Util = util,
                Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            };

            requisicoes.Add(registro);
            if (util) uteis.Add(registro);

            SalvarCSV("requisicoes.csv", requisicoes);
            SalvarCSV("uteis.csv", uteis);

            Console.WriteLine("💾 Feedback registrado com sucesso!\n");
        }
    }

    static string GerarResposta()
    {
        int id = random.Next(1, 5001);
        return $"Resposta gerada automaticamente para a pergunta número {id}.";
    }

    static void SalvarCSV(string nomeArquivo, List<Requisicao> dados)
    {
        using (StreamWriter sw = new(nomeArquivo))
        {
            sw.WriteLine("Pergunta,Resposta,Útil,Data");
            foreach (var d in dados)
            {
                sw.WriteLine($"\"{d.Pergunta.Replace("\"", "\"\"")}\",\"{d.Resposta.Replace("\"", "\"\"")}\",{d.Util},{d.Data}");
            }
        }
    }

    class Requisicao
    {
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public bool Util { get; set; }
        public string Data { get; set; }
    }
}