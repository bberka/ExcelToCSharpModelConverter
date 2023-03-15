namespace ExcelToCSharpModelConverter.Core.Exceptions;

public class NoColumnHeadersFoundException : Exception
{
    public NoColumnHeadersFoundException() : base("No column headers found")
    {
        
    }
}
