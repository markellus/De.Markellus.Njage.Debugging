/***********************************************************/
/* NJAGE Engine - Debugging Helpers                        */
/*                                                         */
/* Copyright 2020 Marcel Bulla. All rights reserved.       */
/* Licensed under the MIT License. See LICENSE in the      */
/* project root for license information.                   */
/***********************************************************/

namespace De.Markellus.Njage.Debugging
{
    public enum njLogSeverity
    {
        /// <summary>
        /// Reserved for system messages
        /// </summary>
        System,
        /// <summary>
        /// Errors which affect the system stability
        /// </summary>
        Error,
        /// <summary>
        /// Errors which do not affect the system stability
        /// </summary>
        Warning,
        /// <summary>
        /// Verbose information
        /// </summary>
        Info
    }
}