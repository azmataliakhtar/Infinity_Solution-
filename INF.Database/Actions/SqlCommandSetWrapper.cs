using System;
using System.Data.SqlClient;
using System.Reflection;

namespace INF.Database.Actions
{
    // as found on: http://richardod.blogspot.com/2009/03/speeding-up-sql-server-inserts-by-using.html
    // Modified by Richard OD to exploit .NET 3.5 08 March 2009
    // Copyright (c) 2005 - 2007 Ayende Rahien (ayende@ayende.com)
    // All rights reserved.
    //
    // Redistribution and use in source and binary forms, with or without modification,
    // are permitted provided that the following conditions are met:
    //
    //     * Redistributions of source code must retain the above copyright notice,
    //     this list of conditions and the following disclaimer.
    //     * Redistributions in binary form must reproduce the above copyright notice,
    //     this list of conditions and the following disclaimer in the documentation
    //     and/or other materials provided with the distribution.
    //     * Neither the name of Ayende Rahien nor the names of its
    //     contributors may be used to endorse or promote products derived from this
    //     software without specific prior written permission.
    //
    // THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
    // ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
    // WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    // DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
    // FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    // DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
    // SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
    // CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
    // OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
    // THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    public class SqlCommandSetWrapper : IDisposable
    {
        private static readonly Type _commandSetType;
        private readonly object _commandSet;
        private readonly Action<SqlCommand> _appenderDel;
        private readonly Action _disposeDel;
        private readonly Func<int> _executeNonQueryDel;
        private readonly Func<SqlConnection> _connectionGetDel;
        private readonly Action<SqlConnection> _connectionSetDel;
        private readonly Action<SqlTransaction> _transactionSetDel;
 
        private int _commandCount;
 
        static SqlCommandSetWrapper()
        {
            Assembly systemData = Assembly.Load("System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            _commandSetType = systemData.GetType("System.Data.SqlClient.SqlCommandSet");
        }
 
        public SqlCommandSetWrapper()
        {
            _commandSet = Activator.CreateInstance(_commandSetType, true);
            _appenderDel = (Action<SqlCommand>)Delegate.CreateDelegate(typeof(Action<SqlCommand>), _commandSet, "Append");
            _disposeDel = (Action)Delegate.CreateDelegate(typeof(Action), _commandSet, "Dispose");
            _executeNonQueryDel = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), _commandSet, "ExecuteNonQuery");
            _connectionGetDel = (Func<SqlConnection>)Delegate.CreateDelegate(typeof(Func<SqlConnection>), _commandSet, "get_Connection");
            _connectionSetDel = (Action<SqlConnection>)Delegate.CreateDelegate(typeof(Action<SqlConnection>), _commandSet, "set_Connection");
            _transactionSetDel = (Action<SqlTransaction>)Delegate.CreateDelegate(typeof(Action<SqlTransaction>), _commandSet, "set_Transaction");
        }
 
        public void Append(SqlCommand command)
        {
            _commandCount++;
            _appenderDel.Invoke(command);
        }
 
        public int ExecuteNonQuery()
        {
            return _executeNonQueryDel.Invoke();
        }
 
        public SqlConnection Connection
        {
            get
            {
                return _connectionGetDel.Invoke();
            }
            set
            {
                _connectionSetDel.Invoke(value);
            }
        }
 
        public SqlTransaction Transaction
        {
            set
            {
                _transactionSetDel.Invoke(value);
            }
        }
 
        public int CommandCount
        {
            get
            {
                return _commandCount;
            }
        }
 
        public void Dispose()
        {
            _disposeDel.Invoke();
        }
    }
}
