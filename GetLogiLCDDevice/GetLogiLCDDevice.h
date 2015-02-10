// GetLogiLCDDevice.h

#pragma once

#include <windows.h>
#include <string>

namespace GetLogiLCDDevice {

	public ref class Class1
	{
		// TODO: Die Methoden für diese Klasse hier hinzufügen.
	};

	HANDLE OpenDevice(void)
	{
		HANDLE deviceHandle = INVALID_HANDLE_VALUE;
		HKEY deviceKey;
		LONG result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, L"SYSTEM\\CurrentControlSet\\Enum\\HID\\VID_046D&PID_C22D&MI_01&Col02", 0, KEY_READ, &deviceKey);
		if (result != ERROR_SUCCESS)
		{
			return deviceHandle;
		}
		bool nameFound = false;
		HKEY instanceKey;
		for (unsigned int i = 0, enumStatus = ERROR_SUCCESS; enumStatus == ERROR_SUCCESS && nameFound == false; i++)
		{
			wchar_t keyName[255];
			DWORD keyNameLength = sizeof(keyName);
			enumStatus = RegEnumKeyEx(deviceKey, i, keyName, &keyNameLength, NULL, NULL, NULL, NULL);
			if (enumStatus == ERROR_SUCCESS)
			{
				result = RegOpenKeyEx(deviceKey, keyName, 0, KEY_READ, &instanceKey);
				if (result != ERROR_SUCCESS)
				{
					DWORD error = GetLastError();
					instanceKey = NULL;
				}
			}
			else{
				DWORD error = GetLastError();
				instanceKey = NULL;
			}
			if (instanceKey != NULL)
			{
				std::wstring deviceName = std::wstring(L"\\\\?\\HID#VID_046D&PID_C22D&MI_01&Col02#") + keyName + L"#{4d1e55b2-f16f-11cf-88cb-001111000030}";
				deviceHandle = CreateFile(deviceName.c_str(), GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, 0, NULL);

				if (deviceHandle != INVALID_HANDLE_VALUE)
					nameFound = true;

				RegCloseKey(instanceKey);
			}
		}
		RegCloseKey(deviceKey);
		return deviceHandle;
	}
}
