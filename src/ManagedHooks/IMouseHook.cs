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
	/// <include file='ManagedHooks.xml' path='Docs/IMouseHook/Interface/*'/>
	public interface IMouseHook : ISystemHook, IDisposable
	{
		/// <include file='ManagedHooks.xml' path='Docs/IMouseHook/MouseEvent/*'/>
		event MouseEventHandler MouseEvent;

		/// <include file='ManagedHooks.xml' path='Docs/IMouseHook/FilterMessage/*'/>
		void FilterMessage( MouseEvents eventType );
	}
}
