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

const DWORD HIBIT_FLAG = 0x8000;

int GetMousePosition(LPARAM lparam, int & x, int & y, 
					 int & alt, int & ctrl, int & shift)
{
	MOUSEHOOKSTRUCT * pMouseStruct = (MOUSEHOOKSTRUCT *)lparam;

	if (pMouseStruct == NULL)
	{
		x = 0;
		y = 0;

		shift = 0;
		ctrl = 0;
		alt = 0;

		return 0;
	}
	
	x = pMouseStruct->pt.x;
	y = pMouseStruct->pt.y;

	//
	// Use the GetKeyState method to determine which, if any, of the monitored
	// system keys were pressed. 
	//
	alt = ((GetKeyState(VK_MENU) & HIBIT_FLAG) == HIBIT_FLAG) ? 1 : 0;
	ctrl = ((GetKeyState(VK_CONTROL) & HIBIT_FLAG) == HIBIT_FLAG) ? 1 : 0;
	shift = ((GetKeyState(VK_SHIFT) & HIBIT_FLAG) == HIBIT_FLAG) ? 1 : 0;
	
	return 1;
}

int GetKeyboardReading(LPARAM lparam, int & vkCode, int & alt,
						int & ctrl, int & shift, int & capsLock)
{
	KBDLLHOOKSTRUCT * pKeyboardStruct = (KBDLLHOOKSTRUCT *)lparam;

	if (pKeyboardStruct == NULL)
	{
		vkCode = 0;
		shift = 0;
		ctrl = 0;
		alt = 0;
		capsLock = 0;
		
		return 0;
	}
	
	vkCode = pKeyboardStruct->vkCode;

	//
	// Use the GetKeyState method to determine which, if any, of the monitored
	// system keys were pressed. 
	//
	alt = ((GetKeyState(VK_MENU) & HIBIT_FLAG) == HIBIT_FLAG) ? 1 : 0;
	ctrl = ((GetKeyState(VK_CONTROL) & HIBIT_FLAG) == HIBIT_FLAG) ? 1 : 0;
	capsLock = ((GetKeyState(VK_CAPITAL) & HIBIT_FLAG) == HIBIT_FLAG) ? 1 : 0;
	shift = ((GetKeyState(VK_SHIFT) & HIBIT_FLAG) == HIBIT_FLAG) ? 1 : 0;

	return 1;
}

