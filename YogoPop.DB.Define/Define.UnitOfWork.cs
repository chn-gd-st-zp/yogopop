namespace YogoPop.DB.Define;

public class UnitOfWork : IDisposable, ITransient
{
    protected TransactionScope TransactionScope;

    public UnitOfWork() { TransactionScope = Generate(); }

    public void Dispose() { TransactionScope.Dispose(); }

    public void Complete() { TransactionScope.Complete(); }

    protected virtual TransactionScope Generate() { return new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled); }

    //public static TransactionScope GenerateTransactionScope(TransactionScope transactionScope = null) => transactionScope != null ? transactionScope : new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);

    /// <summary>
    /// 开启工作单元/事务
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="isolationLevel">
    /// sqlserver
    /// ReadUncommitted:    默认× 行周期锁× 行瞬时锁× 表周期锁× 表瞬时锁× 会脏读√ 会复读√ 会幻读√
    /// ReadCommitted:      默认√ 行周期锁× 行瞬时锁√ 表周期锁× 表瞬时锁√ 会脏读× 会复读√ 会幻读√
    /// RepeatableRead:     默认× 行周期锁√ 行瞬时锁√ 表周期锁√ 表瞬时锁√ 会脏读× 会复读× 会幻读√
    /// Serializable:       默认× 行周期锁√ 行瞬时锁√ 表周期锁√ 表瞬时锁√ 会脏读× 会复读× 会幻读×
    /// Snapshot:           默认× 行周期锁× 行瞬时锁√ 表周期锁× 表瞬时锁√ 会脏读× 会复读× 会幻读×
    /// Chaos:              支持，但不要使用
    /// Unspecified:        使用当前连接的隔离级别
    /// 
    /// mysql
    /// ReadUncommitted:    默认× 行周期锁× 行瞬时锁× 表周期锁× 表瞬时锁× 会脏读√ 会复读√ 会幻读√
    /// ReadCommitted:      默认× 行周期锁× 行瞬时锁√ 表周期锁× 表瞬时锁√ 会脏读× 会复读√ 会幻读√
    /// RepeatableRead:     默认√ 行周期锁√ 行瞬时锁√ 表周期锁√ 表瞬时锁√ 会脏读× 会复读× 会幻读× MySQL 有间隙锁，所以不会重复读
    /// Serializable:       默认× 行周期锁√ 行瞬时锁√ 表周期锁√ 表瞬时锁√ 会脏读× 会复读× 会幻读×
    /// Snapshot:           不支持。MySQL 使用 MVCC（多版本并发控制），在功能上类似于快照隔离，但没有明确的快照隔离级别
    /// Chaos:              不支持
    /// Unspecified:        使用当前连接的隔离级别
    /// </param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static TransactionScope GenerateTransactionScope(TransactionScopeOption scope = TransactionScopeOption.Required, System.Transactions.IsolationLevel isolationLevel = System.Transactions.IsolationLevel.RepeatableRead, int timeout = 30)
        => new TransactionScope(scope, new TransactionOptions { IsolationLevel = isolationLevel, Timeout = TimeSpan.FromSeconds(30) }, TransactionScopeAsyncFlowOption.Enabled);
}