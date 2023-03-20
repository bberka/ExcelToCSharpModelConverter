namespace XSharp.Shared.Abstract;

public interface IXCellValidator : IXBaseValidator
{
    public bool IsIgnore(object? value);

    public object? GetValidValue(object? value);
}