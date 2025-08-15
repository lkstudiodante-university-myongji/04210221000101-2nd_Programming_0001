//#define E11_PHYSICS_01
#define E11_PHYSICS_02

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 물리 엔진이란?
 * - Unity 씬 상에 배치 된 물체들 간에 상호 작용 여부를 처리해주는 기능을 의미한다. (즉, 물리 엔진을 활용하면 물체들 간의
 * 충돌 판정 등을 손쉽게 처리하는 것이 가능하다.)
 * 
 * Unity 물리 엔진 종류
 * - Box2D
 * - PhysicX
 * 
 * Box2D 물리 엔진이란?
 * - 2 차원 물리 연산을 처리해주는 엔진을 의미한다. (즉, Box2D 엔진은 2 차원 전용 엔진이기 때문에 3 차원 물체를 대상으로는
 * 사용하는 것이 불가능하다는 것을 알 수 있다.)
 * 
 * PhysicX 물리 엔진이란?
 * - 3 차원 물리 연산을 처리해주는 엔진을 의미한다. (즉, 해당 엔진을 활용하면 2 차원 물체 및 3 차원 물체를 대상으로 물리
 * 연산을 수행하는 것이 가능하다는 것을 알 수 있다.)
 * 
 * Unity 물리 관련 컴포넌트
 * - Collider
 * - Rigidbody
 * 
 * Collider 컴포넌트란?
 * - Unity 씬 상에 배치 된 물체의 실질적인 모양을 나타내는 역할을 수행한다. (즉, Unity 물리 엔진은 Collider 컴포넌트를 
 * 기반으로 해당 물체의 모양 및 크기를 계산하기 때문에 화면에 그려지는 물체의 모양과 물리 엔진 상에서의 모양은 서로 다를 수 
 * 있다는 것을 알 수 있다.)
 * 
 * Collider 컴포넌트 종류
 * - Box
 * - Sphere
 * - Capsule
 * - Mesh
 * 
 * Unity 는 여러 종류의 Collider 컴포넌트를 지원하지만 가능하면 단순한 모양을 지니는 Collider 를 사용할 것을 추천한다.
 * (즉, Collider 모양이 복잡 할 수록 내부적으로 많은 연산이 필요하다는 것을 알 수 있다.)
 * 
 * Rigidbody 컴포넌트란?
 * - 물리적인 상호 작용을 처리하는 컴포넌트를 의미한다. (즉, Unity 씬 상에 배치 된 물체가 Rigidbody 컴포넌트를 지니고 있다면
 * Unity 물리 엔진에 의해서 중력과 같은 물리 연산이 처리된다는 것을 알 수 있다.)
 */
namespace Example
{
	/** Example 11 */
	public partial class CE01Example_11 : CSceneManager
	{
		#region 변수
		/*
		 * Header 속성은 명시 된 문장을 Unity 인스펙터 뷰에 출력하는 역할을 수행한다. (즉, 해당 속성을 활용하면 인스펙터
		 * 뷰에 출력되는 필드를 좀 더 깔끔하게 정리하는 것이 가능하다.)
		 */
		[Header("=====> UIs <=====")]
		[SerializeField] private Text m_oPhysics02ShootPowerText = null;

		[Header("=====> Game Objects <=====")]
		[SerializeField] private GameObject m_oPhysics01Target = null;
		[SerializeField] private GameObject m_oPhysics02Target = null;
		[SerializeField] private GameObject m_oPhysics02ObstacleRoot = null;

		[SerializeField] private List<GameObject> m_oPhysics01RootList = new List<GameObject>();
		[SerializeField] private List<GameObject> m_oPhysics02RootList = new List<GameObject>();

#if E11_PHYSICS_02
		private bool m_bIsShoot = false;
		private float m_fShootPower = 0.0f;
#endif // #if E11_PHYSICS_01
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_11;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

			m_oPhysics01RootList.ForEach((a_oGameObj) => a_oGameObj.SetActive(false));
			m_oPhysics02RootList.ForEach((a_oGameObj) => a_oGameObj.SetActive(false));

#if E11_PHYSICS_01
			m_oPhysics01RootList.ForEach((a_oGameObj) => a_oGameObj.SetActive(true));
#elif E11_PHYSICS_02
			this.SetupObstacleRigidbodyState(false);
			m_oPhysics02RootList.ForEach((a_oGameObj) => a_oGameObj.SetActive(true));

			var oRigidbody = m_oPhysics02Target.GetComponent<Rigidbody>();
			oRigidbody.useGravity = false;
#endif // #if E11_PHYSICS_01
		}

