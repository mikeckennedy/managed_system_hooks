using System;

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/IMouseHookExt/Interface/*'/>
	public interface IMouseHookExt : IMouseHookLite, IDisposable
	{
		/// <include file='ManagedHooks.xml' path='Docs/IMouseHookExt/Move/*'/>
		event MouseEventHandlerExt Move;
	}
}
