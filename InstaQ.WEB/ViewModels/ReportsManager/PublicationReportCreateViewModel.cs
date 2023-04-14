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
    public bool AllParticipants { get; set; } = true;


    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Укажите количество публикаций")]
    [Range(1, 500, ErrorMessage = "Количество публикаций должно быть в диапазоне от 100 до 500")]
    public int CountPublicationsToGet { get; set; }

    [Display(Name = "Выберите соавторов")] public List<Guid>? CoAuthors { get; set; }

    [Display(Name = "Укажите через сколько начать")]
    [DataType(DataType.Time)]
    public TimeSpan? Timer { get; set; }
}