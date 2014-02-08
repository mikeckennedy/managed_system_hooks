#region File Header
// /////////////////////////////////////////////////////////////
// Date: 5/15/2005				Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004-2005
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////
#endregion

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/IKeyboardHookImpl/Interface/*'/>
	public interface IKeyboardHookImpl : IHookImpl
	{
		/// <include file='Internal.xml' path='Docs/IKeyboardHookImpl/KeyboardEvent/*'/>
		event KeyboardEventHandler KeyboardEvent;

		/// <include file='Internal.xml' path='Docs/IKeyboardHookImpl/FilterMessage/*'/>
		void FilterMessage( KeyboardEvents eventType );
	}
}
