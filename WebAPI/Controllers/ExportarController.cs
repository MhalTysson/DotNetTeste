using Business;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ExportarController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> ExportarDados()
        {
            try
            {
                MemoryCache memoryCache = MemoryCache.Default;

                var Parametros = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                string TipoDados = Parametros.Get("dados");
                string ExtensaoSaida = Parametros.Get("extensao");

                IEnumerable<dynamic> Retorno = (IEnumerable<dynamic>)memoryCache.Get(TipoDados.ToLower());                

                string TipoMime = string.Empty;
                byte[] ArquivoBytes = null;
                switch (ExtensaoSaida)
                {
                    case "csv":
                        TipoMime = "text/csv";                        
                        ArquivoBytes = GeraCsv(Retorno);
                        break;

                    case "xlsx":
                        TipoMime = "application/vnd.ms-excel";
                        ArquivoBytes = GeraExcel(Retorno, TipoDados);
                        break;
                }

                var DadosStream = new MemoryStream(ArquivoBytes);

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(DadosStream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(TipoMime);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Relatorio_" + TipoDados + "_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + "." + ExtensaoSaida
                };
                response.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");


                return response;

            }
            catch(Exception ex)
            {
                Log.GravaLog(ex.Message.Replace("\n\r", " "));
                throw;
            }
        }

        private byte[] GeraCsv(IEnumerable<dynamic> DadosArquivo)
        {
            try
            {
                string DadosSaida = string.Empty;
                foreach (object Dado in DadosArquivo)
                {
                    PropertyInfo[] propriedades = Dado.GetType().GetProperties();

                    if (DadosSaida == string.Empty)
                    {
                        List<string> NomeColunas = new List<string>();
                        foreach (var propriedade in propriedades)
                        {
                            NomeColunas.Add(propriedade.Name);
                        }
                        DadosSaida = string.Join(";", NomeColunas) + Environment.NewLine;
                    }

                    List<string> Valores = new List<string>();
                    foreach (var propriedade in propriedades)
                    {
                        PropertyInfo property = Dado.GetType().GetProperty(propriedade.Name);

                        Valores.Add(property.GetValue(Dado, null).ToString());
                    }

                    DadosSaida += string.Join(";", Valores) + Environment.NewLine;
                }

                return Encoding.UTF8.GetBytes(DadosSaida).ToArray();
            }
            catch(Exception ex)
            {
                Log.GravaLog(ex.Message.Replace("\n\r", " "));
                throw;
            }
        }

        private byte[] GeraExcel(IEnumerable<dynamic> DadosArquivo, string Filtro)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {                    
                    var wb = new XLWorkbook();
                    var ws = wb.Worksheets.Add(Filtro);

                    int Linha = 1;
                    foreach (object Dado in DadosArquivo)
                    {
                        PropertyInfo[] propriedades = Dado.GetType().GetProperties();

                        int NroCol = 1;
                        if (Linha == 1)
                        {
                            foreach (var propriedade in propriedades)
                            {
                                ws.Cell(Linha, NroCol).Value = propriedade.Name;
                                NroCol++;
                            }
                        }
                        else
                        {
                            foreach (var propriedade in propriedades)
                            {
                                PropertyInfo property = Dado.GetType().GetProperty(propriedade.Name);

                                ws.Cell(Linha, NroCol).Value = property.GetValue(Dado, null).ToString();
                                NroCol++;
                            }
                        }
                        Linha++;
                    }

                    //As colunas se ajustam ao tamanho do campo interno de cada uma 
                    ws.Columns().AdjustToContents();

                    //salva o arquivo e finaliza o excel
                    wb.SaveAs(memoryStream);

                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                Log.GravaLog(ex.Message.Replace("\n\r", " "));
                throw;
            }
        }
    }
}
