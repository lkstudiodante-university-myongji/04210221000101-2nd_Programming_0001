using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 카메라란?
 * - 3 차원 공간에서 특정 부분을 인지하기 위한 시야를 의미한다. (즉, 3 차원 공간은 무한히 펼쳐져 있는 공간이기 때문에 해당
 * 공간을 모두 표현하는 것은 불가능하다는 것을 알 수 있다.)
 * 
 * Unity 는 카메라를 통해서 3 차원 공간의 특정 장면을 표현하기 때문에 Unity 씬 상에 배치 된 물체를 화면 상에 출력하기 
 * 위해서는 반드시 1 개 이상의 카메라가 필요하다. (즉, 씬 상에 카메라가 존재하지 않을 경우 아무런 결과물도 출력되지 않는다는
 * 것을 알 수 있다.)
 * 
 * Unity 카메라 투영 방식
 * - 직교 투영
 * - 원근 투영
 * 
 * Unity 카메라는 2 가지 투영 방식을 제공하며 투영 방식에 따라서 2 차원 또는 3 차원 결과물을 만들어내는 것이 가능하다.
 * 
 * 직교 투영이란?
 * - 직교 투영은 물체와 카메라와의 거리가 달라진다 하더라도 물체의 크기에는 변화가 없다. (즉, 깊이감이 느껴지지 않기 때문에
 * 2 차원 결과물을 제작 할 때 적합하다는 것을 알 수 있다.)
 * 
 * 원근 투영이란?
 * - 원근 투영은 물체와 카메라의 거리에 따라 물체의 크기도 달라지는 특징이 존재한다. (즉, 물체가 카메라에 가까울수록
 * 물체가 커지고 멀어질 수록 작아진다는 것을 알 수 있다.)
 * 
 * 따라서, 원근 투영은 3 차원 결과물을 제작하는데 주로 활용된다.
 */
namespace Example
{
	/** Example 2 */
	public partial class CE01Example_02 : CSceneManager
	{
		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_02;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
		}
		#endregion // 함수
	}
}
