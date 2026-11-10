using System.Globalization;
using Heranca;

Gerente gerente = new Gerente();
gerente.Nome = "Carlos";
gerente.Codigo = 123;
gerente.Trabalhar();
gerente.GerenciarEquipe();


Gerente gerente2 = new Gerente
{
    Nome ="Marcos",
   
};
gerente2.DefinirSalario(2000m);
gerente2.ExibirSalario();


Funcionario f = new Gerente(); 
/* Crie um objeto do tipo gerente e permita que a varivel f,
 declarada como funcionario, referêncie esse objeto*/


IPagamento p1 = new PagamentoPix();
IPagamento p2 = new PagamentoCartao();

p1.Pagar(150m);
p2.Pagar(20m);