using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Unity 사운드 관련 컴포넌트
 * - Audio Source
 * - Audio Listener
 * 
 * Audio Source 컴포넌트란?
 * - 사운드를 발생시키는 역할을 수행하는 컴포넌트이다. (즉, 해당 컴포넌트를 활용하면 간단하게 사운드를 재생하는 것이 
 * 가능하다.)
 * 
 * Audio Source 컴포넌트는 사운드를 재생하기 위한 2 가지 방법을 제공하며 해당 방식 중 현재 상황에 맞는 방법을 선택해서
 * 사용하면 된다. (즉, PlayClipAtPoint 메서드를 사용하거나 Audio Source 컴포넌트를 직접적으로 사용하면 사운드를 재생하는
 * 것이 가능하다.)
 * 
 * 단, Audio Source 컴포넌트를 직접적으로 사용하는 방법은 한번에 하나의 사운드만 재생 가능하기 때문에 2 개 이상의 사운드를
 * 재생하고 싶다면 재생하고 싶은 사운드 개수만큼 Audio Source 컴포넌트를 생성해야한다.
 * 
 * Audio Listener 컴포넌트란?
 * - 사운드를 듣는 역할을 수행하는 컴포넌트이다. (즉, 해당 컴포넌트는 Unity 씬 상에 존재하는 귀에 해당한다는 것을 알 수
 * 있다.)
 * 
 * Audio Listener 컴포넌트는 Audio Source 컴포넌트와 달리 현재 로드가 된 Unity 씬 중에서 1 개만 존재해야한다. (즉, 중복을
 * 허용하지 않는다는 것을 알 수 있다.)
 * 
 * PlayClipAtPoint 메서드 vs Audio Source 컴포넌트
 * - PlayClipAtPoint 메서드를 활용하면 간단하게 3 차원 사운드를 재생하는 것이 가능하다.
 * 
 * 단, 해당 방식을 통한 사운드 재생은 임시 객체를 생성하고 제거하기 때문에 사운드를 빈번하게 재생해야 될 경우 해당 방식은
 * 적합하지 않다는 단점이 존재한다.
 * 
 * 반면, Audio Source 컴포넌트를 직접적으로 사용하는 방법은 PlayClipAtPoint 메서드 방식에 비해서 좀 더 복잡하지만 사운드를
 * 재생하는 과정에서 임시 객체가 생성되지 않기 때문에 사운드를 빈번하게 재생해야 될 경우 적접하다는 것을 알 수 있다.
 */
namespace Example
{
	/** Example 25 */
	public class CE01Example_25 : CSceneManager
	{
		#region 변수
		[SerializeField] private AudioClip m_oFXAudioClip = null;
		[SerializeField] private AudioSource m_oBGAudioSource = null;
		[SerializeField] private List<AudioSource> m_oFXAudioSourceList = new List<AudioSource>();
		#endregion // 변수

		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_25;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
		}

		/** 상태를 갱신한다 */
		public override void Update()
		{
			base.Update();

			// 스페이스 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Space))
			{
				m_oBGAudioSource.Stop();

				for(int i = 0; i < m_oFXAudioSourceList.Count; ++i)
				{
					m_oFXAudioSourceList[i].Stop();
				}
			}
		}

		/** 배경음 버튼을 눌렀을 경우 */
		public void OnTouchBGSndBtn()
		{
			m_oBGAudioSource.Play();
		}

		/** 효과음 버튼을 눌렀을 경우 */
		public void OnTouchFXSndsBtn()
		{
			AudioSource.PlayClipAtPoint(m_oFXAudioClip, Camera.main.transform.position);
		}

		/** 효과음 풀링 버튼을 눌렀을 경우 */
		public void OnTouchFXSndsPoolingBtn()
		{
			for(int i = 0; i < m_oFXAudioSourceList.Count; ++i)
			{
				// 플레이 중 일 경우
				if(m_oFXAudioSourceList[i].isPlaying)
				{
					continue;
				}

				m_oFXAudioSourceList[i].clip = m_oFXAudioClip;
				m_oFXAudioSourceList[i].Stop();

				m_oFXAudioSourceList[i].Play();
				return;
			}
		}
		#endregion // 함수
	}
}
