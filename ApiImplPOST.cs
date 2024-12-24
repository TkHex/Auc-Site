namespace AucSite;

#nullable disable

public partial class ApiImpl {
    static void auc_create(Request req, Response res) {        
        var form_info = req.ParseForm();
        
        Auction a = new() {
            auct_name = form_info["auct_name"],
            starting_date = DateOnly.Parse(form_info["starting_date"]),
            ending_date = DateOnly.Parse(form_info["ending_date"]),
        };
        
        db.Auctions.Add(a);
        db.SaveChanges();

        var lotIds = (
            from fd in form_info
            where fd.Key.Contains("lot_ids")
            select Convert.ToInt32(fd.Value)
        ).ToArray();

        foreach (var id_lot in lotIds) {
            var lot = db.Lots.Find(id_lot);
            if (lot != null) {
                lot.id_auction = a.id_auction;
            }
        }

        db.SaveChanges();
        res.ResponseText("Auction was added and lots updated.");
    }

    public static void CreateAuction(Request req, Response res) {
        try {
            auc_create(req, res);
        } catch (Exception err) {
            logger.Error("CreateAuction", err.Message);
            res.ResponseError(500, "Exception, youpta");
        }
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

    public static void RedactAuction(Request req, Response res) {
        var form_info = req.ParseForm();

        int.TryParse(form_info["id_auction"], out int auctionId);
        Auction a = db.Auctions.Find(auctionId);

        a.auct_name = form_info["auct_name"];
        a.starting_date = DateOnly.Parse(form_info["starting_date"]);
        a.ending_date = DateOnly.Parse(form_info["ending_date"]);
        a.auct_step = int.Parse(form_info["auct_step"]);

        db.SaveChanges();
        res.ResponseText("Аукцион успешно обновлен.");
    }
}