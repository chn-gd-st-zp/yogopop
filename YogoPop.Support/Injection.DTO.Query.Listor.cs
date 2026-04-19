namespace YogoPop.Support;

[DIModeForService(DIModeEnum.Exclusive, typeof(IDTOListor<>))]
public class DTOListor<TDTOSort> : DTOSearch<TDTOSort>, IDTOListor<TDTOSort>
    where TDTOSort : IDTOSort
{
    //
}