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
using System.Runtime.InteropServices;
using System.Drawing;

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/MouseHook/Class/*'/>
	public class MouseHookImpl : SystemHook, IMouseHookImpl
	{
		/// <include file='Internal.xml' path='Docs/MouseHook/MouseEvent/*'/>
		public event MouseEventHandler MouseEvent;

		/// <include file='Internal.xml' path='Docs/MouseHook/ctor/*'/>
		public MouseHookImpl() : base(HookTypes.MouseLL)
		{
		}

		/// <include file='Internal.xml' path='Docs/MouseHook/HookCallback/*'/>
		protected override bool HookCallback(int code, UIntPtr wparam, IntPtr lparam)
		{
			if (MouseEvent == null)
			{
				return false;
			}

			MouseHookEventArgs mouseArgs = MouseHookEventArgs.Empty;

			MouseEvents mEvent = (MouseEvents)wparam.ToUInt32();

			switch(mEvent)
			{
				case MouseEvents.LeftButtonDown:
					mouseArgs = GetMousePosition( lparam );
					break;
				case MouseEvents.LeftButtonUp:
					mouseArgs = GetMousePosition( lparam );
					break;
				case MouseEvents.MouseWheel:
					break;
				case MouseEvents.Move:
					mouseArgs = GetMousePosition( lparam );
					break;
				case MouseEvents.RightButtonDown:
					mouseArgs = GetMousePosition( lparam );
					break;
				case MouseEvents.RightButtonUp:
					mouseArgs = GetMousePosition( lparam );
					break;
				default:
					//System.Diagnostics.Debug.WriteLine( "Unrecognized mouse event: " + mEvent );
					break;
			}

			MouseEvent( mEvent, mouseArgs );
			
			return mouseArgs.Cancel;
		}

		/// <include file='Internal.xml' path='Docs/MouseHook/FilterMessage/*'/>
		public void FilterMessage(MouseEvents eventType)
		{
			ThrowIfDisposed();

			base.FilterMessage( this.HookType, (int)eventType );
		}

		/// <include file='Internal.xml' path='Docs/MouseHook/GetMousePosition/*'/>
		protected MouseHookEventArgs GetMousePosition(IntPtr lparam)
		{
			ThrowIfDisposed();

			int x = 0;
			int y = 0;

			int alt = 0;
			int ctrl = 0;
			int shift = 0;

			if (InternalGetMousePosition(lparam, out x, out y, out alt, out ctrl, out shift) != 1)
			{
				throw new ManagedHooksException( "Failed to access mouse position." );
			}

			MouseHookEventArgs mouseArgs = new MouseHookEventArgs(
				new Point(x, y), alt == 1, ctrl == 1, shift == 1);

			return mouseArgs;
		}

		#region Imported Static Methods from SystemHookCore.dll

		/// <include file='Internal.xml' path='Docs/MouseHook/InternalGetMousePosition/*'/>
		[DllImport("SystemHookCore.dll", EntryPoint="GetMousePosition",  SetLastError=true,
			 CharSet=CharSet.Unicode, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
		private static extern int InternalGetMousePosition(IntPtr lparam, out int x, out int y,
			out int alt, out int ctrl, out int shift);

		#endregion
	}
}
