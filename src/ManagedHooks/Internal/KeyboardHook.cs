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
using Kennedy.ManagedHooks;

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/KeyboardHook/Class/*'/>
	internal class KeyboardHook : IKeyboardHook, IDisposable
	{
		/// <include file='Internal.xml' path='Docs/KeyboardHook/KeyboardEvent/*'/>
		public event KeyboardEventHandler KeyboardEvent;

		private IKeyboardHookImpl keyboardImpl;

		/// <include file='Internal.xml' path='Docs/KeyboardHook/ctor/*'/>
		internal KeyboardHook(IKeyboardHookImpl keyboardImpl)
		{
			if (keyboardImpl == null)
			{
				throw new ArgumentNullException( "keyboardImpl" );
			}

			this.keyboardImpl = keyboardImpl;
			keyboardImpl.AddReference();

			keyboardImpl.KeyboardEvent += new KeyboardEventHandler( keyboardImpl_KeyboardEvent );
		}

		~KeyboardHook()
		{
			Dispose( false );
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHook/Dispose/*'/>
		public void Dispose()
		{
			GC.SuppressFinalize( this );
			Dispose( true );
		}

		protected virtual void Dispose(bool disposing)
		{
			if (keyboardImpl != null)
			{
				keyboardImpl.Release();
			}
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHook/FilterMessage/*'/>
		public void FilterMessage(KeyboardEvents eventType)
		{
			keyboardImpl.FilterMessage( eventType );
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHook/InstallHook/*'/>
		public void InstallHook()
		{
			keyboardImpl.InstallHook();
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHook/UninstallHook/*'/>
		public void UninstallHook()
		{
			keyboardImpl.UninstallHook();
		}

		public bool IsHooked
		{
			get
			{
				return keyboardImpl.IsHooked;
			}
		}

		private void keyboardImpl_KeyboardEvent(KeyboardEvents kEvent, KeyboardHookEventArgs kea)
		{
			OnKeyboardEvent( kEvent, kea );
		}

		protected virtual void OnKeyboardEvent(KeyboardEvents kEvent, KeyboardHookEventArgs kea)
		{
			if (KeyboardEvent == null)
			{
				return;
			}

			KeyboardEvent( kEvent, kea );
		}
	}
}
