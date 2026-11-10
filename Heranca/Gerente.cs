    namespace Heranca
    {
        public class Gerente : Funcionario
        {
            public int Codigo {get; set;}



    public void GerenciarEquipe ()
            {
                Console.WriteLine("O gestor está gerenciando a equipe.");
            }

            public Gerente()
        {
            
        }

            public Gerente(string nome, decimal salario) :base(nome, salario)
            {
                
            }


            public void ExibirSalario()
        {
            Console.WriteLine($"Salário: {Salario:C}");  // Valor moeda
        }

        public void DefinirSalario
        (decimal Valorsalario)
        {
            Salario = Valorsalario;
        }

        public override decimal CalcularBonus()
        {
            return 1000m;
        }
    }
    }