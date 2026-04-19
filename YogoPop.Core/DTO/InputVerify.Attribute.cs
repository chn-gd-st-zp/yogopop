namespace YogoPop.Core.DTO;

[AspectInjector.Broker.Injection(typeof(InputVerifyAspect))]
[AttributeUsage(AttributeTargets.Method)]
public class InputVerifyAttribute : Attribute { }