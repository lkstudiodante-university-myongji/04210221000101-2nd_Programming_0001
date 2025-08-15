using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 애니메이션 상태 전달자 */
public partial class CAniStateDispatcher : StateMachineBehaviour
{
	#region 프로퍼티
	public System.Action<CAniStateDispatcher, Animator, AnimatorStateInfo, int> AniStateEnterCallback { get; private set; } = null;
	public System.Action<CAniStateDispatcher, Animator, AnimatorStateInfo, int> AniStateExitCallback { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 애니메이션이 시작 되었을 경우 */
	public override void OnStateEnter(Animator a_oSender, AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{
		base.OnStateEnter(a_oSender, a_stStateInfo, a_nLayerIdx);
		this.AniStateEnterCallback?.Invoke(this, a_oSender, a_stStateInfo, a_nLayerIdx);
	}

	/** 애니메이션이 종료 되었을 경우 */
	public override void OnStateExit(Animator a_oSender, AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{
		base.OnStateExit(a_oSender, a_stStateInfo, a_nLayerIdx);
		this.AniStateExitCallback?.Invoke(this, a_oSender, a_stStateInfo, a_nLayerIdx);
	}
	#endregion // 함수

	#region 접근 함수
	/** 애니메이션 상태 시작 콜백을 변경한다 */
	public void SetAniStateEnterCallback(
		System.Action<CAniStateDispatcher, Animator, AnimatorStateInfo, int> a_oCallback)
	{

		this.AniStateEnterCallback = a_oCallback;
	}

	/** 애니메이션 상태 종료 콜백을 변경한다 */
	public void SetAniStateExitCallback(
		System.Action<CAniStateDispatcher, Animator, AnimatorStateInfo, int> a_oCallback)
	{

		this.AniStateExitCallback = a_oCallback;
	}
	#endregion // 접근 함수
}
