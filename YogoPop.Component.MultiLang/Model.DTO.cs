namespace YogoPop.Component.MultiLang;

public interface IDTOMultiLangInput : IDTOInput
{
    public List<DTOMultiLangParams> MultiLangs { get; set; }
}

public interface IDTOMultiLangOutput : IDTOOutput { }

public class DTOMultiLangParams : IDTO
{
    public string Module { get; set; }

    public string Field { get; set; }

    public Dictionary<string, string> Languages { get; set; }
}