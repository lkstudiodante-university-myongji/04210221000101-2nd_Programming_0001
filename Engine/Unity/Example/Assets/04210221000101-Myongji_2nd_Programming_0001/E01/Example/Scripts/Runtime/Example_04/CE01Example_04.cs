//#define E04_LIGHT
#define E04_MATERIAL

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 광원이란?
 * - 3 차원 공간에서 물체를 식별 할 수 있게 빛을 발산하는 주체를 의미한다. (즉, 광원은 현실 세계의 태양과 같이 빛을 발산하는
 * 대상이라는 것을 알 수 있다.)
 * 
 * Unity 는 현실 세계에서 물체를 식별하기 위한 원리를 디지털적으로 시뮬레이션하는 엔진이기 때문에 Unity 씬 상에 배치 된
 * 물체를 식별하기 위해서는 반드시 1 개 이상의 광원이 필요하다. (즉, 씬 상에 광원이 존재하지 않을 경우 물체를 식별하는 것이
 * 불가능하다는 것을 알 수 있다.)
 * 
 * Unity 광원 종류
 * - 방향 광원
 * - 포인트 광원
 * - 스포트 라이트 광원
 * - 영역 광원 (베이크 전용)
 * 
 * 방향 광원이란?
 * - 현실 상에는 존재하지 않는 광원으로서 Unity 씬 상에 배치 된 물체들이 모두 같은 방향으로 빛을 받는 특징이 존재한다.
 * 
 * 따라서, 방향 광원은 Unity 씬 상에 배치 된 물체들이 모두 빛을 받을 수 있는 특징이 존재하기 때문에 기본 광원으로 많이 활용
 * 된다. (즉, 방향 광원을 통해 기본적인 빛을 표현하고 다른 광원을 조합함으로서 다양한 결과물을 만들어낸다는 것을 알 수 있다.)
 * 
 * 포인트 광원이란?
 * - 빛의 중심으로부터 사방으로 빛이 퍼져나가는 광원을 의미한다. (즉, 중심으로부터 빛이 퍼져나가기 때문에 광원의 위치에 따라
 * Unity 씬 상에 배치 된 물체들이 받는 빛의 방향이 달라진다는 것을 알 수 있다.)
 * 
 * 스포트 라이트 광원이란?
 * - 빛의 중심으로부터 특정 각도로 빛이 퍼져나가는 광원을 의미한다. (즉, 현실 상의 손전등과 같이 특정 방향으로 퍼져나가는
 * 광원이라는 것을 알 수 있다.)
 * 
 * 영역 광원이란?
 * - 베이크 전용으로 사용되는 광원으로서 특정 영역을 밝게하기 위한 용도로 활용된다. (즉, 영역 광원은 광원 맵을 생성할 때
 * 사용된다는 것을 알 수 있다.)
 * 
 * 재질이란?
 * - 물체의 표면을 표현하는 기능을 의미한다. (즉, 동일한 형태의 물체라고 하더라도 재질에 따라 물체의 색상 등이 달라진다는 것을 
 * 알 수 있다.)
 * 
 * Unity 에서 재질은 쉐이더를 설정하고 제어하기 위한 수단이다. (즉, 재질을 통해 쉐이더를 설정하고 필요한 속성을 지정함으로서
 * 물체의 다양한 재질을 표현하는 것이 가능하다.)
 */
namespace Example
{
	/** Example 4 */
	public partial class CE01Example_04 : CSceneManager
	{
		#region 변수
		[SerializeField] private Material m_oMaterialTarget = null;

		[Header("=====> Game Objects <=====")]
		[SerializeField] private GameObject m_oLightMain = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_04;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
		}

		/** 상태를 갱신한다 */
		public override void Update()
		{
			base.Update();

#if E04_LIGHT
			// 상/하 방향 키를 눌렀을 경우
			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
				float fAngle = Input.GetKey(KeyCode.UpArrow) ? -180.0f : 180.0f;

				/*
				 * Transform 의 Rotate 메서드는 물체를 회전시키는 역할을 수행한다. (즉, 해당 메서드를 활용하면 물체를
				 * 특정 방향으로 회전시키는 것이 가능하다.)
				 * 
				 * Unity 는 특정 물체의 회전 상태를 표현하기 위해서 오일러 회전과 사원수 방식을 지원한다.
				 * 
				 * 오일러 회전이란?
				 * - 물체의 회전을 각 축의 회전으로 나누어서 표현하는 방식을 의미한다. (즉, 각 축의 회전으로 나누어서
				 * 표현하기 때문에 회전의 조합에 따라 서로 다른 결과를 만들어 낼 수 있다는 것을 알 수 있다.)
				 * 
				 * 사원수 회전이란?
				 * - 물체의 회전을 회전이 되는 중심 축과 각도로 표현하는 방식을 의미한다. (즉, 물체의 회전을 축과 각도를
				 * 사용해서 표현하기 때문에 4 개의 요소가 필요하다는 것을 알 수 있다.)
				 * 
				 * 사원수 회전은 오일러 회전에 비해 4 개의 요소만으로 회전을 표현하는 것이 가능하기 때문에 Unity 엔진을
				 * 비롯한 여러 그래픽스 프로그램에서 활용된다는 특징이 존재한다.
				 */
				m_oLightMain.transform.Rotate(Vector3.right, fAngle * Time.deltaTime, Space.World);
			}

			// 좌/우 방향 키를 눌렀을 경우
			if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
				float fAngle = Input.GetKey(KeyCode.LeftArrow) ? -180.0f : 180.0f;
				m_oLightMain.transform.Rotate(Vector3.up, fAngle * Time.deltaTime, Space.World);
			}
#elif E04_MATERIAL
			// 빨간색 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				/*
				 * Material 클래스가 제공하는 여러 기능들을 활용하면 프로그램이 실행 중에 특정 물체의 표면을 표현하기 위한
				 * 정보를 변경하는 것이 가능하다. (즉, Material 클래스는 쉐이더가 동작하는데 필요한 여러 정보를 설정 할 수
				 * 있도록 접근자 메서드를 지원한다는 것을 알 수 있다.)
				 */
				m_oMaterialTarget.color = Color.red;
			}
			// 녹색 키를 눌렀을 경우
			else if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				m_oMaterialTarget.color = Color.green;
			}
			// 파란색 키를 눌렀을 경우
			else if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				m_oMaterialTarget.color = Color.blue;
			}
			// 취소 키를 눌렀을 경우
			else if(Input.GetKeyDown(KeyCode.Space))
			{
				m_oMaterialTarget.color = Color.white;
			}
#endif // #if E04_LIGHT
		}
		#endregion // 함수
	}
}
