
using System;
using System.Transactions;
using Castle.DynamicProxy;

#pragma warning disable 1591

namespace Boipeba.Core.Infra.NHibernate
{
    /// <summary>
    /// Controle de transação automático.
    /// </summary>
    public class TransactionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                try
                {
                    invocation.Proceed();

                    scope.Complete();
                }
                catch(Exception ex)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }
    }
}
