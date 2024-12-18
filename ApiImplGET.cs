namespace AucSite;

public partial class ApiImpl {
    static void SendContent(in Response res, string json_content) {
        res.ContentType = "application/json; charset=utf-8";
        res.StatusCode = (int)HttpStatusCode.OK;
        using (var writer = new StreamWriter(res.OutputStream, new UTF8Encoding())) {
            writer.Write(json_content);
            writer.Flush();
        }
    }
    public static void GetUsers(Request req, Response res) => SendContent(in res,
        JsonSerializer.Serialize(
            db.Users.ToArray()
        )
    );

    public static void GetAuctions(Request req, Response res) => SendContent(in res,
        JsonSerializer.Serialize(
            db.Auctions.ToArray()
        )
    );

    public static void GetLots(Request req, Response res) => SendContent(in res,
        JsonSerializer.Serialize(
            db.Lots.ToArray()
        )
    );

    public static void GetAuctionById(Request req, Response res) {
        int auctionId;
        if (int.TryParse(req.QueryString["id"], out auctionId)) {
            var auction = db.Auctions.FirstOrDefault(a => a.id_auction == auctionId);

            if (auction != null) {
                SendContent(in res, auction.ToJSON());
            } else {
                res.ResponseError((int)HttpStatusCode.NotFound, "Auction not found!");
            }
        } else {
            res.ResponseError((int)HttpStatusCode.BadRequest, "Auction ID is required!");
        }
    }   
    
    public static void GetLotById(Request req, Response res) {
        int lotId;
        if(int.TryParse(req.QueryString["id"], out lotId)) {
            var lot = db.Lots.FirstOrDefault(l => l.id_lot == lotId);

            if (lot != null) {
                SendContent(in res, lot.ToJSON());
            } else
                res.ResponseError((int)HttpStatusCode.NotFound, "Lot not found!");
        } else
            res.ResponseError((int)HttpStatusCode.BadRequest, "Lot ID is required!");
    }

    public static void GetLotsForAuction(Request req, Response res) {
        int aucId;
        if(int.TryParse(req.QueryString["auc_id"], out aucId)){
            var lots = JsonSerializer.Serialize(
                (from l in db.Lots
                where l.id_auction == aucId
                select l).ToArray()
            );
            SendContent(in res, lots);
        } else
            res.ResponseError((int)HttpStatusCode.BadRequest, "Auction id is required!");
    }
}