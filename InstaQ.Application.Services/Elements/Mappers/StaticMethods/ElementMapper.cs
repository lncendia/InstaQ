using InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;
using InstaQ.Domain.Reposts.PublicationReport.Entities;

namespace InstaQ.Application.Services.Elements.Mappers.StaticMethods;

internal static class ElementMapper
{
    public static void InitElementBuilder(PublicationElementBuilder builder, PublicationReportElement element)
    {
        builder.WithAccepted(element.IsAccepted)
            .WithVip(element.Vip)
            .WithParticipantId(element.ParticipantId)
            .WithLikeChatName(element.LikeChatName)
            .WithName(element.Name)
            .WithPk(element.Pk);
        if (element.Note != null) builder.WithNote(element.Note);
    }
}