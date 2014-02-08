// /////////////////////////////////////////////////////////////
// Date: 2/25/2004			Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004-2005
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////

#pragma once

namespace HookCoreErrors
{
	namespace SetCallBack
	{
		const int SUCCESS = 1;
		const int ALREADY_SET = -2;
		const int NOT_IMPLEMENTED = -3;
		const int ARGUMENT_ERROR = -4;
	}
	namespace FilterMessage
	{
		const int SUCCESS = 1;
		const int FAILED = -2;
		const int NOT_IMPLEMENTED = -3;
	}
}

//typedef void (CALLBACK *HookProc)(int code, WPARAM w, LPARAM l);
typedef bool (CALLBACK *UserHookProc)(int code, WPARAM w, LPARAM l);

int SetUserHookCallback(UserHookProc userProc, UINT hookID);
int InitializeHook(UINT hookID, HINSTANCE appInstance, int threadID);
void UninitializeHook(UINT hookID);
void Dispose(UINT hookID);
int FilterMessage(UINT hookID, int message);
int GetMousePosition(WPARAM wparam, LPARAM lparam, int & x, int & y);
int GetKeyboardReading(LPARAM lparam, int & vkCode, int & shift, int & ctrl, int & alt, int & capsLock);

