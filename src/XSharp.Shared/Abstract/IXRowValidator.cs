namespace XSharp.Shared.Abstract;

public interface IXRowValidator : IXBaseValidator
{
    public bool IsIgnore(IXHeader header, int rowIndex, object? cellValue);
}