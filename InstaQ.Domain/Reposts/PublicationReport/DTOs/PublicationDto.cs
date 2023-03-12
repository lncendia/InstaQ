namespace InstaQ.Domain.Reposts.PublicationReport.DTOs;

public class PublicationDto
{
    public PublicationDto(string pk, string ownerPk, string code)
    {
        OwnerPk = ownerPk;
        Code = code;
        Pk = pk;
    }

    public string OwnerPk { get; }
    public string Pk { get; }
    public string Code { get; }
}