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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kennedy.ManagedHooks.Internal
{
	/// <include file='Internal.xml' path='Docs/SystemHook/Class/*'/>
	public abstract class SystemHook : IDisposable
	{
		#region Member Variables and Delegates

		/// <include file='Internal.xml' path='Docs/SystemHook/HookProcessedHandler/*'/>
		protected delegate int HookProcessedHandler( int code, UIntPtr wparam, IntPtr lparam );

		/// <include file='Internal.xml' path='Docs/SystemHook/type/*'/>
		private HookTypes type = HookTypes.None;

		/// <include file='Internal.xml' path='Docs/SystemHook/processHandler/*'/>
		private HookProcessedHandler processHandler = null;

		private bool isHooked = false;
		private bool disposed = false;
		private int references = 0;

		#endregion

		/// <include file='Internal.xml' path='Docs/SystemHook/ctor/*'/>
		public SystemHook(HookTypes type)
		{
			this.type = type;

			processHandler = new HookProcessedHandler( InternalHookCallback );
			SetCallBackResults result = SetUserHookCallback( processHandler, type );
			if (result != SetCallBackResults.Success)
			{
				this.Dispose();
				GenerateCallBackException( type, result );
			}
		}

		/// <include file='Internal.xml' path='Docs/SystemHook/dtor/*'/>
		~SystemHook()
		{
			Trace.WriteLine("SystemHook (" + type + ") WARNING: Finalizer called, " +
				"a reference has not been properly disposed. This instance is still holding " +
				references + " references.");

			Dispose( false );
		}

		/// <include file='Internal.xml' path='Docs/SystemHook/HookCallback/*'/>
		protected abstract bool HookCallback( int code, UIntPtr wparam, IntPtr lparam );

		/// <include file='Internal.xml' path='Docs/SystemHook/InternalHookCallback/*'/>
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int InternalHookCallback(int code, UIntPtr wparam, IntPtr lparam)
		{
			const int Block = 1;
			const int Allow = 0;

			try
			{
				bool cancelRequested = HookCallback( code, wparam, lparam );
				if ( !cancelRequested )
				{
					return Allow;
				}
				else
				{
					return Block;
				}
			}
			catch (Exception e)
			{
				//
				// While it is not generally a good idea to trap and discard all exceptions
				// in a base class, this is a special case. Remember, this is the entry point
				// for the C++ library to call into our .NET code. We don't want to return
				// .NET exceptions to C++. If it gets this far all we can do is drop them.
				//
				Debug.WriteLine( "Exception during hook callback: " + e.Message + " " + e.ToString() );

				return Allow;
			}
		}

		/// <include file='Internal.xml' path='Docs/IHookImpl/AddReference/*'/>
		public void AddReference()
		{
			references++;
		}

		/// <include file='Internal.xml' path='Docs/IHookImpl/Release/*'/>
		public int Release()
		{
			references = Math.Max( 0, references - 1 );

			if (references == 0)
			{
				this.Dispose();
			}

			return references;
		}

		/// <include file='Internal.xml' path='Docs/SystemHook/InstallHook/*'/>
		public void InstallHook()
		{
			ThrowIfDisposed();

			if (InitializeHook(type, 0) != 1)
			{
				throw new ManagedHooksException( "Hook initialization failed." );
			}
			isHooked = true;
		}

		/// <include file='Internal.xml' path='Docs/SystemHook/UninstallHook/*'/>
		public void UninstallHook()
		{
			ThrowIfDisposed();

			isHooked = false;
			UninitializeHook( type );
		}

		/// <include file='Internal.xml' path='Docs/SystemHook/HookType/*'/>
		protected HookTypes HookType
		{
			get
			{
				return type;
			}
		}

		/// <include file='Internal.xml' path='Docs/IHookImpl/Disposed/*'/>
		public bool Disposed
		{
			get
			{
				return disposed;
			}
		}

		/// <include file='Internal.xml' path='Docs/SystemHook/IsHooked/*'/>
		public bool IsHooked
		{
			get
			{
				return isHooked;
			}
		}

		#region Exception Generation Helper Methods

		private void GenerateCallBackException(HookTypes type, SetCallBackResults result)
		{
			if (result == SetCallBackResults.Success)
			{
				return;
			}

			string msg;

			switch (result)
			{
				case SetCallBackResults.AlreadySet:
					msg = "A hook of type " + type + " is already registered. You can only register ";
					msg += "a single instance of each type of hook class. This can also occur when you forget ";
					msg += "to unregister or dispose a previous instance of the class.";

					throw new ManagedHooksException( msg );

				case SetCallBackResults.ArgumentError:
					msg = "Failed to set hook callback due to an error in the arguments.";

					throw new ArgumentException( msg );

				case SetCallBackResults.NotImplemented:
					msg = "The hook type of type " + type + " is not implemented in the C++ layer. ";
					msg += "You must implement this hook type before you can use it. See the C++ function ";
					msg += "SetUserHookCallback.";

					throw new HookTypeNotImplementedException( msg );
			}

			msg = "Unrecognized exception during hook callback setup. Error code " + result + ".";
			throw new ManagedHooksException( msg );
		}

		private void GenerateFilterMessageException(HookTypes type, FilterMessageResults result)
		{
			if (result == FilterMessageResults.Success)
			{
				return;
			}

			string msg;

			if (result == FilterMessageResults.NotImplemented)
			{
					msg = "The hook type of type " + type + " is not implemented in the C++ layer. ";
					msg += "You must implement this hook type before you can use it. See the C++ function ";
					msg += "FilterMessage.";

					throw new HookTypeNotImplementedException( msg );
			}

			//
			// All other errors are general errors.
			//
			msg = "Unrecognized exception during hook FilterMessage call. Error code " + result + ".";
			throw new ManagedHooksException( msg );
		}

		/// <include file='Internal.xml' path='Docs/SystemHook/ThrowIfDisposed/*'/>
		protected void ThrowIfDisposed()
		{
			if (!Disposed)
			{
				return;
			}

			string msg = "The hook class of type " + type + " has been disposed.";
			throw new ObjectDisposedException( "SystemHook", msg );
		}

		#endregion

		#region IDisposable Members

		/// <include file='Internal.xml' path='Docs/SystemHook/Dispose/*'/>
		public void Dispose()
		{
			Dispose( true );
		}

		private void Dispose(bool disposing)
		{
			// TODO: Complain if references not zero.

			try
			{
				if (disposing)
				{
					GC.SuppressFinalize( this );
				}

				if (IsHooked)
				{
					UninstallHook();
				}

				DisposeCppLayer( type );
			}
			finally
			{
				disposed = true;
			}
		}

		#endregion

		#region Win32 API Utility Methods

		/// <include file='Internal.xml' path='Docs/SystemHook/FilterMessage/*'/>
		protected void FilterMessage(HookTypes hookType, int message)
		{
			ThrowIfDisposed();

			FilterMessageResults result = InternalFilterMessage( hookType, message );
			if (result != FilterMessageResults.Success)
			{
				GenerateFilterMessageException( hookType, result );
			}
		}

		#endregion

		#region Imported Static Methods for SystemHookCore.dll

		private enum SetCallBackResults
		{
			Success = 1,
			AlreadySet = -2,
			NotImplemented = -3,
			ArgumentError = -4
		};

		private enum FilterMessageResults
		{
			Success = 1,
			Failed = -2,
			NotImplemented = -3
		};

		/// <include file='Internal.xml' path='Docs/SystemHook/SetUserHookCallback/*'/>
		[DllImport("SystemHookCore.dll", EntryPoint="SetUserHookCallback",  SetLastError=true,
			 CharSet=CharSet.Unicode, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
		private static extern SetCallBackResults SetUserHookCallback( HookProcessedHandler hookCallback, HookTypes hookType );

		/// <include file='Internal.xml' path='Docs/SystemHook/InitializeHook/*'/>
		[DllImport("SystemHookCore.dll", EntryPoint="InitializeHook",  SetLastError=true,
			 CharSet=CharSet.Unicode, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
		private static extern int InitializeHook( HookTypes hookType, int threadID );

		/// <include file='Internal.xml' path='Docs/SystemHook/UninitializeHook/*'/>
		[DllImport("SystemHookCore.dll", EntryPoint="UninitializeHook",  SetLastError=true,
			 CharSet=CharSet.Unicode, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
		private static extern void UninitializeHook( HookTypes hookType );

		/// <include file='Internal.xml' path='Docs/SystemHook/DisposeCppLayer/*'/>
		[DllImport("SystemHookCore.dll", EntryPoint="Dispose",  SetLastError=true,
			 CharSet=CharSet.Unicode, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
		private static extern void DisposeCppLayer( HookTypes hookType );

		/// <include file='Internal.xml' path='Docs/SystemHook/InternalFilterMessage/*'/>
		[DllImport("SystemHookCore.dll", EntryPoint="FilterMessage",  SetLastError=true,
			 CharSet=CharSet.Unicode, ExactSpelling=true,
			 CallingConvention=CallingConvention.StdCall)]
		private static extern FilterMessageResults InternalFilterMessage( HookTypes hookType, int message );

		#endregion

	}
}
