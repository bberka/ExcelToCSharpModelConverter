namespace ExcelToCSharpModelConverter.ExportedModels;

public NPCFunction : BaseSheetModel
{
    [HeaderName("^CharacterKey")]
    public String CharacterKey { get; set; }
    public String isIntimacy { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
