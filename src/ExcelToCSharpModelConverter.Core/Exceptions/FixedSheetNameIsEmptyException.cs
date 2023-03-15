namespace ExcelToCSharpModelConverter.Core.Exceptions;

public class FixedSheetNameIsEmptyException : Exception
{
    public FixedSheetNameIsEmptyException() : base("Fixed Sheet name is empty")
    {
        
    }
}