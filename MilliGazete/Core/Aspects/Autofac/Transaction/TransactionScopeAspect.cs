using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress);
            try
            {
                invocation.Proceed();
                transaction.Complete();
            }
            catch
            {
                transaction.Dispose();
                throw;
            }
        }
    }
}
