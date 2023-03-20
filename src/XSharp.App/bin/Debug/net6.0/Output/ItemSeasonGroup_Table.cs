using System;

namespace XSharp.Test.ExportedModels;

public class ItemSeasonGroup_Table : XSheetBase,BaseSheetModel
{
    public String ItemSubGroupKey { get; set; }
    public String ItemKey { get; set; }
    [InvalidValueType]
    public String EnchantLevel { get; set; }
    [InvalidValueType]
    public String DoPetAddDrop { get; set; }
    [InvalidValueType]
    public String DoSechiAddDrop { get; set; }
    [HeaderName("%SelectRate_0")]
    [InvalidValueType]
    public String SelectRate__0 { get; set; }
    [HeaderName("%MinCount_0")]
    [InvalidValueType]
    public String MinCount__0 { get; set; }
    [HeaderName("%MaxCount_0")]
    [InvalidValueType]
    public String MaxCount__0 { get; set; }
    [HeaderName("%SelectRate_1")]
    [InvalidValueType]
    public String SelectRate__1 { get; set; }
    [HeaderName("%MinCount_1")]
    [InvalidValueType]
    public String MinCount__1 { get; set; }
    [HeaderName("%MaxCount_1")]
    [InvalidValueType]
    public String MaxCount__1 { get; set; }
    [HeaderName("%SelectRate_2")]
    [InvalidValueType]
    public String SelectRate__2 { get; set; }
    [HeaderName("%MinCount_2")]
    [InvalidValueType]
    public String MinCount__2 { get; set; }
    [HeaderName("%MaxCount_2")]
    [InvalidValueType]
    public String MaxCount__2 { get; set; }
    public ExcelErrorValue IntimacyVariation { get; set; }
    [InvalidValueType]
    public String ExplorationPoint { get; set; }
    [InvalidValueType]
    public String ApplyRandomPrice { get; set; }
    [InvalidValueType]
    public String RentTime { get; set; }
    [InvalidValueType]
    public String PriceOption { get; set; }
    [HeaderName("# ItemName")]
    public String ItemName { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
