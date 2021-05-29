using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Log
    {
        public static void GravaLog( string Texto)
        {
            string ArquivoLog = GetArquivoLog();
            using (StreamWriter Log = new StreamWriter(ArquivoLog, true))
            {
                Log.WriteLine(DateTime.Now.ToString() + ";" + Texto);
            }
        }

        public static string GetArquivoLog()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "AppLog.log");
        }
    }
}
