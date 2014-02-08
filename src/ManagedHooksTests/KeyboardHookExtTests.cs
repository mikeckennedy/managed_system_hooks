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
using System.Windows.Forms;
using System.Collections;
using UnitedBinary.UnitTests.Framework;

using Kennedy.ManagedHooks;
using Kennedy.ManagedHooks.Internal;

namespace Kennedy.ManagedHooks.Tests
{
	// Renew is the default, but we explicitly state that here to ensure our
	// array lists are cleared out each test.
	[TestExecutionMode(TestExecutionModes.Renew)]
	public class KeyboardHookExtTests
	{
		private ArrayList eventTest_ArgsList = new ArrayList();

		private ArrayList sortedEventTest_ArgsListUp = new ArrayList();
		private ArrayList sortedEventTest_ArgsListDown = new ArrayList();
		private ArrayList sortedEventTest_ArgsListSysUp = new ArrayList();
		private ArrayList sortedEventTest_ArgsListSysDown = new ArrayList();

		public KeyboardHookExtTests()
		{
		}

		[TestOrder(TestOrderModes.First)]
		[TestMethod]
		public void DefaultCreationTest(TestMethodRecord tmr)
		{
			using (IKeyboardHookExt keyboard = HookFactory.CreateKeyboardHookExt())
			{
				tmr.RunTest( keyboard != null, "Keyboard hook created." );
			}
		}

		[TestMethod]
		public void NullImplementationTest(TestMethodRecord tmr)
		{
			tmr.RegisterException( "Null implementation causes an exception.", typeof(ArgumentNullException) );
			IKeyboardHookExt keyboard = HookFactory.CreateKeyboardHookExt( null );
		}

		//
		// This test requires some setup. We will use our test implementation to
		// trigger some keydown events and then test to make sure that the event
		// signaled the right values.
		//
		[TestMethod]
		public void EventTest(TestMethodRecord tmr)
		{
			KeyboardHookEventArgs[] keyArgs = GenerateRandomKeyData();

			KeyboardHookTestImpl hookImpl = new KeyboardHookTestImpl();

			using (IKeyboardHookExt keyboard = HookFactory.CreateKeyboardHookExt(hookImpl))
			{
				keyboard.InstallHook();

				keyboard.KeyDown += new KeyboardEventHandlerExt( EventTest_KeyDown );

				//
				// Trigger a bunch of keyboard events. The event handler will
				// capture the output and we will then compare it here.
				//
				foreach (KeyboardHookEventArgs keyArg in keyArgs)
				{
					hookImpl.TriggerKeyAction( KeyboardEvents.KeyDown, keyArg );
				}
			}

			tmr.RunTest( keyArgs.Length == eventTest_ArgsList.Count, "The proper number of events were caught." );

			bool same = true;
			for (int i=0; i < keyArgs.Length; i++)
			{
				KeyboardHookEventArgs kea1 = keyArgs[i];
				KeyboardHookEventArgs kea2 = (KeyboardHookEventArgs)eventTest_ArgsList[i];

				if (!CompareKeyArgs(kea1, kea2))
				{
					tmr.WriteLine( "Failed event results comparison at index " + i );
					same = false;
					break;
				}
			}

			tmr.RunTest( same, "The event callback captured all the events successfully." );
		}

		private void EventTest_KeyDown(object sender, KeyboardHookEventArgs kea)
		{
			this.eventTest_ArgsList.Add( kea );
		}

		private KeyboardHookEventArgs[] GenerateRandomKeyData()
		{
			System.Windows.Forms.Keys[] keys = new System.Windows.Forms.Keys[]
				{
					System.Windows.Forms.Keys.Escape,
					System.Windows.Forms.Keys.N,
					System.Windows.Forms.Keys.F10,
					System.Windows.Forms.Keys.C,
					System.Windows.Forms.Keys.Enter,
					System.Windows.Forms.Keys.R,
					System.Windows.Forms.Keys.NumPad2,
					System.Windows.Forms.Keys.Z
				};

			Random rand = new Random( 0 );
			KeyboardHookEventArgs keyArgs;
			ArrayList keysList = new ArrayList();
			for (int i=0; i < 1000; i++)
			{
				System.Windows.Forms.Keys key = keys[rand.Next()%keys.Length];
				bool alt = rand.Next()%2 == 1;
				bool ctrl = rand.Next()%2 == 1;
				bool shift = rand.Next()%2 == 1;
				bool capsLock = rand.Next()%2 == 1;

				keyArgs = new KeyboardHookEventArgs( key, alt, ctrl, shift, capsLock );
				keysList.Add( keyArgs );
			}

			return (KeyboardHookEventArgs[])keysList.ToArray( typeof(KeyboardHookEventArgs) );
		}

		private bool CompareKeyArgs(KeyboardHookEventArgs kea1, KeyboardHookEventArgs kea2)
		{
			if (kea1.Key != kea2.Key)
			{
				return false;
			}
			if (kea1.Alt != kea2.Alt)
			{
				return false;
			}
			if (kea1.Ctrl != kea2.Ctrl)
			{
				return false;
			}
			if (kea1.Shift != kea2.Shift)
			{
				return false;
			}
			if (kea1.CapsLock != kea2.CapsLock)
			{
				return false;
			}
			if (kea1.Cancel != kea2.Cancel)
			{
				return false;
			}

			return true;
		}

