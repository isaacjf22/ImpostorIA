using DotNetEnv; //para usar os comandos do env 
using Google.GenAI; //para usar os comandos do GoogleAi 
using Google.GenAI.Types;
using System;
using System.Threading; //para usar sleep 
 //dependencia para poder utilizar as funções para API do Gemini 

namespace ImpostorIA
{
    public class Program
    {
        //outra forma de colocar o client e apikey 
        
        
        
        private static Client client=null!;   //declarou o cliente 
        public static async Task Main(string[] args)       //é necessario esse tipo de função para utilizar o await
        {
            var root = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName; //forçando para emtrar na pasta do env  
            
            Env.Load(Path.Combine(root, ".env")); //carregando o env dentro da aplicação
            var apiKey = System.Environment.GetEnvironmentVariable("GOOGLE_API_KEY")
                        ?? throw new Exception("GOOGLE_API_KEY não encontrada no .env!"); //pegando a chave da api dentro do .env 
            client = new Client(apiKey:  apiKey); //instanciando o client 
            
            //uma forma de colocar a apikey
            //var apiKey = "apikey"; //cuidado para não vazar essa key 
            //token que da permissão para utlizar a API do Gemini 
            //var client = new Client(apiKey: apiKey);
            //referenciando e instanciando o client responsavel por entregar o comando para a API 
            //ele pega a apikey e faz a autenticação

            bool jogo = true;
            List<string> jogadores = new List<string>();

            while (jogo){
                Console.Clear();
                Console.WriteLine("JOGO DO IMPOSTOR");
                Console.WriteLine();
                Console.WriteLine("1- Começar uma rodada");
                Console.WriteLine("2- Gerenciar jogadores");
                Console.WriteLine("3- Encerrar programa");
                char escolha;

                do
                {
                    escolha = Console.ReadKey(true).KeyChar; //leitura do botao 
                } while (escolha != '1' && escolha != '2' && escolha != '3');

                switch (escolha)
                {
                    case '1':
                        await Rodada(jogadores);
                        break;
                    case '2':
                        jogadores = GerenciarJogador();
                        break;
                    case '3':
                        Console.WriteLine("Obrigado por jogar!");
                        jogo = false;
                        break;
                }
            }
            
        }

        static List<string> GerenciarJogador()
        {       
            Console.Clear();
            List<string> jogadores = new List<string>();
            bool termino = false;
            while (termino==false)
            {
                Console.Clear();
                if (jogadores.Count == 0)
                {
                    Console.WriteLine("Não há nenhum jogador registrado!");
                    Console.WriteLine("Quer registrar um novo jogador?");
                    Console.WriteLine("1-SIM 2-NÃO");
                    char esc; 
                    do
                    {
                        esc = Console.ReadKey(true).KeyChar;
                    }while(esc != '1' && esc != '2');

                    if (esc == '1')
                    {
                        AdicionarJogador(jogadores);
                    }
                    else
                    {
                        termino = true;
                    }
                }
                else
                {
                    Console.WriteLine("GERENCIAMENTO DE JOGADORES");
                    Console.WriteLine();
                    Listar(jogadores);
                    Console.WriteLine();
                    Console.WriteLine("O que deseja realizar?");
                    Console.WriteLine("1-Adicionar Jogador");
                    Console.WriteLine("2-Apagar Jogador");
                    Console.WriteLine("3-Sair do gerenciamento");

                    char escolha;

                    do
                    {
                        escolha = Console.ReadKey(true).KeyChar; //o true faz q nada apreça ao clicar a tecla 
                    } while (escolha!='1' && escolha != '2' && escolha != '3');

                    switch (escolha)
                    {   
                        case '1': 
                            AdicionarJogador(jogadores);
                            break;
                        case '2':
                            ApagarJogador(jogadores);
                            break;
                        case '3':
                            Console.WriteLine("Saindo do gerencimanto...");
                            termino = true;
                            Pausar();
                            break;
                    }
                    


                }
                
                
                
                
                
            }
            
            return jogadores; 
        }


        static void AdicionarJogador(List<string> jogadores) //essas funções vão pegar a lista e vai modificala 
        {
            Console.Write("Nome do jogador à ser adicionado: ");
            string nome = Console.ReadLine();
            jogadores.Add(nome);
            Console.WriteLine("Jogador adicionado com sucesso!");
            Pausar();
        }

