#region File Header
// /////////////////////////////////////////////////////////////
// Date: 2/25/2004					Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004-2005
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////
#endregion

using System;
using System.Windows.Forms;
using System.Text;

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/Class/*'/>
	public class KeyboardHookEventArgs
	{
		private Keys key = Keys.None;

		private bool alt = false;
		private bool ctrl = false;
		private bool shift = false;
		private bool capsLock = false;

		private bool cancel = false;

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/ctor/*'/>
		public KeyboardHookEventArgs()
		{
		}

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/ctor_key/*'/>
		public KeyboardHookEventArgs(Keys key, bool alt, bool ctrl, bool shift, bool capsLock)
		{
			this.key = key;

			this.alt = alt;
			this.ctrl = ctrl;
			this.shift = shift;
			this.capsLock = capsLock;
		}

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/Key/*'/>
		public System.Windows.Forms.Keys Key
		{
			get { return key; }
		}

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/Cancel/*'/>
		public bool Cancel
		{
			get { return cancel; }
			set
			{
				//
				// Once it has been set to cancel, don't allow other clients
				// to undo that.
				//
				cancel = cancel || value;
			}
		}

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/Alt/*'/>
		public bool Alt
		{
			get { return alt; }
		}

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/Ctrl/*'/>
		public bool Ctrl
		{
			get { return ctrl; }
		}

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/Shift/*'/>
		public bool Shift
		{
			get { return shift; }
		}

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/CapsLock/*'/>
		public bool CapsLock
		{
			get { return capsLock; }
		}

		/// <include file='ManagedHooks.xml' path='Docs/KeyboardHookEventArgs/ToString/*'/>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(key).Append(" (alt=").Append( GetDisplayText(alt) );
			sb.Append(", ctrl=").Append( GetDisplayText(ctrl) );
			sb.Append(", shift=").Append( GetDisplayText(shift) );
			sb.Append(", capsLock=").Append( GetDisplayText(capsLock) );
			sb.Append(")");

			return sb.ToString();
		}

		private string GetDisplayText(bool state)
		{
			return ( state ? "On" : "off" );
		}

	}
}
