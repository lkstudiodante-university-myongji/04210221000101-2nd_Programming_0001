using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 가상 메서드란?
 * - 자식 클래스를 통해서 생성 된 객체를 부모 클래스 자료형으로 참조하고 있을 경우 부모 클래스에 존재하는 메서드가 아닌
 * 자식 클래스에 존재하는 메서드를 호출 할 수 있는 메커니즘을 의미한다. (즉, 가상 메서드를 활용하면 특정 객체를 참조하고 있는 
 * 자료형이 다르더라도 동일한 결과를 만들어내는 것이 가능하다.)
 * 
 * 단, 가상 메서드는 일반적인 메서드와 달리 상속 기능을 지원하는 클래스에만 사용 가능하다. (즉, 구조체는 상속을 지원하지 않기 
 * 때문에 가상 메서드를 사용하는 것이 불가능하다는 것을 알 수 있다.)
 * 
 * C# 가상 메서드 구현 방법
 * - virtual + 반환 형 + 메서드 이름 + 매개 변수 + 메서드 몸체
 * 
 * Ex)
 * public virtual void SomeVirtualMethod(int a_nValA, int a_nValB) {
 *     // Do Something
 * }
 */
namespace Example
{
	/** 부모 클래스 */
	public class CE01Base_09
	{
		/** 정보를 출력한다 */
		public void ShowInfo()
		{
			Debug.Log("부모 클래스 ShowInfo 메서드 호출");
		}

		/** 정보를 출력한다 */
		public virtual void ShowInfoVirtual()
		{
			Debug.Log("부모 클래스 ShowInfoVirtual 메서드 호출");
		}
	}

	/** 자식 클래스 */
	public class CE01Derived_09 : CE01Base_09
	{
		/** 정보를 출력한다 */
		public new void ShowInfo()
		{
			Debug.Log("자식 클래스 ShowInfo 메서드 호출");
		}

		/*
		 * override 키워드란?
		 * - 부모 클래스에 존재하는 가상 메서드 대신에 자식 클래스에 존재하는 메서드가 호출 될 수 있도록 해주는 키워드를
		 * 의미한다. (즉, 부모 클래스에 존재하는 메서드 대신에 항상 자식 클래스에 존재하는 메서드가 호출되도록 하고 싶다면
		 * 부모 클래스에 존재하는 가상 메서드를 자식 클래스에서 override 하면 된다는 것을 알 수 있다.)
		 */
		/** 정보를 출력한다 */
		public override void ShowInfoVirtual()
		{
			Debug.Log("자식 클래스 ShowInfoVirtual 메서드 호출");
		}
	}

	/** Example 9 */
	public partial class CE01Example_09 : CSceneManager
	{
		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_09;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

			CE01Base_09 oBase01 = new CE01Base_09();
			CE01Base_09 oBase02 = new CE01Derived_09();

			Debug.Log("=====> 메서드 호출 - 1 <=====");
			oBase01.ShowInfo();
			oBase01.ShowInfoVirtual();

			Debug.Log("=====> 메서드 호출 - 2 <=====");
			oBase02.ShowInfo();
			oBase02.ShowInfoVirtual();
		}
		#endregion // 함수
	}
}