        static void ApagarJogador(List<string> jogadores)
        {
            Console.Clear();
            Listar(jogadores);
            Console.WriteLine();
            Console.Write("Nome do jogador à ser removido: ");
            bool removido = false;
            string nome = "";
            while (removido == false) //validação do nome escolhido 
            {
                nome = Console.ReadLine();
                string busca = jogadores.Find(x=> x == nome);
                if (busca == null)
                {
                    Console.WriteLine("Não há jogador com esse nome!");
                }
                else
                {
                    removido = true;
                }
            }

            jogadores.Remove(nome); //a variavel precisa inicializar de qq forma 
        }
        

        static void Pausar()
        {
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.WriteLine();
            Console.ReadKey(true); //esperando o usuario clicar no console
        }

        static void Listar(List<string> jogadores)
        {
            foreach (string nome in jogadores)
            {
                Console.WriteLine($"Nome: {nome}");
            }
        }

        static async Task Rodada(List<string> jogadores)
        {
            List<string> palavrasJa = new List<string>();
            Console.Clear();
            if (jogadores.Count < 3)
            {
                Console.WriteLine("Jogadores insuficientes para jogar!");
                Pausar();
                return;
            }
            Console.WriteLine("IMPOSTOR IA - INICIO DE RODADA");
            Console.WriteLine("Qual será o TEMA dessa jogada?");
            string tema = Console.ReadLine();
            var palavraSorteada = await EscolhendoTema(tema, palavrasJa); //var é a variavel que se adapta 
            //tudo que envolve a IA e sua resposta, tem q usar o await para esperar a resposta dele 
            //await é uma corrente se um metodo la dentro usa , o método mais de fora tbm vai usar


            string impostor = SorteioImpostor(jogadores);
            Console.WriteLine("Palavra e impostor foram sorteados!");
            Pausar();
            foreach (string jogador in jogadores)
            {
                Console.Clear();
                Console.WriteLine($"Jogador: {jogador}");
                Pausar();
                if (jogador == impostor)
                {
                    Console.WriteLine("VOCÊ É O IMPOSTOR!");
                    Console.WriteLine("Boa sorte!");
                }
                else
                {
                    Console.WriteLine($"Palavra sorteada: {palavraSorteada} ");
                }
                Pausar();
            }
            Console.Clear();
            Console.WriteLine("Aperte qualquer tecla para finalizar a rodada...");
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine($"O impostor da rodada era {impostor}");
            Console.WriteLine($"A palavra sorteada foi {palavraSorteada}");
            Pausar();
        }

        static async Task<string> EscolhendoTema(string tema, List<string> palavraJa) //oq é esperado retornar dessa função
        {
            bool achado = false;
            GenerateContentResponse? respostaIA = null; //variavel para receber a resposta da IA, o ? é para dizer que ela pode ser nula

            Console.Write("Carregando");
            for(int i = 0;i<3; i++)
            {
                Console.Write(".");
                await Task.Delay(1000); //animação do carregamento
            }

            do
            {
                respostaIA = await client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash-lite", //modelo da IA
                contents: $"Você é um gerador de palavras de um jogo. O tema é {tema}. Responda com apenas uma palavra ou nome famoso/conhecido relacionado ao tema, sem pontuação e sem explicação, mas não seja tão obvio");

                string busca = palavraJa.Find(x=> x == respostaIA.Text); //verificando se a palavra já foi usada

                

                if( busca == null)
                {
                    achado = true;
                    palavraJa.Add(respostaIA.Text); //adicionando palavra já utilizada na lista            
                }     

                //acessando o modelo(Models) que a API vai comuninar e tbm o comando que vai ser realizado nele
                //await é pra esperar a resposta do gemini 
                //GenerateContentAsync é a geração da resposta da IA 
            }while(achado == false);
            
            Console.WriteLine("OK");
            return respostaIA.Text!;
        }

        static string SorteioImpostor(List<string> jogadores)
        {
            Random numeroSorteio = new Random(); //instanciando o objeto que sorteia 
            int indexSortiado = numeroSorteio.Next(0, jogadores.Count); //fazendo o sorteio do index da lista , (minimo,maximo)
            return jogadores[indexSortiado];
        }
        
        
    }
}

