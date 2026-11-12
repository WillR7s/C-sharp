using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DadosComCSV.Models
{
    //Representa os dados de um jogo que serão salvos no arquivo .CSV
    public class Jogo
    {
        public int Id{ get; set; }
        public string? Nome { get; set; } = string.Empty;
        public string? Desenvolvedora { get; set; } = string.Empty;
        public string? Genero { get; set; } = string.Empty;
        public int AnoLancamento { get; set; } 
        public decimal Preco{ get; set; } 
    
    }
}

