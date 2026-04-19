namespace DForge.Database.EF;

[DIModeForService(DIModeEnum.Exclusive, typeof(IAccessRecordRepository))]
public partial class SysAccessRecordRepository
{
    public async Task<bool> CreateAsync(AccessRecord accessRecord)
    {
        if (accessRecord.ExecResult is Task)
        {
            var task = accessRecord.ExecResult as Task;
            var awaiter = task.GetAwaiter();
            awaiter.GetResult();

            accessRecord.ExecResult = task.GetType().GetProperty("Result").GetValue(task);
        }

        var groupID = string.Empty;

        var obj = new TBSysAccessRecord
        {
            GroupID = groupID,
            RoleType = accessRecord.RoleType,
            AccountID = accessRecord.AccountID,
            UserName = accessRecord.UserName,
            Action = accessRecord.Code.ToEnum<GlobalPermissionEnum>().GetDesc(),
            OperationType = accessRecord.OperationType,
            TBName = accessRecord.TBName,
            TBValue = accessRecord.TBValue,
            PKName = accessRecord.PKName,
            PKValue = accessRecord.PKValue,
            TriggerName = accessRecord.TriggerName,
            Content = accessRecord.Descriptions.ToJson(),
            ExecResult = accessRecord.ExecResult.ToJson(),
            CreateTime = DateTimeExtension.Now
        };

        return DBContext.Create(obj);
    }
}