using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SystemInput
{
	//TODO: Add Keyboard input (see bottom of script)
	
	//Keys
	const int VK_LBUTTON = 0x01; //Left Mouse Button
	const int VK_RBUTTON = 0x02; //Right Mouse Button
	const int VK_MBUTTON = 0x02; //Middle Mouse Button (Mouse wheel button)
	const int SM_SWAPBUTTON = 23; //0 = default, non-zero = LMB/RMB swapped

	//Key states
	const int BUTTONDOWNFRAME = -32767;
	const int BUTTONDOWN = -32768;
	const int BUTTONUP = 0; //Not sure if there's a specific buttonUp

	[DllImport("user32.dll", EntryPoint = "SetCursorPos")]//마우스 커서 좌표를 찾고 반환해주는 함수 인듯
	[return: MarshalAs(UnmanagedType.Bool)]//MarshalAs 고정크기 배열이나 문자열을 설정하기 위해 쓰는 함수 UnmanagedType은 win32에서 쓰는 여러 비 시발 모르겠다. 여러 변수 종류를 포함한거임 무튼 이거는 bool인데 win32전용으로 만든거지 즉 쓰잘데기 없이 복잡할거 없이 걍 고정크기의 bool형태의 변수를 리턴한다는 건데
	private static extern bool SetCursorPos(int x, int y);//마우스! 마우스!

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(out Vector2Int lpMousePoint);	//Cursor coordinates start top-left, rather than Unity's bottom-left, so y axis will need to be modified

	[DllImport("user32.dll")]
	public static extern short GetAsyncKeyState(int virtualKeyCode);

	[DllImport("user32.dll")]
	public static extern short GetSystemMetrics(int metricsCode);

	//TODO: Work out a way to handle generic key states, so we don't need multiple bools for each key 
	static bool mouseButton0Down = false;
	static bool mouseButton1Down = false;
	static bool lastMouseButton0Down = false;
	static bool lastMouseButton1Down = false;
	static bool hasPressedButton0 = false;
	static bool hasPressedButton1 = false;

	/// <summary>
	///   <para>Returns whether the given mouse button is held down.</para>
	/// </summary>
	/// <param name="button"></param>
	public static bool GetMouseButton(int button = 0)
	{
		return (button == 0) ? hasPressedButton0 : hasPressedButton1;
	}

	/// <summary>
	///   <para>Returns true during the frame the user pressed the given mouse button.</para>
	/// </summary>
	/// <param name="button"></param>
	public static bool GetMouseButtonDown(int button = 0)
	{
		return (button == 0) ? mouseButton0Down : mouseButton1Down;
	}

	/// <summary>
	///   <para>Returns true during the frame the user releases the given mouse button.</para>
	/// </summary>
	/// <param name="button"></param>
	public static bool GetMouseButtonUp(int button = 0)
	{
		return (button == 0) ? (!hasPressedButton0 && lastMouseButton0Down) : (!hasPressedButton1 && lastMouseButton1Down);
	}
	
	public static Vector2Int GetCursorPosition()
	{
		GetCursorPos(out var point);
		return point;
	}

	public static void SetCursorPosition(Vector2Int point)
	{
		SetCursorPos(point.x, point.y);
	}

	public static void Process()
	{
		CheckMouseButtons();
	}

	static void CheckMouseButtons()
	{
		lastMouseButton0Down = hasPressedButton0;
		lastMouseButton1Down = hasPressedButton1;
		mouseButton0Down = false;
		mouseButton1Down = false;

		var mbp0 = MouseButtonPressed(0);
		var mbp1 = MouseButtonPressed(1);

		//Check MouseButton0
		if (!hasPressedButton0 && mbp0)
		{
			hasPressedButton0 = true;
			mouseButton0Down = true;
		}
		else if (hasPressedButton0 && !mbp0)
		{
			hasPressedButton0 = false;
		}

		//Check MouseButton1
		if (!hasPressedButton1 && mbp1)
		{
			hasPressedButton1 = true;
			mouseButton1Down = true;
		}
		else if (hasPressedButton1 && !mbp1)
		{
			hasPressedButton1 = false;
		}
	}

	static bool MouseButtonPressed(int button)
	{
		bool state = false;
		bool swapped = GetSystemMetrics(SM_SWAPBUTTON) > 0;
		switch (button)
		{
			case 0:
				state = GetAsyncKeyState(swapped ? VK_RBUTTON : VK_LBUTTON) == BUTTONDOWN;
				break;
			case 1:
				state = GetAsyncKeyState(swapped ? VK_LBUTTON : VK_RBUTTON) == BUTTONDOWN;
				break;
			case 2:
				state = GetAsyncKeyState(VK_MBUTTON) == BUTTONDOWN;
				break;
			default:
				return false;
		}

		return state;
	}

	//TODO: Keyboard Input stuff
	public static bool GetKey(KeyCode key)
	{
		if (VK_KeyCodes.TryGetValue(key, out var value))
		{
			return GetAsyncKeyState(value) == BUTTONDOWN;
		}

		return false;
	}
	
	static bool KeyCodePressed(int value)
	{
		return GetAsyncKeyState(value) == BUTTONDOWN;
	}

	//Is there an easier way than just adding each key combo manually?
	static Dictionary<KeyCode, int> VK_KeyCodes = new Dictionary<KeyCode, int>()
	{
		{KeyCode.Keypad8, 0x68},
		{KeyCode.Keypad4, 0x64},
		{KeyCode.Keypad6, 0x66},
		{KeyCode.Keypad2, 0x62},
	};
	
	static Dictionary<KeyCode, KeyState> KeyStates = new Dictionary<KeyCode, KeyState>();
	public struct KeyState
	{
		public KeyCode KeyCode;
		public KeyPressType KeyPressType;
	}

	public enum KeyPressType
	{
		None,
		Down,
		Hold,
		Up,
	}
}