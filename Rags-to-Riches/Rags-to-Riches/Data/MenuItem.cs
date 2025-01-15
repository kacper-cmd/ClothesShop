using System;
using System.Collections.Generic;

namespace Data;

public partial class MenuItem
{
    public int IdWebsiteLinks { get; set; }

    public string Link { get; set; } = null!;

    public string LinkName { get; set; } = null!;

    public string? Controller { get; set; }

    public string? AdditionalInfo { get; set; }

    public string? Action { get; set; }

    public string? Icon { get; set; }

    public int Order { get; set; }

    public bool IsActive { get; set; }

    public bool? RightMenu { get; set; }
}
