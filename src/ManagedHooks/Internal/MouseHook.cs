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
using System.Drawing;

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/MouseHook/Class/*'/>
	internal class MouseHook : IMouseHook
	{
		/// <include file='Internal.xml' path='Docs/MouseHook/MouseEvent/*'/>
		public event MouseEventHandler MouseEvent;

		private IMouseHookImpl mouseHookImpl;

		/// <include file='Internal.xml' path='Docs/MouseHook/ctor/*'/>
		internal MouseHook(IMouseHookImpl mouseHookImpl) //: base(HookTypes.MouseLL)
		{
			if (mouseHookImpl == null)
			{
				throw new ArgumentNullException( "mouseHookImpl" );
			}

			this.mouseHookImpl = mouseHookImpl;
			this.mouseHookImpl.AddReference();

			mouseHookImpl.MouseEvent += new MouseEventHandler( mouseHookImpl_MouseEvent );
		}

		~MouseHook()
		{
			Dispose( false );
		}

		public void Dispose()
		{
			GC.SuppressFinalize( this );
			Dispose( true );
		}

		protected virtual void Dispose(bool disposing)
		{
			if (mouseHookImpl != null)
			{
				mouseHookImpl.Release();
			}
		}

		private void mouseHookImpl_MouseEvent(MouseEvents mEvent, MouseHookEventArgs mea)
		{
			OnMouseEvent( mEvent, mea );
		}

		protected virtual void OnMouseEvent(MouseEvents mEvent, MouseHookEventArgs mea)
		{
			if (MouseEvent == null)
			{
				return;
			}

			MouseEvent( mEvent, mea );
		}

		/// <include file='Internal.xml' path='Docs/MouseHook/FilterMessage/*'/>
		public void FilterMessage(MouseEvents eventType)
		{
			mouseHookImpl.FilterMessage( eventType );
		}

		public void InstallHook()
		{
			mouseHookImpl.InstallHook();
		}

		public void UninstallHook()
		{
			mouseHookImpl.UninstallHook();
		}

		public bool IsHooked
		{
			get
			{
				return mouseHookImpl.IsHooked;
			}
		}
	}
}
