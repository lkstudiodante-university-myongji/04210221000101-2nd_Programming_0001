using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Example
{
	/** Example 13 */
	public partial class CE01Example_13 : CSceneManager
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
		private EState m_eState = EState.PLAY;

		[Header("=====> UIs <=====")]
		[SerializeField] private Text m_oScoreText = null;

		[Header("=====> Game Objects <=====")]
		[SerializeField] private GameObject m_oTarget = null;
		[SerializeField] private GameObject m_oObstacleRoot = null;
		[SerializeField] private GameObject m_oOriginObstacle = null;

		private List<GameObject> m_oObstacleList = new List<GameObject>();
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_13;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
			CE01DataStorage_13.Inst.Reset();

			// 전달자를 설정한다
			var oDispatcher = m_oTarget.GetComponent<CTriggerDispatcher>();
			oDispatcher.SetEnterCallback(this.HandleOnTriggerEnter);
			oDispatcher.SetExitCallback(this.HandleOnTriggerExit);
		}

		/** 초기화 */
		public override void Start()
		{
			base.Start();

			/*
			 * StartCoroutine 메서드는 코루틴을 시작하는 역할을 수행한다. (즉, 해당 메서드를 활용하면 여러 작업을 병렬적으로 
			 * 처리하는 것이 가능하다는 것을 알 수 있다.)
			 * 
			 * 코루틴이란?
			 * - 일반적인 메서드와 달리 메서드 호출이 종료된 위치부터 다시 이어서 실행 할 수 있는 메서드를 의미한다. (즉,
			 * 일반적인 메서드는 호출이 종료되고 나면 다시 처음부터 호출되는 특징이 존재하며 이러한 메서드를 서브루틴이라고
			 * 한다.)
			 * 
			 * 따라서, 코루틴을 활용하면 여러 작업을 병렬적으로 처리하는 병렬 처리 구조를 만들어내는 것이 가능하다.
			 */
			StartCoroutine(this.TryCreateObstacles());
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

			// 점프 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Space))
			{
				var oRigidbody = m_oTarget.GetComponent<Rigidbody>();
				oRigidbody.linearVelocity = Vector3.zero;
				oRigidbody.AddForce(Vector3.up * 1000.0f, ForceMode.VelocityChange);
			}

			for(int i = 0; i < m_oObstacleList.Count; ++i)
			{
				m_oObstacleList[i].transform.localPosition += new Vector3(-550.0f * Time.deltaTime, 0.0f, 0.0f);
			}
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
			// 텍스트를 갱신한다
			m_oScoreText.text = $"{CE01DataStorage_13.Inst.Score}";
		}

		/** 충돌 시작을 처리한다 */
		private void HandleOnTriggerEnter(CTriggerDispatcher a_oSender, Collider a_oCollider)
		{
			// 장애물 일 경우
			if(a_oCollider.CompareTag("E01Obstacle_13"))
			{
				m_eState = EState.GAME_OVER;
				m_oTarget.GetComponent<Rigidbody>().useGravity = false;

				CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_14);
			}
		}

		/** 충돌 종료를 처리한다 */
		private void HandleOnTriggerExit(CTriggerDispatcher a_oSender, Collider a_oCollider)
		{
			// 안전 영역 일 경우
			if(a_oCollider.CompareTag("E01SafeArea_13"))
			{
				CE01DataStorage_13.Inst.SetScore(CE01DataStorage_13.Inst.Score + 1);
				this.SetIsDirtyUpdateState(true);
			}
		}
		#endregion // 함수
	}

	/** Example 13 - 코루틴 */
	public partial class CE01Example_13 : CSceneManager
	{
		#region 함수
		/** 장애물 생성을 시도한다 */
		private IEnumerator TryCreateObstacles()
		{
			do
			{
				var oObstacle = Instantiate(m_oOriginObstacle, Vector3.zero, Quaternion.identity);
				oObstacle.transform.SetParent(m_oObstacleRoot.transform, false);
				oObstacle.transform.localPosition = new Vector3((KDefine.G_DESIGN_SCREEN_WIDTH / 2.0f) + 150.0f, 0.0f, 0.0f);

				m_oObstacleList.Add(oObstacle);

				/*
				 * yield return 키워드란?
				 * - 코루틴에서만 사용 가능한 키워드로서 해당 키워드를 명시하면 코루틴을 종료하고 흐름을 해당 메서드를 호출
				 * 한 곳으로 되돌리는 역할을 수행한다. (즉, return 키워드와 비슷한 역할이라는 것을 알 수 있다.)
				 * 
				 * 단, 코루틴은 반드시 IEnumerator 인터페이스를 따르는 객체를 반환해야되기 때문에 일반적인 메서드와 달리
				 * 항상 반환 값이 존재한다는 특징이 있다. (즉, 코루틴 내부에서는 해당 키워드를 반드시 1 번 이상 명시해야되는
				 * 것을 알 수 있다.)
				 * 
				 * 만약, 코루틴에서 적절한 반환 값이 없을 경우에는 null 값을 반환하면 된다.
				 */
				yield return new WaitForSeconds(1.5f);
			} while(m_eState != EState.GAME_OVER);
		}
		#endregion // 함수
	}
}
