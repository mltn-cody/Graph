using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;

namespace Lexicon.WebAPI.Server
{
    public static class ServerStartup
    {
        private static int serverStartupRetries;
        private static int retryCount;

        private static readonly List<Exception> StartupExceptions = new List<Exception>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="core"></param>
        /// <param name="args"></param>
        /// <remarks>
        /// This was specifically designed or intended so that each sevice could load it's own configuration,
        /// assists in the deployment of different software versions. 
        /// </remarks>
        private static void Start<TStartup>(ICore<ServiceApi> core, string[] args = null)
            where TStartup : IServiceStartup
        {
        }

    }

    public class EnglishModule : NinjectModule
    {
        public override void Load()
        {

        }
    }

    public class GrammerModule : NinjectModule
    {
        public override void Load()
        {
        }
    }


    internal class ServiceApi
    {
    }

    internal interface IServiceStartup
    {
    }

    internal interface ICore<T>
    {
    }
}
