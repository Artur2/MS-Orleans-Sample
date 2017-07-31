using System;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;

namespace SampleOrdering.Server
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // First, configure and start a local silo
            var siloConfig = ClusterConfiguration.LocalhostPrimarySilo();
            siloConfig.AddMemoryStorageProvider();
            
            var silo = new SiloHost("TestSilo", siloConfig);
            silo.InitializeOrleansSilo();
            silo.StartOrleansSilo();

            Console.WriteLine("Silo started.");

            Console.WriteLine("\nPress Enter to terminate...");
            Console.ReadLine();

            // Shut down
            silo.ShutdownOrleansSilo();
        }
    }
}
