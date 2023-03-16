namespace ExcelToCSharpModelConverter.ExportedModels;

[SheetName("TradeItemGroup_Table")]
public TradeItemGroup_Table : BaseSheetModel
{
    public String ItemSubGroupKey { get; set; }
    public String ItemKey { get; set; }
    [InvalidValueType]
    public String EnchantLevel { get; set; }
    [HeaderName("%SelectRate")]
    [InvalidValueType]
    public String SelectRate { get; set; }
    [InvalidValueType]
    public String Condition { get; set; }
    [HeaderName("%MinCount")]
    [InvalidValueType]
    public String MinCount { get; set; }
    [HeaderName("%MaxCount")]
    [InvalidValueType]
    public String MaxCount { get; set; }
    [InvalidValueType]
    public String IntimacyVariation { get; set; }
    [InvalidValueType]
    public String ExplorationPoint { get; set; }
    [InvalidValueType]
    public String GetExpRate { get; set; }
    [HeaderName("# ItemName")]
    [InvalidValueType]
    public String ItemName { get; set; }
    public String isSellingItem { get; set; }
    [HeaderName("%MaxSellCount")]
    public String MaxSellCount { get; set; }
    public String isBuyItem { get; set; }
    public String NeedAmountForStock { get; set; }
    public String MinRemainAmountForStock { get; set; }
    public String MaxRemainAmountForStock { get; set; }
    public String VariedAmountPerTickForStock { get; set; }
    [HeaderName("%ReverseAmountForStock")]
    public String ReverseAmountForStock { get; set; }
    [HeaderName("%MinSteadyAmountForStock")]
    public String MinSteadyAmountForStock { get; set; }
    [HeaderName("%MaxSteadyAmountForStock")]
    public String MaxSteadyAmountForStock { get; set; }
    [HeaderName("#재고 변화량(1% 가격 변경)")]
    public String _1 { get; set; }
    [HeaderName("#재고 변화율(%, Tick 당)")]
    public String Tick { get; set; }
    [HeaderName("#필요 Tick")]
    public String Tick { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
