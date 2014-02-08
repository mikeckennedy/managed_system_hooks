using System;

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/ISystemHook/Interface/*'/>
	public interface ISystemHook : IDisposable
	{
		/// <include file='ManagedHooks.xml' path='Docs/SystemHook/IsHooked/*'/>
		bool IsHooked { get; }

		/// <include file='ManagedHooks.xml' path='Docs/IMouseHook/InstallHook/*'/>
		void InstallHook();
		/// <include file='ManagedHooks.xml' path='Docs/IMouseHook/UninstallHook/*'/>
		void UninstallHook();
	}
}
