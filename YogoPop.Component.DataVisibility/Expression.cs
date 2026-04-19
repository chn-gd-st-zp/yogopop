namespace YogoPop.Component.DataVisibility;

public interface IDataVisiExpression<TDataVisiAssign, TDataVisiSource, TPrimaryKey> : ITransient
    where TDataVisiAssign : class, IDataVisiAssign
    where TDataVisiSource : class, IDataVisiSource<TPrimaryKey>
{
    public Expression<Func<TDataVisiSource, bool>> ToExpression(TDataVisiAssign assign);
}