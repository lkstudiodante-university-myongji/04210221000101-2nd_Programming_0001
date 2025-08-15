using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * 기말 프로젝트 필수 기능
 * - 사운드
 * - 쉐이더
 * - 저장 및 로드 (PlayerPrefs or File Stream)
 * - 멀티 해상도 대응 (Expand or Shrink)
 */
namespace Example
{
	/** 메뉴 씬 */
	public partial class CE01Example_00 : CSceneManager
	{
		#region 변수
		[Header("=====> Game Objects <=====")]
		[SerializeField] private GameObject m_oOriginText = null;
		[SerializeField] private GameObject m_oScrollViewContents = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_00;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

			/*
			 * SceneManager 클래스를 이용하면 현재 빌드 설정에 등록 된 씬 개수를 가져오는 것이 가능하다.
			 */
			for(int i = 1; i < SceneManager.sceneCountInBuildSettings; ++i)
			{
				/*
				 * SceneUtility 클래스는 씬과 관련 된 기능을 지원한다.
				 */
				string oScenePath = SceneUtility.GetScenePathByBuildIndex(i);

				var oText = Instantiate(m_oOriginText, Vector3.zero, Quaternion.identity);
				oText.transform.SetParent(m_oScrollViewContents.transform, false);

				/*
				 * Path 클래스란?
				 * - 경로와 관련 된 여러가지 기능을 제공하는 클래스를 의미한다. (즉, 해당 클래스를 활용하면 특정 파일의
				 * 경로를 계산하는 등의 연산을 수행하는 것이 가능하다.)
				 */
				oText.GetComponent<Text>().text = Path.GetFileNameWithoutExtension(oScenePath);
				oText.GetComponent<Button>().onClick.AddListener(() => this.OnTouchText(oScenePath));
			}
		}

		/** 초기화 */
		public override void Start()
		{
			base.Start();
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_oScrollViewContents.transform as RectTransform);
		}

		/** 텍스트를 눌렀을 경우 */
		private void OnTouchText(string a_oScenePath)
		{
			CSceneLoader.Inst.LoadScene(a_oScenePath);
		}
		#endregion // 함수
	}
}
