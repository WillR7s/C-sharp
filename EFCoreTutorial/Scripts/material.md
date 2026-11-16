# 1. EF Core e persistência com Database First

## 1.1. Fundamentos do EF Core: ORM e comunicação entre C# e banco de dados

O Entity Framework Core (EF Core) é um ORM (Object-Relational Mapper) que permite trabalhar com bancos de dados relacionais utilizando objetos e classes C#.

Com ele, não é necessário escrever comandos SQL manualmente para todas as operações. O EF Core realiza o mapeamento entre:

- Tabelas: representadas por classes.
- Colunas: representadas por propriedades.
- Registros: representados por objetos.
- Relacionamentos: representados por propriedades de navegação.

Exemplo de tabela no SQL Server:

```sql
CREATE TABLE Jogo (
    IdJogo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(150) NOT NULL,
    Genero VARCHAR(50) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL
);
```

Representação simplificada em C#:

```csharp
public class Jogo
{
    public int IdJogo { get; set; }
    public string Nome { get; set; }
    public string Genero { get; set; }
    public decimal Preco { get; set; }
}
```

O EF Core utiliza essa classe para mapear os dados da tabela `Jogo`.

## 1.2. Database First: utilização de um banco existente

Existem duas abordagens comuns para trabalhar com o EF Core:

| Abordagem | Funcionamento |
| --- | --- |
| Code First | As classes C# são criadas primeiro e utilizadas para gerar o banco de dados. |
| Database First | O banco de dados já existe e é utilizado para gerar as classes C#. |

Neste conteúdo, utilizaremos Database First.

O banco de dados `DB_GestaoJogos` será nosso ponto de partida. Ele possui as seguintes tabelas:

| Tabela | Finalidade |
| --- | --- |
| `Jogador` | Armazena os jogadores cadastrados. |
| `PerfilJogador` | Armazena informações complementares dos jogadores. |
| `Desenvolvedora` | Armazena as desenvolvedoras de jogos. |
| `Jogo` | Armazena os jogos cadastrados. |
| `JogadorJogo` | Relaciona jogadores e jogos, registrando aquisições e horas jogadas. |

Os relacionamentos representam diferentes cardinalidades:

- `Jogador` e `PerfilJogador`: 1:1.
- `Desenvolvedora` e `Jogo`: 1:N.
- `Jogador` e `Jogo`: N:N, por meio da tabela intermediária `JogadorJogo`.

## 1.3. Scaffold: geração de entidades e DbContext

O Scaffold é o processo utilizado pelo EF Core para analisar um banco de dados existente e gerar automaticamente as classes C# correspondentes.

Primeiro, crie um projeto Console:

```
dotnet new console -n GestaoJogos
cd GestaoJogos
```

Instale os pacotes necessários:

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Instale a ferramenta de linha de comando:

```
dotnet tool install --global dotnet-ef
```

Caso já esteja instalada, atualize:

```
dotnet tool update --global dotnet-ef
```

Execute o Scaffold:

```
dotnet ef dbcontext scaffold "Server=localhost;Database=DB_GestaoJogos;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Data --context GestaoJogosContext --no-onconfiguring
```

> Ajuste `Server=localhost` conforme a instância do SQL Server utilizada. A autenticação integrada pressupõe que o usuário do Windows tenha acesso ao banco.
> 

Após a execução, o EF Core gerará uma estrutura semelhante a esta:

```
GestaoJogos/
│
├── Data/
│   └── GestaoJogosContext.cs
│
├── Models/
│   ├── Jogador.cs
│   ├── PerfilJogador.cs
│   ├── Desenvolvedora.cs
│   ├── Jogo.cs
│   └── JogadorJogo.cs
│
├── Program.cs
└── GestaoJogos.csproj
```

Os nomes e o conteúdo exato das classes são definidos pelo banco de dados e pelas configurações identificadas pelo Scaffold.

Atenção: o Scaffold gera as classes a partir da estrutura do banco. Ele não cria automaticamente os registros de exemplo; para isso, o banco precisa estar previamente populado.

## 1.4. DbContext e DbSet: contexto de persistência

O `DbContext` representa a conexão e a unidade de trabalho entre a aplicação e o banco de dados.

Ele é responsável por:

- Gerenciar a conexão com o banco.
- Mapear as entidades para as tabelas.
- Acompanhar alterações nos objetos.
- Executar operações de consulta e persistência.

O Scaffold gera uma classe semelhante a esta:

