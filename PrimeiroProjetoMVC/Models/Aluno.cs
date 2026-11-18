using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiroProjetoMVC.Models
{
    public class Aluno
    {
        public int Id {get;set;}

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(50,MinimumLength = 3 , ErrorMessage = "Nome deve ter entre 2 e 50 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email { get; set; } = "";

        [Range(16,100,ErrorMessage ="Idade Deve ser entre 16 e 100")]
        public int Idade { get; set; }
    }
}