namespace XSharp.Shared.Abstract;

public interface IXFileNameValidator: IXBaseValidator
{
    public bool IsIgnore(string filePath);
}