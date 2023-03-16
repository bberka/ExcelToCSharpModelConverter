namespace ExcelToCSharpModelConverter.ExportedModels;

public List : BaseSheetModel
{
    public String Index { get; set; }
    [InvalidValueType]
    public String DisplayName { get; set; }
    [InvalidValueType]
    public String DropKey { get; set; }
    [HeaderName("Nod Key")]
    [InvalidValueType]
    public String NodKey { get; set; }
    [HeaderName("Nod Name")]
    [InvalidValueType]
    public String NodName { get; set; }
    [HeaderName("#비고1")]
    [InvalidValueType]
    public String _1 { get; set; }
    [HeaderName("#비고2")]
    [InvalidValueType]
    public String _2 { get; set; }
    [HeaderName("#비고3")]
    [InvalidValueType]
    public String _3 { get; set; }
    [InvalidValueType]
    public String Intimacy { get; set; }
    [HeaderName("#^E_Merge Index")]
    public ExcelErrorValue E_MergeIndex { get; set; }
}
