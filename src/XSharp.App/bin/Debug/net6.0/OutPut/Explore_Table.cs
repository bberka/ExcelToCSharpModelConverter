namespace ExcelToCSharpModelConverter.ExportedModels;

public Explore_Table : BaseSheetModel
{
    public String GivingDropItemKeyOnDiscovery { get; set; }
    [InvalidValueType]
    public String GivingDropItemKeyOnInvestment { get; set; }
    [HeaderName("~Name")]
    [InvalidValueType]
    public String Name { get; set; }
    [HeaderName("^WaypointKey")]
    [InvalidValueType]
    public String WaypointKey { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
