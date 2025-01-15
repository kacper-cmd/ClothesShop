using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data;

public partial class Address
{
    public int HomeNumber { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? ZipCode { get; set; }

    public string? Country { get; set; }

    [Key]
    public int AddressesId { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
