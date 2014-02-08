#region File Header
// /////////////////////////////////////////////////////////////
// Date: 5/15/2005			Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004-2005
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////
#endregion

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/IMouseHookImpl/Interface/*'/>
	public interface IMouseHookImpl : IHookImpl
	{
		/// <include file='Internal.xml' path='Docs/IMouseHookImpl/MouseEvent/*'/>
		event MouseEventHandler MouseEvent;

		/// <include file='Internal.xml' path='Docs/IMouseHookImpl/FilterMessage/*'/>
		void FilterMessage( MouseEvents eventType );
	}
}
