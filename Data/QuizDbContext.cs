using Microsoft.EntityFrameworkCore;
using quizsergipe_api.Models;

namespace quizsergipe_api.Data;

public class QuizDbContext(DbContextOptions<QuizDbContext> options) : DbContext(options)
{
    public DbSet<Pergunta> Perguntas => Set<Pergunta>();
    public DbSet<Alternativa> Alternativas => Set<Alternativa>();
    public DbSet<QuizSessao> QuizSessoes => Set<QuizSessao>();
    public DbSet<QuizSessaoPergunta> QuizSessaoPerguntas => Set<QuizSessaoPergunta>();
    public DbSet<RespostaUsuario> RespostasUsuario => Set<RespostaUsuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pergunta>(entity =>
        {
            entity.ToTable("Perguntas");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Enunciado).IsRequired().HasMaxLength(500);
            entity.HasMany(x => x.Alternativas)
                .WithOne(x => x.Pergunta)
                .HasForeignKey(x => x.PerguntaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Alternativa>(entity =>
        {
            entity.ToTable("Alternativas");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Letra).IsRequired().HasMaxLength(1);
            entity.Property(x => x.Texto).IsRequired().HasMaxLength(250);
        });

        modelBuilder.Entity<QuizSessao>(entity =>
        {
            entity.ToTable("QuizSessoes");
            entity.HasKey(x => x.Id);
        });

        modelBuilder.Entity<QuizSessaoPergunta>(entity =>
        {
            entity.ToTable("QuizSessaoPerguntas");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => new { x.QuizSessaoId, x.Ordem }).IsUnique();
            entity.HasIndex(x => new { x.QuizSessaoId, x.PerguntaId }).IsUnique();
            entity.HasOne(x => x.QuizSessao)
                .WithMany(x => x.Perguntas)
                .HasForeignKey(x => x.QuizSessaoId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Pergunta)
                .WithMany()
                .HasForeignKey(x => x.PerguntaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RespostaUsuario>(entity =>
        {
            entity.ToTable("RespostasUsuario");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => new { x.QuizSessaoId, x.PerguntaId }).IsUnique();
            entity.HasOne(x => x.QuizSessao)
                .WithMany(x => x.Respostas)
                .HasForeignKey(x => x.QuizSessaoId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Pergunta)
                .WithMany()
                .HasForeignKey(x => x.PerguntaId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Alternativa)
                .WithMany()
                .HasForeignKey(x => x.AlternativaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Pergunta>().HasData(BuildPerguntasSeed());
        modelBuilder.Entity<Alternativa>().HasData(BuildAlternativasSeed());
    }

    private static IEnumerable<Pergunta> BuildPerguntasSeed()
    {
        return
        [
            new Pergunta { Id = 1, Enunciado = "Qual e a capital de Sergipe?" },
            new Pergunta { Id = 2, Enunciado = "Qual rio marca parte importante da divisa entre Sergipe e Alagoas?" },
            new Pergunta { Id = 3, Enunciado = "Qual e a orla mais famosa de Aracaju?" },
            new Pergunta { Id = 4, Enunciado = "Qual cidade sergipana e famosa pelo comercio ligado a caminhoes?" },
            new Pergunta { Id = 5, Enunciado = "Sergipe e o menor estado brasileiro em qual criterio?" },
            new Pergunta { Id = 6, Enunciado = "Qual oceano banha o litoral sergipano?" },
            new Pergunta { Id = 7, Enunciado = "Qual cidade historica foi a primeira capital de Sergipe?" },
            new Pergunta { Id = 8, Enunciado = "Qual universidade federal fica em Sergipe?" },
            new Pergunta { Id = 9, Enunciado = "Qual bioma aparece em parte do territorio sergipano, principalmente no sertao?" },
            new Pergunta { Id = 10, Enunciado = "Qual municipio e a principal porta de entrada para os canions do Xingó em Sergipe?" },
            new Pergunta { Id = 11, Enunciado = "Qual e o nome do estadio principal de Aracaju?" },
            new Pergunta { Id = 12, Enunciado = "Qual cidade e conhecida pela producao de laranja em Sergipe?" },
            new Pergunta { Id = 13, Enunciado = "Qual cidade abriga o Museu da Gente Sergipana?" },
            new Pergunta { Id = 14, Enunciado = "Qual ritmo musical e fortemente associado aos festejos juninos em Sergipe?" },
            new Pergunta { Id = 15, Enunciado = "Quantas estrelas existem na bandeira de Sergipe?" },
            new Pergunta { Id = 16, Enunciado = "Qual cor predomina na bandeira de Sergipe?" },
            new Pergunta { Id = 17, Enunciado = "Qual e o gentilico de quem nasce em Sergipe?" },
            new Pergunta { Id = 18, Enunciado = "Em qual data e comemorada a emancipacao politica de Sergipe?" },
            new Pergunta { Id = 19, Enunciado = "Qual mercado tradicional de Aracaju e um ponto turistico conhecido?" },
            new Pergunta { Id = 20, Enunciado = "Qual cidade sergipana e conhecida pelas festas juninas do pais do forro?" },
            new Pergunta { Id = 21, Enunciado = "Qual municipio e conhecido pela renda irlandesa em Sergipe?" },
            new Pergunta { Id = 22, Enunciado = "Qual cidade fica proxima ao encontro do rio Sao Francisco com o mar em Sergipe?" },
            new Pergunta { Id = 23, Enunciado = "Qual prato com fruto do mar e bastante associado a culinaria sergipana no litoral?" },
            new Pergunta { Id = 24, Enunciado = "Qual usina hidreletrica esta relacionada ao canion do Xingó?" },
            new Pergunta { Id = 25, Enunciado = "Qual cidade foi planejada para ser a capital de Sergipe no seculo XIX?" },
            new Pergunta { Id = 26, Enunciado = "Qual atividade economica teve grande importancia no periodo colonial sergipano?" },
            new Pergunta { Id = 27, Enunciado = "Qual municipio integra a regiao metropolitana e fica em frente a Aracaju, separado pelo rio Sergipe?" },
            new Pergunta { Id = 28, Enunciado = "Qual rio corta a capital Aracaju e da nome ao estado?" },
            new Pergunta { Id = 29, Enunciado = "Qual cidade sergipana e conhecida como Cidade Jardim?" },
            new Pergunta { Id = 30, Enunciado = "Qual municipio sergipano e historicamente conhecido por seu conjunto arquitetonico colonial e tradicao cultural?" }
        ];
    }

    private static IEnumerable<Alternativa> BuildAlternativasSeed()
    {
        return
        [
            NovaAlternativa(1, 1, "A", "Aracaju", true), NovaAlternativa(2, 1, "B", "Estancia"), NovaAlternativa(3, 1, "C", "Lagarto"), NovaAlternativa(4, 1, "D", "Itabaiana"),
            NovaAlternativa(5, 2, "A", "Rio Sergipe"), NovaAlternativa(6, 2, "B", "Rio Sao Francisco", true), NovaAlternativa(7, 2, "C", "Rio Real"), NovaAlternativa(8, 2, "D", "Rio Vaza-Barris"),
            NovaAlternativa(9, 3, "A", "Orla do Farol"), NovaAlternativa(10, 3, "B", "Orla de Atalaia", true), NovaAlternativa(11, 3, "C", "Orla do Saco"), NovaAlternativa(12, 3, "D", "Orla de Pirambu"),
            NovaAlternativa(13, 4, "A", "Itabaiana", true), NovaAlternativa(14, 4, "B", "Lagarto"), NovaAlternativa(15, 4, "C", "Capela"), NovaAlternativa(16, 4, "D", "Estancia"),
            NovaAlternativa(17, 5, "A", "Populacao"), NovaAlternativa(18, 5, "B", "PIB"), NovaAlternativa(19, 5, "C", "Extensao territorial", true), NovaAlternativa(20, 5, "D", "Quantidade de municipios"),
            NovaAlternativa(21, 6, "A", "Oceano Atlantico", true), NovaAlternativa(22, 6, "B", "Oceano Pacifico"), NovaAlternativa(23, 6, "C", "Mar Mediterraneo"), NovaAlternativa(24, 6, "D", "Mar do Caribe"),
            NovaAlternativa(25, 7, "A", "Sao Cristovao", true), NovaAlternativa(26, 7, "B", "Laranjeiras"), NovaAlternativa(27, 7, "C", "Propria"), NovaAlternativa(28, 7, "D", "Neopolis"),
            NovaAlternativa(29, 8, "A", "Universidade de Sergipe"), NovaAlternativa(30, 8, "B", "Universidade Federal de Sergipe", true), NovaAlternativa(31, 8, "C", "Instituto Federal de Sergipe"), NovaAlternativa(32, 8, "D", "Universidade Estadual de Aracaju"),
            NovaAlternativa(33, 9, "A", "Pantanal"), NovaAlternativa(34, 9, "B", "Mata de Araucaria"), NovaAlternativa(35, 9, "C", "Caatinga", true), NovaAlternativa(36, 9, "D", "Pampa"),
            NovaAlternativa(37, 10, "A", "Caninde de Sao Francisco", true), NovaAlternativa(38, 10, "B", "Pirambu"), NovaAlternativa(39, 10, "C", "Pacatuba"), NovaAlternativa(40, 10, "D", "Estancia"),
            NovaAlternativa(41, 11, "A", "Batistao", true), NovaAlternativa(42, 11, "B", "Mangueirao"), NovaAlternativa(43, 11, "C", "Rei Pele"), NovaAlternativa(44, 11, "D", "Fonte Nova"),
            NovaAlternativa(45, 12, "A", "Boquim", true), NovaAlternativa(46, 12, "B", "Umbauba"), NovaAlternativa(47, 12, "C", "Lagarto"), NovaAlternativa(48, 12, "D", "Itabaianinha"),
            NovaAlternativa(49, 13, "A", "Aracaju", true), NovaAlternativa(50, 13, "B", "Sao Cristovao"), NovaAlternativa(51, 13, "C", "Estancia"), NovaAlternativa(52, 13, "D", "Tobias Barreto"),
            NovaAlternativa(53, 14, "A", "Frevo"), NovaAlternativa(54, 14, "B", "Forro", true), NovaAlternativa(55, 14, "C", "Carimbo"), NovaAlternativa(56, 14, "D", "Samba-reggae"),
            NovaAlternativa(57, 15, "A", "4"), NovaAlternativa(58, 15, "B", "5", true), NovaAlternativa(59, 15, "C", "6"), NovaAlternativa(60, 15, "D", "7"),
            NovaAlternativa(61, 16, "A", "Verde"), NovaAlternativa(62, 16, "B", "Amarelo"), NovaAlternativa(63, 16, "C", "Azul", true), NovaAlternativa(64, 16, "D", "Vermelho"),
            NovaAlternativa(65, 17, "A", "Sergipano", true), NovaAlternativa(66, 17, "B", "Sergiense"), NovaAlternativa(67, 17, "C", "Sergipista"), NovaAlternativa(68, 17, "D", "Sergipano do norte"),
            NovaAlternativa(69, 18, "A", "24 de outubro"), NovaAlternativa(70, 18, "B", "8 de julho", true), NovaAlternativa(71, 18, "C", "7 de setembro"), NovaAlternativa(72, 18, "D", "15 de novembro"),
            NovaAlternativa(73, 19, "A", "Mercado Municipal Antonio Franco", true), NovaAlternativa(74, 19, "B", "Mercado Central do Sertao"), NovaAlternativa(75, 19, "C", "Mercado de Atalaia"), NovaAlternativa(76, 19, "D", "Mercado do Xingó"),
            NovaAlternativa(77, 20, "A", "Capela", true), NovaAlternativa(78, 20, "B", "Nossa Senhora da Gloria"), NovaAlternativa(79, 20, "C", "Aquidaba"), NovaAlternativa(80, 20, "D", "Cristinapolis"),
            NovaAlternativa(81, 21, "A", "Divina Pastora", true), NovaAlternativa(82, 21, "B", "Boquim"), NovaAlternativa(83, 21, "C", "Itaporanga d'Ajuda"), NovaAlternativa(84, 21, "D", "Pacatuba"),
            NovaAlternativa(85, 22, "A", "Brejo Grande", true), NovaAlternativa(86, 22, "B", "Propria"), NovaAlternativa(87, 22, "C", "Barra dos Coqueiros"), NovaAlternativa(88, 22, "D", "Estancia"),
            NovaAlternativa(89, 23, "A", "Feijoada"), NovaAlternativa(90, 23, "B", "Caranguejada", true), NovaAlternativa(91, 23, "C", "Pamonha"), NovaAlternativa(92, 23, "D", "Galinhada"),
            NovaAlternativa(93, 24, "A", "Sobradinho"), NovaAlternativa(94, 24, "B", "Xingo", true), NovaAlternativa(95, 24, "C", "Paulo Afonso IV"), NovaAlternativa(96, 24, "D", "Moxoto"),
            NovaAlternativa(97, 25, "A", "Aracaju", true), NovaAlternativa(98, 25, "B", "Estancia"), NovaAlternativa(99, 25, "C", "Laranjeiras"), NovaAlternativa(100, 25, "D", "Sao Cristovao"),
            NovaAlternativa(101, 26, "A", "Industria automobilistica"), NovaAlternativa(102, 26, "B", "Cana-de-acucar", true), NovaAlternativa(103, 26, "C", "Tecnologia da informacao"), NovaAlternativa(104, 26, "D", "Mineracao de cobre"),
            NovaAlternativa(105, 27, "A", "Barra dos Coqueiros", true), NovaAlternativa(106, 27, "B", "Lagarto"), NovaAlternativa(107, 27, "C", "Boquim"), NovaAlternativa(108, 27, "D", "Neopolis"),
            NovaAlternativa(109, 28, "A", "Rio Vaza-Barris"), NovaAlternativa(110, 28, "B", "Rio Sergipe", true), NovaAlternativa(111, 28, "C", "Rio Poxim"), NovaAlternativa(112, 28, "D", "Rio Real"),
            NovaAlternativa(113, 29, "A", "Estancia"), NovaAlternativa(114, 29, "B", "Aracaju", true), NovaAlternativa(115, 29, "C", "Itabaiana"), NovaAlternativa(116, 29, "D", "Sao Cristovao"),
            NovaAlternativa(117, 30, "A", "Laranjeiras", true), NovaAlternativa(118, 30, "B", "Caninde de Sao Francisco"), NovaAlternativa(119, 30, "C", "Boquim"), NovaAlternativa(120, 30, "D", "Poco Redondo")
        ];
    }

    private static Alternativa NovaAlternativa(int id, int perguntaId, string letra, string texto, bool correta = false)
    {
        return new Alternativa
        {
            Id = id,
            PerguntaId = perguntaId,
            Letra = letra,
            Texto = texto,
            Correta = correta
        };
    }
}
