using System.ComponentModel.DataAnnotations;

namespace InstaQ.WEB.ViewModels.ReportsManager;

public class PublicationReportCreateViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Введите хештег")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Hashtag { get; set; } = null!;
    
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Загрузить всех участников")]
    public bool AllParticipants { get; set; }

    [Display(Name = "Укажите дату начала поиска публикаций")]
    [DataType(DataType.DateTime)]
    public DateTimeOffset? SearchStartDate { get; set; }

    [Display(Name = "Выберите соавторов")] public List<Guid>? CoAuthors { get; set; }

    [Display(Name = "Укажите через сколько начать")]
    [DataType(DataType.Time)]
    public TimeSpan? Timer { get; set; }
}