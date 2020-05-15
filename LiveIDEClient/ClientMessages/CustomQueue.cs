using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using ZeroMQ;

namespace ClassLibrary
{
    /*
    This class is custom Queue that we built for using event every time we
    insert message to him
    */
    class CustomQueue<T> : ConcurrentQueue<T>
    {
        public event EventHandler<ZSocket> NewMessage;

        public CustomQueue() : base() {}

        public void Notify(dynamic e)
        {
            NewMessage.Invoke(this, e);
        }
    }
}
