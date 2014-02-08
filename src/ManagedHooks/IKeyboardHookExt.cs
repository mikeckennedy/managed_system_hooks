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
	/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHookExt/Interface/*'/>
	public interface IKeyboardHookExt : ISystemHook, IDisposable
	{
		/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHookExt/KeyDown/*'/>
		event KeyboardEventHandlerExt KeyDown;
		/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHookExt/KeyUp/*'/>
		event KeyboardEventHandlerExt KeyUp;
		/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHookExt/SystemKeyDown/*'/>
		event KeyboardEventHandlerExt SystemKeyDown;
		/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHookExt/SystemKeyUp/*'/>
		event KeyboardEventHandlerExt SystemKeyUp;

		/// <include file='ManagedHooks.xml' path='Docs/IKeyboardHook/FilterMessage/*'/>
		void FilterMessage( KeyboardEvents eventType );
	}
}
