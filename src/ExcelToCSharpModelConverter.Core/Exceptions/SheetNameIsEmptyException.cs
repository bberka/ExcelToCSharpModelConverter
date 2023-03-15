namespace ExcelToCSharpModelConverter.Core.Exceptions;

public class SheetNameIsEmptyException : Exception
{
    public SheetNameIsEmptyException() : base("Sheet name is empty")
    {
        
    }
}