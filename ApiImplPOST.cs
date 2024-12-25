namespace AucSite;

#nullable disable

public partial class ApiImpl {
    public static void CreateAuction(Request req, Response res)
    {
        var form_info = req.ParseForm();
        Auction a = new()
        {
            auct_name = form_info["auct_name"],
            starting_date = DateOnly.Parse(form_info["starting_date"]),
            ending_date = DateOnly.Parse(form_info["ending_date"]),
            auct_step = int.Parse(form_info["auct_step"]),
        };

        db.Auctions.Add(a);
        db.SaveChanges();

        int auctionId = a.id_auction;

        var selectedLotIds = form_info["lot_ids"].Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray();
        foreach (var lotIdStr in selectedLotIds)
        {
            if (int.TryParse(lotIdStr, out int lotId))
            {
                var lot = db.Lots.Find(lotId);
                if (lot != null)
                {
                    lot.id_auction = auctionId;
                }
            }
        }

        db.SaveChanges();
        res.ResponseText("Auction was added and lot was update");
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

    public static void RedactAuction(Request req, Response res)
    {
        var form_info = req.ParseForm();

        int.TryParse(form_info["id_auction"], out int auctionId);

        Auction auction = db.Auctions.Find(auctionId);

        auction.auct_name = form_info["auct_name"];
        auction.starting_date = DateOnly.Parse(form_info["starting_date"]);
        auction.ending_date = DateOnly.Parse(form_info["ending_date"]);
        auction.auct_step = int.Parse(form_info["auct_step"]);


        var selectedLotIds = form_info["lot_in_auc"].Split(',').Select(id => int.Parse(id)).ToList();
        var existingLots = db.Lots.Where(lot => lot.id_auction == auctionId).ToList();
        foreach (var lot in existingLots)
        {
            lot.id_auction = null;
        }

        foreach (int lotId in selectedLotIds)
        {
            var lot = db.Lots.Find(lotId);
            if (lot != null)
            {
                lot.id_auction = auctionId;
            }
        }

        db.SaveChanges();
        res.ResponseText("Auc was updated");
    }

    public static void DeleteAuction(Request req, Response res)
    {
        var form_info = req.ParseForm();
        
        int.TryParse(form_info["id_auction"], out int auctionId);

        Auction auction = db.Auctions.Find(auctionId);
        var associatedLots = db.Lots.Where(lot => lot.id_auction == auctionId).ToList();
        
        foreach (var lot in associatedLots)
        {
            lot.id_auction = null;
        }

        db.SaveChanges();

        db.Auctions.Remove(auction);
        db.SaveChanges();

        res.ResponseText("Auction was deleted");
    }
}