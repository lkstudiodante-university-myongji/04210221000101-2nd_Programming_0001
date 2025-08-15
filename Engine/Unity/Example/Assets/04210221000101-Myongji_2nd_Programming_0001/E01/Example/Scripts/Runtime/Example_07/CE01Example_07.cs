using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 클래스란?
 * - 사용자 정의 자료형 중 하나로서 변수 및 메서드를 하나의 그룹으로 관리 할 수 있는 기능을 의미한다. (즉, 클래스를 활용하면
 * 관련 된 데이터와 메서드를 하나의 데이터로 관리하는 것이 가능하다.)
 * 
 * 클래스는 참조 형식 자료형이기 때문에 해당 자료형으로 선언 된 변수를 다른 변수에 할당하면 얕은 복사가 발생한다는 것을 알 수
 * 있다.
 * 
 * 클래스는 구조체와 달리 상속 및 다형성을 지원하기 때문에 객체 지향 프로그래밍에서 핵심이 되는 객체 (사물) 을 표현하는 것이
 * 가능하다. (즉, 클래스를 통해 객체가 지니는 속성과 행위를 표현하는 것이 가능하다.)
 * 
 * C# 클래스 선언 방법
 * - class + 클래스 이름 + 클래스 멤버 (변수, 메서드 등등...)
 * 
 * Ex)
 * class CCharacter {
 *     public int m_nLV = 0;
 *     public int m_nHP = 0;
 *     public int m_nATK = 0;
 *     
 *     public void ShowInfo() {
 *         // Do Something
 *     }
 * }
 */
namespace Example
{
	/*
	 * 접근 제어 지시자 (한정자) 란?
	 * - 클래스 또는 구조체가 지니고 있는 멤버를 보호하기 위한 보호 수준을 의미한다. (즉, 명시 된 접근 제어 지시자에 따라
	 * 특정 멤버에 접근이 불가능 할 수 있다는 것을 알 수 있다.)
	 * 
	 * C# 접근 제어 지시자 종류
	 * - public			<- 내/외부에서 모두 접근 가능
	 * - protected		<- 클래스 내부 및 자식 클래스에서 접근 가능
	 * - private		<- 클래스 내부에서 접근 가능
	 */
	/** 캐릭터 */
	public class CE01Character_07
	{
		public int m_nLV = 0;
		public int m_nHP = 0;
		public int m_nATK = 0;

		/*
		 * 생성자란?
		 * - 클래스 또는 구조체를 통해 생성 된 데이터를 초기화하기 위한 특별한 메서드를 의미한다. (즉, 생성자는 구조체 및
		 * 클래스 이외에는 사용하는 것이 불가능하다는 것을 알 수 있다.)
		 * 
		 * 생성자는 일반적인 메서드와 달리 직접적으로 호출하는 것이 불가능하며 C# 컴파일러에 의해서 자동으로 호출되는 특징이
		 * 존재한다.
		 * 
		 * 또한, 클래스를 통해서 객체를 생성하기 위해서는 반드시 생성자가 호출되어야한다는 규칙이 존재한다. (즉, 적절한
		 * 생성자가 클래스에 구현되어있지 않을 경우 객체를 생성하는 과정에서 컴파일 에러가 발생한다는 것을 알 수 있다.)
		 * 
		 * 따라서, 클래스 내부에 생성자가 존재하지 않을 경우 C# 컴파일러에 의해서 자동으로 기본 생성자가 추가 된다. (즉,
		 * 클래스 내부에 생성자가 존재 할 경우 C# 컴파일러는 더이상 기본 생성자를 자동으로 추가해주지 않는다는 것을 알 수
		 * 있다.)
		 */
		/** 생성자 */
		public CE01Character_07()
		{
			// Do Something
		}

		/** 생성자 */
		public CE01Character_07(int a_nLV, int a_nHP, int a_nATK)
		{
			m_nLV = a_nLV;
			m_nHP = a_nHP;
			m_nATK = a_nATK;
		}

		/** 정보를 출력한다 */
		public void ShowInfo()
		{
			Debug.Log($"레벨 : {m_nLV}");
			Debug.Log($"체력 : {m_nHP}");
			Debug.Log($"공격력 : {m_nATK}");
		}
	}