		/** 상태를 갱신한다 */
		public override void Update()
		{
			base.Update();

#if E11_PHYSICS_01
			/*
			 * Input.GetAxis 계열 메서드를 활용하면 조이패드와 같은 기울기를 지니는 입력 장치를 처리하는 것이 가능하다.
			 * (즉, 해당 메서드는 -1 ~ 1 범위 사이에 있는 실수 값을 반환하며 해당 값을 이용하면 입력 장치의 기울기를 기반으로
			 * 다양한 결과를 만들어내는 것이 가능하다.)
			 */
			float fVertical = Input.GetAxis("Vertical");
			float fHorizontal = Input.GetAxis("Horizontal");

			/*
			 * Transform 컴포넌트란?
			 * - Unity 씬 상에 배치되는 물체의 변환에 대한 처리를 지원하는 컴포넌트를 의미한다. (즉, 해당 컴포넌트를
			 * 활용하면 물체의 위치 등을 변경하는 것이 가능하다.)
			 * 
			 * Vector3 구조체란?
			 * - float 변수 3 개를 멤버로 지니는 구조체를 의미한다. (즉, 해당 구조체를 활용하면 위치 및 방향 등을 표현하는
			 * 것이 가능하다.)
			 * 
			 * Unity 는 3 차원 엔진이기 때문에 특정 물체의 위치 등을 표현하기 위해서는 3 가지 요소 (X, Y, Z) 가 항상 
			 * 필요하다는 것을 알 수 있다.
			 */
			m_oPhysics01Target.transform.Rotate(Vector3.up, 180.0f * fHorizontal * Time.deltaTime, Space.World);
			m_oPhysics01Target.transform.Translate(Vector3.forward * 1000.0f * fVertical * Time.deltaTime, Space.Self);

			var stRay = new Ray(m_oPhysics01Target.transform.position, m_oPhysics01Target.transform.forward);

			/*
			 * Physics 클래스란?
			 * - 물리 연산과 관련 된 다양한 기능을 제공하는 클래스를 의미한다. (즉, 해당 클래스를 활용하면 광선 추적 등
			 * 게임을 제작하는데 필요한 여러 물리 연산을 손쉽게 처리하는 것이 가능하다.)
			 * 
			 * 단, Physics 클래스는 3 차원에 대한 물리 연산을 지원하기 때문에 2 차원 물리 연산이 필요하다면 Physics2D
			 * 클래스를 사용하는 것을 추천한다. (즉, Physics2D 클래스는 2 차원 전용이기 때문에 3 차원 연산에 비해서 좀 더
			 * 빠르게 동작한다는 것을 알 수 있다.)
			 */
			// 충돌체가 존재 할 경우
			if(Physics.Raycast(stRay, out RaycastHit stRaycastHit, 500.0f)) {
				Debug.LogFormat("{0} 물체와 충돌했습니다.", stRaycastHit.collider.name);
			}
#elif E11_PHYSICS_02
			// 발사 상태 일 경우
			if(m_bIsShoot)
			{
				return;
			}

			float fVertical = Input.GetAxis("Vertical");

			/*
			 * 3 차원 물체 회전 처리 방법
			 * - 오일러 회전
			 * - 사원수 회전
			 * 
			 * 오일러 회전이란?
			 * - 3 차원 상에 존재하는 물체의 회전 정도를 X, Y, Z 축을 기준으로하는 3 가지의 회전을 조합해서 표현하는 방법을
			 * 의미한다.
			 * 
			 * 따라서, 오일러 회전 방식은 각 축에 대한 회전을 어떻게 조합하는지에 따라 회전 결과가 달라지는 단점이 존재한다.
			 * (즉, Unity 는 해당 부분을 고려해서 YXZ 축 순으로 회전을 조합하며 이를 줄여서 YPR 회전이라고도 한다.)
			 * 
			 * 오일러 회전 관련 용어
			 * - Yaw	<- Y 축을 기준으로 하는 회전
			 * - Pitch	<- X 축을 기준으로 하는 회전
			 * - Roll	<- Z 축을 기준으로 하는 회전
			 * 
			 * 사원수 회전이란?
			 * - 물체의 회전 정도를 회전의 기준이 되는 축 (X, Y, Z) 과 회전 각도를 이용해서 표현하는 방법을 의미한다.
			 * 
			 * 사원수 회전은 오일러 회전 방식에 비해 적은 연산으로도 물체의 회전을 처리 할 수 있기 때문에 Unity 를 비롯한
			 * 많은 게임 엔진에서 채택하고 있는 방식이다. (즉, Unity 는 표면적으로 오일러 회전 인터페이스를 지원하지만
			 * 내부적으로는 해당 연산을 사원수 방식으로 처리한다는 것을 알 수 있다.)
			 */
			var stRotate = m_oPhysics02Target.transform.localEulerAngles;
			stRotate.z = Mathf.Clamp(stRotate.z + (fVertical * 180.0f * Time.deltaTime), 0.0f, 80.0f);

			m_oPhysics02Target.transform.localEulerAngles = stRotate;
			this.UpdateUIsState();

			// 발사 키를 눌렀을 경우
			if(Input.GetKey(KeyCode.Space))
			{
				m_fShootPower = Mathf.Clamp01(m_fShootPower + Time.deltaTime);
			}

			// 발사 키 입력을 종료했을 경우
			if(Input.GetKeyUp(KeyCode.Space))
			{
				var stForce = m_oPhysics02Target.transform.right * 2500.0f * m_fShootPower;
				var stForcePos = m_oPhysics02Target.transform.position + (Vector3.up * 50.0f);

				/*
				 * GetComponent 계열 메서드는 특정 게임 객체가 지니고 있는 컴포넌트를 가져오는 역할을 수행한다. (즉, 해당
				 * 메서드를 활용하면 특정 게임 객체의 컴포넌트를 가져와서 프로그램의 목적에 맞게 제어하는 것이 가능하다.)
				 * 
				 * 단, 특정 게임 객체에 원하는 컴포넌트가 존재하지 않을 경우 null 참조 값을 반환하기 때문에 게임 객체가
				 * 특정 컴포넌트를 지니고 있다는 보장이 없을 경우 반드시 해당 컴포넌트를 사용하기 전에 조건문 등을 활용해서
				 * 예외 처리를 해줘야한다.
				 */
				var oRigidbody = m_oPhysics02Target.GetComponent<Rigidbody>();
				oRigidbody.useGravity = true;
				oRigidbody.AddForceAtPosition(stForce, stForcePos, ForceMode.VelocityChange);

				m_bIsShoot = true;
				this.SetupObstacleRigidbodyState(true);
			}
#endif // E11_PHYSICS_01
		}

