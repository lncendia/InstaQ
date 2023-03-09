using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.ReportsManager;

public class ParticipantReportCreateViewModel
{
    [Display(Name = "Укажите через сколько начать")]
    [DataType(DataType.Time)]
    public TimeSpan? Timer { get; set; }
}