using System;
using date = System.DateOnly;

namespace AucSite;

public class User {
    public int id_user { get; set; }
    public string lastname { get; set; }
    public string name { get; set; }
    public string middlename { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string bank_info { get; set; }
    public int id_role { get; set; }
}

public class Auctions {
    public int id_auction { get; set; }
    public string auct_name { get; set; }
    public int id_lot  { get; set; }
    public date starting_date { get; set; }
    public date ending_date { get; set; }
}

public class Lots {
    public int id_lot { get; set; }
    public string lot_name { get; set; }
    public string description { get; set; }
    public int start_price { get; set; }
    public int id_auction { get; set; }
}