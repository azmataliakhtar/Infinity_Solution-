using Castle.DynamicProxy;
using INF.Database.Metadata;

namespace INF.Database
{
    public class LazyLoadingInterceptor : IInterceptor
    {
        private readonly TableInfo _tableInfo;
        private readonly Session _session;
        private bool _needsToBeInitialized = true;

        public LazyLoadingInterceptor(TableInfo tableInfo, Session session)
        {
            this._tableInfo = tableInfo;
            this._session = session;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name.Equals("get_" + _tableInfo.PrimaryKey.PropertyInfo.Name) ||
                invocation.Method.Name.Equals("set_" + _tableInfo.PrimaryKey.PropertyInfo.Name))
            {
                invocation.Proceed();
                return;
            }

            if (_needsToBeInitialized)
            {
                _needsToBeInitialized = false;
                _session.InitializeProxy(invocation.Proxy, invocation.TargetType);
            }

            invocation.Proceed();
        }
    }
}