```csharp
public partial class GestaoJogosContext : DbContext
{
    public virtual DbSet<Jogador> Jogadores { get; set; }

    public virtual DbSet<Jogo> Jogos { get; set; }

    public virtual DbSet<Desenvolvedora> Desenvolvedoras { get; set; }

    public virtual DbSet<PerfilJogador> PerfisJogador { get; set; }

    public virtual DbSet<JogadorJogo> JogadoresJogos { get; set; }
}
```

O `DbSet` representa uma coleção de entidades de determinado tipo e permite realizar operações sobre os registros da tabela correspondente.

Exemplo:

```csharp
using var context = new GestaoJogosContext();
```

Nesse caso, criamos uma instância do contexto para acessar o banco de dados.

Importante: o nome das propriedades `DbSet` pode variar conforme a geração do Scaffold. Utilize os nomes presentes no projeto gerado.

## 1.5. Operações CRUD: inclusão, consulta, atualização e exclusão

O EF Core permite executar as quatro operações básicas de persistência, conhecidas como CRUD:

| Operação | Método ou recurso |
| --- | --- |
| Create (inclusão) | `Add()` |
| Read (consulta) | `ToList()`, `FirstOrDefault()`, `Find()` |
| Update (atualização) | Alteração das propriedades do objeto |
| Delete (exclusão) | `Remove()` |

Antes de executar as operações, precisamos configurar o contexto.

Como utilizamos `--no-onconfiguring` no Scaffold, a conexão será configurada na aplicação:

```csharp
using Microsoft.EntityFrameworkCore;
using GestaoJogosContext = GestaoJogos.Data.GestaoJogosContext;

string connectionString =
    "Server=localhost;Database=DB_GestaoJogos;Trusted_Connection=True;TrustServerCertificate=True;";

var options = new DbContextOptionsBuilder<GestaoJogosContext>()
    .UseSqlServer(connectionString)
    .Options;

using var context = new GestaoJogosContext(options);
```

### Create — Inserir registros

Utilizamos `Add()` para adicionar uma nova entidade ao contexto.

```csharp
var jogo = new Jogo
{
    Nome = "The Legend of Zelda",
    Genero = "Aventura",
    AnoLancamento = 1986,
    Preco = 199.90m,
    IdDesenvolvedora = 1
};

context.Jogos.Add(jogo);

context.SaveChanges();
```

O EF Core prepara a inclusão e o `SaveChanges()` envia a operação ao banco de dados.

### Read — Consultar registros

Utilizamos métodos como `ToList()` e `FirstOrDefault()` para consultar os dados.

```csharp
var jogos = context.Jogos.ToList();

foreach (var jogo in jogos)
{
    Console.WriteLine($"{jogo.Nome} - R$ {jogo.Preco}");
}
```

Consultar um registro pelo identificador:

```csharp
var jogo = context.Jogos.Find(1);

if (jogo != null)
{
    Console.WriteLine(jogo.Nome);
}
```

### Update — Atualizar registros

Ao consultar uma entidade pelo contexto, o EF Core acompanha suas alterações.

```csharp
var jogo = context.Jogos.Find(1);

if (jogo != null)
{
    jogo.Preco = 149.90m;

    context.SaveChanges();
}
```

Não é necessário executar um comando SQL `UPDATE` manualmente.

### Delete — Excluir registros

Utilizamos `Remove()` para marcar uma entidade para exclusão.

```csharp
var jogo = context.Jogos.Find(1);

if (jogo != null)
{
    context.Jogos.Remove(jogo);

    context.SaveChanges();
}
```

O EF Core executará a exclusão quando `SaveChanges()` for chamado, desde que as regras de integridade do banco permitam a operação.

## 1.6. Persistência: SaveChanges() e SaveChangesAsync()

Os métodos `SaveChanges()` e `SaveChangesAsync()` são responsáveis por enviar ao banco de dados as alterações realizadas no contexto.

Entre essas alterações estão:

- Inclusões realizadas com `Add()`.
- Modificações em entidades acompanhadas pelo contexto.
- Exclusões realizadas com `Remove()`.

SaveChanges(): executa a operação de forma síncrona, aguardando sua conclusão.

```csharp
context.Jogos.Add(jogo);

context.SaveChanges();
```

SaveChangesAsync(): executa a operação de forma assíncrona, permitindo que a aplicação continue trabalhando enquanto aguarda a resposta do banco.

```csharp
context.Jogos.Add(jogo);

await context.SaveChangesAsync();
```

A utilização de `await` exige que o método seja declarado com `async`.

```csharp
static async Task CadastrarJogo()
{
    // Operação de cadastro
    await context.SaveChangesAsync();
}
```

