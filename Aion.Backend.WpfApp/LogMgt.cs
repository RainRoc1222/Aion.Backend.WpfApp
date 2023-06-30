using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aion.Backend.WpfApp
{
    public class LogMgt
    {
        public static ILog Logger { get; set; }
        static LogMgt()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo($"{Environment.CurrentDirectory}\\log4net.config"));
            Logger = LogManager.GetLogger("訊息 :");
        }
    }
}
