using System;

namespace CsSeleniumFrame.src.Ex
{
    public class CsSeElementShouldNot : CsSeAssertion
    {
        public CsSeElementShouldNot(string message) : base(message)
        {
        }

        public CsSeElementShouldNot(string message, Exception e) : base(message, e)
        {
        }
    }
}
