using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/** 씬 관리자 */
public abstract partial class CSceneManager : CComponent
{
	#region 클래스 변수
	private static Dictionary<string, CSceneManager> m_oSceneManagerDict = new Dictionary<string, CSceneManager>();
	#endregion // 클래스 변수

	#region 프로퍼티
	public abstract string SceneName { get; }

	public Camera MainCamera { get; private set; } = null;
	public EventSystem UIsEventSystem { get; private set; } = null;

	public GameObject UIs { get; private set; } = null;
	public GameObject PopupUIs { get; private set; } = null;

	public GameObject Objs { get; private set; } = null;
	public GameObject StaticObjs { get; private set; } = null;

	/*
	 * 액티브 씬이란?
	 * - 기본 씬을 의미한다. (즉, Unity 가 제공하는 몇몇 메서드는 액티브 씬을 대상으로 동작하기 때문에 현재 액티브 씬에
	 * 따라 메서드 호출 결과가 달라질 수 있다.)
	 * 
	 * 일반적으로 액티브 씬은 현재 로드 된 씬 중에 가장 먼저 로드 된 씬을 의미한다. (즉, 특정 씬을 Single 모드로
	 * 로드했을 경우 해당 씬이 액티브 씬이 된다는 것을 알 수 있다.)
	 */
	public bool IsActiveScene => SceneManager.GetActiveScene().name.Equals(this.SceneName);
	public bool IsAdditiveScene => !this.IsActiveScene;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		this.SetupDefObjs();

		Physics.gravity = KDefine.G_PHYSICS_GRAVITY;
		Application.targetFrameRate = Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value);

		CSceneManager.m_oSceneManagerDict.TryAdd(this.SceneName, this);
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();

		// 씬 관리자가 존재 할 경우
		if(CSceneManager.m_oSceneManagerDict.ContainsKey(this.SceneName))
		{
			CSceneManager.m_oSceneManagerDict.Remove(this.SceneName);
		}
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// 뒤로가기 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Escape) && !this.SceneName.Equals(KDefine.G_N_SCENE_EXAMPLE_00))
		{
			CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_00);
		}
	}

	/** 기본 객체를 설정한다 */
	private void SetupDefObjs()
	{
		var oGameObjList = new List<GameObject>();
		this.gameObject.scene.GetRootGameObjects(oGameObjList);

		for(int i = 0; i < oGameObjList.Count; ++i)
		{
			/*
			 * Transform 컴포넌트의 Find 메서드를 활용하면 특정 게임 객체의 자식 객체를 탐색하는 것이 가능하다. 
			 * 또한, 해당 메서드는 경로를 지정함으로서 자식 객체 뿐만 아니라 후손 객체도 탐색 할 수 있는 특징이 존재한다.
			 */
			var oUIs = oGameObjList[i].transform.Find("Canvas/UIs")?.gameObject;
			var oPopupUIs = oGameObjList[i].transform.Find("Canvas/PopupUIs")?.gameObject;

			var oObjs = oGameObjList[i].transform.Find("Objs")?.gameObject;
			var oStaticObjs = oGameObjList[i].transform.Find("StaticObjs")?.gameObject;

			var oMainCamera = oGameObjList[i].transform.Find("MainCamera");
			var oUIsEventSystem = oGameObjList[i].transform.Find("EventSystem");

			this.UIs = this.UIs ?? oUIs;
			this.PopupUIs = this.PopupUIs ?? oPopupUIs;

			this.Objs = this.Objs ?? oObjs;
			this.StaticObjs = this.StaticObjs ?? oStaticObjs;

			this.MainCamera = this.MainCamera ?? oMainCamera?.GetComponent<Camera>();
			this.UIsEventSystem = this.UIsEventSystem ?? oUIsEventSystem?.GetComponent<EventSystem>();
		}

		this.MainCamera?.gameObject.SetActive(this.IsActiveScene);
		this.UIsEventSystem?.gameObject.SetActive(this.IsActiveScene);
	}
	#endregion // 함수

	#region 제네릭 함수
	/** 씬 관리자를 반환한다 */
	public static T GetSceneManager<T>(string a_oSceneName) where T : CSceneManager
	{
		return CSceneManager.m_oSceneManagerDict.GetValueOrDefault(a_oSceneName) as T;
	}
	#endregion // 제네릭 함수
}
