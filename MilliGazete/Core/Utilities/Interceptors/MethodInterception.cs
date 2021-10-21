using Castle.DynamicProxy;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation)
        {
        }
        protected virtual void OnAfter(IInvocation invocation)
        {
        }
        protected virtual void OnException(IInvocation invocation, Exception ex)
        {
        }
        protected virtual void OnSuccess(IInvocation invocation)
        {
        }

        public override void Intercept(IInvocation invocation)
        {
            if (IsAsyncMethod(invocation.Method))
            {
                InterceptAsync(invocation);
            }
            else
            {
                InterceptSync(invocation);
            }
        }

        private static async Task<T> InterceptAsync<T>(Task<T> task)
        {
            T result = await task.ConfigureAwait(false);
            return result;
        }
        private void InterceptAsync(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
                var returnType = invocation.Method.ReturnType;
                if (returnType != typeof(void))
                {
                    var returnValue = invocation.ReturnValue;
                    if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                    {
                        invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue);
                    }
                }
                //((Task)invocation.ReturnValue)
                //    .ContinueWith(antecedent =>
                //    {
                //        try
                //        {
                //            var result = antecedent.GetType()
                //                                   .GetProperty("Result")
                //                                   .GetValue(antecedent, null);
                //            invocation.ReturnValue = result;
                //        }
                //        catch (Exception ex)
                //        {
                //            isSuccess = false;
                //            OnException(invocation, ex);
                //            throw;
                //        }

                //    });
            }
            catch (Exception ex)
            {
                isSuccess = false;
                OnException(invocation, ex);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);

        }

        private void InterceptSync(IInvocation invocation)
        {

            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                OnException(invocation, ex);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);

        }

        public static bool IsAsyncMethod(MethodInfo method)
        {
            return
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }
    }
}