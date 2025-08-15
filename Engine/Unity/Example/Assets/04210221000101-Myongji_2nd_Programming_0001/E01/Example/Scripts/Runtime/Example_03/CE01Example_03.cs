using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 프리팹이란?
 * - 게임 객체를 에셋으로 저장 및 제어 할 수 있는 기능을 의미한다. (즉, 프리팹을 활용하면 특정 게임 객체의 사본 게임 객체를
 * 손쉽게 생성하는 것이 가능하다.)
 * 
 * 프리팹을 통해서 생성 된 사본 게임 객체는 원본 프리팹과 변경 사항을 공유한다는 장점이 존재한다. (즉, 원본 프리팹의 변경
 * 사항을 사본 게임 객체에 반영하거나 반대로 사본 게임 객체의 변경 사항을 원본 프리팹에 반영하는 것이 가능하다.)
 * 
 * 스크립트 컴포넌트란?
 * - Unity 가 제공하는 기본 컴포넌트 이외에 필요에 따라 사용자가 직접 제작하는 컴포넌트를 의미한다. (즉, Unity 가 제공해주는
 * 컴포넌트만으로는 다양한 프로그램을 제작하는 것이 불가능하다는 것을 알 수 있다.)
 * 
 * 스크립트 컴포넌트는 C# 클래스를 통해서 제작하는 것이 가능하다.
 * 
 * Unity 스크립트 컴포넌트 제작 규칙
 * - 파일 이름과 C# 클래스 이름 일치 (2021 이하 버전)
 * - 직/간접적으로 MonoBehaviour 클래스 상속
 * 
 * C# 클래스가 스크립트 컴포넌트가 되기 위해서는 반드시 위의 규칙을 따라야한다. (즉, 스크립트 컴포넌트 제작 규칙 중 하나라도
 * 따르지 않았을 경우 해당 클래스는 게임 객체에 추가시키는 것이 불가능하다는 것을 알 수 있다.)
 */
namespace Example
{
	/** Example 3 */
	public partial class CE01Example_03 : CSceneManager
	{
		#region 변수
		/*
		 * SerializeField 속성은 C# 클래스에 존재하는 멤버 변수를 Unity 에디터 상에 노출시키는 역할을 수행한다.
		 */
		[SerializeField] private GameObject m_oTargetRoot = null;
		[SerializeField] private GameObject m_oOriginTarget = null;
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_03;
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

			/*
			 * Input 클래스란?
			 * - 키보드와 같은 입력 장치에서 발생하는 입력 신호를 제어 할 수 있는 클래스를 의미한다. (즉, 해당 클래스를 
			 * 활용하면 키보드, 마우스 및 조이패드와 같은 입력 장치를 손쉽게 제어 할 수 있다는 것을 알 수 있다.)
			 */
			// 스페이스 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Space))
			{
				/*
				 * Instantiate 메서드는 원본 게임 객체로부터 사본 게임 객체를 생성하는 역할을 수행한다. (즉, 해당 메서드를
				 * 활용하면 새로운 게임 객체를 손쉽게 생성하는 것이 가능하다.)
				 */
				var oGameObj = Instantiate(m_oOriginTarget, Vector3.zero, Quaternion.identity);

				/*
				 * Transform 의 SetParent 메서드는 부모 객체를 지정하는 역할을 수행한다. (즉, 해당 메서드를 활용하면
				 * 특정 게임 객체의 자식으로 특정 객체를 추가하는 것이 가능하다.)
				 */
				oGameObj.transform.SetParent(m_oTargetRoot.transform, false);
			}
		}
		#endregion // 함수
	}
}
