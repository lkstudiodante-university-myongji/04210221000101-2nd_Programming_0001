using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Practice
{
	/** Practice 1 */
	public partial class CP01Practice_02 : CSceneManager
	{
		#region 변수
		[Header("=====> UIs <=====")]
		[SerializeField] private InputField m_oInput = null;

		[Header("=====> Game Objects <=====")]
		[SerializeField] private GameObject m_oOriginText = null;
		[SerializeField] private GameObject m_oScrollViewContents = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_PRACTICE_02;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
		}

		/** 확인 버튼을 눌렀을 경우 */
		public void OnTouchOKBtn()
		{
			// 입력 된 텍스트가 없을 경우
			if(m_oInput.text.Trim().Length <= 0)
			{
				return;
			}

			var oText = Instantiate(m_oOriginText, Vector3.zero, Quaternion.identity);
			oText.transform.SetParent(m_oScrollViewContents.transform, false);

			oText.GetComponentInChildren<Text>().text = m_oInput.text;
			oText.GetComponentInChildren<Button>().onClick.AddListener(() => this.OnTouchTextBtn(oText));

			StartCoroutine(this.CoUpdateUIsState());
			LayoutRebuilder.ForceRebuildLayoutImmediate(oText.transform as RectTransform);
		}

		/** 텍스트 버튼을 눌렀을 경우 */
		private void OnTouchTextBtn(GameObject a_oSender)
		{
			for(int i = 0; i < m_oScrollViewContents.transform.childCount; ++i)
			{
				var oChild = m_oScrollViewContents.transform.GetChild(i);

				// 제거 할 텍스트 일 경우
				if(oChild.gameObject == a_oSender)
				{
					Destroy(oChild.gameObject);
				}
			}
		}
		#endregion // 함수
	}

	/** Practice 1 - 코루틴 */
	public partial class CP01Practice_02 : CSceneManager
	{
		#region 함수
		/** UI 상태를 갱신한다 */
		private IEnumerator CoUpdateUIsState()
		{
			yield return new WaitForEndOfFrame();
			m_oScrollViewContents.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0.0f;
		}
		#endregion // 함수
	}
}
