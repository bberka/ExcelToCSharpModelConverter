using System.ComponentModel.DataAnnotations.Schema;

namespace XSharp.Models;

public class XSheet<T>
{
  private IEnumerable<XRow<T>>? _enumerableRows;
  private List<XRow<T>>? _readRows;
  public Type SheetModelType => typeof(T);
  public string FileName { get; set; }
  public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FileName);
  public string Name { get; set; }
  public string? FixedName { get; set; }
  public ExcelAddressBase Dimension { get; set; }

  /// <summary>
  ///   Returns rows as List if read once
  /// </summary>
  public IEnumerable<XRow<T>> Rows {
    get {
      if (_enumerableRows is null) throw new ArgumentNullException(nameof(Rows));
      _readRows ??= _enumerableRows.ToList();
      return _readRows;
    }
    set => _enumerableRows = value;
  }


  public List<XHeader> Headers { get; set; }
}

public class XSheet
{
  private IEnumerable<XRow<object>>? _enumerableRows;
  private List<XRow<object>>? _readRows;
  public string FileName { get; set; }
  public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(FileName);

  public string Name { get; set; }
  public string? FixedName { get; set; }
  [NotMapped]
  public ExcelAddressBase Dimension { get; set; }

  /// <summary>
  ///   Returns rows as List if read once
  /// </summary>
  public IEnumerable<XRow<object>> Rows {
    get {
      if (_enumerableRows is null) throw new ArgumentNullException(nameof(Rows));
      _readRows ??= _enumerableRows.ToList();
      return _readRows;
    }
    set => _enumerableRows = value;
  }


  public List<XHeader> Headers { get; set; }

  public IEnumerable<XRow<T>> GetRowsAs<T>() {
    return Rows.Select(x => new XRow<T> {
      Index = x.Index,
      Data = (T)x.Data
    });
  }

  public XSheet<T> GetAsXSheetT<T>() {
    var sheet = new XSheet<T> {
      Name = Name,
      FixedName = FixedName,
      Rows = GetRowsAs<T>(),
      Dimension = Dimension,
      FileName = FileName,
      Headers = Headers
    };
    return sheet;
  }
}