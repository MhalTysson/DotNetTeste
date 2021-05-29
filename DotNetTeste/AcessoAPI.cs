using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DotNetTeste
{
    class AcessoAPI
    {        
        public static async Task<IEnumerable<dynamic>> GetDadosApiAsync(string Filtro)
        {                        
            try
            { 
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.Timeout = TimeSpan.FromSeconds(120);
                    cliente.DefaultRequestHeaders.Accept.Clear();
                    cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string UrlBase = "http://localhost:50677/api/dados/";
                    if (Filtro != "sem agrupamento")
                    {
                        var uriBuilder = new UriBuilder(UrlBase);
                        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                        query["agrupamento"] = Filtro;
                        uriBuilder.Query = query.ToString();
                        UrlBase = uriBuilder.ToString();
                    }

                    HttpResponseMessage resposta = await cliente.GetAsync(UrlBase);
                    
                    string retorno = await resposta.Content.ReadAsStringAsync();

                    if (resposta.IsSuccessStatusCode == true)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(retorno);
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task<byte[]> GetArquivoAsync(string Filtro, string TipoArquivo)
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.Timeout = TimeSpan.FromSeconds(120);
                    cliente.DefaultRequestHeaders.Accept.Clear();
                    cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string UrlBase = "http://localhost:50677/api/Exportar/";

                    var uriBuilder = new UriBuilder(UrlBase);
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                    query["dados"] = Filtro;
                    query["extensao"] = TipoArquivo;
                    uriBuilder.Query = query.ToString();
                    UrlBase = uriBuilder.ToString();

                    HttpResponseMessage resposta = await cliente.GetAsync(UrlBase);

                    var retorno = await resposta.Content.ReadAsByteArrayAsync();                    

                    if (resposta.IsSuccessStatusCode == true)
                    {
                        return retorno;
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