		//
		// Test to make sure that key down events trigger the KeyDown event.
		// And so on for the other events.
		//
		[TestMethod]
		public void SortedEventTest(TestMethodRecord tmr)
		{
			KeyboardHookTestImpl hookImpl = new KeyboardHookTestImpl();
			IKeyboardHookExt keyboard = HookFactory.CreateKeyboardHookExt( hookImpl );
			keyboard.InstallHook();

			keyboard.KeyDown += new KeyboardEventHandlerExt( SortedEventTest_KeyDown );
			keyboard.KeyUp += new KeyboardEventHandlerExt( SortedEventTest_KeyUp );
			keyboard.SystemKeyUp += new KeyboardEventHandlerExt( SortedEventTest_SystemKeyUp );
			keyboard.SystemKeyDown += new KeyboardEventHandlerExt( SortedEventTest_SystemKeyDownKeyUp );

			KeyboardHookEventArgs[] keyArgs = GenerateRandomKeyData();

			KeyboardHookEventArgs kea1 = keyArgs[0];
			KeyboardHookEventArgs kea2 = keyArgs[1];
			KeyboardHookEventArgs kea3 = keyArgs[2];
			KeyboardHookEventArgs kea4 = keyArgs[3];

			hookImpl.TriggerKeyAction( KeyboardEvents.KeyDown, kea1 );
			hookImpl.TriggerKeyAction( KeyboardEvents.KeyUp, kea2 );
			hookImpl.TriggerKeyAction( KeyboardEvents.SystemKeyDown, kea3 );
			hookImpl.TriggerKeyAction( KeyboardEvents.SystemKeyUp, kea4 );

			tmr.RunTest( sortedEventTest_ArgsListDown.Count == 1, "One KeyDown event trigged." );
			tmr.RunTest( sortedEventTest_ArgsListUp.Count == 1, "One KeyUp event trigged." );
			tmr.RunTest( sortedEventTest_ArgsListSysDown.Count == 1, "One SystemKeyDown event trigged." );
			tmr.RunTest( sortedEventTest_ArgsListSysUp.Count == 1, "One SystemKeyUp event trigged." );

			KeyboardHookEventArgs keaB1 = (KeyboardHookEventArgs)sortedEventTest_ArgsListDown[0];
			KeyboardHookEventArgs keaB2 = (KeyboardHookEventArgs)sortedEventTest_ArgsListUp[0];
			KeyboardHookEventArgs keaB3 = (KeyboardHookEventArgs)sortedEventTest_ArgsListSysDown[0];
			KeyboardHookEventArgs keaB4 = (KeyboardHookEventArgs)sortedEventTest_ArgsListSysUp[0];

			tmr.RunTest( CompareKeyArgs(kea1, keaB1), "Correct KeyDown event caught." );
			tmr.RunTest( CompareKeyArgs(kea2, keaB2), "Correct KeyUp event caught." );
			tmr.RunTest( CompareKeyArgs(kea3, keaB3), "Correct SysKeyDown event caught." );
			tmr.RunTest( CompareKeyArgs(kea4, keaB4), "Correct SysKeyUp event caught." );
		}

		private void SortedEventTest_KeyDown(object sender, KeyboardHookEventArgs kea)
		{
			sortedEventTest_ArgsListDown.Add( kea );
		}

		private void SortedEventTest_KeyUp(object sender, KeyboardHookEventArgs kea)
		{
			sortedEventTest_ArgsListUp.Add( kea );
		}

		private void SortedEventTest_SystemKeyUp(object sender, KeyboardHookEventArgs kea)
		{
			sortedEventTest_ArgsListSysUp.Add( kea );
		}

		private void SortedEventTest_SystemKeyDownKeyUp(object sender, KeyboardHookEventArgs kea)
		{
			sortedEventTest_ArgsListSysDown.Add( kea );
		}

		[TestMethod]
		public void ReferenceCountingTest(TestMethodRecord tmr)
		{
			//
			// In this test we are observing how the various keyboard hook
			// classes are interacting with their implementation classes. They
            // should add references in their constructor and release them when
			// disposed. The implementation should only be disposed when the ref
			// count is zero.
			//
			KeyboardHookTestImpl testImpl = new KeyboardHookTestImpl();

			tmr.RunTest( testImpl.ReferenceCount == 0, "Now references count is 0." );
			tmr.RunTest( testImpl.Disposed == false, "Not initially disposed." );

			using (IKeyboardHook hook1 = HookFactory.CreateKeyboardHook(testImpl))
			{
				tmr.RunTest( testImpl.ReferenceCount == 1, "Now references count is 1." );
				tmr.RunTest( testImpl.Disposed == false, "Not disposed." );

				using (IKeyboardHookExt hook2 = HookFactory.CreateKeyboardHookExt(testImpl))
				{
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
			using (IKeyboardHook kHook = HookFactory.CreateKeyboardHook())
			{
				TestInstallHook( tmr, kHook, "IKeyboardHook" );
			}

			using (IKeyboardHookExt kHookExt = HookFactory.CreateKeyboardHookExt())
			{
				TestInstallHook( tmr, kHookExt, "IKeyboardHookExt" );
			}
		}

		private void TestInstallHook(TestMethodRecord tmr, ISystemHook hook, string name)
		{
			tmr.WriteLine( "Testing keyboard hook of type: " + name );

			tmr.RunTest( hook.IsHooked == false, "Hook is not installed." );

			hook.InstallHook();
			tmr.RunTest( hook.IsHooked, "Hook installed successfully." );

			hook.UninstallHook();
			tmr.RunTest( hook.IsHooked == false, "Hook is unstalled successfully." );
		}
	}
}
