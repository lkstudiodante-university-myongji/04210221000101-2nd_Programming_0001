using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
	/** Example 17 */
	public partial class CE01Example_17 : CSceneManager
	{
		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_17;
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
			CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_18);
		}
		#endregion // 함수
	}
}
