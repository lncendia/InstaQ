using InstaQ.WEB.ViewModels.Elements.Base;
using InstaQ.Application.Abstractions.Elements.DTOs.ElementDto;

namespace InstaQ.WEB.Mappers.Abstractions;

public interface IElementMapperUnit<in TElement, out TViewModel> where TElement : ElementDto
    where TViewModel : ElementViewModel
{
    TViewModel Map(TElement element);
}