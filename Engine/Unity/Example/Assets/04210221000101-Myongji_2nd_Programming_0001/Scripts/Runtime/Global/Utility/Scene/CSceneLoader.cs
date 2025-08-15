using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 씬 로더 */
public partial class CSceneLoader : CSingleton<CSceneLoader>
{
	#region 함수
	/** 씬을 로드한다 */
	public void LoadScene(string a_oName, bool a_bIsSingle = true)
	{
		/*
		 * SceneManager 클래스란?
		 * - 씬을 제어 할 수 있는 여러 기능을 지원하는 클래스를 의미한다. (즉, 해당 클래스를 활용하면 프로그램이 실행
		 * 중에 다른 씬으로 전환하는 것이 가능하다.)
		 * 
		 * Unity 씬 로드 방식 종류
		 * - Single
		 * - Additive
		 * 
		 * Single vs Additive
		 * - Single 로드 방식은 기존에 로드 되어있는 모든 씬을 제거하고 새로운 씬을 로드하는 특징이 존재한다. (즉, 해당
		 * 방식으로 씬을 로드하면 기존에 존재하는 모든 씬이 제거된다는 것을 알 수 있다.)
		 * 
		 * 반면, Additive 로드 방식은 기존에 존재하는 씬은 그대로 두고 새로운 씬을 중첩으로 로드하는 차이점이 존재한다.
		 * (즉, Additive 로드 방식은 여러 씬을 로드 할 때 사용하는 방식이라는 것을 알 수 있다.)
		 */
		SceneManager.LoadScene(a_oName, a_bIsSingle ? LoadSceneMode.Single : LoadSceneMode.Additive);
	}

	/** 씬을 비동기 로드한다 */
	public void LoadSceneAsync(string a_oName, System.Action<CSceneLoader, AsyncOperation, bool> a_oCallback, bool a_bIsSingle = true)
	{
		StartCoroutine(this.CoLoadSceneAsync(a_oName, a_oCallback, a_bIsSingle));
	}
	#endregion // 함수
}

/** 씬 로더 - 코루틴 */
public partial class CSceneLoader : CSingleton<CSceneLoader>
{
	#region 함수
	/** 씬을 비동기 로드한다 */
	private IEnumerator CoLoadSceneAsync(string a_oName, System.Action<CSceneLoader, AsyncOperation, bool> a_oCallback, bool a_bIsSingle)
	{
		var oAsyncOperation = SceneManager.LoadSceneAsync(a_oName, a_bIsSingle ? LoadSceneMode.Single : LoadSceneMode.Additive);

		do
		{
			yield return null;
			a_oCallback?.Invoke(this, oAsyncOperation, false);
		} while(!oAsyncOperation.isDone);

		a_oCallback?.Invoke(this, oAsyncOperation, true);
	}
	#endregion // 함수
}
