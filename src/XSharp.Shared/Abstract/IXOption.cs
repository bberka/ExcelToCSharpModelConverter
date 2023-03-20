using Microsoft.Extensions.Logging;
using ValueType = XSharp.Shared.Constants.ValueType;

namespace XSharp.Shared.Abstract;

public interface IXOption
{
    //string? InFolderPath { get; set; }
    //string? OutFolderPath { get; set; }

    /// <summary>
    ///     Namespace of the generated files.
    /// </summary>
    string NameSpace { get; set; }

    /// <summary>
    ///     Minimum log level to log.
    /// </summary>
    LogLevel MinimumLogLevel { get; set; }

    /// <summary>
    ///     Default value type for the model class.
    /// </summary>
    ValueType DefaultValueType { get; set; }

    /// <summary>
    ///     Inheritance string for the model class.
    /// </summary>
    string ModelInheritanceString { get; set; }

    /// <summary>
    ///     Path to the dll file that contains the extend validator for use of the XSharp.App.
    /// </summary>
    string ExtendValidatorDllFilePath { get; set; }

    /// <summary>
    ///     Column number to set header names.
    /// </summary>
    long HeaderColumnNumber { get; set; }

    /// <summary>
    ///     Row number to set value types.
    /// </summary>
    int SetValueTypesAtRowNumber { get; set; }

    /// <summary>
    ///     Using assembly list to be added to the top of the generated files.
    /// </summary>
    List<string> UsingNameSpaceList { get; set; }

    /// <summary>
    ///     String values that will be converted to null value.
    /// </summary>
    List<string> NullValueStrings { get; set; }
}