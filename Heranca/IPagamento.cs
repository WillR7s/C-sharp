

namespace Heranca
{
    public interface IPagamento // uma interface define um contrato que 
   // estabelece quais metodos, prioridades ou ouris membros uma classe deve 
   // disponibilizar
    {
        void Pagar(decimal valor);
       // void Receber(decimal valor); // interfaces pdem conter diversos metodos
        
    }
}