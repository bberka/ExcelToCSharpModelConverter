using System;

namespace XSharp.Test.ExportedModels;

[SheetName("Character_TableMonster")]
public class Character_TableMonster : XSheetBase,BaseSheetModel
{
    public String Index { get; set; }
    public String CharName { get; set; }
    [InvalidValueType]
    public String DisplayName { get; set; }
    [InvalidValueType]
    public String CharacterTitle { get; set; }
    [InvalidValueType]
    public String IsHiddenName { get; set; }
    [InvalidValueType]
    public String CharKind { get; set; }
    [InvalidValueType]
    public String GradeType { get; set; }
    [InvalidValueType]
    public String AttributeType { get; set; }
    [InvalidValueType]
    public String Level { get; set; }
    [InvalidValueType]
    public String Exp { get; set; }
    [InvalidValueType]
    public String SkillPointExp { get; set; }
    [InvalidValueType]
    public String DropID { get; set; }
    [InvalidValueType]
    public String CampFireDropID { get; set; }
    [InvalidValueType]
    public String InstanceDropID { get; set; }
    [InvalidValueType]
    public String CollectDropID { get; set; }
    [InvalidValueType]
    public String VanishTime { get; set; }
    [InvalidValueType]
    public String LootAuthorityTime { get; set; }
    [InvalidValueType]
    public String PosthumousTreatable { get; set; }
    [InvalidValueType]
    public String HP { get; set; }
    [InvalidValueType]
    public String MP { get; set; }
    [InvalidValueType]
    public String HPRegen { get; set; }
    [InvalidValueType]
    public String MPRegen { get; set; }
    [InvalidValueType]
    public String Weight { get; set; }
    [InvalidValueType]
    public String CriticalRate { get; set; }
    [InvalidValueType]
    public String DDD { get; set; }
    [InvalidValueType]
    public String DHIT { get; set; }
    [InvalidValueType]
    public String DDV { get; set; }
    [InvalidValueType]
    public String HDDV { get; set; }
    [InvalidValueType]
    public String DPV { get; set; }
    [InvalidValueType]
    public String HDPV { get; set; }
    [InvalidValueType]
    public String RDD { get; set; }
    [InvalidValueType]
    public String RHIT { get; set; }
    [InvalidValueType]
    public String RDV { get; set; }
    [InvalidValueType]
    public String HRDV { get; set; }
    [InvalidValueType]
    public String RPV { get; set; }
    [InvalidValueType]
    public String HRPV { get; set; }
    [InvalidValueType]
    public String MDD { get; set; }
    [InvalidValueType]
    public String MHIT { get; set; }
    [InvalidValueType]
    public String MDV { get; set; }
    [InvalidValueType]
    public String HMDV { get; set; }
    [InvalidValueType]
    public String MPV { get; set; }
    [InvalidValueType]
    public String HMPV { get; set; }
    [InvalidValueType]
    public String FireElementResist { get; set; }
    [InvalidValueType]
    public String IceElementResist { get; set; }
    [InvalidValueType]
    public String LightningElementResist { get; set; }
    [InvalidValueType]
    public String PoisonElementResist { get; set; }
    [InvalidValueType]
    public String FireElementAttackDamage { get; set; }
    [InvalidValueType]
    public String IceElementAttackDamage { get; set; }
    [InvalidValueType]
    public String LightningElementAttackDamage { get; set; }
    [InvalidValueType]
    public String PoisonElementAttackDamage { get; set; }
    [InvalidValueType]
    public String VariedAttackSpeedRate { get; set; }
    [InvalidValueType]
    public String VariedMoveSpeedRate { get; set; }
    [InvalidValueType]
    public String VariedCastingSpeedRate { get; set; }
    [InvalidValueType]
    public String AttackRange { get; set; }
    [InvalidValueType]
    public String Logging { get; set; }
    [InvalidValueType]
    public String BodySize { get; set; }
    [InvalidValueType]
    public String BodyHeight { get; set; }
    [InvalidValueType]
    public String BoneCheck { get; set; }
    [InvalidValueType]
    public String RenderSize { get; set; }
    [InvalidValueType]
    public String AiScriptClassName { get; set; }
    [HeaderName("ActionScript FilePrefix")]
    [InvalidValueType]
    public String ActionScriptFilePrefix { get; set; }
    [HeaderName("ActionScript File0")]
    [InvalidValueType]
    public String ActionScriptFile_0 { get; set; }
    [InvalidValueType]
    public String VehicleType { get; set; }
    [InvalidValueType]
    public String IsFixed { get; set; }
    [InvalidValueType]
    public String IsCatchedable { get; set; }
    [InvalidValueType]
    public String ResetDistance { get; set; }
    [InvalidValueType]
    public String IsGiant { get; set; }
    [InvalidValueType]
    public String SpawnDelayTime { get; set; }
    [InvalidValueType]
    public String SpawnVariableTime { get; set; }
    [InvalidValueType]
    public String SpawnCondition { get; set; }
    [InvalidValueType]
    public String TalkAble { get; set; }
    [InvalidValueType]
    public String Tamable { get; set; }
    [InvalidValueType]
    public String DarkNightPowerUp { get; set; }
    [InvalidValueType]
    public String FusionGroupKey { get; set; }
    [InvalidValueType]
    public String IsPushable { get; set; }
    [InvalidValueType]
    public String IsClientAI { get; set; }
    [InvalidValueType]
    public String VehicleSeatCount { get; set; }
    [InvalidValueType]
    public String possessableWeight { get; set; }
    [InvalidValueType]
    public String CarriageType { get; set; }
    [InvalidValueType]
    public String VehicleDriverRidable { get; set; }
    [InvalidValueType]
    public String VoiceBankFileName { get; set; }
    [InvalidValueType]
    public String InventoryMax { get; set; }
    [InvalidValueType]
    public String ReSpawnType { get; set; }
    [HeaderName("#^E_Merge Index")]
    public String E_MergeIndex { get; set; }
}
