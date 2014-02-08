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

namespace Kennedy.ManagedHooks
{
	/// <include file='ManagedHooks.xml' path='Docs/ManagedHooksException/Class/*'/>
	public class ManagedHooksException : ApplicationException
	{
		/// <include file='ManagedHooks.xml' path='Docs/ManagedHooksException/ctor/*'/>
		public ManagedHooksException()
		{
		}

		/// <include file='ManagedHooks.xml' path='Docs/ManagedHooksException/ctor_string/*'/>
		public ManagedHooksException(string message) : base(message)
		{
		}

		/// <include file='ManagedHooks.xml' path='Docs/ManagedHooksException/ctor_string_exception/*'/>
		public ManagedHooksException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
