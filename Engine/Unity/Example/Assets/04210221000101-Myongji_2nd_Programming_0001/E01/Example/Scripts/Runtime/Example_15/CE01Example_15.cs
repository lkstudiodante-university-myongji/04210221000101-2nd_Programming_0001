//#define E15_SPRITE
#define E15_ANIMATION

#if E15_ANIMATION
//#define E15_ANIMATION_TWEEN
#define E15_ANIMATION_KEYFRAME
#endif // #if E15_ANIMATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * 스프라이트란?
 * - 3 차원 공간 상에 출력되는 2 차원 이미지를 의미한다. (즉, 스프라이트를 활용하면 색상 정보가 담겨 있는 텍스처를 손쉽게
 * 화면 상에 출력하는 것이 가능하다.)
 * 
 * 따라서, 스프라이트는 2 차원 게임을 제작 할 때 주로 활용된다는 것을 알 수 있다.
 * 
 * 텍스처란?
 * - GPU 사용하는 데이터를 저장하기 위한 버퍼를 의미한다. (즉, 해당 버퍼에 어떤 데이터를 저장하는지에 따라 텍스처의 용도가
 * 달라진다는 것을 알 수 있다.)
 * 
 * 대표적인 텍스처로는 색상 정보를 저장하고 있는 이미지가 있다. (즉, 이미지란 색상 정보가 저장 된 텍스처를 의미하며 해당 
 * 텍스처는 디퓨즈 맵이라고 불린다.)
 * 
 * 애니메이션이란?
 * - 시간의 흐름에 따라 물체의 상태를 변경함으로서 생동감 있는 물체를 표현하는 방법을 의미한다. (즉, 애니메이션을 활용하면
 * 장면 연출 등 여러가지 다이나믹한 연출을 만들어내는 것이 가능하다.)
 * 
 * Unity 애니메이션 종류
 * - 트윈
 * - 키 프레임 (레거시)
 * - 메카님
 * 
 * 트윈 애니메이션이란?
 * - 데이터의 보간을 통해 애니메이션을 연출하는 방법을 의미한다. (즉, 트윈 애니메이션은 단순한 애니메이션을 연출하는데는
 * 효과적이지만 복잡한 애니메이션에는 적합하지 않는다는 것을 알 수 있다.)
 * 
 * 단, Unity 는 트윈 애니메이션을 지원하지 않기 때문에 Unity 에서 트윈 애니메이션을 연출하기 위해서는 별도의 에셋을 사용 할
 * 필요가 있다.
 * 
 * Unity 대표 트윈 애니메이션 에셋
 * - iTween
 * - DOTween
 * 
 * 해당 에셋은 모두 에셋 스토어를 통해서 사용가능하며 무료이기 때문에 현업에서도 자주 활용된다.
 * 
 * 키 프레임 애니메이션이란?
 * - 물체의 상태 정보를 지니고 있는 키 프레임을 시간의 흐름에 따라 보간함으로서 애니메이션을 연출하는 방법을 의미한다. 
 * (즉, 키 프레임 애니메이션을 활용하면 복잡한 애니메이션도 연출이 가능하다는 것을 알 수 있다.)
 * 
 * 단, 키 프레임 애니메이션 방식은 미리 저장 된 데이터를 기반으로 애니메이션 연출을 하는 방식이기 때문에 실행 중에 동적으로
 * 변경되는 애니메이션을 연출하는 것이 불가능하다. (즉, Unity 는 동적으로 키 프레임 애니메이션에서 활용하는 키 프레임을
 * 생성 할 수 있지만 해당 방법이 단순하지 않기 때문에 동적인 애니메이션을 연출하는데는 사용하지 않는다는 것을 알 수 있다.)
 * 
 * 메카님 애니메이션이란?
 * - FSM (Finite State Machine) 을 활용해서 애니메이션을 연출하는 방법을 의미한다. (즉, 메카님 애니메이션 자체는
 * 애니메이션을 전환하고 제어하는 시스템이라는 것을 알 수 있다.)
 * 
 * 메카님 애니메이션은 애니메이션을 제어하는 시스템이기 때문에 단독으로 사용하는 것은 불가능하며 제작 된 애니메이션을 기반으로
 * 조건을 부여함으로서 특정 상황에 적절한 애니메이션이 연출되도록 제어하는 기능을 제공한다.
 */
namespace Example
{
	/** Example 15 */
	public partial class CE01Example_15 : CSceneManager
	{
		#region 변수
		[SerializeField] private SpriteRenderer m_oSprite = null;
		[SerializeField] private SpriteRenderer m_oAniKeyframeSprite = null;

		[Header("=====> Game Objects <=====")]
		[SerializeField] private GameObject m_oSpriteRoot = null;
		[SerializeField] private GameObject m_oAniTweenRoot = null;
		[SerializeField] private GameObject m_oAniKeyframeRoot = null;

