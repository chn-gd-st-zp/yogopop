namespace YogoPop.Component.Permission;

public class AccessRecord
{
    public string Code { get; set; } = string.Empty;

    public RoleEnum RoleType { get; set; } = RoleEnum.None;

    public string AccountID { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public OperationTypeEnum OperationType { get; set; } = OperationTypeEnum.None;

    public string TBName { get; set; } = string.Empty;

    public string TBValue { get; set; } = string.Empty;

    public string PKName { get; set; } = string.Empty;

    public string PKValue { get; set; } = string.Empty;

    public string TriggerName { get; set; } = string.Empty;

    public List<AccessRecordDescription> Descriptions { get; set; } = null;

    public object ExecResult { get; set; } = null;
}