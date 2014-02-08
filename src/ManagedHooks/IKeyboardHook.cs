#region File Header
// /////////////////////////////////////////////////////////////
// Date: 5/15/2005		Author: Michael Kennedy
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
	/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHook/Interface/*'/>
	public interface IKeyboardHook : ISystemHook, IDisposable
	{
		/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHook/KeyboardEvent/*'/>
		event KeyboardEventHandler KeyboardEvent;

		/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHook/FilterMessage/*'/>
		void FilterMessage( KeyboardEvents eventType );
	}
}
