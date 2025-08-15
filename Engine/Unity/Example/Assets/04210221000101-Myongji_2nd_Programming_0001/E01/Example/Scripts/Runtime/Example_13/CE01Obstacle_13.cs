using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
	/** 장애물 */
	public partial class CE01Obstacle_13 : CComponent
	{
		#region 변수
		[SerializeField] private GameObject m_oSafeArea = null;
		[SerializeField] private GameObject m_oUpObstacle = null;
		[SerializeField] private GameObject m_oDownObstacle = null;
		#endregion // 변수

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

			float fSafeAreaPercent = 0.35f;
			float fObstaclePercent = 1.0f - fSafeAreaPercent;

			float fUpObstaclePercent = Random.Range(0.1f, 0.9f);
			float fDownObstaclePercent = 1.0f - fUpObstaclePercent;

			var stSafeAreaScale = m_oSafeArea.transform.localScale;
			stSafeAreaScale.y = KDefine.G_DESIGN_SCREEN_HEIGHT * fSafeAreaPercent;

			var stUpObstacleScale = m_oUpObstacle.transform.localScale;
			stUpObstacleScale.y = KDefine.G_DESIGN_SCREEN_HEIGHT * (fObstaclePercent * fUpObstaclePercent);

			var stDownObstacleScale = m_oDownObstacle.transform.localScale;
			stDownObstacleScale.y = KDefine.G_DESIGN_SCREEN_HEIGHT * (fObstaclePercent * fDownObstaclePercent);

			var stUpObstaclePos = m_oUpObstacle.transform.localPosition;
			stUpObstaclePos.y = (KDefine.G_DESIGN_SCREEN_HEIGHT / 2.0f) - (stUpObstacleScale.y / 2.0f);

			var stDownObstaclePos = m_oDownObstacle.transform.localPosition;
			stDownObstaclePos.y = (KDefine.G_DESIGN_SCREEN_HEIGHT / -2.0f) + (stDownObstacleScale.y / 2.0f);

			m_oSafeArea.transform.localScale = stSafeAreaScale;

			m_oUpObstacle.transform.localScale = stUpObstacleScale;
			m_oUpObstacle.transform.localPosition = stUpObstaclePos;

			m_oDownObstacle.transform.localScale = stDownObstacleScale;
			m_oDownObstacle.transform.localPosition = stDownObstaclePos;
		}
		#endregion // 함수
	}
}
