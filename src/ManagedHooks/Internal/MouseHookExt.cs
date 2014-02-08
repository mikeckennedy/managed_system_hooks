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
	/// <include file='Internal.xml' path='Docs/MouseHookExt/Class/*'/>
	internal class MouseHookExt : MouseHook, IMouseHookExt
	{
		/// <include file='Internal.xml' path='Docs/MouseHookExt/LeftButtonDown/*'/>
		public event MouseEventHandlerExt LeftButtonDown;
		/// <include file='Internal.xml' path='Docs/MouseHookExt/RightButtonDown/*'/>
		public event MouseEventHandlerExt RightButtonDown;
		/// <include file='Internal.xml' path='Docs/MouseHookExt/LeftButtonUp/*'/>
		public event MouseEventHandlerExt LeftButtonUp;
		/// <include file='Internal.xml' path='Docs/MouseHookExt/RightButtonUp/*'/>
		public event MouseEventHandlerExt RightButtonUp;
		/// <include file='Internal.xml' path='Docs/MouseHookExt/MouseWheel/*'/>
		public event MouseEventHandlerExt MouseWheel;
		/// <include file='Internal.xml' path='Docs/MouseHookExt/Move/*'/>
		public event MouseEventHandlerExt Move;

		/// <include file='Internal.xml' path='Docs/MouseHookExt/ctor/*'/>
		internal MouseHookExt(IMouseHookImpl mouseHookImpl) : base(mouseHookImpl)
		{
		}

		protected override void OnMouseEvent(MouseEvents mEvent, MouseHookEventArgs mea)
		{
			switch (mEvent)
			{
				case MouseEvents.LeftButtonUp:
					Fire_LeftButtonUp( mea );
					break;
				case MouseEvents.LeftButtonDown:
					Fire_LeftButtonDown( mea );
					break;
				case MouseEvents.RightButtonUp:
					Fire_RightButtonUp( mea );
					break;
				case MouseEvents.RightButtonDown:
					Fire_RightButtonDown( mea );
					break;
				case MouseEvents.MouseWheel:
					Fire_MouseWheel( mea );
					break;
				case MouseEvents.Move:
					Fire_Move( mea );
					break;
			}
		}

		private void Fire_LeftButtonDown(MouseHookEventArgs mea)
		{
			if (LeftButtonDown != null)
			{
				LeftButtonDown( mea );
			}
		}

		private void Fire_LeftButtonUp(MouseHookEventArgs mea)
		{
			if (LeftButtonUp != null)
			{
				LeftButtonUp( mea );
			}
		}

		private void Fire_RightButtonDown(MouseHookEventArgs mea)
		{
			if (RightButtonDown != null)
			{
				RightButtonDown( mea );
			}
		}

		private void Fire_RightButtonUp(MouseHookEventArgs mea)
		{
			if (RightButtonUp != null)
			{
				RightButtonUp( mea );
			}
		}

		private void Fire_Move(MouseHookEventArgs mea)
		{
			if (Move != null)
			{
				Move( mea );
			}
		}

		private void Fire_MouseWheel(MouseHookEventArgs mea)
		{
			if (MouseWheel != null)
			{
				MouseWheel( mea );
			}
		}
	}
}
