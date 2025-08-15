using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 싱글턴 패턴이란?
 * - 프로그램이 실행 중에 생성하는 특정 객체의 개수를 하나로 제한하기 위한 패턴을 의미한다. (즉, 해당 패턴을 활용하면
 * 프로그램이 실행 중에 전역적으로 사용되는 객체를 생성 및 제어하는 것이 가능하다.)
 */
/** 싱글턴 */
public partial class CSingleton<T> : CComponent where T : CSingleton<T>
{
	#region 클래스 변수
	private static T m_tInst = null;
	#endregion // 클래스 변수

	#region 클래스 프로퍼티
	/*
	 * static 키워드는 해당 멤버가 객체에 종속되는 인스턴스 멤버가 아닌 클래스에 종속되는 정적 멤버라는 것을 알리는
	 * 역할을 수행한다. (즉, 해당 키워드로 명시 된 멤버는 클래스에 종속되기 때문에 객체를 생성하지 않고도 사용하는 것이
	 * 가능하다는 것을 알 수 있다.)
	 */
	public static T Inst
	{
		get
		{
			// 인스턴스가 없을 경우
			if(CSingleton<T>.m_tInst == null)
			{
				/*
				 * new 키워드를 활용하면 Unity 씬 상에 게임 객체를 생성하는 것이 가능하다. 단, 해당 방법을 통해 게임
				 * 객체를 생성 할 경우 생성 된 게임 객체가 동작하기 위해서 필요한 컴포넌트를 수동으로 설정해줘야하기
				 * 때문에 특별한 경우가 아니라면 해당 방법을 통한 게임 객체 생성은 추천하지 않는다.
				 */
				var oGameObj = new GameObject(typeof(T).ToString());

				/*
				 * AddComponent 메서드를 활용하면 특정 게임 객체에 새로운 컴포넌트를 추가하는 것이 가능하다.
				 * 
				 * 단, 해당 메서드는 중복 검사를 따로 처리하지 않기 때문에 특정 컴포넌트를 대상으로 해당 메서드를 여러번
				 * 호출 할 경우 중복으로 컴포넌트가 추가되는 특징이 존재한다. (즉, 컴포넌트가 중복으로 추가되는 현상을
				 * 방지하기 위해서는 컴포넌트를 추가하기 전에 추가하기 위한 컴포넌트가 이미 존재하는지 검사 할 필요가
				 * 있다.)
				 */
				oGameObj.AddComponent<T>();
			}

			return CSingleton<T>.m_tInst;
		}
	}
	#endregion // 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CSingleton<T>.m_tInst = this as T;

		/*
		 * DontDestroyOnLoad 메서드는 씬이 전환되어도 객체를 제거하지 않도록 설정하는 역할으르 수행한다. (즉, 
		 * 일반적으로 Unity 는 씬이 전환되면서 기존 씬이 제거 될 경우 해당 씬 안에 존재하는 모든 게임 객체를 제거한다는 
		 * 것을 알 수 있다.)
		 * 
		 * 따라서, 특정 씬 간에 데이터를 공유하고 싶을 경우에는 씬이 전환 되어도 기존 게임 객체가 제거 되지 않도록 해당 
		 * 메서드를 이용해야한다. (즉, 해당 메서드의 입력으로 전달 된 게임 객체는 명시적으로 제거하지 않는 이상 계속 
		 * Unity 씬 상에 존재한다는 것을 알 수 있다.)
		 */
		DontDestroyOnLoad(CSingleton<T>.m_tInst.gameObject);
	}
	#endregion // 함수

	#region 클래스 함수
	/** 인스턴스를 생성한다 */
	public static T Create()
	{
		return CSingleton<T>.m_tInst;
	}
	#endregion // 클래스 함수
}
