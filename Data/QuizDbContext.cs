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
            entity.Property(x => x.Enunciado).IsRequired().HasMaxLength(1000);
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
            NovaPergunta(1, "Qual manifestação folclórica de Laranjeiras utiliza melaço de cana para pintar o corpo dos participantes que representam escravizados fugidos?"),
            NovaPergunta(2, "Como é chamada a figura histórica que media 2,25 metros e é considerada a mulher mais alta de Sergipe?"),
            NovaPergunta(3, "Quem era o prático que atravessava a barra do Rio Sergipe a nado para guiar as embarcações?"),
            NovaPergunta(4, "Qual é o artefato de pirotecnia artesanal que corre num arame e é o maior símbolo de Estância?"),
            NovaPergunta(5, "Qual apelido carinhoso Sergipe recebe durante o mês de junho pela animação das suas festas?"),
            NovaPergunta(6, "Qual crustáceo é servido tradicionalmente com pirão e vinagrete na Orla da Atalaia?"),
            NovaPergunta(7, "Qual fruto é tão importante que o nome da capital, Aracaju, deriva de um termo indígena ligado a ele?"),
            NovaPergunta(8, "Qual cidade sergipana é a 4ª mais antiga do Brasil e possui uma praça que é Patrimônio da Humanidade?"),
            NovaPergunta(9, "Qual movimento social do sertão teve seu fim trágico na Grota de Angicos, em território sergipano?"),
            NovaPergunta(10, "Qual grupo folclórico utiliza armas longas (carregadas com pólvora seca) para saudar santos e autoridades?"),
            NovaPergunta(11, "Qual grupo folclórico usa trajes espelhados e chapéus em forma de castelo para celebrar o Dia de Reis?"),
            NovaPergunta(12, "Qual petisco, geralmente vendido cozido em carrinhos, é considerado Patrimônio Imaterial de Sergipe?"),
            NovaPergunta(13, "Como o Rio São Francisco é carinhosamente chamado pela população ribeirinha de Sergipe?"),
            NovaPergunta(14, "Qual mercado central de Aracaju é famoso pela venda de artesanato, rendas e artigos de palha?"),
            NovaPergunta(15, "Qual iguaria junina é feita de massa de puba (mandioca fermentada), coco e açúcar, assada tradicionalmente no forno a lenha?"),
            NovaPergunta(16, "Qual fruto silvestre é considerado o fruto símbolo de Sergipe e é base de sorvetes e doces amados localmente?"),
            NovaPergunta(17, "Como se chama o gênero literário popular, escrito em versos rimados e métrica rígida, que tradicionalmente é impresso em pequenos folhetos e exposto em cordas nas feiras?"),
            NovaPergunta(18, "Na tradicional festa de Laranjeiras, qual grupo representa os indígenas catequizados e usa cocares de penas?"),
            NovaPergunta(19, "Quem foi a primeira mulher a entrar oficialmente para o bando de Lampião, tornando-se Rainha do Cangaço?"),
            NovaPergunta(20, "Qual cidade sergipana é conhecida como a \"Capital do Caminhão\" e maior centro de castanha de caju?"),
            NovaPergunta(21, "Qual banda de Aracaju conquistou o Brasil nos anos 2000 com o \"Forró Eletrônico\" e tinha Paulinha Abelha como uma de suas estrelas?"),
            NovaPergunta(22, "Em qual data de outubro comemora-se oficialmente o Dia da Sergipanidade?"),
            NovaPergunta(23, "Qual material é misturado ao melaço de cana para dar a cor preta à pele dos participantes dos Lambe-Sujos?"),
            NovaPergunta(24, "Qual cantora sergipana é conhecida como a \"Rainha do Forró\" e imortalizou o sucesso \"Prenda o Tadeu\"?"),
            NovaPergunta(25, "Qual das figuras bíblicas abaixo é a grande homenageada nas festas de Reisado em Sergipe?"),
            NovaPergunta(26, "Qual cidade sergipana é famosa pela produção de cerâmica artesanal, especialmente as \"bonecas\" de barro?"),
            NovaPergunta(27, "Qual é o bairro de Aracaju onde se localiza a famosa \"Passarela do Caranguejo\"?"),
            NovaPergunta(28, "Como é chamado o ritual junino de Capela que envolve a busca por uma árvore na mata para ser plantada no centro da cidade?"),
            NovaPergunta(29, "Como é chamado o maior evento de arte e cultura que ocorre anualmente na cidade de São Cristóvão?"),
            NovaPergunta(30, "Qual o ritmo musical que define a identidade de Sergipe como o \"País do Forró\"?")
        ];
    }

    private static IEnumerable<Alternativa> BuildAlternativasSeed()
    {
        return
        [
            NovaAlternativa(1, 1, "A", "Reisado"), NovaAlternativa(2, 1, "B", "Bacamarteiros"), NovaAlternativa(3, 1, "C", "Lambe-Sujos", true), NovaAlternativa(4, 1, "D", "Caboclinhos"),
            NovaAlternativa(5, 2, "A", "Maria Bonita"), NovaAlternativa(6, 2, "B", "Maria Feliciana", true), NovaAlternativa(7, 2, "C", "Clemilda"), NovaAlternativa(8, 2, "D", "Dona Suja"),
            NovaAlternativa(9, 3, "A", "Zé Peixe", true), NovaAlternativa(10, 3, "B", "Mateus"), NovaAlternativa(11, 3, "C", "Lampião"), NovaAlternativa(12, 3, "D", "Pedro Bombacho"),
            NovaAlternativa(13, 4, "A", "Bacamarte"), NovaAlternativa(14, 4, "B", "Buscapé"), NovaAlternativa(15, 4, "C", "Espada"), NovaAlternativa(16, 4, "D", "Barco de Fogo", true),
            NovaAlternativa(17, 5, "A", "Terra da Luz"), NovaAlternativa(18, 5, "B", "País do Forró", true), NovaAlternativa(19, 5, "C", "Capital do Caju"), NovaAlternativa(20, 5, "D", "Cidade Sorriso"),
            NovaAlternativa(21, 6, "A", "Siri"), NovaAlternativa(22, 6, "B", "Caranguejo", true), NovaAlternativa(23, 6, "C", "Camarão"), NovaAlternativa(24, 6, "D", "Lagosta"),
            NovaAlternativa(25, 7, "A", "Mangaba"), NovaAlternativa(26, 7, "B", "Manga"), NovaAlternativa(27, 7, "C", "Caju", true), NovaAlternativa(28, 7, "D", "Umbu"),
            NovaAlternativa(29, 8, "A", "Laranjeiras"), NovaAlternativa(30, 8, "B", "São Cristóvão", true), NovaAlternativa(31, 8, "C", "Itabaiana"), NovaAlternativa(32, 8, "D", "Estância"),
            NovaAlternativa(33, 9, "A", "Cabanagem"), NovaAlternativa(34, 9, "B", "Cangaço", true), NovaAlternativa(35, 9, "C", "Revolta da Chibata"), NovaAlternativa(36, 9, "D", "Messianismo"),
            NovaAlternativa(37, 10, "A", "Guerreiro"), NovaAlternativa(38, 10, "B", "Reisado"), NovaAlternativa(39, 10, "C", "Bacamarteiros", true), NovaAlternativa(40, 10, "D", "Parafusos"),
            NovaAlternativa(41, 11, "A", "Reisado", true), NovaAlternativa(42, 11, "B", "Chegança"), NovaAlternativa(43, 11, "C", "Lambe-Sujos"), NovaAlternativa(44, 11, "D", "Cacumbi"),
            NovaAlternativa(45, 12, "A", "Acarajé"), NovaAlternativa(46, 12, "B", "Amendoim cozido", true), NovaAlternativa(47, 12, "C", "Macaxeira"), NovaAlternativa(48, 12, "D", "Milho assado"),
            NovaAlternativa(49, 13, "A", "Rio Mar"), NovaAlternativa(50, 13, "B", "Velho Chico", true), NovaAlternativa(51, 13, "C", "Rio das Garças"), NovaAlternativa(52, 13, "D", "Opará (embora seja o nome indígena, o apelido carinhoso popular é Velho Chico)"),
            NovaAlternativa(53, 14, "A", "Mercado Albano Franco"), NovaAlternativa(54, 14, "B", "Mercado Thales Ferraz", true), NovaAlternativa(55, 14, "C", "Mercado da Piçarra"), NovaAlternativa(56, 14, "D", "Mercado de Itabaiana"),
            NovaAlternativa(57, 15, "A", "Bolo de Milho"), NovaAlternativa(58, 15, "B", "Pé de Moleque Sergipano", true), NovaAlternativa(59, 15, "C", "Canjica"), NovaAlternativa(60, 15, "D", "Pamonha"),
            NovaAlternativa(61, 16, "A", "Graviola"), NovaAlternativa(62, 16, "B", "Mangaba", true), NovaAlternativa(63, 16, "C", "Pitomba"), NovaAlternativa(64, 16, "D", "Seriguela"),
            NovaAlternativa(65, 17, "A", "Repente"), NovaAlternativa(66, 17, "B", "Cordel", true), NovaAlternativa(67, 17, "C", "Soneto"), NovaAlternativa(68, 17, "D", "Crônica"),
            NovaAlternativa(69, 18, "A", "Lambe-Sujos"), NovaAlternativa(70, 18, "B", "Caboclinhos", true), NovaAlternativa(71, 18, "C", "Taieira"), NovaAlternativa(72, 18, "D", "Cacumbi"),
            NovaAlternativa(73, 19, "A", "Dadá"), NovaAlternativa(74, 19, "B", "Enedina"), NovaAlternativa(75, 19, "C", "Maria Bonita", true), NovaAlternativa(76, 19, "D", "Maria Feliciana"),
            NovaAlternativa(77, 20, "A", "Estância"), NovaAlternativa(78, 20, "B", "Itabaiana", true), NovaAlternativa(79, 20, "C", "Propriá"), NovaAlternativa(80, 20, "D", "Lagarto"),
            NovaAlternativa(81, 21, "A", "Mastruz com Leite"), NovaAlternativa(82, 21, "B", "Calcinha Preta", true), NovaAlternativa(83, 21, "C", "Cavaleiros do Forró"), NovaAlternativa(84, 21, "D", "Magníficos"),
            NovaAlternativa(85, 22, "A", "12 de outubro"), NovaAlternativa(86, 22, "B", "24 de outubro", true), NovaAlternativa(87, 22, "C", "17 de março"), NovaAlternativa(88, 22, "D", "08 de julho"),
            NovaAlternativa(89, 23, "A", "Tinta guache"), NovaAlternativa(90, 23, "B", "Lama de mangue"), NovaAlternativa(91, 23, "C", "Pó de fuligem (carvão)", true), NovaAlternativa(92, 23, "D", "Graxa de sapato"),
            NovaAlternativa(93, 24, "A", "Amorosa"), NovaAlternativa(94, 24, "B", "Clemilda", true), NovaAlternativa(95, 24, "C", "Paulinha Abelha"), NovaAlternativa(96, 24, "D", "Maysa"),
            NovaAlternativa(97, 25, "A", "São João"), NovaAlternativa(98, 25, "B", "Santo Antônio"), NovaAlternativa(99, 25, "C", "Reis Magos", true), NovaAlternativa(100, 25, "D", "São Pedro"),
            NovaAlternativa(101, 26, "A", "Itabaiana"), NovaAlternativa(102, 26, "B", "Santana do São Francisco", true), NovaAlternativa(103, 26, "C", "Umbaúba"), NovaAlternativa(104, 26, "D", "Simão Dias"),
            NovaAlternativa(105, 27, "A", "Centro"), NovaAlternativa(106, 27, "B", "Atalaia", true), NovaAlternativa(107, 27, "C", "Jardins"), NovaAlternativa(108, 27, "D", "Bairro Industrial"),
            NovaAlternativa(109, 28, "A", "Queima do Judas"), NovaAlternativa(110, 28, "B", "Festa do Mastro", true), NovaAlternativa(111, 28, "C", "Casamento Caipira"), NovaAlternativa(112, 28, "D", "Batalha de Buscapés"),
            NovaAlternativa(113, 29, "A", "Forró Caju"), NovaAlternativa(114, 29, "B", "FASC (Festival de Artes de São Cristóvão)", true), NovaAlternativa(115, 29, "C", "Verão Sergipe"), NovaAlternativa(116, 29, "D", "Festa do Mastro"),
            NovaAlternativa(117, 30, "A", "Axé"), NovaAlternativa(118, 30, "B", "Samba"), NovaAlternativa(119, 30, "C", "Forró", true), NovaAlternativa(120, 30, "D", "Pagode")
        ];
    }

    private static Pergunta NovaPergunta(int id, string enunciado)
    {
        return new Pergunta
        {
            Id = id,
            Enunciado = enunciado
        };
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
