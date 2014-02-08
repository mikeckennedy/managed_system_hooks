using System;

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/IMouseHookLite/Interface/*'/>
	public interface IMouseHookLite : IDisposable, ISystemHook
	{
		/// <include file='ManagedHooks.xml' path='Docs/IMouseHookExt/LeftButtonDown/*'/>
		event MouseEventHandlerExt LeftButtonDown;
		/// <include file='ManagedHooks.xml' path='Docs/IMouseHookExt/RightButtonDown/*'/>
		event MouseEventHandlerExt RightButtonDown;
		/// <include file='ManagedHooks.xml' path='Docs/IMouseHookExt/LeftButtonUp/*'/>
		event MouseEventHandlerExt LeftButtonUp;
		/// <include file='ManagedHooks.xml' path='Docs/IMouseHookExt/RightButtonUp/*'/>
		event MouseEventHandlerExt RightButtonUp;
		/// <include file='ManagedHooks.xml' path='Docs/IMouseHookExt/MouseWheel/*'/>
		event MouseEventHandlerExt MouseWheel;

		/// <include file='ManagedHooks.xml' path='Docs/IMouseHook/FilterMessage/*'/>
		void FilterMessage( MouseEvents eventType );
	}
}
