
namespace XSharp.Shared.Abstract;

public interface IXFile
{
    string Name { get; }
    List<XSheet<object>> Sheets { get; }
    void SetName(string name);
    void SetSheets(List<XSheet<object>> sheets);
    Result AddSheet(XSheet<object> sheet);
}
