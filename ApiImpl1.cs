global using System.Net;
global using System.Text;
global using System.Text.Json;
global using System.Linq;
global using System.Security.Cryptography;
global using Request = System.Net.HttpListenerRequest;
global using Response = System.Net.HttpListenerResponse;
global using RoleRequirement = Webtech.AccessConstraint<AucSite.Roles>;
global using static System.IO.File;

namespace AucSite;

public partial class ApiImpl {
    public static string WorkDir = "I:/Учеба/3й курс/5 семак/Лабы/Безопасность БД/Лаба 1/Auc_site/";
    public static Logger logger = new() {LogFile = $"{WorkDir}journal.log"};
    static ApplicationContext db = new();
    static AccessManager accessor = new(1); //Для выдачи токенов авторизации, живущих 1 час
    static RolesManager<Roles> roler = new();

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