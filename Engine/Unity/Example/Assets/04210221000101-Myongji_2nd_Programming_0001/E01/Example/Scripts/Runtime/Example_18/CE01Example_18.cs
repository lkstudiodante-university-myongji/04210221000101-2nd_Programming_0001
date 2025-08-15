using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Example
{
	/** Example 18 */
	public partial class CE01Example_18 : CSceneManager
	{
		/** 상태 */
		private enum EState
		{
			NONE = -1,
			PLAY,
			GAME_OVER,
			[HideInInspector] MAX_VAL
		}

		#region 변수
		private float m_fRemainTime = 30.0f;
		private EState m_eState = EState.PLAY;

		[Header("=====> UIs <=====")]
		[SerializeField] private TMP_Text m_oTimeText = null;
		[SerializeField] private TMP_Text m_oScoreText = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_18;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
			CE01DataStorage_18.Inst.Reset();
		}

		/** 상태를 갱신한다 */
		public override void Update()
		{
			base.Update();

			// 플레이 상태가 아닐 경우
			if(m_eState != EState.PLAY)
			{
				return;
			}

			m_fRemainTime = Mathf.Max(0.0f, m_fRemainTime - Time.deltaTime);

			// 마우스 버튼을 눌렀을 경우
			if(Input.GetMouseButtonDown((int)EMouseBtn.LEFT))
			{
				this.HandleOnMouseBtnDown();
			}

			/*
			 * 부동 소수점은 값에 오차가 존재하기 때문에 비교 연산을 수행하기 위해서는 항상 오차를 고려해서 명령문을 작성 할
			 * 필요가 있다. (Ex. float.Epsilon 상수 사용 등등...)
			 */
			// 남은 시간이 없을 경우
			if(m_fRemainTime <= float.Epsilon)
			{
				m_eState = EState.GAME_OVER;
				CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_19, false);
			}

			this.SetIsDirtyUpdateState(true);
		}

		/** 상태를 갱신한다 */
		public override void UpdateState()
		{
			base.UpdateState();
			this.UpdateUIsState();
		}

		/** UI 상태를 갱신한다 */
		private void UpdateUIsState()
		{
			m_oTimeText.text = $"{m_fRemainTime:0.00}";
			m_oScoreText.text = $"{CE01DataStorage_18.Inst.Score}";
		}

		/** 마우스 버튼 눌림을 처리한다 */
		private void HandleOnMouseBtnDown()
		{
			/*
			 * Camera 컴포넌트의 ScreenPointToRay 메서드를 활용하면 화면 좌표를 기반으로 Unity 씬 상에 배치 된 물체를
			 * 검출하기 위한 광선을 계산하는 것이 가능하다. (즉, 해당 메서드를 활용하면 마우스가 클릭 된 시점에 클릭 된
			 * 물체를 판별하는 것이 가능하다.)
			 */
			var stRay = this.MainCamera.ScreenPointToRay(Input.mousePosition);
			bool bIsHit = Physics.Raycast(stRay, out RaycastHit stRaycastHit);

			// 터치 된 물체가 없을 경우
			if(!bIsHit || !stRaycastHit.collider.TryGetComponent(out CE01Target_18 oTarget))
			{
				return;
			}

			bool bIsCatch = oTarget.TryCatch();

			// 캐치 상태 일 경우
			if(bIsCatch)
			{
				int nScore = CE01DataStorage_18.Inst.Score;
				int nExtraScore = (oTarget.TargetType <= CE01Target_18.ETargetType.A) ? 10 : -20;

				CE01DataStorage_18.Inst.SetScore(Mathf.Max(0, nScore + nExtraScore));
			}
		}
		#endregion // 함수
	}
}
