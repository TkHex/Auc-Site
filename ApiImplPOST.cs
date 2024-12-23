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

        var selectedLotIds = form_info["lot_ids[]"];

        if (!string.IsNullOrEmpty(selectedLotIds)) {
            var lotIds = selectedLotIds.Split(',').Select(id_lot => int.Parse(id_lot.Trim())).ToArray();

            foreach (var id_lot in lotIds) {
                var lot = db.Lots.Find(id_lot);
                if (lot != null) {
                    lot.id_auction = a.id_auction;
                }
            }
            db.SaveChanges();
        }
        res.ResponseText("Auction was added and lots updated.");
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