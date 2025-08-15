//#define E16_IMGUI
#define E16_UNITY_GUI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * GUI 란?
 * - 화면 상에 UI 를 출력하기 위한 기능을 의미한다. (즉, Unity 는 UI 를 출력하기 위한 여러가지 방법을 제공하며 해당 방법을
 * 사용하면 손쉽게 UI 를 출력 및 제어하는 것이 가능하다.)
 * 
 * Unity GUI 종류
 * - ImGUI
 * - Unity GUI
 * - UI Toolkit
 * 
 * ImGUI 란?
 * - 과거 Unity 에서 주로 활용하던 UI 를 제어하기 위한 시스템을 의미한다. (즉, 현재는 해당 시스템이 필요한 몇몇 부분을
 * 제외하고는 거의 활용되지 않는다는 것을 알 수 있다.)
 * 
 * ImGUI 는 스크립트만으로 UI 를 출력하고 제어하기 때문에 플레이 모드로 진입해야지만 UI 결과물을 확인 할 수 있다는 단점이
 * 존재한다.
 * 
 * 또한, ImGUI 는 UI 를 출력하기 위한 단순한 기능만을 제공하기 때문에 출력하는 UI 구조가 복잡 할수록 작업 시간이 오래
 * 소용된다. (즉, 단순한 UI 를 제작하는데 적합하다는 것을 알 수 있다.)
 * 
 * Unity GUI 란?
 * - 현재 가장 많이 활용되는 UI 를 제어하기 위한 시스템을 의미한다. (즉, 해당 방식은 ImGUI 방식에 비해 수월하게 UI 를
 * 출력하고 제어 할 수 있다는 것을 알 수 있다.)
 * 
 * Unity GUI 방식은 Unity 에디터 상에서 UI 를 배치하고 결과물을 바로 확인 할 수 있기 때문에 복잡한 구조를 지니고 있는 UI 도
 * 손쉽게 제작하는 것이 가능하다.)
 * 
 * UI Toolkit 이란?
 * - 차세대 UI 를 제어하기 위한 시스템을 의미한다. (즉, 해당 방식은 현재 가장 많이 활용되는 Unity GUI 에서 존재하는
 * 비효율적인 부분을 개선했다는 것을 알 수 있다.)
 * 
 * 단, 해당 방식은 아직 개발이 진행 중에 있기 때문에 상용 프로젝트를 진행하는데 사용하는 것은 리스크가 있다는 단점이 존재한다.
 */
namespace Example
{
	/** Example 16 */
	public partial class CE01Example_16 : CSceneManager
	{
		#region 변수
		[Header("=====> UIs <=====")]
		[SerializeField] private Button m_oUnityGUIBtn = null;
		[SerializeField] private TMP_Dropdown m_oUnityGUIDrop = null;
		[SerializeField] private TMP_InputField m_oUnityGUIInput = null;

		[SerializeField] private List<Toggle> m_oUnityGUIToggleList = new List<Toggle>();

		[Header("=====> Game Objects <=====")]
		[SerializeField] private GameObject m_oUnityGUIRoot = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_16;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
			m_oUnityGUIRoot.SetActive(false);

#if E16_IMGUI
			// Do Something
#elif E16_UNITY_GUI
			m_oUnityGUIRoot.SetActive(true);

			/*
			 * 사용자와 상호 작용 할 수 있는 UI 컴포넌트는 상호 작용 여부를 처리 할 수 있도록 메서드를 설정하는 것이
			 * 가능하다. (즉, 프로그램이 실행 중에 이벤트를 처리 할 메서드를 설정하는 것이 가능하다는 것을 알 수 있다.)
			 */
			m_oUnityGUIBtn.onClick.AddListener(this.OnTouchUnityGUIBtn);
			m_oUnityGUIDrop.onValueChanged.AddListener(this.OnChangeUnityGUIDrop);
			m_oUnityGUIInput.onValueChanged.AddListener(this.OnInputUnityGUIInput);

			for(int i = 0; i < m_oUnityGUIToggleList.Count; ++i)
			{
				m_oUnityGUIToggleList[i].onValueChanged.AddListener(this.OnChangeUnityGUIToggle);
			}
#endif // #if E16_UNITY_GUI
		}

		/*
		 * OnGUI 메서드란?
		 * - ImGUI 를 활용해서 화면 상에 UI 를 출력하기 위한 메서드를 의미한다. (즉, ImGUI 관련 기능은 OnGUI 와 같은 
		 * 특정 메서드 내부에서만 사용 가능하다는 것을 알 수 있다.)
		 */
		/** GUI 를 그린다 */
		public virtual void OnGUI()
		{
#if E16_IMGUI
			var stRect = new Rect(0.0f, 0.0f, Camera.main.pixelWidth, 50.0f);

			/*
			 * ImGUI 를 활용해서 특정 UI 를 화면 상에 출력하기 위해서는 GUI 클래스를 활용하면 된다. (즉, 해당 클래스는
			 * OnGUI 와 같은 특정 메서드 내부에서만 사용 가능하다는 것을 알 수 있다.)
			 */
			// 버튼을 눌렀을 경우
			if(GUI.Button(stRect, "버튼")) {
				Debug.Log("버튼을 눌렀습니다.");
			}
#endif // #if E16_IMGUI
		}

		/** Unity GUI 버튼을 눌렀을 경우 */
		private void OnTouchUnityGUIBtn()
		{
			Debug.Log("OnTouchUnityGUIBtn");
		}

		/** Unity GUI 토글이 변경 되었을 경우 */
		private void OnChangeUnityGUIToggle(bool a_bIsOn)
		{
			Debug.Log($"OnChangeUnityGUIToggle: {a_bIsOn}");
		}

		/** Unity GUI 드롭 다운이 변경 되었을 경우 */
		private void OnChangeUnityGUIDrop(int a_nIdx)
		{
			Debug.Log($"OnChangeUnityGUIDrop: {a_nIdx}");
		}

		/** Unity GUI 입력 필드가 입력 되었을 경우 */
		private void OnInputUnityGUIInput(string a_oStr)
		{
			Debug.Log($"OnInputUnityGUIInput: {a_oStr}");
		}
		#endregion // 함수
	}
}
