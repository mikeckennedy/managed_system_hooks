#region File Header
// /////////////////////////////////////////////////////////////
// Date: 2/25/2004					Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////
#endregion

//
// SOFTWARE REQUIREMENT: To run these tests you will need to download and install HarnessIt.
// You can download it free of charge at http://www.unittesting.com and I strongly encourage
// you to do so, especially if you plan to modify the library.
//

using System;

namespace Kennedy.ManagedHooks.Tests
{
	//
	// This class serves as a concrete instance of the SystemHook class for testing.
	// Also, it exposes otherwise hidden elements (via protected access). This allows
	// us to write tests against them.
	//
	public class SystemHookTestWrapper : Kennedy.ManagedHooks.Internal.SystemHook
	{
		public SystemHookTestWrapper(Kennedy.ManagedHooks.HookTypes type) : base(type)
		{
		}

		protected override bool HookCallback(int code, System.UIntPtr wparam, System.IntPtr lparam)
		{
			return false;
		}

		private static IntPtr MyApplicationInstance
		{
			get
			{
				System.Reflection.Assembly A = typeof(SystemHookTestWrapper).Assembly;
				System.Reflection.Module module = A.GetModules(false)[0];
				IntPtr appInstance = System.Runtime.InteropServices.Marshal.GetHINSTANCE( module );

				return appInstance;
			}
		}

		public void FilterMessageWrapper(int message)
		{
			base.FilterMessage( this.HookType, message );
		}

		public void FilterMessageWrapper(HookTypes type, int message)
		{
			base.FilterMessage( type, message );
		}
	}
}
