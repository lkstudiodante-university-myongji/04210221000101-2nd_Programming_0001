using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
	/** 상호 작용 컴포넌트 */
	public abstract class CE01Interactable_23 : CComponent
	{
		#region 함수
		/** 타격 되었을 경우 */
		public abstract void OnHit();
		#endregion // 함수
	}
}
