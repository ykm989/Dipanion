using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;//EventSystem 이란 UI를 생성할 때와 같이 생성되는 것으로, UI 요소들을 손이나 마우스로 누를 때, 땔 때, 드래그할 때 등을 감지하는 것입니다.
using UnityEngine.UI;//https://m.blog.naver.com/PostView.nhn?blogId=ocy1011&logNo=220761155376&proxyReferer=https:%2F%2Fwww.google.com%2F 13일날 보자
using Debug = UnityEngine.Debug; //디버깅?
/// <summary>
/// 이 시벌 제목부터 투명 윈도우 잖아? 투명해 지겠지
/// </summary>
[RequireComponent(typeof(Camera))]//카메라 컴포넌트 추가
public class TransparentWindow : MonoBehaviour
{//이하 번역은 파파고다.
	public static TransparentWindow Main = null;
	public static Camera Camera = null;	//Used instead of Camera.main 메인 카메라 치워버림
	//Tooltip은 변수에 대한 설명을 다는 것
	[Tooltip("마우스와 오브젝트가 겹치는지 확인할때 사용하는 변수로 layermask 한계치 설정")] //마우스가 개체 위로 지나갈 때 어떤 게임 개체 레이어가 창 포커스를 트리거해야합니까?
	[SerializeField] LayerMask clickLayerMask = ~0;//인스펙터창에서 접근 가능하게 하고자 함

	[Tooltip("Allows Input to be detected even when focus is lost")] //비록 포커스를 잃었을 지라도 
	[SerializeField] bool useSystemInput = false;

	[Tooltip("Should the window be fullscreen?")] //전체화면인가?
	[SerializeField] bool fullscreen = true;

	[Tooltip("Force the window to match ScreenResolution")] //윈도우가 화면 해상도와 일치하도록 강제 설정
	[SerializeField] bool customResolution = true;

	[Tooltip("Resolution the overlay should run at")] //오버레이가 실행되어야하는 해상도
	[SerializeField] Vector2Int screenResolution = new Vector2Int(2560, 1440);//요걸 1280720에서 25601440로 수정 테스트

	[Tooltip("The framerate the overlay should try to run at")] //오버레이가 실행되어야하는 프레임 속도
	[SerializeField] int targetFrameRate = 30;

	
	/////////////////////
	//Windows DLL 넣는 것//
	/////////////////////
	
	[DllImport("user32.dll")]
	static extern IntPtr GetActiveWindow();//아마도 handle을 가지고 있는 윈도우를 찾아 주는 듯
	
