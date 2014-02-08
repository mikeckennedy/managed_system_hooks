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

using System;
using Kennedy.ManagedHooks.Internal;

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/HookFactory/Class/*'/>
	public class HookFactory
	{
		private static IKeyboardHookImpl keyboardImpl = null;
		private static IMouseHookImpl mouseImpl = null;

		private HookFactory() { }

		//
		// Keyboard hook related methods
		//

		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateKeyboardHook/*'/>
		public static IKeyboardHook CreateKeyboardHook()
		{
			if (keyboardImpl == null || keyboardImpl.Disposed)
			{
				keyboardImpl = new KeyboardHookImpl();
			}

			return CreateKeyboardHook( keyboardImpl );
		}

		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateKeyboardHook_impl/*'/>
		public static IKeyboardHook CreateKeyboardHook(IKeyboardHookImpl keyboardImpl)
		{
			return new KeyboardHook( keyboardImpl );
		}

		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateKeyboardHookExt/*'/>
		public static IKeyboardHookExt CreateKeyboardHookExt()
		{
			if (keyboardImpl == null || keyboardImpl.Disposed)
			{
				keyboardImpl = new KeyboardHookImpl();
			}

			return CreateKeyboardHookExt( keyboardImpl );
		}

		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateKeyboardHookExt_impl/*'/>
		public static IKeyboardHookExt CreateKeyboardHookExt(IKeyboardHookImpl keyboardImpl)
		{
			return new KeyboardHookExt( keyboardImpl );
		}

		//
		// Mouse hook related methods.
		//

		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateMouseHook/*'/>
		public static IMouseHook CreateMouseHook()
		{
			if (mouseImpl == null || mouseImpl.Disposed)
			{
				mouseImpl = new MouseHookImpl();
			}

			return CreateMouseHook( mouseImpl );
		}

		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateMouseHook_impl/*'/>
		public static IMouseHook CreateMouseHook(IMouseHookImpl mouseImpl)
		{
			return new MouseHook( mouseImpl );
		}


		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateMouseHookExt/*'/>
		public static IMouseHookExt CreateMouseHookExt()
		{
			if (mouseImpl == null || mouseImpl.Disposed)
			{
				mouseImpl = new MouseHookImpl();
			}

			return CreateMouseHookExt( mouseImpl );
		}

		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateMouseHookExt_impl/*'/>
		public static IMouseHookExt CreateMouseHookExt(IMouseHookImpl mouseImpl)
		{
			return new MouseHookExt( mouseImpl );
		}


		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateMouseHookLite/*'/>
		public static IMouseHookLite CreateMouseHookLite()
		{
			if (mouseImpl == null || mouseImpl.Disposed)
			{
				mouseImpl = new MouseHookImpl();
			}

			return CreateMouseHookLite( mouseImpl );
		}

		/// <include file='ManagedHooks.xml' path='Docs/HookFactory/CreateMouseHookLite_impl/*'/>
		public static IMouseHookExt CreateMouseHookLite(IMouseHookImpl mouseImpl)
		{
			MouseHookExt mouseHookExt = new MouseHookExt( mouseImpl );
			mouseHookExt.FilterMessage( MouseEvents.Move );

			return mouseHookExt;
		}

	}
}
