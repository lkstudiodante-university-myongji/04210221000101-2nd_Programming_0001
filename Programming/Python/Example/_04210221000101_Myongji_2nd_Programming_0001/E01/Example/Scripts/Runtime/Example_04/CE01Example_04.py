import os
import sys
import random


# Example 4
class CE01Example_04:
	# 초기화
	@classmethod
	def Start(cls, args):
		# cls.E04_01(args)
		# cls.E04_02(args)
		# cls.E04_03(args)
		cls.E04_04(args)
	
	# 4 - 1
	@classmethod
	def E04_01(cls, args):
		oListTokens = input("정수 (2 개) 입력 : ").split()
		
		nLhs = int(oListTokens[0])
		nRhs = int(oListTokens[1])
		
		"""
		비트 연산자란?
		- 컴퓨터의 기본 단위는 비트이지만 프로그래밍 언어의 기본 단위는 바이트이다.

		따라서, 프로그래밍 언어에서 모든 데이터는 바이트 단위로 처리되기 때문에 만약 비트 단위로 데이터를 처리 할 필요가 있을 경우 사용하는
		것이 바로 비트 연산자이다.

		또한, 비트 연산자는 컴퓨터가 가장 빠르게 처리 할 수 있는 연산자이기 때문에 성능이 민감한 프로그램을 제작해야 될 경우 해당 연산자를
		적극적으로 활용해야한다.
		"""
		print("=====> 비트 연산자 <=====")
		print(f"{nLhs:08b} & {nRhs:08b} = {nLhs & nRhs:08b}")
		print(f"{nLhs:08b} | {nRhs:08b} = {nLhs | nRhs:08b}")
		print(f"{nLhs:08b} ^ {nRhs:08b} = {nLhs ^ nRhs:08b}")
		print(f"~{nLhs:08b} = {~nLhs:08b}")
		print(f"{nLhs:08b} << 1 = {nLhs << 1:08b}")
		print(f"{nRhs:08b} >> 1 = {nRhs >> 1:08b}")
	
	# 4 - 2
	@classmethod
	def E04_02(cls, args):
		nAnswer = random.randint(1, 100)
		print("정답 : {0}\n".format(nAnswer))
		
		while True:
			nNum = int(input("숫자 입력 : "))
			
			# 정답 일 경우
			if nNum == nAnswer:
				print("정답입니다!")
				break
			else:
				"""
				if ~ else 축약 (조건 연산자)
				- Python 은 조건 연산자를 제공하지 않지만 if ~ else 축약 기능을 활용하면 조건 연산자와 비슷한 구문을 작성하는 것이 가능하다.
				if 조건문을 기준으로 참 일 경우 왼쪽에 존재하는 데이터를 가져오고 거짓 일 경우 else 오른쪽에 존재하는 데이터를 가져온다.

				Ex)
				nLhs = 10
				nRhs = 20

				nLhs if nLhs < nRhs else nRhs		<= 결과 : 10
				"""
				oStr = "큽니다." if nNum < nAnswer else "작습니다."
				print("정답은 {0} 보다 {1}".format(nNum, oStr))
			
			print("")
	
	# 4 - 3
	@classmethod
	def E04_03(cls, args):
		nWinCount = 0
		nDrawCount = 0
		
		while True:
			"""
			random 모듈이란?
			- Python 에서 제공하는 난수를 생성 할 수 있는 모듈을 의미한다. (즉, 해당 모듈을 활용하면 특정 범위에 있는 숫자 중 랜덤하게
			숫자를 가져오는 것이 가능하다는 것을 알 수 있다.)
			"""
			nSel = int(input("바위(1), 가위(2), 보(3) 선택 : "))
			nSelComputer = random.randint(1, 3)
			
			"""
			format 메서드란?
			- 문자열 자료형 데이터에 존재하는 메서드로 해당 메서드를 활용하면 서식화 된 문자열을 생성하는 것이 가능하다. (즉, f 문자열 포맷팅과
			동일한 역할을 수행한다.)
			"""
			print("컴퓨터 선택 결과 : {0}\n".format(nSelComputer))
			
			oResultTable = [
				[0, 1, -1],
				[-1, 0, 1],
				[1, -1, 0]
			]
			
			"""
			oResultTable 는 리스트를 요소로 하는 리스트이기 때문에 리스트의 특정 데이터에 접근하기 위해서는 [ ] (인덱스 연산자) 를 2 번 
			명시해야 한다는 것을 알 수 있다.

			바위 가위 보 게임은 선택에 따른 결과가 이미 정해져있기 때문에 선택에 따른 결과를 미리 테이블 형태로 제작하는 것이 가능하며 이를
			결정 테이블이라고 한다.
			"""
			# 졌을 경우
			if oResultTable[nSel - 1][nSelComputer - 1] < 0:
				print("졌습니다!")
				break
			else:
				nResult = oResultTable[nSel - 1][nSelComputer - 1]
				oResultStr = "이겼습니다!" if nResult > 0 else "비겼습니다!"
				
				print(oResultStr)
				
				nWinCount += 1 if nResult > 0 else 0
				nDrawCount += 1 if nResult == 0 else 0
			
			print("")
		
		print("결과 : {0} 승 {1} 무".format(nWinCount, nDrawCount))
	
	# 4 - 4
	@classmethod
	def E04_04(cls, args):
		oWordList = [
			"Apple", "Google", "Samsung", "Microsoft"
		]
		
		"""
		Python 컬렉션 데이터에 * (곱셈 연산자) 를 사용하는 것이 가능하다. (즉, 컬렉션 데이터에 해당 연산자를 적용하면 기존 컬렉션이 지니고
		있는 요소를 연산한 횟수만큼 반복적으로 요소를 지니는 컬렉션이 생성된다.)

		Ex)
		[0, 1] * 3		<= 결과 : [0, 1, 0, 1, 0, 1]
		"""
		oAnswer = oWordList[random.randint(0, len(oWordList) - 1)]
		oInputLetters = ["_"] * len(oAnswer)
		
		print("정답 : {0}\n".format(oAnswer))
		
		while True:
			for i in range(0, len(oInputLetters)):
				print("{0} ".format(oInputLetters[i]), end="")
			
			oLetter = input("\n문자 입력 : ")
			
			for i in range(0, len(oAnswer)):
				# 문자가 존재 할 경우
				if str(oAnswer[i]) == oLetter:
					oInputLetters[i] = str(oAnswer[i])
			
			"""
			in 키워드를 활용하면 데이터가 특정 컬렉션에 포함 되어있는지 검사하는 것이 가능하다. (즉, 일반적인 고수준 언어는 특정 컬렉션에
			존재하는 데이터를 검사하기 위해서 반복문 등을 활용하는데 Python 은 in 키워드가 존재하기 때문에 단순한 데이터의 포함 여부를 검사
			하기 위해서 반복문을 작성하지 않아도 된다.)

			not in 키워드 또한 마찬가지로 포함 되어있지 않는 경우를 검사하는데 활용된다.
			"""
			# 단어를 모두 완성했을 경우
			if "_" not in oInputLetters:
				break
			
			print("")
