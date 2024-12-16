namespace AucSite;

public partial class ApiImpl {
    public static void GetUsers(Request req, Response res) {
        res.ContentType = "application/json; charset=utf-8";

        var jsonResponse = JsonSerializer.Serialize(
            db.Users.ToArray()
        );

        res.StatusCode = (int)HttpStatusCode.OK;
        using (var writer = new StreamWriter(res.OutputStream, new UTF8Encoding())) {
            writer.Write(jsonResponse);
            writer.Flush();
        }
    }

    public static void GetAuctionById(Request req, Response res) {
        int auctionId = int.Parse(req.QueryString["id"]);
        if (auctionId != null) {
            var auction = db.Auctions.FirstOrDefault(a => a.id_auction == auctionId);

            if (auction != null) {
                res.ContentType = "application/json; charset=utf-8";
                var jsonResponse = auction.ToJSON();
                res.StatusCode = (int)HttpStatusCode.OK;
                using (var writer = new StreamWriter(res.OutputStream, new UTF8Encoding())) {
                    writer.Write(jsonResponse);
                    writer.Flush();
                }
            } else {
                res.StatusCode = (int)HttpStatusCode.NotFound;
                res.ResponseText("Auction not found!");
            }
        } else {
            res.StatusCode = (int)HttpStatusCode.BadRequest;
            res.ResponseText("Auction ID is required!");
        }
    }   
    
    public static void GetLotById(Request req, Response res) {
        int lotId = int.Parse(req.QueryString["id"]);
        if (lotId != null) {
            var lot = db.Lots.FirstOrDefault(l => l.id_lot == lotId);

            if (lot != null) {
                res.ContentType = "application/json; charset=utf-8";
                var jsonResponse = lot.ToJSON();
                res.StatusCode = (int)HttpStatusCode.OK;
                using (var writer = new StreamWriter(res.OutputStream, new UTF8Encoding())) {
                    writer.Write(jsonResponse);
                    writer.Flush();
                }
            } else {
                res.StatusCode = (int)HttpStatusCode.NotFound;
                res.ResponseText("Lot not found!");
            }
        } else {
            res.StatusCode = (int)HttpStatusCode.BadRequest;
            res.ResponseText("Lot ID is required!");
        }
    }
}