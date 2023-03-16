namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("AlchemyStoneChange_Table")]
public AlchemyStoneChange_Table : BaseSheetModel
{
    public String ItemKey { get; set; }
    [HeaderName("#ItemName")]
    public String ItemName { get; set; }
    [InvalidValueType]
    public String NeedItemKey { get; set; }
    [HeaderName("#NeedItemName")]
    [InvalidValueType]
    public String NeedItemName { get; set; }
    [InvalidValueType]
    public String NeedItemCount { get; set; }
    [InvalidValueType]
    public String MainGroup { get; set; }
    [InvalidValueType]
    public String BreakRate { get; set; }
    [InvalidValueType]
    public String DownRate { get; set; }
    [InvalidValueType]
    public String DownGroup { get; set; }
    [HeaderName("#DropList")]
    [InvalidValueType]
    public String DropList { get; set; }
    [InvalidValueType]
    public String Condition { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
