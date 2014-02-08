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
using System.Drawing;
using Kennedy.ManagedHooks;
using Kennedy.ManagedHooks.Internal;

namespace Kennedy.ManagedHooks.Tests
{
	public class MouseHookTestImpl : IMouseHookImpl
	{
		private bool hookInstalled = false;
		private int referenceCount = 0;
		private bool disposed = false;

		public MouseHookTestImpl()
		{
		}

		public void MethodName()
		{
			if (!hookInstalled)
			{
				string msg = "Mouse trigger cannot be used when the hook is uninstalled.";
				throw new InvalidOperationException( msg );
			}
		}

		//
		// Test methods
		//
		public void TriggerMouseAction(MouseEvents mEvent, MouseHookEventArgs mea)
		{
			if (!hookInstalled)
			{
				string msg = "Mouse trigger cannot be used when the hook is uninstalled.";
				throw new InvalidOperationException( msg );
			}

			OnMouseEvent( mEvent, mea );
		}

		private void OnMouseEvent(MouseEvents mEvent, MouseHookEventArgs mea)
		{
			if (MouseEvent == null)
			{
				return;
			}

			MouseEvent( mEvent, mea );
		}

		public int ReferenceCount
		{
			get
			{
				return referenceCount;
			}
		}

		//
		// IMouseHookImpl Members
		//

		public event Kennedy.ManagedHooks.MouseEventHandler MouseEvent;

		public void FilterMessage(Kennedy.ManagedHooks.MouseEvents eventType)
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
			hookInstalled = true;
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
			hookInstalled = false;
		}

		public bool IsHooked
		{
			get
			{
				return hookInstalled;
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
