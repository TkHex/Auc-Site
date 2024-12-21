namespace AucSite;

#nullable disable

public partial class ApiImpl {
    private static string HashPassword(string password) {
        using (var hmac = new HMACSHA256()) {
            // Генерируем соль
            byte[] salt = hmac.Key;
            // Хешируем пароль
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            // Сохраняем соль и хеш как Base64 строки
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }
    }

    private static bool VerifyPassword(string enteredPassword, string storedHash) {
        // Разделяем соль и хеш
        var parts = storedHash.Split(':');
        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedPasswordHash = Convert.FromBase64String(parts[1]);

        using (var hmac = new HMACSHA256(salt)) {
            // Хешируем введенный пароль с той же солью
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
            // Сравниваем хеши
            return hash.SequenceEqual(storedPasswordHash);
        }
    }

    public static void UserRegistration(Request req, Response res) {
        var form_info = req.ParseForm();
        User u = new() {
            login = form_info["login"],
            lastname = form_info["lastname"],
            name = form_info["name"],
            middle_name = form_info["middle_name"],
            bank_info = form_info["bank_info"],
            password = HashPassword(form_info["password"]), //Тут есть хеша!
        };
        db.Users.Add(u);
        db.SaveChanges();
        res.ResponseText("User was registered.");
        res.Redirect("/index");
    } 
    public static void UserLogin(Request req, Response res) {
        var form_info = req.ParseForm();
        string login = form_info["login"];
        string password = form_info["password"];

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)) {
            res.ResponseError((int)HttpStatusCode.BadRequest, "Login and password are required!");
            return;
        }

        var user = db.Users.FirstOrDefault(u => u.login == login);
        if (user != null && VerifyPassword(password, user.password)) {
            var t = accessor.Authorize(res);
            roler.AssociateRole(t.Value, (Roles)user.id_role);
            res.ResponseText("Login successful.");
            logger.Info("UserLogin", $"Logined user {user.name}");
        } else {
            res.ResponseError((int)HttpStatusCode.Unauthorized, "Invalid login or password.");
        }
    }
    public static void Test(Request req, Response res) {
        res.ResponseText($"Token:{req.Cookies["ac_token"].Value}\nAuthorization: {req.Headers["Authorization"]}");
    }
    public static void GetRole(Request req, Response res) {
        var t = req.Cookies["ac_token"]?.Value;
        logger.Debug("GetRole", t);
        if(t != null)
            res.ResponseText(((int)roler.GetRole(t)).ToString());
        else
            res.ResponseError((int)HttpStatusCode.BadRequest, "No token for getting role.");
    }
}