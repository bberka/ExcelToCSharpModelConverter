using EasMe.Result;
using XSharp.Shared.Models;

namespace XSharp.Shared.Abstract;

public interface IXFile
{
    string Name { get; internal set;}
    List<IXSheet> Sheets { get; internal set;}
    void SetName(string name);
    void SetSheets(List<IXSheet> sheets);
    Result AddSheet(IXSheet sheet);
}