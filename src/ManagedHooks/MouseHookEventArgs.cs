using System;
using System.Drawing;
using System.Text;

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/Class/*'/>
	public class MouseHookEventArgs
	{
		private static MouseHookEventArgs empty = new MouseHookEventArgs();

		private Point location = Point.Empty;
		private bool cancel = false;

		private bool alt = false;
		private bool ctrl = false;
		private bool shift = false;

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/ctor/*'/>
		public MouseHookEventArgs()
		{
		}

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/ctor_key/*'/>
		public MouseHookEventArgs(Point location, bool alt, bool ctrl, bool shift)
		{
			Location = location;

			this.alt = alt;
			this.ctrl = ctrl;
			this.shift = shift;
		}

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/Location/*'/>
		public Point Location
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
			}
		}

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/Alt/*'/>
		public bool Alt
		{
			get { return alt; }
		}

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/Ctrl/*'/>
		public bool Ctrl
		{
			get { return ctrl; }
		}

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/Shift/*'/>
		public bool Shift
		{
			get { return shift; }
		}

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/Cancel/*'/>
		public bool Cancel
		{
			get { return cancel; }
			set
			{
				//
				// Once it has been set to cancel, don't allow other clients to
				// undo that.
				//
				cancel = cancel || value;
			}
		}

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/Empty/*'/>
		public static MouseHookEventArgs Empty
		{
			get
			{
				return empty;
			}
		}

		/// <include file='ManagedHooks.xml' path='Docs/MouseHookEventArgs/ToString/*'/>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append( "Location=(").Append(Location.X).Append(",").Append(Location.Y).Append(")" );
			sb.Append(" (alt=").Append( GetDisplayText(alt) );
			sb.Append(", ctrl=").Append( GetDisplayText(ctrl) );
			sb.Append(", shift=").Append( GetDisplayText(shift) );
			sb.Append(")");

			return sb.ToString();
		}

		private string GetDisplayText(bool state)
		{
			return ( state ? "On" : "Off" );
		}
	}
}
