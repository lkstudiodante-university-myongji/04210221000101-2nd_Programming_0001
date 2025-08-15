using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 해상도 처리자 */
public partial class CResolutionHandler : CComponent
{
	#region 변수
	[SerializeField] private EProjection m_eProjection = EProjection._3D;
	#endregion // 변수

	#region 프로퍼티
	public float PlaneDistance
	{
		get
		{
			float fAngle = this.FOV / 2.0f;
			float fHeight = KDefine.G_DESIGN_SCREEN_HEIGHT / 2.0f;

			return fHeight / Mathf.Tan(fAngle);
		}
	}

	public float FOV => Mathf.PI / 4.0f;
	public float OrthographicSize => KDefine.G_DESIGN_SCREEN_HEIGHT / 2.0f;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.SetupResolution();
	}

	/** 상태를 리셋한다 */
	public override void Reset()
	{
		base.Reset();
		this.SetupResolution(true);
	}

	/** 해상도를 설정한다 */
	public void SetupResolution(bool a_bIsResetPos = false)
	{
		// 카메라가 없을 경우
		if(!this.TryGetComponent(out Camera oCamera))
		{
			return;
		}

		oCamera.fieldOfView = this.FOV * Mathf.Rad2Deg;
		oCamera.farClipPlane = KDefine.G_DISTANCE_CAMERA_FAR_PLANE;
		oCamera.nearClipPlane = KDefine.G_DISTANCE_CAMERA_NEAR_PLANE;

		oCamera.orthographic = m_eProjection == EProjection._2D;
		oCamera.orthographicSize = this.OrthographicSize;

		// 위치 리셋 모드 일 경우
		if(a_bIsResetPos)
		{
			oCamera.transform.position = new Vector3(0.0f, 0.0f, -this.PlaneDistance);
		}
	}
	#endregion // 함수
}
