//#define E10_STRUCTURE
#define E10_ENUMERATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 사용자 정의 자료형이란?
 * - 사용자가 필요에 따라 직접 정의해서 사용하는 자료형을 의미한다. (즉, 사용자 정의 자료형을 활용하면 제작하는 프로그램에
 * 맞춰 자료형을 직접 정의하는 것이 가능하다.)
 * 
 * 단, 사용자 정의 자료형이라고 하더라도 완전히 새로운 자료형을 정의하는 것이 아니라 언어가 미리 제공해주는 기본 자료형을
 * 조합 및 변형해서 사용자가 의도한 자료형을 정의 할 수 있다.
 * 
 * C# 사용자 정의 자료형 종류
 * - 클래스
 * - 구조체
 * - 열거형
 */
namespace Example
{
#if E10_STRUCTURE
	/*
	 * 구조체란?
	 * - 사용자 정의 자료형 중 하나로서 변수 및 메서드를 하나의 그룹으로 관리 할 수 있는 기능을 의미한다. (즉, 구조체를 
	 * 활용하면 관련 된 데이터와 메서드를 하나의 데이터로 관리하는 것이 가능하다.)
	 * 
	 * 구조체는 값 형식 자료형이기 때문에 해당 자료형으로 선언 된 변수를 다른 변수에 할당하면 완전한 사본이 만들어진다는 
	 * 특징이 존재한다.
	 * 
	 * C# 구조체 선언 방법
	 * - struct + 구조체 이름 + 구조체 멤버 (변수, 메서드 등등...)
	 * 
	 * Ex)
	 * struct STData {
	 *     public int m_nVal;
	 *     public float m_fVal;
	 * }
	 * 
	 * 구조체 vs 클래스
	 * - 구조체는 단순한 데이터의 집합을 표현하는데 주로 활용되기 때문에 클래스에서 지원하는 상속과 다형성을 사용하는 것이
	 * 불가능하다. (즉, 구조체를 통해서도 특정 사물을 표현하는 것이 가능하지만 클래스에 비해서 사물을 표현 할 수 있는 방법이
	 * 제한적이라는 것을 알 수 있다.)
	 * 
	 * 반면, 클래스는 특정 사물을 표현하는데 주로 활용된다. (즉, 클래스는 상속과 다형성을 지원하기 때문에 구조체 보다 좀 더 
	 * 다양한 방법으로 사물을 표현하는 것이 가능하다.)
	 * 
	 * 따라서, 단순한 데이터의 집합을 표현 할 때는 구조체를 활용하고 특정 사물을 표현 할 때는 클래스를 사용하는 것이 일반적인
	 * 관례이다.
	 */
	/** 데이터 */
	public struct STE01Data_10 {
		public int m_nVal;
		public float m_fVal;

