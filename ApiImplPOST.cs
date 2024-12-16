namespace AucSite;

public partial class ApiImpl {
    
    public static void CreateAuction(Request req, Response res) {
        using (var reader = new StreamReader(req.InputStream, new UTF8Encoding())) {
            var requestBody = reader.ReadToEnd();

            var newAuction = JsonSerializer.Deserialize<Auction>(requestBody);

            if (newAuction != null) {
                db.Auctions.Add(newAuction);
                db.SaveChanges();
                res.ResponseText("Auction created successfully.");
            } else {
                res.ResponseError((int)HttpStatusCode.BadRequest, "Invalid auction data!");
            }
        }
    }

    public static void CreateLot(Request req, Response res) {
        using (var reader = new StreamReader(req.InputStream, new UTF8Encoding())) {
            var requestBody = reader.ReadToEnd();

            var newLot = JsonSerializer.Deserialize<Lot>(requestBody);

            if (newLot != null) {
                db.Lots.Add(newLot);
                db.SaveChanges();
                res.ResponseText("Lot created successfully.");
            } else {
                res.ResponseError((int)HttpStatusCode.BadRequest, "Invalid lot data!");
            }
        }
    }

    public static void UserRegistration(Request req, Response res) {
        using (var reader = new StreamReader(req.InputStream, new UTF8Encoding())) {
            var requestBody = reader.ReadToEnd();

            var newUser = JsonSerializer.Deserialize<User>(requestBody);

            if (newUser != null) {
                db.Users.Add(newUser);
                db.SaveChanges();
                res.ResponseText("User was registered.");
            } else {
                res.ResponseError((int)HttpStatusCode.BadRequest, "Invalid user!");
            }
        }
    }
}