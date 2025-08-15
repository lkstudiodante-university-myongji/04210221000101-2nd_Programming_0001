using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 이벤트 전달자 */
public partial class CEventDispatcher : CComponent
{
	#region 프로퍼티
	public System.Action<CEventDispatcher, string> AniEventCallback { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 애니메이션 이벤트를 수신했을 경우 */
	public void OnReceiveAniEvent(string a_oParams)
	{
		this.AniEventCallback?.Invoke(this, a_oParams);
	}
	#endregion // 함수

	#region 접근 함수
	/** 애니메이션 이벤트 콜백을 변경한다 */
	public void SetAniEventCallback(System.Action<CEventDispatcher, string> a_oCallback)
	{
		this.AniEventCallback = a_oCallback;
	}
	#endregion // 접근 함수
}
