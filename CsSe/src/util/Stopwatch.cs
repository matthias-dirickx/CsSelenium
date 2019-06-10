/*
 * Copyright 2019 Matthias Dirickx
 * 
 * This file is part of CsSeSelenium.
 * 
 * CsSeSelenium is free software:
 * you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License,
 * or (at your option) any later version.
 * 
 * CsSeSelenium is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * WITHOUT even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with CsSeSelenium.
 * 
 * If not, see http://www.gnu.org/licenses/.
 */

using System;
using System.Threading;

namespace CsSeleniumFrame.src.Core
{
    public class Stopwatch
    {
        private readonly double endTime;

        private double startTime;
        private double chronoEnd;
        private double lastRoundTime;

        public Stopwatch()
        {

        }

        public Stopwatch(long timeoutMilliseconds)
        {
            this.endTime = GetMillis() + timeoutMilliseconds;
        }

        public bool IsTimoutReached()
        {
            return GetMillis() > endTime;
        }

        public void Start()
        {
            this.startTime = GetMillis();
            this.lastRoundTime = this.startTime;
        }

        public double GetRound()
        {
            return GetMillis() - this.lastRoundTime;
        }

        public double Stop()
        {
            this.chronoEnd = GetMillis();

            return chronoEnd - startTime;
        }

        public double GetElapsedMs()
        {
            return endTime - startTime;
        }

        private double GetMillis()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public void sleep(int milliseconds)
        {
            try
            {
                Thread.Sleep(milliseconds);
            } catch(ThreadInterruptedException e)
            {
                Thread.CurrentThread.Interrupt();
                throw new ThreadInterruptedException(e.ToString());
            }
        }
    }
}
