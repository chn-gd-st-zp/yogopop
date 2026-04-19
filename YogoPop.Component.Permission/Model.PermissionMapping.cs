namespace YogoPop.Component.Permission;

public class PermissionMapping
{
    public Type InputType { get; private set; }

    public Type DBDestinationType { get; private set; }

    public PermissionMapping(Type inputType, Type dbDestinationType)
    {
        InputType = inputType;
        DBDestinationType = dbDestinationType;
    }
}