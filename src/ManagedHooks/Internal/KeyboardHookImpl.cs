#region File Header
// /////////////////////////////////////////////////////////////
// Date: 5/15/2005				Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004-2005
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////
#endregion

using System;
using System.Runtime.InteropServices;

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/KeyboardHook/Class/*'/>
	internal class KeyboardHookImpl : SystemHook, IKeyboardHookImpl
	{
		/// <include file='Internal.xml' path='Docs/KeyboardHook/KeyboardEvent/*'/>
		public event KeyboardEventHandler KeyboardEvent;

		/// <include file='Internal.xml' path='Docs/KeyboardHook/ctor/*'/>
		internal KeyboardHookImpl() : base(HookTypes.KeyboardLL)
		{
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHook/HookCallback/*'/>
		protected override bool HookCallback(int code, UIntPtr wparam, IntPtr lparam)
		{
			if (KeyboardEvent == null)
			{
				return false;
			}

			KeyboardEvents kEvent = (KeyboardEvents)wparam.ToUInt32();

			if (kEvent != KeyboardEvents.KeyDown &&
				kEvent != KeyboardEvents.KeyUp &&
				kEvent != KeyboardEvents.SystemKeyDown &&
				kEvent != KeyboardEvents.SystemKeyUp)
			{
				return false;
			}

			KeyboardHookEventArgs kea = GetKeyboardReading( lparam );
			if (kea == null)
			{
				return false;
			}

			KeyboardEvent( kEvent, kea );

			return kea.Cancel;
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHook/FilterMessage/*'/>
		public void FilterMessage(KeyboardEvents eventType)
		{
			base.FilterMessage( this.HookType, (int)eventType );
		}

		/// <include file='Internal.xml' path='Docs/KeyboardHook/GetKeyboardReading/*'/>
		protected KeyboardHookEventArgs GetKeyboardReading(IntPtr lparam)
		{
			ThrowIfDisposed();

			int vkCode;
			int alt;
			int ctrl;
			int shift;
			int capsLock;

			if (!InternalGetKeyboardReading(lparam, out vkCode, out alt, out ctrl, out shift, out capsLock))
			{
				throw new ManagedHooksException( "Failed to access keyboard settings." );
			}

			VirtualKeys vk = (VirtualKeys)vkCode;
			System.Windows.Forms.Keys key  = ConvertKeyCode( vk );

			if ((int)key == -1)
			{
				return null;
			}

			return new KeyboardHookEventArgs( key, alt == 1, ctrl == 1, shift == 1, capsLock == 1 );
		}

		#region Imported Static Methods from SystemHookCore.dll

		/// <include file='Internal.xml' path='Docs/KeyboardHook/IntenralGetKeyboardReading/*'/>
		[DllImport("SystemHookCore.dll", EntryPoint="GetKeyboardReading",  SetLastError=true,
			 CharSet=CharSet.Unicode, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
		private static extern bool InternalGetKeyboardReading(IntPtr lparam, out int vkCode,
			out int alt, out int ctrl, out int shift, out int capsLock);

		#endregion

		#region ConvertKeyCode Method

		/// <include file='Internal.xml' path='Docs/KeyboardHook/ConvertKeyCode/*'/>
		private System.Windows.Forms.Keys ConvertKeyCode(VirtualKeys vk)
		{
			System.Windows.Forms.Keys key = (System.Windows.Forms.Keys)( -1 );

			switch (vk)
			{
				case VirtualKeys.ShiftLeft:
					key = System.Windows.Forms.Keys.Shift;
					break;
				case VirtualKeys.ShiftRight:
					key = System.Windows.Forms.Keys.Shift;
					break;
				case VirtualKeys.ControlLeft:
					key = System.Windows.Forms.Keys.Control;
					break;
				case VirtualKeys.ControlRight:
					key = System.Windows.Forms.Keys.Control;
					break;
				case VirtualKeys.AltLeft:
					key = System.Windows.Forms.Keys.Alt;
					break;
				case VirtualKeys.AltRight:
					key = System.Windows.Forms.Keys.Alt;
					break;
				case VirtualKeys.Back:
					key = System.Windows.Forms.Keys.Back;
					break;
				case VirtualKeys.Tab:
					key = System.Windows.Forms.Keys.Tab;
					break;
				case VirtualKeys.Clear:
					key = System.Windows.Forms.Keys.Clear;
					break;
				case VirtualKeys.Return:
					key = System.Windows.Forms.Keys.Return;
					break;
				case VirtualKeys.Menu:
					key = System.Windows.Forms.Keys.Menu;
					break;
				case VirtualKeys.Pause:
					key = System.Windows.Forms.Keys.Pause;
					break;
				case VirtualKeys.Capital:
					key = System.Windows.Forms.Keys.Capital;
					break;
				case VirtualKeys.Escape:
					key = System.Windows.Forms.Keys.Escape;
					break;
				case VirtualKeys.Space:
					key = System.Windows.Forms.Keys.Space;
					break;
				case VirtualKeys.Prior:
					key = System.Windows.Forms.Keys.Prior;
					break;
				case VirtualKeys.Next:
					key = System.Windows.Forms.Keys.Next;
					break;
				case VirtualKeys.End:
					key = System.Windows.Forms.Keys.End;
					break;
				case VirtualKeys.Home:
					key = System.Windows.Forms.Keys.Home;
					break;
				case VirtualKeys.Left:
					key = System.Windows.Forms.Keys.Left;
					break;
				case VirtualKeys.Up:
					key = System.Windows.Forms.Keys.Up;
					break;
				case VirtualKeys.Right:
					key = System.Windows.Forms.Keys.Right;
					break;
				case VirtualKeys.Down:
					key = System.Windows.Forms.Keys.Down;
					break;
				case VirtualKeys.Select:
					key = System.Windows.Forms.Keys.Select;
					break;
				case VirtualKeys.Print:
					key = System.Windows.Forms.Keys.Print;
					break;
				case VirtualKeys.Execute:
					key = System.Windows.Forms.Keys.Execute;
					break;
				case VirtualKeys.Snapshot:
					key = System.Windows.Forms.Keys.Snapshot;
					break;
				case VirtualKeys.Insert:
					key = System.Windows.Forms.Keys.Insert;
					break;
				case VirtualKeys.Delete:
					key = System.Windows.Forms.Keys.Delete;
					break;
				case VirtualKeys.Help:
					key = System.Windows.Forms.Keys.Help;
					break;
				case VirtualKeys.D0:
					key = System.Windows.Forms.Keys.D0;
					break;
				case VirtualKeys.D1:
					key = System.Windows.Forms.Keys.D1;
					break;
				case VirtualKeys.D2:
					key = System.Windows.Forms.Keys.D2;
					break;
				case VirtualKeys.D3:
					key = System.Windows.Forms.Keys.D3;
					break;
				case VirtualKeys.D4:
					key = System.Windows.Forms.Keys.D4;
					break;
				case VirtualKeys.D5:
					key = System.Windows.Forms.Keys.D5;
					break;
				case VirtualKeys.D6:
					key = System.Windows.Forms.Keys.D6;
					break;
				case VirtualKeys.D7:
					key = System.Windows.Forms.Keys.D7;
					break;
				case VirtualKeys.D8:
					key = System.Windows.Forms.Keys.D8;
					break;
				case VirtualKeys.D9:
					key = System.Windows.Forms.Keys.D9;
					break;
				case VirtualKeys.A:
					key = System.Windows.Forms.Keys.A;
					break;
				case VirtualKeys.B:
					key = System.Windows.Forms.Keys.B;
					break;
				case VirtualKeys.C:
					key = System.Windows.Forms.Keys.C;
					break;
				case VirtualKeys.D:
					key = System.Windows.Forms.Keys.D;
					break;
				case VirtualKeys.E:
					key = System.Windows.Forms.Keys.E;
					break;
				case VirtualKeys.F:
					key = System.Windows.Forms.Keys.F;
					break;
				case VirtualKeys.G:
					key = System.Windows.Forms.Keys.G;
					break;
				case VirtualKeys.H:
					key = System.Windows.Forms.Keys.H;
					break;
				case VirtualKeys.I:
					key = System.Windows.Forms.Keys.I;
					break;
				case VirtualKeys.J:
					key = System.Windows.Forms.Keys.J;
					break;
				case VirtualKeys.K:
					key = System.Windows.Forms.Keys.K;
					break;
				case VirtualKeys.L:
					key = System.Windows.Forms.Keys.L;
					break;
				case VirtualKeys.M:
					key = System.Windows.Forms.Keys.M;
					break;
				case VirtualKeys.N:
					key = System.Windows.Forms.Keys.N;
					break;
				case VirtualKeys.O:
					key = System.Windows.Forms.Keys.O;
					break;
				case VirtualKeys.P:
					key = System.Windows.Forms.Keys.P;
					break;
				case VirtualKeys.Q:
					key = System.Windows.Forms.Keys.Q;
					break;
				case VirtualKeys.R:
					key = System.Windows.Forms.Keys.R;
					break;
				case VirtualKeys.S:
					key = System.Windows.Forms.Keys.S;
					break;
				case VirtualKeys.T:
					key = System.Windows.Forms.Keys.T;
					break;
				case VirtualKeys.U:
					key = System.Windows.Forms.Keys.U;
					break;
				case VirtualKeys.V:
					key = System.Windows.Forms.Keys.V;
					break;
				case VirtualKeys.W:
					key = System.Windows.Forms.Keys.W;
					break;
				case VirtualKeys.X:
					key = System.Windows.Forms.Keys.X;
					break;
				case VirtualKeys.Y:
					key = System.Windows.Forms.Keys.Y;
					break;
				case VirtualKeys.Z:
					key = System.Windows.Forms.Keys.Z;
					break;
				case VirtualKeys.LWindows:
					key = System.Windows.Forms.Keys.LWin;
					break;
				case VirtualKeys.RWindows:
					key = System.Windows.Forms.Keys.RWin;
					break;
				case VirtualKeys.Apps:
					key = System.Windows.Forms.Keys.Apps;
					break;
				case VirtualKeys.NumPad0:
					key = System.Windows.Forms.Keys.NumPad0;
					break;
				case VirtualKeys.NumPad1:
					key = System.Windows.Forms.Keys.NumPad1;
					break;
				case VirtualKeys.NumPad2:
					key = System.Windows.Forms.Keys.NumPad2;
					break;
				case VirtualKeys.NumPad3:
					key = System.Windows.Forms.Keys.NumPad3;
					break;
				case VirtualKeys.NumPad4:
					key = System.Windows.Forms.Keys.NumPad4;
					break;
				case VirtualKeys.NumPad5:
					key = System.Windows.Forms.Keys.NumPad5;
					break;
				case VirtualKeys.NumPad6:
					key = System.Windows.Forms.Keys.NumPad6;
					break;
				case VirtualKeys.NumPad7:
					key = System.Windows.Forms.Keys.NumPad7;
					break;
				case VirtualKeys.NumPad8:
					key = System.Windows.Forms.Keys.NumPad8;
					break;
				case VirtualKeys.NumPad9:
					key = System.Windows.Forms.Keys.NumPad9;
					break;
				case VirtualKeys.Multiply:
					key = System.Windows.Forms.Keys.Multiply;
					break;
				case VirtualKeys.Add:
					key = System.Windows.Forms.Keys.Add;
					break;
				case VirtualKeys.Separator:
					key = System.Windows.Forms.Keys.Separator;
					break;
				case VirtualKeys.Subtract:
					key = System.Windows.Forms.Keys.Subtract;
					break;
				case VirtualKeys.Decimal:
					key = System.Windows.Forms.Keys.Decimal;
					break;
				case VirtualKeys.Divide:
					key = System.Windows.Forms.Keys.Divide;
					break;
				case VirtualKeys.F1:
					key = System.Windows.Forms.Keys.F1;
					break;
				case VirtualKeys.F2:
					key = System.Windows.Forms.Keys.F2;
					break;
				case VirtualKeys.F3:
					key = System.Windows.Forms.Keys.F3;
					break;
				case VirtualKeys.F4:
					key = System.Windows.Forms.Keys.F4;
					break;
				case VirtualKeys.F5:
					key = System.Windows.Forms.Keys.F5;
					break;
				case VirtualKeys.F6:
					key = System.Windows.Forms.Keys.F6;
					break;
				case VirtualKeys.F7:
					key = System.Windows.Forms.Keys.F7;
					break;
				case VirtualKeys.F8:
					key = System.Windows.Forms.Keys.F8;
					break;
				case VirtualKeys.F9:
					key = System.Windows.Forms.Keys.F9;
					break;
				case VirtualKeys.F10:
					key = System.Windows.Forms.Keys.F10;
					break;
				case VirtualKeys.F11:
					key = System.Windows.Forms.Keys.F11;
					break;
				case VirtualKeys.F12:
					key = System.Windows.Forms.Keys.F12;
					break;
				case VirtualKeys.F13:
					key = System.Windows.Forms.Keys.F13;
					break;
				case VirtualKeys.F14:
					key = System.Windows.Forms.Keys.F14;
					break;
				case VirtualKeys.F15:
					key = System.Windows.Forms.Keys.F15;
					break;
				case VirtualKeys.F16:
					key = System.Windows.Forms.Keys.F16;
					break;
				case VirtualKeys.F17:
					key = System.Windows.Forms.Keys.F17;
					break;
				case VirtualKeys.F18:
					key = System.Windows.Forms.Keys.F18;
					break;
				case VirtualKeys.F19:
					key = System.Windows.Forms.Keys.F19;
					break;
				case VirtualKeys.F20:
					key = System.Windows.Forms.Keys.F20;
					break;
				case VirtualKeys.F21:
					key = System.Windows.Forms.Keys.F21;
					break;
				case VirtualKeys.F22:
					key = System.Windows.Forms.Keys.F22;
					break;
				case VirtualKeys.F23:
					key = System.Windows.Forms.Keys.F23;
					break;
				case VirtualKeys.F24:
					key = System.Windows.Forms.Keys.F24;
					break;
				case VirtualKeys.NumLock:
					key = System.Windows.Forms.Keys.NumLock;
					break;
				case VirtualKeys.Scroll:
					key = System.Windows.Forms.Keys.Scroll;
					break;
			}

			return key;
		}

		#endregion

	}
}
