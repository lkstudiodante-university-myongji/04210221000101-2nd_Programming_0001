using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
	/** 타겟 */
	public partial class CE01Target_18 : CComponent
	{
		/** 타겟 타입 */
		public enum ETargetType
		{
			NONE = -1,
			A,
			D,
			[HideInInspector] MAX_VAL
		}

		#region 변수
		private bool m_bIsOpen = false;
		private Animator m_oAnimator = null;

		[SerializeField] private RuntimeAnimatorController m_oAniController01 = null;
		[SerializeField] private RuntimeAnimatorController m_oAniController02 = null;
		#endregion // 변수

		#region 프로퍼티
		public ETargetType TargetType { get; private set; } = ETargetType.NONE;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
			m_oAnimator = this.GetComponent<Animator>();
		}

		/** 초기화 */
		public override void Start()
		{
			base.Start();
			StartCoroutine(this.TryOpen());
		}

		/** 캐치 상태를 처리한다 */
		public bool TryCatch()
		{
			// 오픈 상태 일 경우
			if(m_bIsOpen)
			{
				m_bIsOpen = false;

				m_oAnimator.SetTrigger("Catch");
				m_oAnimator.ResetTrigger("Open");

				return true;
			}

			return false;
		}

		/** 애니메이션이 종료 상태를 처리한다 */
		private void HandleOnAniStateExit(
			CAniStateDispatcher a_oSender, Animator a_oAnimator, AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
		{

			StartCoroutine(this.TryOpen());
		}

		/** 등장 상태를 처리한다 */
		private IEnumerator TryOpen()
		{
			/*
			 * WaitForSeconds 계열 클래스를 활용하면 일정 시간 동안 코루틴의 흐름을 지연시키는 것이 가능하다.
			 * 
			 * WaitForSeconds vs WaitForSecondsRealtime
			 * - 두 클래스 모두 일정 시간 동안 코루틴의 흐름을 지연시키는 역할을 수행한다.
			 * 
			 * WaitForSeconds 클래스는 Time.timeScale 의 영향을 받는 Time.deltaTime 을 내부적으로 사용하기 때문에
			 * Time.timeScale 값이 0 일 경우 주의가 필요하다. (즉, Time.timeScale 값이 0 일 경우 WaitForSeconds 클래스에
			 * 의해서 코루틴이 계속 지연 될 수 있다는 것을 알 수 있다.)
			 * 
			 * 반면, WaitForSecondsRealtime 클래스는 Time.timeScale 의 영향을 받지 않는 Time.unscaledDeltaTime 을
			 * 내부적으로 사용하기 때문에 Time.timeScale 값이 0 이라고 하더라도 코루틴이 정상적으로 동작한다는 차이점이
			 * 존재한다.
			 */
			yield return new WaitForSeconds(Random.Range(1.0f, 8.0f));

			/*
			 * Random.Range 메서드를 사용해서 정수형 난수를 계산 할 경우 최대 값은 포함되지 않는 특징이 존재한다.
			 * (즉, 실수는 최대 값이 난수의 범위에 포함되지만 정수는 최대 값이 난수의 범위에 포함되지 않는다는 것을 알 수
			 * 있다.)
			 */
			this.SetTargetType((ETargetType)Random.Range(0, (int)ETargetType.MAX_VAL));

			m_bIsOpen = true;

			/*
			 * runtimeAnimatorController 프로퍼티를 활용하면 메카님 시스템에 의해서 제어 될 애니메이션 컨트롤러를 실행 중에
			 * 변경하는 것이 가능하다. (즉, 해당 프로퍼티를 활용하면 동일한 게임 객체를 대상으로 여러 애니메이션 컨트롤러를
			 * 상황에 따라 설정 할 수 있다.)
			 * 
			 * 단, 해당 프로퍼티를 이용해서 애니메이션 컨트롤러를 변경하는 것은 반드시 매개 변수의 값을 변경하기 이전에
			 * 해줘야한다. (즉, Set 계열 메서드를 통해서 특정 매개 변수의 값을 변경하는 것은 현재 설정 된 애니메이션 
			 * 컨트롤러를 대상으로 수행된다는 것을 알 수 있다.)
			 */
			m_oAnimator.runtimeAnimatorController =
				(this.TargetType <= ETargetType.A) ? m_oAniController01 : m_oAniController02;

			/*
			 * Set 계열 메서드를 이용하면 애니메이션 컨트롤러의 특정 매개 변수의 값을 변경하는 것이 가능하다. (즉, 해당 
			 * 메서드를 이용해서 매개 변수의 값을 설정함으로서 애니메이션을 전환하는 것이 가능하다.)
			 */
			m_oAnimator.SetTrigger("Open");
			m_oAnimator.ResetTrigger("Catch");

			/*
			 * StateMachineBehaviour 클래스를 활용하면 애니메이션 컨트롤러에서 각 애니메이션의 전환 여부를 검사하는 것이
			 * 가능하다. (즉, 해당 클래스를 상속하는 컴포넌트를 애니메이션 컨트롤러의 각 애니메이션에 설정함으로서 각
			 * 애니메이션의 전환 여부를 제어하는 것이 가능하다.)
			 */
			var oAniEventDispatchers = m_oAnimator.GetBehaviours<CAniStateDispatcher>();

			for(int i = 0; i < oAniEventDispatchers.Length; ++i)
			{
				oAniEventDispatchers[i].SetAniStateExitCallback(this.HandleOnAniStateExit);
			}
		}
		#endregion // 함수

		#region 접근 함수
		/** 타겟 타입을 변경한다 */
		public void SetTargetType(ETargetType a_eType)
		{
			this.TargetType = a_eType;
		}
		#endregion // 접근 함수
	}
}
