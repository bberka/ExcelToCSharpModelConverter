namespace ExcelToCSharpModelConverter.ExportedModels;

public Theme_Table : BaseSheetModel
{
    [HeaderName("^Theme")]
    public String Theme { get; set; }
    [HeaderName("~Name")]
    [InvalidValueType]
    public String Name { get; set; }
    [HeaderName("표시용2")]
    [InvalidValueType]
    public String _2 { get; set; }
    [HeaderName("표시용1")]
    [InvalidValueType]
    public String _1 { get; set; }
    [InvalidValueType]
    public String Parent { get; set; }
    [InvalidValueType]
    public String IncreaseWP { get; set; }
    [HeaderName("#CountCard")]
    [InvalidValueType]
    public String CountCard { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
