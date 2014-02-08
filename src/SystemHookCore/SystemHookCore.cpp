// /////////////////////////////////////////////////////////////
// Date: 2/25/2004			 Author: Michael Kennedy
//
// Copyright: Copyright (c) Michael Kennedy, 2004-2005
// /////////////////////////////////////////////////////////////
// License: See License.txt file included with application.
// Description: See compiled documentation (Managed Hooks.chm)
// /////////////////////////////////////////////////////////////

#include "stdafx.h"
#include <windows.h>
#include "SystemHookCore.h"
#include "MessageFilter.h"

UserHookProc UserMouseHookCallback = NULL;
UserHookProc UserKeyboardHookCallback = NULL;
HHOOK hookMouse = NULL;
HHOOK hookKeyboard = NULL;

//
// Store the application instance of this module to pass to
// hook initialization. This is set in DLLMain().
//
HINSTANCE g_appInstance = NULL;

MessageFilter mouseFilter;
MessageFilter keyboardFilter;

static LRESULT CALLBACK InternalKeyboardHookCallback(int code, WPARAM wparam, LPARAM lparam);
static LRESULT CALLBACK InternalMouseHookCallback(int code, WPARAM wparam, LPARAM lparam);

int SetUserHookCallback(UserHookProc userProc, UINT hookID)
{	
	if (userProc == NULL)
	{
		return HookCoreErrors::SetCallBack::ARGUMENT_ERROR;
	}

	if (hookID == WH_KEYBOARD_LL)
	{
		if (UserKeyboardHookCallback != NULL)
		{
			return HookCoreErrors::SetCallBack::ALREADY_SET;
		}

		UserKeyboardHookCallback = userProc;
		keyboardFilter.Clear();
		return HookCoreErrors::SetCallBack::SUCCESS;
	}
	else if (hookID == WH_MOUSE_LL)
	{
		if (UserMouseHookCallback != NULL)
		{
			return HookCoreErrors::SetCallBack::ALREADY_SET;
		}

		UserMouseHookCallback = userProc;
		mouseFilter.Clear();
		return HookCoreErrors::SetCallBack::SUCCESS;
	}

	return HookCoreErrors::SetCallBack::NOT_IMPLEMENTED;
}

int InitializeHook(UINT hookID, int threadID)
{
	if (g_appInstance == NULL)
	{
		return 0;
	}

	if (hookID == WH_KEYBOARD_LL)
	{
		if (UserKeyboardHookCallback == NULL)
		{
			return 0;
		}

		hookKeyboard = SetWindowsHookEx(hookID, (HOOKPROC)InternalKeyboardHookCallback, g_appInstance, threadID);
		return (hookKeyboard != NULL) ? 1 : 0;
	}
	else if (hookID == WH_MOUSE_LL)
	{
		if (UserMouseHookCallback == NULL)
		{
			return 0;
		}

		hookMouse = SetWindowsHookEx(hookID, (HOOKPROC)InternalMouseHookCallback, g_appInstance, threadID);
		return (hookMouse != NULL) ? 1 : 0;
	}

	return 0;
}

void UninitializeHook(UINT hookID)
{
	if (hookID == WH_KEYBOARD_LL)
	{
		if (hookKeyboard != NULL)
		{
			UnhookWindowsHookEx(hookKeyboard);
		}
		hookKeyboard = NULL;
	}
	else if (hookID == WH_MOUSE_LL)
	{
		if (hookMouse != NULL)
		{
			UnhookWindowsHookEx(hookMouse);
		}
		hookMouse = NULL;
	}
}

void Dispose(UINT hookID)
{
	if (hookID == WH_KEYBOARD_LL)
	{
		UserKeyboardHookCallback = NULL;
	}
	else if (hookID == WH_MOUSE_LL)
	{
		UserMouseHookCallback = NULL;
	}
}

int FilterMessage(UINT hookID, int message)
{
	if (hookID == WH_KEYBOARD_LL)
	{
		if (keyboardFilter.AddMessage(message))
		{
			return HookCoreErrors::FilterMessage::SUCCESS;
		}
		else
		{
			return HookCoreErrors::FilterMessage::FAILED;
		}
	}
	else if (hookID == WH_MOUSE_LL)
	{
		if(mouseFilter.AddMessage(message))
		{
			return HookCoreErrors::FilterMessage::SUCCESS;
		}
		else
		{
			return HookCoreErrors::FilterMessage::FAILED;
		}
	}

	return HookCoreErrors::FilterMessage::NOT_IMPLEMENTED;
}

static LRESULT CALLBACK InternalMouseHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
	if (code < 0)
	{
		return CallNextHookEx(hookMouse, code, wparam, lparam);
	}

	if (UserMouseHookCallback != NULL && !mouseFilter.IsFiltered((int)wparam))
	{
		bool result = UserMouseHookCallback(code, wparam, lparam);
		if (result)
		{
			return 1;
		}
	}

	return CallNextHookEx(hookMouse, code, wparam, lparam);
}

static LRESULT CALLBACK InternalKeyboardHookCallback(int code, WPARAM wparam, LPARAM lparam)
{
	if (code < 0)
	{
		return CallNextHookEx(hookKeyboard, code, wparam, lparam);
	}

	if (UserKeyboardHookCallback != NULL && !keyboardFilter.IsFiltered((int)wparam))
	{
		bool result = UserKeyboardHookCallback(code, wparam, lparam);
		if (result)
		{
			return 1;
		}
	}

	return CallNextHookEx(hookKeyboard, code, wparam, lparam);

}
