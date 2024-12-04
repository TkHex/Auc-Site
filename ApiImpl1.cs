using System.Net;
using Request = System.Net.HttpListenerRequest;
using Response = System.Net.HttpListenerResponse;
using static System.IO.File;

namespace AucSite;

public class ApiImpl {
    public static string WorkDir = "I:/Учеба/3й курс/5 семак/Лабы/Безопасность БД/Лаба 1/Auc_site/";
    public static Logger logger = new() {LogFile = $"{WorkDir}journal.log"};

    public static void Page(Request req, Response res) {
        if(Exists($"{WorkDir}{req.RawUrl}.html"))
            res.ResponseFile($"{WorkDir}{req.RawUrl}.html");
        else
            res.ResponseError(404, "Page not found!!!");
    }

    public static void Resource(Request req, Response res) {
        if(Exists($"{WorkDir}{req.RawUrl}"))
            res.ResponseFile($"{WorkDir}{req.RawUrl}");
        else
            res.ResponseError(404, "Resource not found!!!");
    }

}