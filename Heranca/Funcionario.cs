namespace Heranca
{
    
public abstract class Funcionario
    {
        public string? Nome {get; set;}
        protected decimal Salario;

    public void Trabalhar ()
        {
            Console.WriteLine("Funcionário está executando Atividades.");
        }

            public Funcionario()
                    {
                        
                    }
                    
            public Funcionario(string nome, decimal salario)
                    {
                        Nome = nome;
                        Salario = salario;
                    }

        public abstract decimal CalcularBonus();


   }
   }