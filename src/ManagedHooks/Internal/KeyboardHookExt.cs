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

using System;

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/KeyboardHookExt/Class/*'/>
	internal class KeyboardHookExt : KeyboardHook, IKeyboardHookExt
	{
		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/KeyDown/*'/>
		public event KeyboardEventHandlerExt KeyDown;
		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/KeyUp/*'/>
		public event KeyboardEventHandlerExt KeyUp;
		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/SystemKeyDown/*'/>
		public event KeyboardEventHandlerExt SystemKeyDown;
		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/SystemKeyUp/*'/>
		public event KeyboardEventHandlerExt SystemKeyUp;

		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/ctor/*'/>
		internal KeyboardHookExt(IKeyboardHookImpl keyboardImpl) : base(keyboardImpl)
		{
		}

		protected override void OnKeyboardEvent(KeyboardEvents kEvent, KeyboardHookEventArgs kea)
		{
			switch (kEvent)
			{
				case KeyboardEvents.KeyDown:
					OnKeyDown( kea );
					break;
				case KeyboardEvents.KeyUp:
					OnKeyUp( kea );
					break;
				case KeyboardEvents.SystemKeyDown:
					OnSysKeyDown( kea );
					break;
				case KeyboardEvents.SystemKeyUp:
					OnSysKeyUp( kea );
					break;
			}
		}


		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/OnKeyDown/*'/>
		protected virtual void OnKeyDown(KeyboardHookEventArgs kea)
		{
			Fire_KeyDown( kea );
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/OnKeyUp/*'/>
		protected virtual void OnKeyUp(KeyboardHookEventArgs kea)
		{
			Fire_KeyUp( kea );
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/OnSysKeyDown/*'/>
		protected virtual void OnSysKeyDown(KeyboardHookEventArgs kea)
		{
			Fire_SystemKeyDown( kea );
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHookExt/OnSysKeyUp/*'/>
		protected virtual void OnSysKeyUp(KeyboardHookEventArgs kea)
		{
			Fire_SystemKeyUp( kea );
		}

		private void Fire_KeyDown(KeyboardHookEventArgs kea)
		{
			if (KeyDown != null)
			{
				KeyDown( this, kea );
			}
		}

		private void Fire_KeyUp(KeyboardHookEventArgs kea)
		{
			if (KeyUp != null)
			{
				KeyUp( this, kea );
			}
		}

		private void Fire_SystemKeyDown(KeyboardHookEventArgs kea)
		{
			if (SystemKeyDown != null)
			{
				SystemKeyDown( this, kea );
			}
		}

		private void Fire_SystemKeyUp(KeyboardHookEventArgs kea)
		{
			if (SystemKeyUp != null)
			{
				SystemKeyUp( this, kea );
			}
		}

	}
}
