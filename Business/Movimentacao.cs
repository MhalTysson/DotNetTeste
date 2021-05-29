using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Business
{
    public class Movimentacao
    {

        public long id { get; set; }
        public DateTime dateTime { get; set; }
        public char tipoOperacao { get; set; }
        public string ativo { get; set; }
        public int quantidade { get; set; }
        public double preco { get; set; }
        public int conta { get; set; }

        public static void GerarMassaTeste(long QtdTotalMassa, string Diretorio, string NomeArquivo)
        {
            try
            {
                Random ValorRandom = new Random();
                DateTime DataAtual = DateTime.Now;
                DateTime DataInicio = new DateTime(DataAtual.Year, DataAtual.Month, 1);
                string ArqSaida = Path.Combine(Diretorio, NomeArquivo);
                string[] Acoes = InicializaListaAcoes();

                List<Movimentacao> LstMovimentacoes = new List<Movimentacao>();

                for (long i = 1; i <= QtdTotalMassa; i++)
                {
                    Movimentacao NovaMovimentacao = new Movimentacao
                    {
                        id = i,
                        dateTime = GetDataRandomica(ValorRandom, DataInicio, DataAtual),
                        tipoOperacao = GetTipoOpRandomico(ValorRandom),
                        ativo = Acoes[ValorRandom.Next(Acoes.Length)],
                        quantidade = ValorRandom.Next(1, 5000),
                        preco = ValorRandom.Next(1, 200) + Math.Round(ValorRandom.NextDouble(), 2),
                        conta = ValorRandom.Next(1, 3000)
                    };

                    LstMovimentacoes.Add(NovaMovimentacao);
                }

                string jsonString = JsonConvert.SerializeObject(LstMovimentacoes, Formatting.Indented);

                File.WriteAllText(ArqSaida, jsonString);
            }
            catch(Exception)
            {
                throw;
            }

        }

        private static DateTime GetDataRandomica(Random random, DateTime DataIinicial, DateTime DataFinal)
        {
            try
            {
                int range = (DataFinal - DataIinicial).Days;
                return DataIinicial.AddDays(random.Next(range)).AddHours(random.Next(24)).AddMinutes(random.Next(60)).AddSeconds(random.Next(60));
            }
            catch(Exception)
            {
                throw;
            }
        }

        private static char GetTipoOpRandomico(Random random)
        {
            try
            {
                char TipoOperacao = Convert.ToChar("C");
                int Valor = random.Next(2);
                if (Valor == 1)
                {
                    TipoOperacao = Convert.ToChar("V");
                }

                return TipoOperacao;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string[] InicializaListaAcoes()
        {
            try
            {
                string[] Acoes = new string[]
                {
                "ABEV3","ASAI3","AZUL4","BTOW3","B3SA3","BBSE3","VRML3","BBDC4","BBDC3",
                "BRAP4","BBAS3","BRKM5","BRFS3","CRFB3","CCRO3","CMIG4","HGTX3","CIEL3","COGN3","CPLE6","CSAN3","CPFE3","CVCB3",
                "CYRE3","ECOR3","ELET3","ELET6","EMBR3","ENBR3","ENGI11","ENEV3","ENGIE3","EQTL3","EZTC3","FLRY3","GGBR4","GOAU4",
                "GOLL4","NTCO3","HAPV3","HYPE3","IGTA3","GNDI3","IRBR3","ITSA4","ITUB4","JBSS3","JHSF3","KLBN11","RENT3","LCAM3",
                "LAME4","LREN3","MGLU3","MRFG3","BEEF3","MRVE3","MULT3","PCAR3","PETR3","PETR4","BRDT3","PRIO3","QUAL3","RADL3",
                "RAIL3","SBSP3"
                };

                return Acoes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<dynamic> GetAgrupamento(string AgruparPor, List<Movimentacao> Dados)
        {
            try
            {
                string[] FiltrosPermitidos = new string[] { "padrao", "ativo", "tipooperacao", "conta" };

                bool FiltroOK = false;
                foreach (string filtro in FiltrosPermitidos)
                {
                    if (AgruparPor.ToLower() == filtro)
                    {
                        FiltroOK = true;
                        break;
                    }
                }

                if (FiltroOK == false)
                {
                    return null;
                }

                IEnumerable<dynamic> Retornos = null;

                switch (AgruparPor)
                {
                    case "padrao":
                        Retornos = Dados.GroupBy(x => new
                        {
                            x.conta,
                            x.ativo,
                            x.tipoOperacao
                        }).Select(Grupo => new
                        {
                            Conta = Grupo.Key.conta,
                            Ativo = Grupo.Key.ativo,
                            TipoOperacao = Grupo.Key.tipoOperacao,
                            Quantidade = Grupo.Sum(g => g.quantidade),
                            ValorMedio = Math.Round(Grupo.Sum(g => g.quantidade * g.preco) / Grupo.Sum(g => g.quantidade), 2)
                        }).OrderBy(g => g.Conta);
                        break;

                    case "ativo":
                        Retornos = Dados.GroupBy(x => new
                        {
                            x.ativo
                        }).Select(Grupo => new
                        {
                            Ativo = Grupo.Key.ativo,
                            Quantidade = Grupo.Sum(g => g.quantidade),
                            ValorMedio = Math.Round(Grupo.Sum(g => g.quantidade * g.preco) / Grupo.Sum(g => g.quantidade), 2)
                        }).OrderBy(g => g.Ativo);
                        break;

                    case "tipooperacao":
                        Retornos = Dados.GroupBy(x => new
                        {
                            x.tipoOperacao
                        }).Select(Grupo => new
                        {
                            TipoOperacao = Grupo.Key.tipoOperacao,
                            Quantidade = Grupo.Sum(g => g.quantidade),
                            ValorMedio = Math.Round(Grupo.Sum(g => g.quantidade * g.preco) / Grupo.Sum(g => g.quantidade), 2)
                        }).OrderBy(g => g.TipoOperacao);
                        break;

                    case "conta":
                        Retornos = Dados.GroupBy(x => new
                        {
                            x.conta
                        }).Select(Grupo => new
                        {
                            Conta = Grupo.Key.conta,
                            Quantidade = Grupo.Sum(g => g.quantidade),
                            ValorMedio = Math.Round(Grupo.Sum(g => g.quantidade * g.preco) / Grupo.Sum(g => g.quantidade), 2)
                        }).OrderBy(g => g.Conta);
                        break;
                }

                return Retornos;
            }
            catch(Exception)
            {
                throw;
            }
        }



    }
}
