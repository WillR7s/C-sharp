using System;
using System.Collections.Generic;

namespace EFCoreTutorial.Models;

public partial class Desenvolvedora
{
    public int IdDesenvolvedora { get; set; }

    public string Nome { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public virtual ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
}
