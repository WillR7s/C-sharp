using DadosComCSV.Repositories;
using DadosComCSV.Services;

var repository =new JogoRepository();
var Services = new JogoService(repository);

bool executando =true;

while (executando)
{
    
    Console.WriteLine("========== GESTÃO DE JOGOS ===========");
    Console.WriteLine("""
    1 - CADASTRAR JOGO
    2 - LISTAR JOGOS 
    0 - SAIR
    """);
Console.WriteLine("Escolha uma opção:");
string? opcao= Console.ReadLine();

try
    {
        switch (opcao)
        {
            case "1":
            //Cadastrar();
            break;

            case "2":
            var listaJogos=Services.Listar();

            foreach(var item in listaJogos)
                {
                    Console.WriteLine($"Nome: {item.Nome} - Preço: {item.Preco:C}");
                }
            break;

            case "0":
            executando = false;
            break;
        default: 
        Console.WriteLine("Opção Inválida");
        break;
        }
    }

    catch(ArgumentException ex)
    {
         Console.WriteLine($"\nERRO: {ex.Message}");
        throw;
    }
}