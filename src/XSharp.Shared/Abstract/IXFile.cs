
namespace XSharp.Shared.Abstract;

public interface IXFile
{
    string Name { get; set; }
    List<XSheet<object>> Sheets { get; set; }

}
