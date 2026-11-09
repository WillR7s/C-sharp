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
    salario= "3000m"
};



Funcionario f = new Gerente(); 
/* Crie um objeto do tipo gerente e permita que a varivel f,
 declarada como funcionario, referêncie esse objeto*/