		/*
		 * 구조체는 클래스와 달리 기본 생성자를 구현하는 것이 불가능하다. (즉, 구조체의 기본 생성자는 C# 컴파일러에 의해서
		 * 항상 자동으로 구현된다는 특징이 존재한다.)
		 * 
		 * 또한, 구조체의 생성자 내부에서는 반드시 구조체에 존재하는 모든 멤버를 초기화해줘야한다. (즉, 하나라도 초기화하지 
		 * 않을 경우 컴파일 에러가 발생한다는 것을 알 수 있다.)
		 */
		/** 생성자 */
		public STE01Data_10(int a_nVal, float a_fVal) {
			m_nVal = a_nVal;
			m_fVal = a_fVal;
		}
	}
#elif E10_ENUMERATION
	/*
	 * 열거형이란?
	 * - 심볼릭 상수를 정의 할 수 있는 기능을 의미한다. (즉, 열거형을 이용하면 특정 데이터를 구분 할 수 있는 식별자를 좀 더
	 * 수월하게 정의하는 것이 가능하다.)
	 * 
	 * C# 열거형 선언 방법
	 * - enum + 열거형 이름 + 열거형 상수
	 * 
	 * Ex)
	 * enum ESomeEnum {
	 *     NONE = -1,
	 *     CONST_01,
	 *     CONST_02,
	 *     MAX_VAL
	 * }
	 * 
	 * 상수란?
	 * - 데이터를 저장하고 읽어들일 수 있는 공간을 의미한다. 
	 * 
	 * 단, 데이터를 자유롭게 저장하고 읽어들 일 수 있는 변수와 달리 상수는 데이터를 한번 저장하고나면 더이상 저장 된 데이터를
	 * 변경하는 것이 불가능하다.
	 * 
	 * C# 상수 종류
	 * - 리터널 상수
	 * - 심볼릭 상수
	 * 
	 * 리터널 상수란?
	 * - 이름이 존재하지 않는 상수를 의미한다.
	 * 
	 * 따라서, 일반적으로 리터널 상수는 재사용하는 것이 불가능하다는 것을 알 수 있다.  (즉, 리너털 상수는 일회성 상수라는 
	 * 것을 알 수 있다.)
	 * 
	 * 심볼릭 상수란?
	 * - 이름이 존재하는 상수를 의미한다.
	 * 
	 * 따라서, 심볼릭 상수는 필요에 따라 얼마든지 재사용하는 것이 가능하다. (즉, 변수와 마찬가지로 심볼릭 상수는 이름이
	 * 존재하기 때문에 해당 이름을 활용해서 언제든지 상수에 접근하는 것이 가능하다는 것을 알 수 있다.)
	 */
	public enum EE01ItemType_10
	{
		NONE = -1,
		GOLD,
		POTION,
		EQUIPMENT,
		[HideInInspector] MAX_VAL
	}
#endif // #if E10_STRUCTURE

	/** Example 10 */
	public partial class CE01Example_10 : CSceneManager
	{
		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_10;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

#if E10_STRUCTURE
			var stData01 = new STE01Data_10();
			stData01.m_nVal = 10;
			stData01.m_fVal = 3.14f;

			var stData02 = new STE01Data_10() {
				m_nVal = 10, m_fVal = 3.14f
			};

			var stData03 = new STE01Data_10(10, 3.14f);

			Debug.Log("=====> 데이터 - 1 <=====");
			Debug.Log($"정수 : {stData01.m_nVal}");
			Debug.Log($"실수 : {stData01.m_fVal}");

			Debug.Log("=====> 데이터 - 2 <=====");
			Debug.Log($"정수 : {stData02.m_nVal}");
			Debug.Log($"실수 : {stData02.m_fVal}");

			Debug.Log("=====> 데이터 - 3 <=====");
			Debug.Log($"정수 : {stData03.m_nVal}");
			Debug.Log($"실수 : {stData03.m_fVal}");
#elif E10_ENUMERATION
			/*
			 * 열거형은 사용자 정의 자료형 중 하나이기 때문에 특정 열거형을 사용해서 변수를 선언할 수 있다.  (즉, 열거형을 
			 * 자료형처럼 사용하는 것이 가능하다.)
			 * 
			 * Random 클래스란?
			 * - 불규칙한 값을 생성 할 수 있는 여러 기능을 제공하는 클래스를 의미한다. (즉, 해당 클래스를 활용하면 손쉽게
			 * 난수를 생성하는 것이 가능하다는 것을 알 수 있다.)
			 */
			EE01ItemType_10 eItemType = (EE01ItemType_10)Random.Range(0, (int)EE01ItemType_10.MAX_VAL);

			switch(eItemType)
			{
				case EE01ItemType_10.GOLD:
					Debug.Log("골드를 획득했습니다.");
					break;
				case EE01ItemType_10.POTION:
					Debug.Log("포션을 획득했습니다.");
					break;
				case EE01ItemType_10.EQUIPMENT:
					Debug.Log("장비를 획득했습니다.");
					break;
			}
#endif // #if E10_STRUCTURE
		}
		#endregion // 함수
	}
}
