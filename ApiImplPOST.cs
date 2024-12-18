namespace AucSite;

public partial class ApiImpl {

    public static void CreateAuction(Request req, Response res) {
        var form_info = req.ParseForm();
        Auction a = new() {
            auct_name = form_info["auct_name"],
            starting_date = DateOnly.Parse(form_info["starting_date"]),
            ending_date = DateOnly.Parse(form_info["ending_date"]),
        };
        db.Auctions.Add(a);
        db.SaveChanges();
        res.ResponseText("Auction was added.");
    }

    public static void CreateLot(Request req, Response res) {
        var form_info = req.ParseForm();
        Lot l = new() {
            lot_name = form_info["lot_name"],
            description = form_info["description"],
            start_price = int.Parse(form_info["start_price"]),
        };
        db.Lots.Add(l);
        db.SaveChanges();
        res.ResponseText("Lot was added.");
    }
}