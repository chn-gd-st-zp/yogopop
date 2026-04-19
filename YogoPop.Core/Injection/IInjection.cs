namespace YogoPop.Core.Injection;

public interface IInjection { }

public interface ITransient : IInjection { }

public interface IScope : IInjection { }

public interface ISingleton : IInjection { }