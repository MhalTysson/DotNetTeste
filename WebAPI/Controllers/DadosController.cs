using Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class DadosController : ApiController
    {
        

        [HttpGet]
        public async Task<IHttpActionResult> GetDados()
        {
            try
            {
                MemoryCache memoryCache = MemoryCache.Default;
                
                List<Movimentacao> LstMovimentacoes = (List<Movimentacao>)memoryCache.Get("sem agrupamento");                

                if (LstMovimentacoes == null)
                {
                    LstMovimentacoes = await CarregaDadosAsync();
                    memoryCache.Add("sem agrupamento", LstMovimentacoes, DateTimeOffset.Now.AddHours(24));
                }                                
                
                var Parametros = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                string TipoAgrupamento = Parametros.Get("agrupamento");

                if(TipoAgrupamento != null)
                {
                    IEnumerable<dynamic> Retorno = (IEnumerable<dynamic>)memoryCache.Get(TipoAgrupamento.ToLower());

                    if (Retorno == null)
                    {
                        Retorno = Movimentacao.GetAgrupamento(TipoAgrupamento.ToLower(), LstMovimentacoes);

                        if (Retorno == null)
                        {
                            return Ok("Filtro não permitido");                            
                        }

                        memoryCache.Add(TipoAgrupamento.ToLower(), Retorno, DateTimeOffset.Now.AddHours(24));
                    }
                    
                    return Ok(Retorno);                    
                }
                               
                return Ok(LstMovimentacoes);
            }
            catch (Exception ex)
            {
                Log.GravaLog(ex.Message.Replace("\n\r", " "));
                throw;
            }
        }

        private async Task<List<Movimentacao>> CarregaDadosAsync()
        {
            try
            {
                const long QtdDadosMassa = 20000;
                const string NomeMassaDados = "MassaDados.json";
                string ArquivoDados;
                string DiretorioDados = HttpContext.Current.Server.MapPath("~/Dados/");

                if (!Directory.Exists(DiretorioDados))
                {
                    Directory.CreateDirectory(DiretorioDados);
                }

                ArquivoDados = Path.Combine(DiretorioDados, NomeMassaDados);
                if (!File.Exists(ArquivoDados))
                {
                    Movimentacao.GerarMassaTeste(QtdDadosMassa, DiretorioDados, NomeMassaDados);
                }

                using (StreamReader reader = new StreamReader(ArquivoDados))
                {
                    string jsonDados = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<List<Movimentacao>>(jsonDados);
                }
            }
            catch(Exception ex)
            {
                Log.GravaLog(ex.Message.Replace("\n\r", " "));
                throw;
            }
        }
    }
}