O mesmo princípio se aplica às consultas assíncronas, utilizando métodos como `ToListAsync()` e `FirstOrDefaultAsync()`.

Regra prática: em aplicações modernas, especialmente APIs e aplicações web, prefira operações assíncronas para acesso ao banco de dados.

## 1.7. Consultas com LINQ: filtros, ordenações e operações no banco de dados

O LINQ permite consultar e manipular dados utilizando expressões C#.

Quando utilizado com EF Core, o LINQ é convertido em comandos SQL e executado diretamente no banco de dados.

### Where() — Filtrar registros

Retorna os registros que atendem a determinada condição.

```csharp
var jogos = context.Jogos
    .Where(j => j.Preco < 100)
    .ToList();
```

Consulta jogos com preço inferior a R$ 100,00.

### OrderBy() e OrderByDescending() — Ordenar registros

```csharp
var jogos = context.Jogos
    .OrderBy(j => j.Nome)
    .ToList();
```

Ordena os jogos pelo nome em ordem crescente.

```csharp
var jogos = context.Jogos
    .OrderByDescending(j => j.Preco)
    .ToList();
```

Ordena os jogos pelo preço em ordem decrescente.

### FirstOrDefault() — Buscar um registro

Retorna o primeiro registro que atende à condição ou `null`, caso nenhum seja encontrado.

```csharp
var jogo = context.Jogos
    .FirstOrDefault(j => j.IdJogo == 1);
```

### Any() — Verificar existência

Retorna `true` se existir pelo menos um registro que atenda à condição.

```csharp
bool existe = context.Jogos
    .Any(j => j.Genero == "Aventura");
```

### Select() — Selecionar propriedades

Permite retornar somente as informações necessárias.

```csharp
var jogos = context.Jogos
    .Select(j => new
    {
        j.Nome,
        j.Preco
    })
    .ToList();
```

### Include() — Consultar relacionamentos

O `Include()` permite carregar entidades relacionadas por meio das propriedades de navegação.

Exemplo: consultar jogos incluindo suas desenvolvedoras.

```csharp
var jogos = context.Jogos
    .Include(j => j.IdDesenvolvedoraNavigation)
    .ToList();
```

O nome da propriedade de navegação pode variar conforme o Scaffold. Utilize o nome gerado no projeto.

### Consultando relacionamentos N:N

No banco `DB_GestaoJogos`, o relacionamento entre jogadores e jogos utiliza a tabela intermediária `JogadorJogo`.

Podemos consultar um jogador e seus jogos:

```csharp
var jogador = context.Jogadores
    .Include(j => j.JogadoresJogos)
        .ThenInclude(jj => jj.IdJogoNavigation)
    .FirstOrDefault(j => j.IdJogador == 1);
```

Nesse exemplo:

- `Include()` carrega os registros relacionados de `JogadorJogo`.
- `ThenInclude()` carrega os jogos associados a cada registro.
- `FirstOrDefault()` localiza o jogador pelo identificador.

> Os nomes das propriedades de navegação devem ser conferidos nas classes geradas pelo Scaffold.
> 

### Resumo dos principais métodos LINQ

| Método | Finalidade |
| --- | --- |
| `Where()` | Filtrar registros |
| `OrderBy()` | Ordenar em ordem crescente |
| `OrderByDescending()` | Ordenar em ordem decrescente |
| `FirstOrDefault()` | Buscar o primeiro registro ou retornar `null` |
| `Any()` | Verificar se existem registros |
| `Select()` | Selecionar propriedades |
| `Include()` | Carregar entidades relacionadas |
| `ThenInclude()` | Carregar relacionamentos encadeados |
| `ToList()` | Executar a consulta e retornar uma lista |
| `ToListAsync()` | Executar a consulta de forma assíncrona |

Atenção: métodos como `Where()`, `OrderBy()` e `Select()` normalmente apenas compõem a consulta. A execução ocorre quando são utilizados métodos como `ToList()`, `FirstOrDefault()`, `Any()` ou seus equivalentes assíncronos.

## 1.8. Fluxo geral de utilização do EF Core

Banco de dados SQL Server

DB_GestaoJogos — tabelas e relacionamentos

Scaffold

Entidades e DbContext

Classes C# geradas a partir do banco

Aplicação C#

DbSet, LINQ e operações CRUD

Consultas e persistência

Dados persistidos no SQL Server

Conceito fundamental: com o EF Core, trabalhamos com objetos C# enquanto o ORM realiza a comunicação com o banco de dados relacional, convertendo operações e consultas em comandos SQL.