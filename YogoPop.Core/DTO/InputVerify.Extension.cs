namespace YogoPop.Core.DTO;

public static class InputVerifyAttributeExtension
{
    public static void Verify(this object[] paramObjs)
    {
        string errorMsg = string.Empty;

        var actionParams = new List<IDTOInput>();
        foreach (var paramObj in paramObjs)
            LoadParams(paramObj, typeof(IDTOInput), actionParams);

        foreach (object actionParam in actionParams)
        {
            var input = actionParam as IDTOInput;
            Verify(input);
        }
    }

    private static void Verify(IDTOInput input)
    {
        string errorMsg = string.Empty;

        if (input == null)
            return;

        if (!input.Validation(out errorMsg))
            throw new VEParamsValidation(errorMsg);

        foreach (var property in input.GetType().GetProperties())
        {
            if (property.PropertyType.IsClass && property.PropertyType.IsExtendOf(typeof(IDTOInput)))
                Verify(property.GetValue(input) as IDTOInput);
        }
    }

    private static void LoadParams(this object paramObj, Type inputType, List<IDTOInput> paramList)
    {
        if (paramObj == null)
            return;

        Type paramType = paramObj.GetType();

        if (paramType.IsImplementedOf(inputType))
        {
            paramList.Add((IDTOInput)paramObj);
        }
        else
        {
            if (paramType.IsImplementedOf(typeof(ICollection)))
            {
                ICollection objs = null;

                if (paramType.IsImplementedOf(typeof(IDictionary)))
                    objs = ((IDictionary)paramObj).Values;
                else
                    objs = (ICollection)paramObj;

                foreach (var obj in objs)
                    LoadParams(obj, inputType, paramList);
            }
            else
            {
                var piArray = paramType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var pi in piArray)
                {
                    if (pi.PropertyType.IsImplementedOf(inputType))
                    {
                        var value = pi.GetValue(paramObj);
                        paramList.Add((IDTOInput)value);
                    }
                }
            }
        }
    }
}