using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
	/** Example 12 */
	public partial class CE01Example_12 : CSceneManager
	{
		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_12;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
		}

		/** 플레이 버튼을 눌렀을 경우 */
		public void OnTouchPlayBtn()
		{
			CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_13);
		}
		#endregion // 함수
	}
}
