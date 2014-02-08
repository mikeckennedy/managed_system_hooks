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

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/IHookImpl/Interface/*'/>
	public interface IHookImpl : IDisposable
	{
		/// <include file='Internal.xml' path='Docs/IHookImpl/Disposed/*'/>
		bool Disposed { get; }

		/// <include file='Internal.xml' path='Docs/IHookImpl/AddReference/*'/>
		void AddReference();
		/// <include file='Internal.xml' path='Docs/IHookImpl/Release/*'/>
		int Release();

		/// <include file='Internal.xml' path='Docs/SystemHook/IsHooked/*'/>
		bool IsHooked { get; }

		/// <include file='Internal.xml' path='Docs/IHookImpl/InstallHook/*'/>
		void InstallHook();
		/// <include file='Internal.xml' path='Docs/IHookImpl/UninstallHook/*'/>
		void UninstallHook();
	}
}
