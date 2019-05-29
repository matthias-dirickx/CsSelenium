using System;

namespace CsSeleniumFrame.src.Ex
{
    public class CsSeAssertion : Exception
    {
        public CsSeAssertion(Exception e) : base(e.Message, e.InnerException)
        {
        }

        public CsSeAssertion(string message) : base(message)
        {
        }

        public CsSeAssertion(string message, Exception e) : base(message, e)
        {
        }
    }
}
