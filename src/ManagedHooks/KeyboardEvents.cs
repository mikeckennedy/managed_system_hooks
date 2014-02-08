#region File Header
// /////////////////////////////////////////////////////////////
// Date: 2/24/2004		Author: Michael Kennedy
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
	/// <include file='ManagedHooks.xml' path='Docs/KeyboardEvents/KeyboardEventHandler/*'/>
	public delegate void KeyboardEventHandler( KeyboardEvents kEvent, KeyboardHookEventArgs kea );

	/// <include file='ManagedHooks.xml' path='Docs/KeyboardEvents/KeyboardEventHandlerExt/*'/>
	public delegate void KeyboardEventHandlerExt( object sender, KeyboardHookEventArgs kea );

	/// <include file='ManagedHooks.xml' path='Docs/KeyboardEvents/enum/*'/>
	public enum KeyboardEvents
	{
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		KeyDown			= 0x0100,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		KeyUp			= 0x0101,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		SystemKeyDown	= 0x0104,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		SystemKeyUp		= 0x0105
	}
}
