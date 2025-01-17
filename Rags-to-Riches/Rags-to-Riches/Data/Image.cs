﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data;

public partial class Image
{
    [Key]
    public int ImageId { get; set; }
    public int ProductId { get; set; }

    public string? Image1 { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;
}
