namespace ExcelToCSharpModelConverter.ExportedModels;

public Sheet_1 : BaseSheetModel
{
    [HeaderName("신규 행 추가 시 꼭 E_Merge 컬럼 값을 설정해야 합니다. 행 끝에 있는 값보다 1 높은 숫자를 입력해야 합니다.")]
    [InvalidValueType]
    public String E_Merge_1 { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
