namespace InstaQ.Domain.Reposts.CommentReport.DTOs;

public class CommentsDto
{
    public CommentsDto(string pk, IReadOnlyCollection<(string, string)> comments)
    {
        Pk = pk;
        Comments = comments;
        SuccessLoaded = true;
    }

    public CommentsDto(string pk)
    {
        Pk = pk;
        SuccessLoaded = false;
    }
    
    public string Pk { get; }
    public IReadOnlyCollection<(string, string)>? Comments { get; }
    public bool SuccessLoaded { get; }
}