namespace AucSite;

public partial class ApiImpl {
    
    public static void CreateAuction(Request req, Response res) {
        var form_info = req.ParseForm();
        Auction a = new() {
            auct_name = form_info["auct_name"],
            lot_name = form_info["lot_name"],
            starting_date = form_info["starting_date"],
            ending_date = form_info["ending_date"],
            auct_step = form_info["auct_step"],
        };
        db.Lots.Add(a);
        db.SaveChanges();
        res.ResponseText("Auction was added.");
    }

    public static void CreateLot(Request req, Response res) {
        var form_info = req.ParseForm();
        Lot l = new() {
            lot_name = form_info["lot_name"],
            description = form_info["description"],
            start_price = form_info["start_price"],
        };
        db.Lots.Add(l);
        db.SaveChanges();
        res.ResponseText("Lot was added.");
    }

    public static void UserRegistration(Request req, Response res) {
        var form_info = req.ParseForm();
        User u = new() {
            login = form_info["login"],
            lastname = form_info["lastname"],
            name = form_info["name"],
            middle_name = form_info["middle_name"],
            bank_info = form_info["bank_info"],
            password = form_info["password"], //Тут будет хеша!
        };
        db.Users.Add(u);
        db.SaveChanges();
        res.ResponseText("User was registered.");
    }
}