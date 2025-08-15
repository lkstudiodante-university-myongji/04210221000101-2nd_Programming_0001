using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 충돌 전달자 */
public partial class CCollisionDispatcher : CComponent
{
	#region 프로퍼티
	/*
	 * Func 및 Action 델리게이트는 메서드를 저장 및 제어 할 수 있는 기능을 지원한다. (즉, 델리게이트를 활용하면 특정
	 * 메서드를 저장 및 호출하는 것이 가능하다.)
	 * 
	 * 따라서, 델리게이트를 활용하면 메서드를 데이터처럼 취급하는 것이 가능하며 델리게이트를 통해 특정 메서드를 간접적으로
	 * 호출하는 것이 가능하다.
	 * 
	 * Func 는 델리게이트는 반환 값이 존재하는 메서드를 제어 할 수 있으며 Action 델리게이트는 반환 값이 존재하지 않는
	 * 메서드를 제어 할 수 있다.
	 */
	public System.Action<CCollisionDispatcher, Collision> EnterCallback { get; private set; } = null;
	public System.Action<CCollisionDispatcher, Collision> StayCallback { get; private set; } = null;
	public System.Action<CCollisionDispatcher, Collision> ExitCallback { get; private set; } = null;

	public System.Action<CCollisionDispatcher, Collision2D> Enter2DCallback { get; private set; } = null;
	public System.Action<CCollisionDispatcher, Collision2D> Stay2DCallback { get; private set; } = null;
	public System.Action<CCollisionDispatcher, Collision2D> Exit2DCallback { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 충돌이 시작 되었을 경우 */
	public void OnCollisionEnter(Collision a_oCollision)
	{
		this.EnterCallback?.Invoke(this, a_oCollision);
	}

	/** 충돌이 진행 중 일 경우 */
	public void OnCollisionStay(Collision a_oCollision)
	{
		this.StayCallback?.Invoke(this, a_oCollision);
	}

	/** 충돌이 종료 되었을 경우 */
	public void OnCollisionExit(Collision a_oCollision)
	{
		this.ExitCallback?.Invoke(this, a_oCollision);
	}

	/** 충돌이 시작 되었을 경우 */
	public void OnCollisionEnter2D(Collision2D a_oCollision)
	{
		this.Enter2DCallback?.Invoke(this, a_oCollision);
	}

	/** 충돌이 진행 중 일 경우 */
	public void OnCollisionStay2D(Collision2D a_oCollision)
	{
		this.Stay2DCallback?.Invoke(this, a_oCollision);
	}

	/** 충돌이 종료 되었을 경우 */
	public void OnCollisionExit2D(Collision2D a_oCollision)
	{
		this.Exit2DCallback?.Invoke(this, a_oCollision);
	}
	#endregion // 함수

	#region 접근 함수
	/** 시작 콜백을 변경한다 */
	public void SetEnterCallback(System.Action<CCollisionDispatcher, Collision> a_oCallback)
	{
		this.EnterCallback = a_oCallback;
	}

	/** 진행 콜백을 변경한다 */
	public void SetStayCallback(System.Action<CCollisionDispatcher, Collision> a_oCallback)
	{
		this.StayCallback = a_oCallback;
	}

	/** 종료 콜백을 변경한다 */
	public void SetExitCallback(System.Action<CCollisionDispatcher, Collision> a_oCallback)
	{
		this.ExitCallback = a_oCallback;
	}

	/** 시작 콜백을 변경한다 */
	public void SetEnter2DCallback(System.Action<CCollisionDispatcher, Collision2D> a_oCallback)
	{
		this.Enter2DCallback = a_oCallback;
	}

	/** 진행 콜백을 변경한다 */
	public void SetStay2DCallback(System.Action<CCollisionDispatcher, Collision2D> a_oCallback)
	{
		this.Stay2DCallback = a_oCallback;
	}

	/** 종료 콜백을 변경한다 */
	public void SetExit2DCallback(System.Action<CCollisionDispatcher, Collision2D> a_oCallback)
	{
		this.Exit2DCallback = a_oCallback;
	}
	#endregion // 접근 함수
}
