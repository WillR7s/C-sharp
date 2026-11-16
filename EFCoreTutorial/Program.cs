using EFCoreTutorial.Data;
using EFCoreTutorial.Models;
using Microsoft.EntityFrameworkCore;


string connectionString =
    "Server=DESKTOP-23K6RNU\\MSSQLSERVER2;Database=DB_GestaoJogos;Trusted_Connection=True;TrustServerCertificate=True;";

var options = new DbContextOptionsBuilder<GestaoJogosContext>()
    .UseSqlServer(connectionString)
    .Options;

using var context = new GestaoJogosContext(options);

var listaJogos = context.Jogos.ToList();

Console.WriteLine($"{"Nome",-45} {"Preço",5} {"Gênero",15}");
Console.WriteLine(new string('-', 70));

foreach (var jogo in listaJogos)
{
    Console.WriteLine($"{jogo.Nome,-45} {jogo.Preco,5:C2} {jogo.Genero,15}");
}