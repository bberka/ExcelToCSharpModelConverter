namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("LimitedItemSubGroup_Table")]
public LimitedItemSubGroup_Table : BaseSheetModel
{
    public Double ItemSubGroupKey { get; set; }
    public Double ItemKey { get; set; }
    public Double EnchantLevel { get; set; }
    public Double PurchaseSubject { get; set; }
    public Double PurchaseCountLimit { get; set; }
    public String ResetType { get; set; }
    public String ADSummary { get; set; }
    [HeaderName("#ItemName")]
    public String ItemName { get; set; }
}
