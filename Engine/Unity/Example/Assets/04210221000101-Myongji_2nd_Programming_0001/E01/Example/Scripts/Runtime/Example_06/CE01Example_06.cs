//#define E06_LIST
//#define E06_DICTIONARY
#define E06_STACK_QUEUE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 컬렉션이란?
 * - 여러 데이터를 저장 및 관리 할 수 있는 기능을 의미한다. (즉, 컬렉션을 활용하면 많은 데이터를 좀 더 효율적으로 제어하는
 * 것이 가능하다는 것을 알 수 있다.)
 * 
 * C# 주요 컬렉션 종류
 * - 리스트
 * - 딕셔너리
 * - 스택/큐
 * 
 * 스택이란?
 * - LIFO (Last In First Out) 구조를 지니는 컬렉션을 의미한다. (즉, 스택은 데이터의 입/출력 순서가 정해져 있기 때문에
 * 특정 위치에 존재하는 데이터에 자유롭게 접근하는 것이 불가능하다.)
 * 
 * 큐란?
 * - FIFO (First In First Out) 구조를 지니는 컬렉션을 의미한다. (즉, 큐는 스택과 마찬가지로 데이터의 입/출력 순서가 
 * 엄격하게 통제되기 때문에 특정 위치에 존재하는 데이터에 자유롭게 접근하는 것이 불가능하다.)
 */
namespace Example
{
	/** Example 6 */
	public partial class CE01Example_06 : CSceneManager
	{
		#region 프로퍼티
		public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_06;
		#endregion // 프로퍼티

		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();

#if E06_LIST
			var oListValues01 = new List<int>();

			var oListValues02 = new List<int>() {
				1, 2, 3, 4, 5
			};

			for(int i = 0; i < 10; ++i) {
				oListValues01.Add(i + 1);
			}

			Debug.Log("=====> 리스트 - 1 <=====");

			for(int i = 0; i < oListValues01.Count; ++i) {
				Debug.Log($"{oListValues01[i]}");
			}

			Debug.Log("=====> 리스트 - 2 <=====");

			for(int i = 0; i < oListValues02.Count; ++i) {
				Debug.Log($"{oListValues02[i]}");
			}
#elif E06_DICTIONARY
			var oDictValues01 = new Dictionary<string, int>();

			var oDictValues02 = new Dictionary<string, int>() {
				["Key_01"] = 1, ["Key_02"] = 2, ["Key_03"] = 3, ["Key_04"] = 4, ["Key_05"] = 5
			};

			for(int i = 0; i < 10; ++i) {
				string oKey = $"Key_{i + 1:00}";
				oDictValues01.Add(oKey, i + 1);
			}

			Debug.Log("=====> 딕셔너리 - 1 <=====");

			/*
			 * foreach 반복문이란?
			 * - 배열과 같은 컬렉션을 대상으로 사용 가능한 반복문을 의미한다. (즉, foreach 반복문을 활용하면 특정 컬렉션에
			 * 존재하는 모든 데이터에 접근하는 것이 가능하다.)
			 */
			foreach(var stKeyVal in oDictValues01) {
				Debug.Log($"[{stKeyVal.Key}]:{stKeyVal.Value}");
			}

			Debug.Log("=====> 딕셔너리 - 2 <=====");

			foreach(var stKeyVal in oDictValues02) {
				Debug.Log($"[{stKeyVal.Key}]:{stKeyVal.Value}");
			}
#elif E06_STACK_QUEUE
			var oValStack = new Stack<int>();
			var oValQueue = new Queue<int>();

			for(int i = 0; i < 10; ++i)
			{
				oValStack.Push(i + 1);
				oValQueue.Enqueue(i + 1);
			}

			Debug.Log("=====> 스택 <=====");

			/*
			 * 스택/큐 에 존재하는 데이터를 가져오기 위해서는 Pop/Dequeue 메서드를 활용하면 된다.
			 * 
			 * 단, 해당 메서드는 다른 컬렉션에 존재하는 메서드와 달리 컬렉션에 존재하는 데이터를 반환 후 해당 데이터를
			 * 컬렉션에서 제거하는 차이점이 존재한다. (즉, 메서드를 호출하고 나면 컬렉션에 존재하는 데이터가 제거 되기 때문에
			 * 컬렉션의 데이터 개수가 변경 된다는 것을 알 수 있다.)
			 */
			while(oValStack.Count >= 1)
			{
				Debug.Log($"{oValStack.Pop()}");
			}

			Debug.Log("=====> 큐 <=====");

			while(oValQueue.Count >= 1)
			{
				Debug.Log($"{oValQueue.Dequeue()}");
			}
#endif // #if E06_LIST
		}
		#endregion // 함수
	}
}
