using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
	/** 타겟 */
	public partial class CE01Target_11 : CComponent
	{
		#region 함수
		/*
		 * Rigidbody 컴포넌트를 지니고 있는 게임 객체가 다른 물리 객체와 충돌했을 경우 Unity 는 내부적으로 OnTrigger 계열
		 * 메서드를 호출한다. (즉, 해당 메서드를 활용하면 특정 게임 객체의 충돌 여부를 검사하는 것이 가능하다.)
		 * 
		 * 단, OnTrigger 계열 메서드는 충돌한 객체가 지니고 있는 Collider 가 Trigger 상태 일 때만 호출된다. (즉, 충돌한
		 * 물체 중에 하나라도 Trigger 상태 인 Collider 를 지니고 있다면 해당 메서드가 호출된다는 것을 알 수 있다.)
		 * 
		 * 만약, 충돌한 게임 객체 중에 Trigger 상태인 Collider 가 존재하지 않을 경우 OnTrigger 계열 메서드 대신에
		 * OnCollision 계열 메서드가 호출된다.
		 * 
		 * Trigger vs Collision
		 * - Trigger 단순히 물리적인 충돌 여부만을 감지하기 때문에 물리적인 상호 작용은 발생하지 않는 특징이 존재한다.
		 * (즉, 물체가 충돌했을때 서로의 힘에 의해서 각 물체를 밀어내는 물리 연산이 수행되지 않는다는 것을 알 수 있다.)
		 * 
		 * 반면, Collision 물리적인 충돌과 충돌에 의한 물리 연산이 일어나기 때문에 물체의 사실적인 상호 작용을 결과를 
		 * 만들어내는 것이 가능하다. (즉, 내부적으로 물리 연산이 발생하기 때문에 Trigger 에 비해서 처리 비용이 비싸다는
		 * 것을 알 수 있다.)
		 * 
		 * 따라서, 단순한 물리적인 접촉 여부만을 판단하고 싶을 때는 Trigger 를 활용하면 된다는 것을 알 수 있다.
		 */
		/** 충돌이 시작 되었을 경우 */
		public void OnTriggerEnter(Collider a_oCollider)
		{
			Debug.Log($"OnTriggerEnter: {a_oCollider.name}");
		}

		/** 충돌이 진행 중 일 경우 */
		public void OnTriggerStay(Collider a_oCollider)
		{
			Debug.Log($"OnTriggerStay: {a_oCollider.name}");
		}

		/** 충돌이 종료 되었을 경우 */
		public void OnTriggerExit(Collider a_oCollider)
		{
			Debug.Log($"OnTriggerExit: {a_oCollider.name}");
		}

		/** 충돌이 시작 되었을 경우 */
		public void OnCollisionEnter(Collision a_oCollision)
		{
			Debug.Log($"OnCollisionEnter: {a_oCollision.collider.name}");
		}

		/** 충돌이 진행 중 일 경우 */
		public void OnCollisionStay(Collision a_oCollision)
		{
			Debug.Log($"OnCollisionStay: {a_oCollision.collider.name}");
		}

		/** 충돌이 종료 되었을 경우 */
		public void OnCollisionExit(Collision a_oCollision)
		{
			Debug.Log($"OnCollisionExit: {a_oCollision.collider.name}");
		}
		#endregion // 함수
	}
}