	[DllImport("user32.dll")]
	static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);//윈도우의 속성값을 편집 하는 함수

	[DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]//user32.dll 중 SetLayeredWindowAttributes만 가지고 오는 함수 인듯
	static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

	[DllImport("user32.dll", EntryPoint = "GetWindowRect")]
	static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect);//handle이 가지고 있는 윈도우 영역을 표시 x, y, height, width 가 아닌 left, right, top, bottom 으로 나옴 만약 left 가 100, right 가 200 이라면 x 가 100 width 가 100 이랑 같은 거로 보면된다
	//out은 일종의 함수안에서 함수밖에 선언된 변수의 값을 변경하기 위해서 있는거 같아 우리가 C에서 포인터값 넘겨서 수정하던거 생각하면 편할 듯
		[DllImport("user32.dll")]
	static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
	//이건 어려운 부분이라 알아서 찾아 볼 것
	[DllImportAttribute("user32.dll")]
	static extern bool ReleaseCapture();
	//길어서 아래에 적음 마우스 캡처를 푼다. 마우스를 캡처한 윈도우는 커서의 위치에 상관없이 모든 마우스 메시지를 받는데 이 함수로 캡처를 풀면 이상태가 종료되며 마우스 메시지는 커서 아래쪽에 있는 윈도우로 전달된다. SetCapture로 마우스를 캡처한 윈도우는 필요한 동작을 완료한 후 반드시 이 함수를 호출하여 캡처를 풀어 주어야 한다.
	//SetCapture 마우스 버튼의 누름, 이동, 뗌 등의 마우스 메시지는 보통 커서 바로 아래쪽에 있는 윈도우로 전달된다. 커서가 영역밖을 벗어나도 계속적으로 마우스 메시지를 받아야 하는 경우도 있다.
	[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
	static extern int SetWindowPos(IntPtr hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, int uFlags);
	//이 함수는 윈도우의 위치, 크기, Z순서를 동시에 또는 일부만 변경할 때 사용된다. 이 함수는 Z순서를 변경하기 위한 목적으로, 특히 항상 위 속성을 토글하기 위한 용도로 많이 사용되는데 두번째 인수에 HWND_(NO)TOPMOST를 줌으로써 이 속성을 토글할 수 있다. 이 함수로 항상 위 속성을 설정하면 이 윈도우에 소유된 윈도우도 항상 위 속성을 같이 가지게 된다.
	//ShowWIndow는 윈도우의 보이기 상태를 지정한다.
	[DllImport("Dwmapi.dll")]
	static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Rectangle margins);
	//첫번째 인자는 윈도우의 핸들이며 두번쨰 인자는 효과를 줄 영역을 의미 AeroGlass 효과를 주는 함수 반투명한 윈도우 창의 형태를 만들어주는것
	const int GWL_STYLE = -16;//WPF인듯
	const uint WS_POPUP = 0x80000000;
	const uint WS_VISIBLE = 0x10000000;
	const int HWND_TOPMOST = -1;

	const int WM_SYSCOMMAND = 0x112;
	const int WM_MOUSE_MOVE = 0xF012;

	int fWidth;
	int fHeight;
	IntPtr hwnd = IntPtr.Zero;
	Rectangle margins;
	Rectangle windowRect;

	//BUG: Sometimes fails to SetResolution if not focused on startup - if using Start(), WindowBoundsCollider2D sometimes fails to set the correct size
	void Awake()
	{
		Main = this;

		Camera = GetComponent<Camera>();
		Camera.backgroundColor = new Color();//남은 색 설정, 색 설정이 안됨
		Camera.clearFlags = CameraClearFlags.SolidColor;//배경색으로 화면의 나머지 부분을 채웁니다.

		if (fullscreen && !customResolution)
		{
			screenResolution = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);//스크린 해상도 값을 전달
		}
		
		Screen.SetResolution(screenResolution.x, screenResolution.y, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
		//가로픽셀, 세로 픽셀, full screen 유무, 지원되는 해상도 지정
		Application.targetFrameRate = targetFrameRate;//프레임 고정
		Application.runInBackground = true;//백그라운드에서도 실행 된다.

#if !UNITY_EDITOR
		fWidth = screenResolution.x;
		fHeight = screenResolution.y;
		margins = new Rectangle() {Left = -1};
		hwnd = GetActiveWindow();

		if (GetWindowRect(hwnd, out windowRect))
		{
			Debug.LogError("Couldn't get Window Rect");
		}

		SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
		SetWindowPos(hwnd, HWND_TOPMOST, windowRect.Left, windowRect.Top, fWidth, fHeight, 32 | 64);
		DwmExtendFrameIntoClientArea(hwnd, ref margins);
#endif
	}

	void Update()//업데이트는 볼 거 없음
	{
		if (useSystemInput)
		{
			SystemInput.Process();//systemInput 스크립트에 Process라는 함수를 호출 대부분 진행된다.
		}

		SetClickThrough();
	}

	//Returns true if the cursor is over a UI element or 2D physics object
	public bool FocusForInput()//OverlapPoint를 SetClickThrough()로 넘기기 위한 함수
	{
		EventSystem eventSystem = EventSystem.current;
		if (eventSystem && eventSystem.IsPointerOverGameObject())//IsPointerOverGameObject()는 UI에ㅐ 캐릭터가 가려져서 선택 안되는거 방지
		{
			return true;
		}

		Vector2 pos = Camera.ScreenToWorldPoint(Input.mousePosition);//마우스 클릭 또는 손카락 터치에 의한 입력이 발생했을 때, ScreenToWorld
		return Physics2D.OverlapPoint(pos, clickLayerMask);//첫번째 충돌한 오브젝트의 정보를 넘김
	}

	void SetClickThrough()
	{
		var focusWindow = FocusForInput();

		//Get window position
		GetWindowRect(hwnd, out windowRect);

#if !UNITY_EDITOR//https://cinrueom.tistory.com/79 유니티 환경이; 아닌 즉 exe로 실행될떄를 말하는듯
		if (focusWindow)
		{
			SetWindowLong (hwnd, -20, ~(((uint)524288) | ((uint)32)));
			SetWindowPos(hwnd, HWND_TOPMOST, windowRect.Left, windowRect.Top, fWidth, fHeight, 32 | 64);//핸들
		}
		else
		{
			SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);//첫번째 항은 변경을 하고자 하는 핸들,새로운 윈도우 스타일을 설정,팟업 또는 비지블
			SetWindowLong (hwnd, -20, (uint)524288 | (uint)32);
			SetLayeredWindowAttributes (hwnd, 0, 255, 2);//창의 투명도를 설정! 인자는 각각 핸들,바꾸려는 색(0이면 전부), 색의 투명도, 플래그 2면 bAlpha 를 사용 하여 계층화 된 창의 불투명돌을 결정, 투명 색상으로 crKey를 사용(인수 : hwnd,crkey,bAlpha,dwFlages)
			SetWindowPos(hwnd, HWND_TOPMOST, windowRect.Left, windowRect.Top, fWidth, fHeight, 32 | 64);
		}
#endif
	}
	//https://kin.naver.com/qna/detail.nhn?d1id=1&dirId=1040102&docId=346625614&qb=U2V0V2luZG93TG9uZw==&enc=utf8&section=kin.ext&rank=6&search_sort=0&spq=0

	public static void DragWindow()
	{
#if !UNITY_EDITOR
		if (Screen.fullScreenMode != FullScreenMode.Windowed)//풀스크린이 아니면
		{
			return;//끝낸다 즉 풀스크린이 아닐 시 작동하는 코드를 만들고 싶은 듯
		}
		ReleaseCapture ();
		SendMessage(Main.hwnd, WM_SYSCOMMAND, WM_MOUSE_MOVE, 0);
		Input.ResetInputAxes();
#endif		
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Rectangle
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}
}

//별도 파일로 win32에대한 공부 내용을 정리하도록 하겠다.
//SetMapMode와 논리 좌표: 윈도우즈 내부에서 사용되는 좌표를 말한다. 물리 좌표는 실제 화면에 출력되는 좌표이며 픽셀 단위를 사용한다. 모니터의 물리적인 픽셀 단위를 사용하므로 물리 좌표는 그 위치가 정해져 있다.
//public Resolution[] Resolutions 으로 해당 기기의 해상도를 가지고 있는 배열
//Resolutions = Screen.resolutions; 배열에 해당 스크린이 설정 가능한 해상도를 전부 받아온다.