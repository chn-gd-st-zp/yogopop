namespace DForge.Infrastructure.DTO;

public class DTOSysAccessRecordSingleResult : DTOSysAccessRecordResult
{
    [JsonIgnore, PropertyHidden]
    public string Content { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [Description("描述")]
    public List<AccessRecordDescription> Description
    {
        get
        {
            var result = default(List<AccessRecordDescription>);

            try
            {
                result = Content.ToObject<List<AccessRecordDescription>>();
            }
            catch
            {
                //
            }

            return result;
        }
    }

    /// <summary>
    /// 执行结果
    /// </summary>
    [Description("执行结果")]
    [JsonIgnore, PropertyHidden]
    public string ExecResult { get; set; }
}