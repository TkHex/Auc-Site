global using Webtech;
using System;

namespace AucSite;

class Program {
    public static void Main() {
        var api = new REST_API("Config.json", typeof(ApiImpl));

        var backend = new Server(api);
        backend.Work = true;

        ApiImpl.logger.Info("MAIN", "Server started...");
        Console.ReadKey(true);
    }
}