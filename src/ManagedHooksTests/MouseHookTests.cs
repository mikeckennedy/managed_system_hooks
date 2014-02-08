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
using System.Collections;
using System.Drawing;
using Kennedy.ManagedHooks;
using UnitedBinary.UnitTests.Framework;

namespace Kennedy.ManagedHooks.Tests
{
	// Renew is the default, but we explicitly state that here to ensure our
	// array lists are cleared out each test.
	[TestExecutionMode(TestExecutionModes.Renew)]
	public class MouseHookTests
	{
		private ArrayList eventTest_ArgsList = new ArrayList();

		public MouseHookTests()
		{
		}

		[TestOrder(TestOrderModes.First)]
		[TestMethod]
		public void CreationTest(TestMethodRecord tmr)
		{
			using (IMouseHookExt mouseHook = HookFactory.CreateMouseHookExt())
			{
				tmr.RunTest( mouseHook != null, "Mouse hook created." );
			}
		}

		[TestMethod]
		public void NullImplementationTest(TestMethodRecord tmr)
		{
			tmr.RegisterException( "Null implementation causes an exception.", typeof(ArgumentNullException) );
			IMouseHookExt keyboard = HookFactory.CreateMouseHookExt( null );
		}

		//
		// This test requires some setup. We will use our test implementation to
		// trigger some button down events and then test to make sure that the event
		// signaled the right values.
		//
		[TestMethod]
		public void EventSinkTest(TestMethodRecord tmr)
		{
			MouseHookEventArgs[] mouseArgs = GenerateRandomClickArgs();

			MouseHookTestImpl mouseImpl = new MouseHookTestImpl();
			using (IMouseHookExt mouseHook = HookFactory.CreateMouseHookExt(mouseImpl))
			{
				mouseHook.LeftButtonDown += new MouseEventHandlerExt( EventSinkTest_LeftButtonDown );
				mouseHook.InstallHook();

				//
				// Trigger a bunch of mouse down events. The event handler will
				// capture the output and we will then compare it here.
				//
				foreach (MouseHookEventArgs mea in mouseArgs)
				{
					mouseImpl.TriggerMouseAction( MouseEvents.LeftButtonDown, mea );
				}
			}

			tmr.RunTest( mouseArgs.Length == eventTest_ArgsList.Count, "The proper number of events were caught." );

			bool same = true;
			for (int i=0; i < mouseArgs.Length; i++)
			{
				MouseHookEventArgs mea1 = mouseArgs[i];
				MouseHookEventArgs mea2 = (MouseHookEventArgs)eventTest_ArgsList[i];

				if (!CompareMouseArgs(mea1, mea2))
				{
					tmr.WriteLine( "Failed event results comparison at index " + i );
					same = false;
					break;
				}
			}

			tmr.RunTest( same, "The event callback captured all the events successfully." );
		}

		private void EventSinkTest_LeftButtonDown(MouseHookEventArgs mea)
		{
			eventTest_ArgsList.Add( mea );
		}

		private MouseHookEventArgs[] GenerateRandomClickArgs()
		{
			Random rand = new Random( 0 );
			ArrayList clickList = new ArrayList();
			for (int i=0; i < 1000; i++)
			{
				Point pt = new Point( rand.Next()%1000, rand.Next()%1000 );
				bool alt = rand.Next()%2 == 0;
				bool ctrl = rand.Next()%2 == 0;
				bool shift = rand.Next()%2 == 0;

				MouseHookEventArgs mea = new MouseHookEventArgs( pt, alt, ctrl, shift );
				clickList.Add( mea );
			}

			return (MouseHookEventArgs[])clickList.ToArray( typeof(MouseHookEventArgs) );
		}

		private bool CompareMouseArgs(MouseHookEventArgs mea1, MouseHookEventArgs mea2)
		{
			if (mea1.Location.X != mea2.Location.X)
			{
				return false;
			}
			if (mea1.Location.Y != mea2.Location.Y)
			{
				return false;
			}
			if (mea1.Alt != mea2.Alt)
			{
				return false;
			}
			if (mea1.Ctrl != mea2.Ctrl)
			{
				return false;
			}
			if (mea1.Shift != mea2.Shift)
			{
				return false;
			}

			return true;
		}

		[TestMethod]
		public void ReferenceCountingTest(TestMethodRecord tmr)
		{
			//
			// In this test we are observing how the various mouse hook
			// classes are interacting with their implementation classes. They
			// should add references in their constructor and release them when
			// disposed. The implementation should only be disposed when the ref
			// count is zero.
			//
			MouseHookTestImpl testImpl = new MouseHookTestImpl();

			tmr.RunTest( testImpl.ReferenceCount == 0, "Now references count is 0." );
			tmr.RunTest( testImpl.Disposed == false, "Not initially disposed." );

			using (IMouseHook hook1 = HookFactory.CreateMouseHook(testImpl))
			{
				tmr.RunTest( testImpl.ReferenceCount == 1, "Now references count is 1." );
				tmr.RunTest( testImpl.Disposed == false, "Not disposed." );

				using (IMouseHookExt hook2 = HookFactory.CreateMouseHookExt(testImpl))
				{
					tmr.RunTest( testImpl.ReferenceCount == 2, "Now references count is 2." );
					tmr.RunTest( testImpl.Disposed == false, "Not disposed." );

					using (IMouseHookLite hook3 = HookFactory.CreateMouseHookLite(testImpl))
					{
						tmr.RunTest( testImpl.ReferenceCount == 3, "Now references count is 3." );
						tmr.RunTest( testImpl.Disposed == false, "Not disposed." );
					}

					tmr.RunTest( testImpl.ReferenceCount == 2, "Now references count is 2." );
					tmr.RunTest( testImpl.Disposed == false, "Not disposed." );
				}

				tmr.RunTest( testImpl.ReferenceCount == 1, "Now references count is 1." );
				tmr.RunTest( testImpl.Disposed == false, "Not disposed." );
			}

			tmr.RunTest( testImpl.ReferenceCount == 0, "Now references count is 0." );
			tmr.RunTest( testImpl.Disposed == true, "Disposed." );
		}

		[TestMethod]
		public void InstallHookTests(TestMethodRecord tmr)
		{
			using (IMouseHook mHook = HookFactory.CreateMouseHook())
			{
				TestInstallHook( tmr, mHook, "IMouseHook" );
			}

			using (IMouseHookExt mHookExt = HookFactory.CreateMouseHookExt())
			{
				TestInstallHook( tmr, mHookExt, "IMouseHookExt" );
			}

			using (IMouseHookLite mHookLite = HookFactory.CreateMouseHookLite())
			{
				TestInstallHook( tmr, mHookLite, "IMouseHookLite" );
			}
		}

		private void TestInstallHook(TestMethodRecord tmr, ISystemHook hook, string name)
		{
			tmr.WriteLine( "Testing mouse hook of type: " + name );

			tmr.RunTest( hook.IsHooked == false, "Hook is not installed." );

			hook.InstallHook();
			tmr.RunTest( hook.IsHooked, "Hook installed successfully." );

			hook.UninstallHook();
			tmr.RunTest( hook.IsHooked == false, "Hook is unstalled successfully." );
		}
	}
}

