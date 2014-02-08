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
using Kennedy.ManagedHooks.Internal;

namespace Kennedy.ManagedHooks.Tests
{
	public class KeyboardHookTestImpl : IKeyboardHookImpl
	{
		private bool installed = false;
		private int referenceCount = 0;
		private bool disposed = false;

		public KeyboardHookTestImpl()
		{
		}

		//
		// Test methods
		//
		public bool TriggerKeyAction(KeyboardEvents keyEvent, KeyboardHookEventArgs kea)
		{
			if (!installed)
			{
				string msg = "Key trigger cannot be used when the hook is uninstalled.";
				throw new InvalidOperationException( msg );
			}

			return OnKeyboardEvent( keyEvent, kea.Key, kea.Alt, kea.Ctrl, kea.Shift, kea.CapsLock );
		}

		public int ReferenceCount
		{
			get
			{
				return referenceCount;
			}
		}

		//
		// IKeyboardHookImpl Members
		//
		public event Kennedy.ManagedHooks.KeyboardEventHandler KeyboardEvent;

		private bool OnKeyboardEvent(KeyboardEvents keyEvent, Keys key,
			bool alt, bool ctrl, bool shift, bool capsLock)
		{
			if (KeyboardEvent == null)
			{
				return false;
			}

			KeyboardHookEventArgs kea = new KeyboardHookEventArgs( key, alt, ctrl, shift, capsLock );
			KeyboardEvent( keyEvent, kea );

			return kea.Cancel;
		}

		public void FilterMessage(Kennedy.ManagedHooks.KeyboardEvents eventType)
		{
		}

		//
		// IHookImpl Members
		//

		public void AddReference()
		{
			referenceCount++;
		}

		public void InstallHook()
		{
			installed = true;
		}

		public int Release()
		{
			referenceCount--;
			if (referenceCount <= 0)
			{
				referenceCount = 0;
				this.Dispose();
			}

			return referenceCount;
		}

		public bool Disposed
		{
			get
			{
				return disposed;
			}
		}

		public void UninstallHook()
		{
			installed = false;
		}

		public bool IsHooked
		{
			get
			{
				return installed;
			}
		}

		//
		// IDisposable members
		//
		public void Dispose()
		{
			disposed = true;
		}
	}
}
