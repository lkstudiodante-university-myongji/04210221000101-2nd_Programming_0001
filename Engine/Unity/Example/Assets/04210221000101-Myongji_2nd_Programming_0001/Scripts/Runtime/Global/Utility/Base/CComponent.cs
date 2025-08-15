using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 컴포넌트 */
public abstract partial class CComponent : MonoBehaviour
{
	#region 프로퍼티
	public bool IsDirtySaveInfo { get; private set; } = true;
	public bool IsDirtyUpdateState { get; private set; } = true;

	public float SaveSkipTime { get; private set; } = 0.0f;
	public float UpdateSkipTime { get; private set; } = 0.0f;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Awake()
	{
		// Do Something
	}

	/** 초기화 */
	public virtual void Start()
	{
		// Do Something
	}

	/** 상태를 리셋한다 */
	public virtual void Reset()
	{
		// Do Something
	}

	/** 제거 되었을 경우 */
	public virtual void OnDestroy()
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void Update()
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void LateUpdate()
	{
		// 정보 저장이 필요 할 경우
		if(this.IsDirtySaveInfo)
		{
			this.SaveInfo();
			this.SetIsDirtySaveInfo(false);
		}

		/*
		 * 더티 플래그 패턴이란?
		 * - 특정 게임 객체의 상태를 갱신하기 위한 구조를 의미한다. (즉, 더티 플래그 패턴을 활용하면 특정 게임 객체의 
		 * 상태 갱신을 좀 더 효율적으로 처리하는 것이 가능하다.)
		 */
		// 상태 갱신이 필요 할 경우
		if(this.IsDirtyUpdateState)
		{
			this.UpdateState();
			this.SetIsDirtyUpdateState(false);
		}
	}

	/** 상태를 갱신한다 */
	public virtual void FixedUpdate()
	{
		// Do Something
	}

	/** 정보를 저장한다 */
	public virtual void SaveInfo()
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void UpdateState()
	{
		// Do Something
	}
	#endregion // 함수

	#region 접근 함수
	/** 정보 저장 필요 여부를 변경한다 */
	public void SetIsDirtySaveInfo(bool a_bIsDirty, bool a_bIsForce = false)
	{
		// 강제 모드 일 경우
		if(a_bIsForce)
		{
			this.IsDirtySaveInfo = a_bIsDirty;
		}
		else
		{
			this.IsDirtySaveInfo = this.IsDirtySaveInfo ? this.IsDirtySaveInfo : a_bIsDirty;
		}
	}

	/** 상태 갱신 필요 여부를 변경한다 */
	public void SetIsDirtyUpdateState(bool a_bIsDirty, bool a_bIsForce = false)
	{
		// 강제 모드 일 경우
		if(a_bIsForce)
		{
			this.IsDirtyUpdateState = a_bIsDirty;
		}
		else
		{
			this.IsDirtyUpdateState = this.IsDirtyUpdateState ? this.IsDirtyUpdateState : a_bIsDirty;
		}
	}

	/** 저장 누적 시간을 변경한다 */
	public void SetSaveSkipTime(float a_fSkipTime)
	{
		this.SaveSkipTime = Mathf.Max(0.0f, a_fSkipTime);
	}

	/** 갱신 누적 시간을 변경한다 */
	public void SetUpdateSkipTime(float a_fSkipTime)
	{
		this.UpdateSkipTime = Mathf.Max(0.0f, a_fSkipTime);
	}
	#endregion // 접근 함수
}
