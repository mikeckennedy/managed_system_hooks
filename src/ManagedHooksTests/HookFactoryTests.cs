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

//
// SOFTWARE REQUIREMENT: To run these tests you will need to download and install HarnessIt.
// You can download it free of charge at http://www.unittesting.com and I strongly encourage
// you to do so, especially if you plan to modify the library.
//

using System;
using UnitedBinary.UnitTests.Framework;

using Kennedy.ManagedHooks;

namespace Kennedy.ManagedHooks.Tests
{
	public class HookFactoryTests
	{
		public HookFactoryTests()
		{
		}

		//
		// To some extent, these tests are either implicitly, or explicitly, run
		// in the other test classes. But we have run them again here to make
		// sure that all the creation methods are tested.
		//

		[TestMethod]
		public void CreateKeyboardHookTests(TestMethodRecord tmr)
		{
			using (IKeyboardHook hook = HookFactory.CreateKeyboardHook())
			{
				tmr.RunTest( hook != null, "IKeyboardHook created successfully with default options." );
			}

			using (IKeyboardHookExt hookExt = HookFactory.CreateKeyboardHookExt())
			{
				tmr.RunTest( hookExt != null, "IKeyboardHookExt created successfully with default options." );
			}
		}

		[TestMethod]
		public void CreateMouseHookTests(TestMethodRecord tmr)
		{
			using (IMouseHook mouseHook = HookFactory.CreateMouseHook())
			{
				tmr.RunTest( mouseHook != null, "IMouseHook created successfully with default options." );
			}

			using (IMouseHookExt mouseHookExt = HookFactory.CreateMouseHookExt())
			{
				tmr.RunTest( mouseHookExt != null, "IMouseHookExt created successfully with default options." );
			}

			using (IMouseHookLite mouseHookLite = HookFactory.CreateMouseHookLite())
			{
				tmr.RunTest( mouseHookLite != null, "IMouseHookLite created successfully with default options." );
			}
		}

	}
}
