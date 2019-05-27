using System;
using System.Collections.Generic;
using System.Text;

namespace CsSeleniumFrame.src.Logger
{
    /// <summary>
    /// Indication for the type of events logged in the logger library.
    /// </summary>
    public enum CsSeEventType
    {
        /// <summary>
        /// Intended for a Test run level operation.
        /// Typically this will be the Assembly level (before anything starts)
        /// </summary>
        CsSeRun,
        /// <summary>
        /// Intended for a Test Class level operation
        /// </summary>
        CsSeSuite,
        /// <summary>
        /// Intended for a Test Method level operation.
        /// </summary>
        CsSeTest,
        /// <summary>
        /// Intended for an action level operation in a Test.
        /// </summary>
        CsSeAction,
        /// <summary>
        /// Intended for an assertion in a Test.
        /// </summary>
        CsSeCheck
    }
}
