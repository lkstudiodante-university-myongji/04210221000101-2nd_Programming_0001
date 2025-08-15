using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 기본
/** 마우스 버튼 */
public enum EMouseBtn
{
	NONE = -1,
	LEFT,
	RIGHT,
	MIDDLE,
	[HideInInspector] MAX_VAL
}

/** 투영 */
public enum EProjection
{
	NONE = -1,
	_2D,
	_3D,
	[HideInInspector] MAX_VAL
}
#endregion // 기본
