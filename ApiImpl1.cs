using System.Net;

namespace AucSite;

public class ApiImpl {
    public static string WorkDir = "I:/Учеба/3й курс/5 семак/Лабы/Безопасность БД/Лаба 1/Auc_site/";
    public static Logger logger = new() {LogFile = $"{WorkDir}journal.log"};
    public static void Test(HttpListenerRequest req, HttpListenerResponse res) {
        res.ResponseText("Hello from auc.ru by Tema!");
    }
}