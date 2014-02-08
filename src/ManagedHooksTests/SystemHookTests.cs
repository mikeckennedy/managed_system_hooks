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

//
// SOFTWARE REQUIREMENT: To run these tests you will need to download and install HarnessIt.
// You can download it free of charge at http://www.unittesting.com and I strongly encourage
// you to do so, especially if you plan to modify the library.
//

using System;

using Kennedy.ManagedHooks;

using UnitedBinary.UnitTests.Framework;

namespace Kennedy.ManagedHooks.Tests
{
	public class SystemHookTests
	{
		public SystemHookTests() {}

		[TestMethod]
		public void HookInstallTests(TestMethodRecord tmr)
		{
			using (SystemHookTestWrapper hook = new SystemHookTestWrapper(HookTypes.MouseLL))
			{
				//
				// Test that we can install the hook as a local hook.
				//
				hook.InstallHook();

				//
				// Failure is signalled with an exception, so there is nothing
				// to actually test. That's why we're passing true.
				//
				tmr.RunTest( true, "Installed the hook as a global mouse hook." );

				hook.UninstallHook();

				tmr.RunTest( true, "Uninstalled the hook as a global mouse hook." );
			}
		}

		[TestMethod]
		public void ConstructionUnsupportedTypeTests(TestMethodRecord tmr)
		{
			HookTypes type = HookTypes.Hardware;

			tmr.WriteLine("If you implement the hook type HookTypes." + type +
							", change the type parameter to continue testing this senario.");

			tmr.RegisterException("An unimplemented hook type will cause an exception.",
				typeof(HookTypeNotImplementedException));

			SystemHookTestWrapper hook = new SystemHookTestWrapper( type );
		}

		[TestMethod]
		public void UnsupportedFilterTypesTest1(TestMethodRecord tmr)
		{
			HookTypes type = HookTypes.Hardware;

			tmr.WriteLine("If you implement the hook type HookTypes." + type +
				", change the type parameter to continue testing this senario.");

			using (SystemHookTestWrapper hook = new SystemHookTestWrapper(HookTypes.MouseLL))
			{
				tmr.RegisterException("An unimplemented hook type will cause an exception (filter message).",
					typeof(ManagedHooksException));

				hook.FilterMessageWrapper( type, 12345 );
			}
		}

		[TestMethod]
		public void UnsupportedFilterTypesTest2(TestMethodRecord tmr)
		{
			using (SystemHookTestWrapper hook = new SystemHookTestWrapper(HookTypes.MouseLL))
			{
				hook.FilterMessageWrapper( 12345 );

				tmr.RunTest( true, "Filter message succeeded on HookTypes.MouseLL." );
			}
		}

		[TestMethod]
		public void SingletonErrorTest1(TestMethodRecord tmr)
		{
			using (SystemHookTestWrapper hook1 = new SystemHookTestWrapper(HookTypes.MouseLL))
			{
				tmr.RegisterException( "Creating a second hook of the same type will fail.", typeof(ManagedHooksException) );
				SystemHookTestWrapper hook2 = new SystemHookTestWrapper( HookTypes.MouseLL );
			}
		}
	}
}
