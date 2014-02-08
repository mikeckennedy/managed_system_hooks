#region File Header
// /////////////////////////////////////////////////////////////
// Date: 2/25/2004			Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004-2005
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////
#endregion

using System;

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/HookTypes/enum/*'/>
	public enum HookTypes
	{
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		None		= -100,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		Keyboard	= 2,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		Mouse		= 7,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		Hardware	= 8,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		Shell		= 10,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		KeyboardLL	= 13,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		MouseLL		= 14
	};
}

//
// Note: For more details see the link below.
//
// The following hooks seem to result in serious errors
// when installing them in the current implementation of this
// library. Please, only add them back *at your own risk*.
// Remember the Dr. Suess quote from the article...
//

//JournalRecord			= 0,
//JournalPlayback		= 1,
//GetMessage			= 3,
//CallWindowProcedure	= 4,
//ComputerBasedTraining	= 5,
//SystemMessageFilter	= 6,
//Debug					= 9,
//ForegroundIdle		= 11,
//CallWindowProret		= 12,

//
// For more details, please see the discussion accompanying the article here:
//
//  http://www.codeproject.com/csharp/GlobalSystemHook.asp?msg=864172#xx864172xx
//
