using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Exceptions
{
    public class DDException : Exception
    {
        private Exception inner;
        private string message;

        public DDException(string message, Exception inner) : base(message)
        {
            this.message = message;
            this.inner = inner;
        }
    }
}
