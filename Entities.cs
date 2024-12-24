using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using date = System.DateOnly;
using System.Text.Json;
using System.Text;

namespace AucSite;

[Table("users")]
public class User {
    [Key]
    public int id_user { get; set; }
    public string? lastname { get; set; }
    public string? name { get; set; }
    public string? middle_name { get; set; }
    public string? login { get; set; }
    public string? password { get; set; }
    public string? bank_info { get; set; }
    public int? id_role { get; set; }

    public string ToJSON() => JsonSerializer.Serialize(this);
}

[Table("auctions")]
public class Auction {
    [Key]
    public int id_auction { get; set; }
    public string? auct_name { get; set; }
    public date starting_date { get; set; }
    public date ending_date { get; set; }
    public int? auct_step { get; set; }
    public string ToJSON() => JsonSerializer.Serialize(this);
}

[Table("lots")]
public class Lot {
    [Key]
    public int id_lot { get; set; }
    public string? lot_name { get; set; }
    public string? description { get; set; }
    public int? start_price { get; set; }
    public int? id_auction { get; set; }
    public string ToJSON() => JsonSerializer.Serialize(this);
}