		/** UI 상태를 갱신한다 */
		private void UpdateUIsState()
		{
#if E11_PHYSICS_02
			m_oPhysics02ShootPowerText.text = $"{m_fShootPower * 100.0f:0}";
#endif // #if E11_PHYSICS_01
		}

		/** 장애물 강체 상태를 설정한다 */
		private void SetupObstacleRigidbodyState(bool a_bIsEnable)
		{
			/*
			 * Transform 컴포넌트를 활용하면 특정 게임 객체의 자식 게임 객체에 접근하는 것이 가능하다. (즉, Transform
			 * 컴포넌트에는 게임 객체의 변환에 대한 정보와 더불어 계층적인 관계를 제어 할 수 있는 여러 기능을 지원한다는
			 * 것을 알 수 있다.)
			 */
			for(int i = 0; i < m_oPhysics02ObstacleRoot.transform.childCount; ++i)
			{
				var oObstacleRigidbody = m_oPhysics02ObstacleRoot.transform.GetChild(i).GetComponent<Rigidbody>();
				oObstacleRigidbody.useGravity = a_bIsEnable;
				oObstacleRigidbody.constraints = a_bIsEnable ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
			}
		}

#if UNITY_EDITOR
		/*
		 * OnDrawGizmos 계열 메서드를 활용하면 Unity 에디터 씬 뷰에 간단한 그래픽을 출력하는 것이 가능하다. (즉, 해당 
		 * 메서드를 활용하면 프로젝트를 개발하는데 필요한 여러 정보를 씬 뷰에 출력하는 것이 가능하다.)
		 * 
		 * 단, 해당 메서드는 Unity 에디터 상에서만 사용 가능하기 때문에 해당 메서드를 반드시 UNITY_EDITOR 심볼로 감싸주는
		 * 것을 추천한다. (즉, UNITY_EDITOR 심볼로 지정 된 영역은 Unity 에디터 상에서만 동작한다는 것을 알 수 있다.)
		 */
		/** 기즈모를 그린다 */
		public void OnDrawGizmos()
		{
			/*
			 * Gizmos 클래스란?
			 * - Unity 에디터 씬 뷰에 여러가지 정보를 출력하기 위한 역할을 수행하는 클래스를 의미한다.
			 * 
			 * 단, 해당 클래스는 Unity 엔진 상에서 구동되는 모든 컴포넌트가 공유하는 클래스이기 때문에 해당 클래스의 상태를
			 * 변경했다면 반드시 사용을 마치고 나서 원래 상태로 되돌려 줄 필요가 있다.
			 */
			var stPrevColor = Gizmos.color;

			/*
			 * try ~ finally 구문을 이용하면 특정 메서드의 호출이 종료되기 전에 finally 영역의 명령문은 반드시 실행된다는
			 * 것을 보장 받을 수 있다. (즉, 특정 메서드가 종료되기 전에 반드시 실행해야 될 명령문은 try ~ finally 구문을
			 * 사용하면 좀 더 안전하게 작성하는 것이 가능하다.)
			 */
			try
			{
#if E11_PHYSICS_01
				var stEndPos = m_oPhysics01Target.transform.position;
				stEndPos += m_oPhysics01Target.transform.forward * 250.0f;

				Gizmos.color = Color.red;
				Gizmos.DrawLine(m_oPhysics01Target.transform.position, stEndPos);
#endif // #if E11_PHYSICS_01
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