	/** Example 7 */
	public partial class CE01Example_07 : CSceneManager
	{
		/*
		 * 프로퍼티란?
		 * - 접근자 메서드를 구현 할 수 있는 기능을 의미한다. (즉, 프로퍼티를 활용하면 접근자 메서드를 구현하는 전통적인
		 * 방식에 비해 좀 더 수월하게 접근자 메서드를 구현하는 것이 가능하다.)
		 * 
		 * 접근자 메서드란?
		 * - 클래스의 멤버 변수는 일반적으로 외부에서 함부로 접근 할 수 없도록 private 수준으로 보호하는 것이 관례이다.
		 * 
		 * 따라서, 클래스 외부에서 멤버 변수에 접근하기 위한 방법이 필요하면 이때 구현하는 것이 바로 접근자 메서드이다.
		 * (즉, 접근자 메서드는 단순히 클래스 외부에 값을 반환하거나 클래스 외부에서 값을 설정하기 위한 메서드를 의미한다.)
		 * 
		 * Ex)
		 * public class CSomeClass {
		 *     private int m_nVal = 0;
		 *     
		 *     public int GetVal() {
		 *         return m_nVal;
		 *     }
		 *     
		 *     public void SetVal(int a_nVal) {
		 *         m_nVal = a_nVal;
		 *     }
		 * }
		 * 
		 * 위와 같이 클래스 외부에 멤버 변수의 값을 사용 할 수 있도록 반환해주는 Get 계열 메서드를 Getter 라고 하며 외부에서
		 * 멤버 변수의 값을 변경 할 수 있도록 해주는 Set 계열 메서드를 Setter 라고 한다. (즉, 접근자 메서드라는 것은 Getter
		 * 와 Setter 를 지칭한다는 것을 알 수 있다.)
		 * 
		 * C# 프로퍼티 구현 방법
		 * - 자료형 + 프로퍼티 이름 + 접근자 영역
		 * 
		 * Ex)
		 * public class CSomeClass {
		 *     private int m_nVal = 0;
		 * 
		 *     public int Val {
		 *         get {
		 *             return m_nVal;
		 *         } set {
		 *             m_nVal = value;
		 *         }
		 *     }
		 * }
		 * 
		 * var oSomeObj = new CSomeClass();
		 * oSomeObj.Val = 10;					<- Setter 호출
		 * 
		 * Debug.Log($"{oSomeObj.Val}");		<- Getter 호출
		 * 
		 * 프로퍼티를 활용하면 일반적인 변수를 사용하는 것처럼 접근자 메서드를 사용 할 수 있기 때문에 전통적인 접근자 메서드를
		 * 구현하는 방법에 비해서 좀 더 가독성이 향상 된다는 것을 알 수 있다.
		 * 
		 * 프로퍼티의 Getter 와 Setter 는 필요에 따라 생략하는 것이 가능하다. (즉, 둘 중 하나만 구현해도 컴파일 에러가 
		 * 발생하지 않는다는 것을 알 수 있다.)
		 */
		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_07;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

			/*
			 * new 키워드를 활용하면 객체를 생성하는 것이 가능하다. (즉, 해당 키워드로 객체를 생성하면 내부적으로 해당 
			 * 객체의 생성자가 호출된다는 것을 알 수 있다.)
			 * 
			 * var 키워드란?
			 * - 자동으로 변수의 자료형을 지정해주는 키워드를 의마한다. (즉, 해당 키워드를 활용하면 변수의 자료형을 자동으로
			 * 설정해줌으로서 변수 선언 명령문을 좀 더 수월하게 작성하는 것이 가능하다.)
			 * 
			 * 단, 해당 키워드를 통해서 선언 된 변수는 반드시 초기화 값을 명시해줘야한다. (즉, C# 컴파일러는 초기화 값의
			 * 자료형을 기반으로 해당 변수의 자료형을 지정한다는 것을 알 수 있다.)
			 */
			var oCharacter01 = new CE01Character_07();
			oCharacter01.m_nLV = 1;
			oCharacter01.m_nHP = 10;
			oCharacter01.m_nATK = 15;

			var oCharacter02 = new CE01Character_07(1, 10, 15);

			Debug.Log("=====> 캐릭터 - 1 <=====");
			oCharacter01.ShowInfo();

			Debug.Log("=====> 캐릭터 - 2 <=====");
			oCharacter02.ShowInfo();
		}
		#endregion // 함수
	}
}
