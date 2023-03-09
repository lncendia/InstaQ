using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.Proxy;

public class ProxySearchQueryViewModel
{
    public int Page { get; set; } = 1;
    
    [RegularExpression("^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\\.|$)){4}$")]
    public string? Host { get; set; }
}