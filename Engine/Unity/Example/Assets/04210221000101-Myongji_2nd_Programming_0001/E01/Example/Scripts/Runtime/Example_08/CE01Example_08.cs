//#define E08_INHERITANCE
#define E08_POLYMORPHISM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 상속이란?
 * - 클래스 간에 부모/자식 관계를 형성 할 수 있는 기능을 의미한다. (즉, 특정 클래스를 상속하면 자식 클래스에서는 부모
 * 클래스에 존재하는 멤버를 사용하는 것이 가능하다.)
 * 
 * 따라서, 상속을 활용하면 여러 클래스에 중복적으로 존재하는 멤버를 줄이는 것이 가능하다. (즉, 클래스마다 공통적으로
 * 필요한 멤버를 부모 클래스에 정의하고 해당 클래스를 상속함으로서 멤버의 중복을 최소화 시킬 수 있다.)
 * 
 * 단, C# 은 단일 상속만을 지원하기 때문에 특정 클래스가 여러 부모 클래스를 상속하는 것은 불가능하다. (즉, 여러 클래스를
 * 상속 할 수 있는 기능을 다중 상속이라고 하지만 다중 상속은 원치 않는 여러 문제를 일으키기 때문에 C# 에서는 지원하지
 * 않는다는 것을 알 수 있다.)
 * 
 * 다형성이란?
 * - 특정 대상을 바라보는 시선에 따라 여러가지 형태를 지니는 것을 의미한다. (즉, 프로그래밍 언어에서는 데이터의 형태가
 * 정해져 있기 때문에 특정 대상이 여러 형태를 지니는 다형성을 완전하게 표현하는 것은 불가능하지만 상속을 활용하면 다형성을
 * 흉내 낼 수 있다.)
 * 
 * C# 클래스는 부모 클래스 자료형으로 자식 클래스로 생성 된 객체를 참조하는 것이 가능하다. (즉, 자식 클래스 객체를 부모
 * 클래스 자료형으로 참조함으로서 자식 클래스 객체를 부모 클래스 객체로 인지한다는 것을 의미한다.)
 */
namespace Example
{
	/** 부모 클래스 */
	public class CE01Base_08
	{
		private int m_nVal = 0;
		protected float m_fVal = 0.0f;

		/*
		 * 생성자에서는 동일한 클래스에 존재하는 다른 생성자를 호출하는 것이 가능하며 이를 위임 생성자라고 한다. (즉, 위임
		 * 생성자를 활용하면 객체를 초기화하는 명령문을 특정 생성자에만 작성해서 명령문의 중복을 줄이는 것이 가능하다.)
		 */
		/** 생성자 */
		public CE01Base_08() : this(0, 0.0f)
		{
			// Do Something
		}

		/** 생성자 */
		public CE01Base_08(int a_nVal, float a_fVal)
		{
			m_nVal = a_nVal;
			m_fVal = a_fVal;
		}

		/** 정보를 출력한다 */
		public void ShowInfo()
		{
			Debug.Log($"정수 : {m_nVal}");
			Debug.Log($"실수 : {m_fVal}");
		}
	}

	/** 자식 클래스 */
	public class CE01Derived_08 : CE01Base_08
	{
		private string m_oStr = string.Empty;

		/*
		 * 자식 클래스 생성자를 구현 할 경우 해당 생성자에서는 반드시 부모 클래스의 생성자를 호출해줘야한다. (즉, 생성 된
		 * 객체의 생성자는 C# 컴파일러가 호출해주지만 부모 클래스의 생성자는 자식 클래스의 생성자에서 호출해줘야한다는 것을
		 * 알 수 있다.)
		 * 
		 * 만약, 자식 클래스에서 부모 클래스의 생성자를 호출하지 않았을 경우 C# 컴파일러에 의해서 자동으로 부모 클래스의
		 * 기본 생성자를 호출하는 명령문이 추가된다는 특징이 존재한다. (즉, 부모 클래스에 기본 생성자가 존재하지 않을 경우
		 * 컴파일 에러가 발생한다는 것을 알 수 있다.)
		 * 
		 * base 키워드란?
		 * - 부모 클래스를 지칭하는 키워드를 의미한다. (즉, 해당 키워드를 활용하면 부모 클래스를 대상으로 메서드를 호출하거나
		 * 멤버에 접근하는 것이 가능하다는 것을 알 수 있다.)
		 */
		/** 생성자 */
		public CE01Derived_08(int a_nVal, float a_fVal, string a_oStr) : base(a_nVal, a_fVal)
		{
			m_oStr = a_oStr;
		}

		/*
		 * new 키워드를 특정 클래스 멤버에 명시하면 부모 클래스에 동일한 멤버가 존재한다는 컴파일 경고를 제거하는 것이
		 * 가능하다. (즉, 해당 키워드는 의도적으로 자식 클래스에 부모 클래스와 동일한 멤버를 구현했다는 것을 컴파일러에게
		 * 알리는 역할을 수행한다.)
		 */
		/** 정보를 출력한다 */
		public new void ShowInfo()
		{
			/*
			 * base 키워드를 명시함으로서 자식 클래스에 존재하는 ShowInfo 메서드가 아닌 부모 클래스의 ShowInfo 메서드를
			 * 호출한다는 것을 알 수 있다.
			 */
			base.ShowInfo();
			Debug.Log($"문자열 : {m_oStr}");

			/*
			 * private 보호 수준은 클래스 내부에서만 접근하는 것이 가능하기 때문에 자식 클래스에서도 접근이 불가능하다는
			 * 것을 알 수 있다.
			 * 
			 * 반면, protected 보호 수준은 자식 클래스에서도 접근이 허용되기 때문에 private 멤버와 달리 컴파일 에러가
			 * 발생하지 않는다는 것을 알 수 있다.
			 */
			//m_nVal = 10;
			m_fVal = 3.14f;
		}
	}

	/** Example 8 */
	public partial class CE01Example_08 : CSceneManager
	{
		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_08;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

#if E08_INHERITANCE
			var oBase = new CE01Base_08(10, 3.14f);
			var oDerived = new CE01Derived_08(10, 3.14f, "ABC");

			Debug.Log("=====> 부모 클래스 <=====");
			oBase.ShowInfo();

			Debug.Log("=====> 자식 클래스 <=====");
			oDerived.ShowInfo();
#elif E08_POLYMORPHISM
			CE01Base_08 oBase01 = new CE01Base_08(10, 3.14f);
			CE01Derived_08 oDerived = new CE01Derived_08(10, 3.14f, "ABC");

			/*
			 * CE01Derived_08 클래스는 CE01Base_08 클래스를 상속하고 있기 때문에 CE01Base_08 자료형으로 CE01Derived_08
			 * 클래스 객체를 참조하는 것이 가능하다.
			 * 
			 * 반면, CE01Derived_08 자료형으로 CE01Base_08 클래스 객체를 참조하는 것은 불가능하다. (즉, 자식 클래스는
			 * 부모 클래스가 포함하고 있지만 부모 클래스에는 자식 클래스가 존재하지 않기 때문에 참조가 불가능하다는 것을
			 * 알 수 있다.)
			 */
			CE01Base_08 oBase02 = new CE01Derived_08(10, 3.14f, "ABC");

			Debug.Log("=====> 부모 클래스 - 1 <=====");
			oBase01.ShowInfo();

			Debug.Log("=====> 부모 클래스 - 2 <=====");
			oBase02.ShowInfo();

			Debug.Log("=====> 자식 클래스 <=====");
			oDerived.ShowInfo();
#endif // E08_INHERITANCE
		}
		#endregion // 함수
	}
}
