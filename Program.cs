global using Webtech;
using System;

namespace AucSite;

class Program {
    public static void Main() {
        var api = new REST_API(
            "Config.json",                 //Файл конфига API
            typeof(ApiImpl),               //Класс реализации API
            ApiImpl.accessor.Authenticate, //Функция опознования пользователя
            ApiImpl.roler.CheckRole        //Функция опознования роли пользователя
        );

        var backend = new Server(api);
        backend.Work = true;

        ApiImpl.logger.Info("MAIN", "Server started...");
        Console.ReadKey(true);
        ApiImpl.logger.Info("MAIN", "Server closed by terminale...");
    }
}