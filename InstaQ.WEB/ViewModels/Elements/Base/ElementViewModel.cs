namespace InstaQ.WEB.ViewModels.Elements.Base;

public abstract class ElementViewModel
{
    protected ElementViewModel(string name, string pk)
    {
        Name = name;
        Pk = pk;
    }

    public string Name { get; }
    public string Pk { get; }
}