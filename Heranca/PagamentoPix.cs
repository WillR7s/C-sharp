
namespace Heranca
{
    public class PagamentoPix : IPagamento
    {
        public void Pagar(decimal valor)
        {
        //*     throw new NotImplementedException();   classe nativa do c# enquanto a definição do metodo não é criada pelo desenvolvedor
       
       Console.WriteLine($"Pagamento Via Pix: {valor:C}");
       
        }
    }
}