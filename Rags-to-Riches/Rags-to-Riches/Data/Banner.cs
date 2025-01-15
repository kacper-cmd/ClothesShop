using System;
using System.Collections.Generic;

namespace Data;

public partial class Banner
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? SubTitle { get; set; }

    public string? Description { get; set; }

    public string? ImageNameUrl { get; set; }

    public string ButtonText { get; set; } = null!;

    public string? ButtonUrl { get; set; }

    public bool IsActive { get; set; }

    public string? SocialLinks1 { get; set; }

    public string? SocialIcon1 { get; set; }

    public string? SocialLinks2 { get; set; }

    public string? SocialIcon2 { get; set; }

    public string? SocialLinks3 { get; set; }

    public string? SocialIcon3 { get; set; }

    public string? SocialLinks4 { get; set; }

    public string? SocialIcon4 { get; set; }
}
