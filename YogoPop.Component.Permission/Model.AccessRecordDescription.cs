namespace YogoPop.Component.Permission;

public class AccessRecordDescription
{
    public virtual string FieldName { get; set; }

    public virtual string FieldRemark { get; set; }

    public virtual object InputValue { get; set; }

    public virtual object DBValue { get; set; }
}