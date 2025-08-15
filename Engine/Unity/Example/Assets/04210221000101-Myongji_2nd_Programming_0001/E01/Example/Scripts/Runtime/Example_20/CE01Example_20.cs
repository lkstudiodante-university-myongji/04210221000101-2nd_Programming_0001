using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

/*
 * 내비게이션 메쉬란?
 * - Unity 가 지원하는 AI 관련 기능 중 하나로서 최단 거리를 찾기 위한 기능을 제공한다. (즉, 내비게이션 메쉬를 활용하면 특정 
 * 위치까지 이동하기 위한 경로를 손쉽게 계산하는 것이 가능하다.)
 * 
 * 내비게이션 메쉬 기능을 이용해서 경로를 탐색하기 위해서는 반드시 내비게이션 맵 (표면) 정보를 생성 할 필요가 있으며
 * 내비게이션 맵은 Nav Mesh Surface 컴포넌트를 통해 생성하는 것이 가능하다.
 * 
 * Unity 내비게이션 메쉬 관련 컴포넌트
 * - Nav Mesh Agent
 * - Nav Mesh Obstacle
 * - Off Mesh Link
 * 
 * Nav Mesh Agent 컴포넌트란?
 * - 내비게이션 메쉬에 의해서 생성 된 내비게이션 맵을 탐색하는 역할을 수행한다. (즉, 해당 컴포넌트는 항상 내비게이션 맵 위에 
 * 존재해야한다는 것을 알 수 있다.)
 * 
 * Nav Mesh Obstacle 컴포넌트란?
 * - Nav Mesh Agent 의 움직임을 방해하는 장애물에 해당하는 역할을 수행한다.
 * 
 * 내비게이션 맵을 생성할 때 장애물로 인식되는 대상은 정적 게임 객체만 가능하기 때문에 동적으로 움직이는 장애물을 인식하기
 * 위해서는 해당 컴포넌트를 사용하면 된다는 것을 알 수 있다.
 * 
 * Off Mesh Link 컴포넌트란?
 * - 서로 떨어져있는 내비게이션 맵을 연결해주는 역할을 수행한다. (즉, 해당 컴포넌트를 활용하면 떨어져있는 내비게이션 맵으로
 * 이동하는 것이 가능하다.)
 * 
 * 내비게이션 맵은 Agent 설정과 맵을 생성 할 때 사용되는 게임 객체의 상태에 따라 여러개가 생성 될 수 있기 때문에
 * Off Mesh Link 컴포넌트가 없다면 떨어져있는 맵으로 이동하는 것은 불가능하다는 것을 알 수 있다.
 */
namespace Example
{
	/** Example 20 */
	public partial class CE01Example_20 : CSceneManager
	{
		#region 변수
		private Tween m_oMoveAni = null;

		[Header("=====> Game Objects <=====")]
		[SerializeField] private GameObject m_oTarget = null;
		[SerializeField] private GameObject m_oDynamicObstacle = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_20;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
			m_oMoveAni = m_oDynamicObstacle.transform.DOMoveX(300.0f, 2.0f).SetAutoKill().SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		}

		/** 제거 되었을 경우 */
		public override void OnDestroy()
		{
			base.OnDestroy();
			m_oMoveAni?.Kill();
		}

		/** 상태를 갱신한다 */
		public override void Update()
		{
			base.Update();

			// 마우스 버튼을 눌렀을 경우
			if(Input.GetMouseButtonDown((int)EMouseBtn.LEFT))
			{
				this.HandleOnMouseBtnDown();
			}
		}

		/** 마우스 버튼 눌림을 처리한다 */
		private void HandleOnMouseBtnDown()
		{
			var stRay = this.MainCamera.ScreenPointToRay(Input.mousePosition);

			// 터치 된 물체가 존재 할 경우
			if(Physics.Raycast(stRay, out RaycastHit stRaycastHit))
			{
				var oNavMeshAgent = m_oTarget.GetComponent<NavMeshAgent>();

				/*
				 * Nav Mesh Agent 컴포넌트의 SetDestination 메서드를 이용하면 목적지를 설정하는 것이 가능하다. (즉, 해당
				 * 메서드에 의해 목적지가 설정되면 내부적으로 해당 목적지로 이동하기 위한 경로가 계산된다는 것을 알 수 있다.)
				 */
				oNavMeshAgent.SetDestination(stRaycastHit.point);
			}
		}

#if UNITY_EDITOR
		/** 기즈모를 그린다 */
		public void OnDrawGizmos()
		{
			var stPrevColor = Gizmos.color;

			try
			{
				var stEndPos = m_oTarget.transform.position;
				stEndPos += m_oTarget.transform.forward * 250.0f;

				Gizmos.color = Color.red;
				Gizmos.DrawLine(m_oTarget.transform.position, stEndPos);
			}
			finally
			{
				Gizmos.color = stPrevColor;
			}
		}
#endif // #if UNITY_EDITOR
		#endregion // 함수
	}
}
