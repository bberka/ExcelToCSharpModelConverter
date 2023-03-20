namespace XSharp.Shared.Abstract;

public interface IXHeaderValidator: IXBaseValidator
{
    public bool IsIgnore(IXHeader header);
    
}