		[SerializeField] private GameObject m_oAniTweenTarget01 = null;
		[SerializeField] private GameObject m_oAniTweenTarget02 = null;

#if E15_ANIMATION
		private Vector3 m_stAniTweenTarget01Pos = Vector3.zero;
		private Vector3 m_stAniTweenTarget02Pos = Vector3.zero;

		private Tween m_oTweenAni01 = null;
		private Tween m_oTweenAni02 = null;
#endif // #if E15_ANIMATION
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_15;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

			m_oSpriteRoot.SetActive(false);
			m_oAniTweenRoot.SetActive(false);
			m_oAniKeyframeRoot.SetActive(false);

#if E15_SPRITE
			m_oSpriteRoot.SetActive(true);
#elif E15_ANIMATION_TWEEN
			m_oAniTweenRoot.SetActive(true);
#elif E15_ANIMATION_KEYFRAME
			m_oAniKeyframeRoot.SetActive(true);
#endif // #if E15_ANIMATION_KEYFRAME

#if E15_ANIMATION
			m_stAniTweenTarget01Pos = m_oAniTweenTarget01.transform.localPosition;
			m_stAniTweenTarget02Pos = m_oAniTweenTarget02.transform.localPosition;

			var oEventDispatcher = m_oAniKeyframeSprite.GetComponent<CEventDispatcher>();
			oEventDispatcher.SetAniEventCallback(this.HandleOnAniEvent);
#endif // #if E15_ANIMATION
		}

		/** 제거 되었을 경우 */
		public override void OnDestroy()
		{
			base.OnDestroy();

#if E15_ANIMATION
			/*
			 * Kill 메서드는 특정 트윈 애니메이션을 취소하는 역할을 수행한다. (즉, 이미 제거 된 물체를 대상으로 트윈
			 * 애니메이션을 적용하면 내부적으로 예외가 발생하기 때문에 특정 물체를 제거하기 전에 반드시 트윈 애니메이션을
			 * 취소해야한다는 것을 알 수 있다.)
			 */
			m_oTweenAni01?.Kill();
			m_oTweenAni02?.Kill();
#endif // #if E15_ANIMATION
		}

		/** 상태를 갱신한다 */
		public override void Update()
		{
			base.Update();

#if E15_SPRITE
			// 스페이스 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Space)) {
				m_oSprite.maskInteraction = (m_oSprite.maskInteraction == SpriteMaskInteraction.VisibleInsideMask) ?
					SpriteMaskInteraction.VisibleOutsideMask : SpriteMaskInteraction.VisibleInsideMask;
			}
#elif E15_ANIMATION_TWEEN
			// 스페이스 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Space)) {
				m_oTweenAni01?.Kill();
				m_oTweenAni02?.Kill();

				m_oAniTweenTarget01.transform.localPosition = m_stAniTweenTarget01Pos;
				m_oAniTweenTarget01.transform.localEulerAngles = Vector3.zero;

				m_oAniTweenTarget02.transform.localPosition = m_stAniTweenTarget02Pos;
				m_oAniTweenTarget02.transform.localEulerAngles = Vector3.zero;

				/*
				 * Sequence 는 여러 트윈 애니메이션을 동시에 실행하는 역할을 수행한다. (즉, Sequence 를 활용하면 여러
				 * 애니메이션을 적용함으로서 다양한 결과물을 만들어내는 것이 가능하다.)
				 * 
				 * 또한, Sequence 는 여러 트윈 애니메이션을 차례대로 실행하는 방법을 제공한다. (즉, Join 메서드는 여러
				 * 애니메이션을 동시에 실행하고 Append 메서드는 여러 애니메이션을 차례대로 실행한다는 것을 알 수 있다.)
				 */
				var oSequence01 = DOTween.Sequence().SetAutoKill();
				oSequence01.Join(m_oAniTweenTarget01.transform.DORotate(Vector3.up * 360.0f, 2.0f, RotateMode.LocalAxisAdd));
				oSequence01.Join(m_oAniTweenTarget01.transform.DOLocalMoveX(m_stAniTweenTarget01Pos.x + 300.0f, 2.0f));

				var oSequence02 = DOTween.Sequence().SetAutoKill();
				oSequence02.Append(m_oAniTweenTarget02.transform.DORotate(Vector3.up * 360.0f, 2.0f, RotateMode.LocalAxisAdd));
				oSequence02.Append(m_oAniTweenTarget02.transform.DOLocalMoveX(m_stAniTweenTarget02Pos.x + 300.0f, 2.0f));

				m_oTweenAni01 = oSequence01;
				m_oTweenAni02 = oSequence02;
			}
#endif // #if E15_SPRITE
		}

		/** 애니메이션 이벤트를 처리한다 */
		private void HandleOnAniEvent(CEventDispatcher a_oSender, string a_oParams)
		{
			Debug.Log($"HandleOnAniEvent: {a_oParams}");
		}
		#endregion // 함수
	}
}
