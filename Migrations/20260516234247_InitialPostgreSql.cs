using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace quizsergipe_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgreSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perguntas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Enunciado = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perguntas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizSessoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IniciadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinalizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PontuacaoFinal = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSessoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alternativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PerguntaId = table.Column<int>(type: "integer", nullable: false),
                    Letra = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    Texto = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Correta = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alternativas_Perguntas_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Perguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizSessaoPerguntas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuizSessaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PerguntaId = table.Column<int>(type: "integer", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSessaoPerguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizSessaoPerguntas_Perguntas_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Perguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizSessaoPerguntas_QuizSessoes_QuizSessaoId",
                        column: x => x.QuizSessaoId,
                        principalTable: "QuizSessoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostasUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuizSessaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PerguntaId = table.Column<int>(type: "integer", nullable: false),
                    AlternativaId = table.Column<int>(type: "integer", nullable: false),
                    Acertou = table.Column<bool>(type: "boolean", nullable: false),
                    RespondidaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostasUsuario_Alternativas_AlternativaId",
                        column: x => x.AlternativaId,
                        principalTable: "Alternativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespostasUsuario_Perguntas_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Perguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespostasUsuario_QuizSessoes_QuizSessaoId",
                        column: x => x.QuizSessaoId,
                        principalTable: "QuizSessoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Perguntas",
                columns: new[] { "Id", "Enunciado" },
                values: new object[,]
                {
                    { 1, "Qual manifestação folclórica de Laranjeiras utiliza melaço de cana para pintar o corpo dos participantes que representam escravizados fugidos?" },
                    { 2, "Como é chamada a figura histórica que media 2,25 metros e é considerada a mulher mais alta de Sergipe?" },
                    { 3, "Quem era o prático que atravessava a barra do Rio Sergipe a nado para guiar as embarcações?" },
                    { 4, "Qual é o artefato de pirotecnia artesanal que corre num arame e é o maior símbolo de Estância?" },
                    { 5, "Qual apelido carinhoso Sergipe recebe durante o mês de junho pela animação das suas festas?" },
                    { 6, "Qual crustáceo é servido tradicionalmente com pirão e vinagrete na Orla da Atalaia?" },
                    { 7, "Qual fruto é tão importante que o nome da capital, Aracaju, deriva de um termo indígena ligado a ele?" },
                    { 8, "Qual cidade sergipana é a 4ª mais antiga do Brasil e possui uma praça que é Patrimônio da Humanidade?" },
                    { 9, "Qual movimento social do sertão teve seu fim trágico na Grota de Angicos, em território sergipano?" },
                    { 10, "Qual grupo folclórico utiliza armas longas (carregadas com pólvora seca) para saudar santos e autoridades?" },
                    { 11, "Qual grupo folclórico usa trajes espelhados e chapéus em forma de castelo para celebrar o Dia de Reis?" },
                    { 12, "Qual petisco, geralmente vendido cozido em carrinhos, é considerado Patrimônio Imaterial de Sergipe?" },
                    { 13, "Como o Rio São Francisco é carinhosamente chamado pela população ribeirinha de Sergipe?" },
                    { 14, "Qual mercado central de Aracaju é famoso pela venda de artesanato, rendas e artigos de palha?" },
                    { 15, "Qual iguaria junina é feita de massa de puba (mandioca fermentada), coco e açúcar, assada tradicionalmente no forno a lenha?" },
                    { 16, "Qual fruto silvestre é considerado o fruto símbolo de Sergipe e é base de sorvetes e doces amados localmente?" },
                    { 17, "Como se chama o gênero literário popular, escrito em versos rimados e métrica rígida, que tradicionalmente é impresso em pequenos folhetos e exposto em cordas nas feiras?" },
                    { 18, "Na tradicional festa de Laranjeiras, qual grupo representa os indígenas catequizados e usa cocares de penas?" },
                    { 19, "Quem foi a primeira mulher a entrar oficialmente para o bando de Lampião, tornando-se Rainha do Cangaço?" },
                    { 20, "Qual cidade sergipana é conhecida como a \"Capital do Caminhão\" e maior centro de castanha de caju?" },
                    { 21, "Qual banda de Aracaju conquistou o Brasil nos anos 2000 com o \"Forró Eletrônico\" e tinha Paulinha Abelha como uma de suas estrelas?" },
                    { 22, "Em qual data de outubro comemora-se oficialmente o Dia da Sergipanidade?" },
                    { 23, "Qual material é misturado ao melaço de cana para dar a cor preta à pele dos participantes dos Lambe-Sujos?" },
                    { 24, "Qual cantora sergipana é conhecida como a \"Rainha do Forró\" e imortalizou o sucesso \"Prenda o Tadeu\"?" },
                    { 25, "Qual das figuras bíblicas abaixo é a grande homenageada nas festas de Reisado em Sergipe?" },
                    { 26, "Qual cidade sergipana é famosa pela produção de cerâmica artesanal, especialmente as \"bonecas\" de barro?" },
                    { 27, "Qual é o bairro de Aracaju onde se localiza a famosa \"Passarela do Caranguejo\"?" },
                    { 28, "Como é chamado o ritual junino de Capela que envolve a busca por uma árvore na mata para ser plantada no centro da cidade?" },
                    { 29, "Como é chamado o maior evento de arte e cultura que ocorre anualmente na cidade de São Cristóvão?" },
                    { 30, "Qual o ritmo musical que define a identidade de Sergipe como o \"País do Forró\"?" }
                });

            migrationBuilder.InsertData(
                table: "Alternativas",
                columns: new[] { "Id", "Correta", "Letra", "PerguntaId", "Texto" },
                values: new object[,]
                {
                    { 1, false, "A", 1, "Reisado" },
                    { 2, false, "B", 1, "Bacamarteiros" },
                    { 3, true, "C", 1, "Lambe-Sujos" },
                    { 4, false, "D", 1, "Caboclinhos" },
                    { 5, false, "A", 2, "Maria Bonita" },
                    { 6, true, "B", 2, "Maria Feliciana" },
                    { 7, false, "C", 2, "Clemilda" },
                    { 8, false, "D", 2, "Dona Suja" },
                    { 9, true, "A", 3, "Zé Peixe" },
                    { 10, false, "B", 3, "Mateus" },
                    { 11, false, "C", 3, "Lampião" },
                    { 12, false, "D", 3, "Pedro Bombacho" },
                    { 13, false, "A", 4, "Bacamarte" },
                    { 14, false, "B", 4, "Buscapé" },
                    { 15, false, "C", 4, "Espada" },
                    { 16, true, "D", 4, "Barco de Fogo" },
                    { 17, false, "A", 5, "Terra da Luz" },
                    { 18, true, "B", 5, "País do Forró" },
                    { 19, false, "C", 5, "Capital do Caju" },
                    { 20, false, "D", 5, "Cidade Sorriso" },
                    { 21, false, "A", 6, "Siri" },
                    { 22, true, "B", 6, "Caranguejo" },
                    { 23, false, "C", 6, "Camarão" },
                    { 24, false, "D", 6, "Lagosta" },
                    { 25, false, "A", 7, "Mangaba" },
                    { 26, false, "B", 7, "Manga" },
                    { 27, true, "C", 7, "Caju" },
                    { 28, false, "D", 7, "Umbu" },
                    { 29, false, "A", 8, "Laranjeiras" },
                    { 30, true, "B", 8, "São Cristóvão" },
                    { 31, false, "C", 8, "Itabaiana" },
                    { 32, false, "D", 8, "Estância" },
                    { 33, false, "A", 9, "Cabanagem" },
                    { 34, true, "B", 9, "Cangaço" },
                    { 35, false, "C", 9, "Revolta da Chibata" },
                    { 36, false, "D", 9, "Messianismo" },
                    { 37, false, "A", 10, "Guerreiro" },
                    { 38, false, "B", 10, "Reisado" },
                    { 39, true, "C", 10, "Bacamarteiros" },
                    { 40, false, "D", 10, "Parafusos" },
                    { 41, true, "A", 11, "Reisado" },
                    { 42, false, "B", 11, "Chegança" },
                    { 43, false, "C", 11, "Lambe-Sujos" },
                    { 44, false, "D", 11, "Cacumbi" },
                    { 45, false, "A", 12, "Acarajé" },
                    { 46, true, "B", 12, "Amendoim cozido" },
                    { 47, false, "C", 12, "Macaxeira" },
                    { 48, false, "D", 12, "Milho assado" },
                    { 49, false, "A", 13, "Rio Mar" },
                    { 50, true, "B", 13, "Velho Chico" },
                    { 51, false, "C", 13, "Rio das Garças" },
                    { 52, false, "D", 13, "Opará (embora seja o nome indígena, o apelido carinhoso popular é Velho Chico)" },
                    { 53, false, "A", 14, "Mercado Albano Franco" },
                    { 54, true, "B", 14, "Mercado Thales Ferraz" },
                    { 55, false, "C", 14, "Mercado da Piçarra" },
                    { 56, false, "D", 14, "Mercado de Itabaiana" },
                    { 57, false, "A", 15, "Bolo de Milho" },
                    { 58, true, "B", 15, "Pé de Moleque Sergipano" },
                    { 59, false, "C", 15, "Canjica" },
                    { 60, false, "D", 15, "Pamonha" },
                    { 61, false, "A", 16, "Graviola" },
                    { 62, true, "B", 16, "Mangaba" },
                    { 63, false, "C", 16, "Pitomba" },
                    { 64, false, "D", 16, "Seriguela" },
                    { 65, false, "A", 17, "Repente" },
                    { 66, true, "B", 17, "Cordel" },
                    { 67, false, "C", 17, "Soneto" },
                    { 68, false, "D", 17, "Crônica" },
                    { 69, false, "A", 18, "Lambe-Sujos" },
                    { 70, true, "B", 18, "Caboclinhos" },
                    { 71, false, "C", 18, "Taieira" },
                    { 72, false, "D", 18, "Cacumbi" },
                    { 73, false, "A", 19, "Dadá" },
                    { 74, false, "B", 19, "Enedina" },
                    { 75, true, "C", 19, "Maria Bonita" },
                    { 76, false, "D", 19, "Maria Feliciana" },
                    { 77, false, "A", 20, "Estância" },
                    { 78, true, "B", 20, "Itabaiana" },
                    { 79, false, "C", 20, "Propriá" },
                    { 80, false, "D", 20, "Lagarto" },
                    { 81, false, "A", 21, "Mastruz com Leite" },
                    { 82, true, "B", 21, "Calcinha Preta" },
                    { 83, false, "C", 21, "Cavaleiros do Forró" },
                    { 84, false, "D", 21, "Magníficos" },
                    { 85, false, "A", 22, "12 de outubro" },
                    { 86, true, "B", 22, "24 de outubro" },
                    { 87, false, "C", 22, "17 de março" },
                    { 88, false, "D", 22, "08 de julho" },
                    { 89, false, "A", 23, "Tinta guache" },
                    { 90, false, "B", 23, "Lama de mangue" },
                    { 91, true, "C", 23, "Pó de fuligem (carvão)" },
                    { 92, false, "D", 23, "Graxa de sapato" },
                    { 93, false, "A", 24, "Amorosa" },
                    { 94, true, "B", 24, "Clemilda" },
                    { 95, false, "C", 24, "Paulinha Abelha" },
                    { 96, false, "D", 24, "Maysa" },
                    { 97, false, "A", 25, "São João" },
                    { 98, false, "B", 25, "Santo Antônio" },
                    { 99, true, "C", 25, "Reis Magos" },
                    { 100, false, "D", 25, "São Pedro" },
                    { 101, false, "A", 26, "Itabaiana" },
                    { 102, true, "B", 26, "Santana do São Francisco" },
                    { 103, false, "C", 26, "Umbaúba" },
                    { 104, false, "D", 26, "Simão Dias" },
                    { 105, false, "A", 27, "Centro" },
                    { 106, true, "B", 27, "Atalaia" },
                    { 107, false, "C", 27, "Jardins" },
                    { 108, false, "D", 27, "Bairro Industrial" },
                    { 109, false, "A", 28, "Queima do Judas" },
                    { 110, true, "B", 28, "Festa do Mastro" },
                    { 111, false, "C", 28, "Casamento Caipira" },
                    { 112, false, "D", 28, "Batalha de Buscapés" },
                    { 113, false, "A", 29, "Forró Caju" },
                    { 114, true, "B", 29, "FASC (Festival de Artes de São Cristóvão)" },
                    { 115, false, "C", 29, "Verão Sergipe" },
                    { 116, false, "D", 29, "Festa do Mastro" },
                    { 117, false, "A", 30, "Axé" },
                    { 118, false, "B", 30, "Samba" },
                    { 119, true, "C", 30, "Forró" },
                    { 120, false, "D", 30, "Pagode" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_PerguntaId",
                table: "Alternativas",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessaoPerguntas_PerguntaId",
                table: "QuizSessaoPerguntas",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessaoPerguntas_QuizSessaoId_Ordem",
                table: "QuizSessaoPerguntas",
                columns: new[] { "QuizSessaoId", "Ordem" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizSessaoPerguntas_QuizSessaoId_PerguntaId",
                table: "QuizSessaoPerguntas",
                columns: new[] { "QuizSessaoId", "PerguntaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespostasUsuario_AlternativaId",
                table: "RespostasUsuario",
                column: "AlternativaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasUsuario_PerguntaId",
                table: "RespostasUsuario",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasUsuario_QuizSessaoId_PerguntaId",
                table: "RespostasUsuario",
                columns: new[] { "QuizSessaoId", "PerguntaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizSessaoPerguntas");

            migrationBuilder.DropTable(
                name: "RespostasUsuario");

            migrationBuilder.DropTable(
                name: "Alternativas");

            migrationBuilder.DropTable(
                name: "QuizSessoes");

            migrationBuilder.DropTable(
                name: "Perguntas");
        }
    }
}
