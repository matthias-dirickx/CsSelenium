﻿using System;

namespace CsSeleniumFrame.src.Ex
{
    public class CsSeElementShould : CsSeAssertion
    {
        public CsSeElementShould(string message) : base(message)
        {
        }

        public CsSeElementShould(string message, Exception e) : base(message, e)
        {
        }
    }
}