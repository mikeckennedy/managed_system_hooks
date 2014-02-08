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
using System.Drawing;

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/MouseEvents/MouseEventHandler/*'/>
	public delegate void MouseEventHandler( MouseEvents mEvent, MouseHookEventArgs mea );

	/// <include file='ManagedHooks.xml' path='Docs/MouseEvents/MouseEventHandlerExt/*'/>
	public delegate void MouseEventHandlerExt( MouseHookEventArgs mea );

	/// <include file='ManagedHooks.xml' path='Docs/MouseEvents/enum/*'/>
	public enum MouseEvents
	{
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		LeftButtonDown	= 0x0201,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		LeftButtonUp	= 0x0202,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		Move			= 0x0200,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		MouseWheel		= 0x020A,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		RightButtonDown = 0x0204,
		/// <include file='ManagedHooks.xml' path='Docs/General/Empty/*'/>
		RightButtonUp	= 0x0205
	}
}
