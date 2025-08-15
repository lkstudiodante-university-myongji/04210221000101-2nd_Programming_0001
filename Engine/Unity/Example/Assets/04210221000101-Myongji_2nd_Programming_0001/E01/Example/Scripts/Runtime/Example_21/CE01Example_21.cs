using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 쉐이더란?
 * - GPU (Graphic Processing Unit) 에 의해서 실행되는 프로그램을 의미하며 화면 상에 출력되는 픽셀의 색상을 계산하는 역할을
 * 수행한다. (즉, GPU 에 의해서 실행되는 일반적인 프로그램과 달리 쉐이더는 그래픽 카드 상에 실행되는 그래픽 프로그램이라는
 * 것을 알 수 있다.)
 * 
 * 쉐이더는 픽셀의 색상을 계산하는 것이 주요 목적이기 때문에 쉐이더를 사용하면 화면 상에 출력되는 물체의 다양한 재질을
 * 표현하는 것이 가능하다. (즉, 쉐이더를 활용하면 다양한 효과를 구현하는 것이 가능하다.)
 * 
 * Unity 쉐이더 제작 방법
 * - Shader Lab
 * - Vertex / Fragment Shader
 * - Surface Shader
 * - Shader Graph
 * 
 * Shader Lab 이란?
 * - Unity 가 자체적으로 지원하는 쉐이더 언어로서 해당 언어를 활용하면 간단하게 쉐이더를 제작하는 것이 가능하다.
 * 
 * 단, 해당 방식은 제한적인 쉐이더만을 제작하는 것이 가능하기 때문에 현재는 활용되지 않는 방법이라는 단점이 존재한다. (즉,
 * 특정 상황을 제외하고는 잘 사용되지 않는다는 것을 알 수 있다.)
 * 
 * Vertex / Fragment Shader 란?
 * - 쉐이더를 제작하는 전통적인 방법으로서 정점 쉐이더와 프래그먼트 쉐이더를 직접 제작함으로서 다양한 효과를 구현하는 것이
 * 가능하다.
 * 
 * 해당 방식은 컴퓨터 그래픽스에 대한 높은 이해도를 요구하기 때문에 진입 장벽이 높다는 단점이 존재하지만 그래픽스 이해도를
 * 갖추고 있다면 높은 퀄리티의 성능 좋은 쉐이더를 제작하는 것이 가능하다.
 * 
 * Surface Shader 란?
 * - Vertex / Fragment Shader 를 통하지 않고도 퀄리티 좋은 쉐이더를 제작하는 방식을 의미한다. (즉, Vertex / Fragment
 * Shader 방식에 비해 컴퓨터 그래픽스의 높은 이해도를 요구하지 않는다는 것을 알 수 있다.)
 * 
 * 해당 방식은 많은 부분이 Unity 에 의해서 자동으로 처리 되기 때문에 Vertex / Fragment Shader 방식에 비해서 적은 노력으로도
 * 준수한 퀄리티의 쉐이더를 제작 할 수 있다는 장점이 존재한다.
 * 
 * Shader Graph 란?
 * - 명령문을 통해서 쉐이더를 작성하는 다른 방법과 달리 비주얼 툴을 이용해서 쉐이더를 제작하는 방식을 의미한다. (즉, 해당
 * 방식을 활용하면 복잡한 명령문을 사용하지 않고도 퀄리티 좋은 쉐이더를 제작하는 것이 가능하다.)
 * 
 * 단, Shader Graph 는 Built-in 렌더링 파이프라인에서는 지원하지 않기 떄문에 해당 방식을 통해서 쉐이더를 제작하기 위해서는
 * Universal or High Definition 렌더링 파이프라인을 사용해야한다.
 * 
 * 렌더링 파이프라인이란?
 * - Unity 씬 상에 배치 된 물체가 화면 상에 출력되기 위해서 거치는 일련의 단계를 의미한다. (즉, 특정 물체가 화면 상에
 * 출력되기 위해서는 여러 가지 과정이 필요하며 이 중 특정 과정을 제어하는 것은 쉐이더를 통해서 가능하다.)
 * 
 * Unity 렌더링 파이프라인 종류
 * - Built-in
 * - Universal
 * - High Definition
 */
namespace Example
{
	/** Example 21 */
	public partial class CE01Example_21 : CSceneManager
	{
		#region 변수
		private float m_fCutout = 0.0f;
		private float m_fRefraction = 0.0f;

		[SerializeField] private GameObject m_oTarget01 = null;
		[SerializeField] private GameObject m_oTarget02 = null;
		[SerializeField] private GameObject m_oTargetRoot = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_21;
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

			// 스페이스 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Space))
			{
				m_fRefraction = 0.0f;
			}

			// 상/하 방향 키를 눌렀을 경우
			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
			{
				float fOffset = Input.GetKey(KeyCode.UpArrow) ? Time.deltaTime : -Time.deltaTime;
				m_fCutout = Mathf.Clamp01(m_fCutout + fOffset);
			}

			// 좌/우 방향 키를 눌렀을 경우
			if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
			{
				float fOffset = Input.GetKey(KeyCode.LeftArrow) ? -Time.deltaTime : Time.deltaTime;
				m_fRefraction = Mathf.Clamp(m_fRefraction + fOffset, -0.5f, 0.5f);
			}

			for(int i = 0; i < m_oTargetRoot.transform.childCount; ++i)
			{
				var oTarget = m_oTargetRoot.transform.GetChild(i);
				oTarget.Rotate(Vector3.up * 90.0f * Time.deltaTime, Space.World);
			}

			var oMeshRenderer01 = m_oTarget01.GetComponent<MeshRenderer>();
			oMeshRenderer01.material.SetFloat("_Cutout", m_fCutout);

			var oMeshRenderer02 = m_oTarget02.GetComponent<MeshRenderer>();
			oMeshRenderer02.material.SetFloat("_Refraction", m_fRefraction);
		}
		#endregion // 함수
	}
}
