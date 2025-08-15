using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Example
{
	/** Example 24 */
	public partial class CE01Example_24 : CSceneManager
	{
		#region 변수
		[Header("=====> UIs <=====")]
		[SerializeField] private Text m_oResultText = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_24;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
		}

		/** 상태를 갱신한다 */
		public override void UpdateState()
		{
			base.UpdateState();
			this.UpdateUIsState();
		}

		/** 다시하기 버튼을 눌렀을 경우 */
		public void OnTouchRetryBtn()
		{
			CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_23);
		}

		/** 그만두기 버튼을 눌렀을 경우 */
		public void OnTouchLeaveBtn()
		{
			CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_22);
		}

		/** UI 상태를 갱신한다 */
		private void UpdateUIsState()
		{
			m_oResultText.text = string.Format("점수 : {0}", CE01DataStorage_23.Inst.Score);
		}
		#endregion // 함수
	}
}
