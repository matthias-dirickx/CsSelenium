﻿using System;
using System.Collections.Generic;
using System.Text;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Actions;

namespace CsSeleniumFrame.src.core
{
    public abstract class Interaction
    {
        public static ShouldAction Should(Condition[] conditions)
        {
            return new ShouldAction(conditions);
        }

        public static ShouldNotAction ShouldNot(Condition[] conditions)
        {
            return new ShouldNotAction(conditions);
        }

        public static WaitUntilAction WaitUntil(Condition condition, long timeoutMs, long pollMs)
        {
            return new WaitUntilAction(condition, timeoutMs, pollMs);
        }

        public static WaitWhileAction WaitWhile(Condition condition, long timeoutMs, long pollMs)
        {
            return new WaitWhileAction(condition, timeoutMs, pollMs);
        }

        public abstract CsSeElement Execute(IWebDriver driver, IWebElement element);

        private readonly string name;

        public Interaction(string name)
        {
            this.name = name;
        }
    }
}
