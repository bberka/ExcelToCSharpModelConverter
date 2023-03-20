using XSharp.Shared.Attributes;
using XSharp.Shared.Models;

namespace XSharp.Test;

public class ItemSubGroup_Table : XSheetBase
{
    public string ItemSubGroupKey { get; set; }
    public string ItemKey { get; set; }

    [XCellValueTypeInvalid] public string EnchantLevel { get; set; }

    [XCellValueTypeInvalid] public string DoPetAddDrop { get; set; }

    [XCellValueTypeInvalid] public string DoSechiAddDrop { get; set; }

    [XHeaderName("%SelectRate_0")]
    [XCellValueTypeInvalid]
    public string SelectRate__0 { get; set; }

    [XHeaderName("%MinCount_0")]
    [XCellValueTypeInvalid]
    public string MinCount__0 { get; set; }

    [XHeaderName("%MaxCount_0")]
    [XCellValueTypeInvalid]
    public string MaxCount__0 { get; set; }

    [XHeaderName("%SelectRate_1")]
    [XCellValueTypeInvalid]
    public string SelectRate__1 { get; set; }

    [XHeaderName("%MinCount_1")]
    [XCellValueTypeInvalid]
    public string MinCount__1 { get; set; }

    [XHeaderName("%MaxCount_1")]
    [XCellValueTypeInvalid]
    public string MaxCount__1 { get; set; }

    [XHeaderName("%SelectRate_2")]
    [XCellValueTypeInvalid]
    public string SelectRate__2 { get; set; }

    [XHeaderName("%MinCount_2")]
    [XCellValueTypeInvalid]
    public string MinCount__2 { get; set; }

    [XHeaderName("%MaxCount_2")]
    [XCellValueTypeInvalid]
    public string MaxCount__2 { get; set; }

    public double IntimacyVariation { get; set; }

    [XCellValueTypeInvalid] public string ExplorationPoint { get; set; }

    [XCellValueTypeInvalid] public string ApplyRandomPrice { get; set; }

    [XCellValueTypeInvalid] public string RentTime { get; set; }

    [XCellValueTypeInvalid] public string PriceOption { get; set; }

    [XHeaderName("# ItemName")] public string ItemName { get; set; }

    [XHeaderName("#^E_Merge Index")] public string E_MergeIndex { get; set; }
}
