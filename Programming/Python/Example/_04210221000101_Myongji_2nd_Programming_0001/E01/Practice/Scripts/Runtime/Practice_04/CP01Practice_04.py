import os
import sys
import random

"""
과제 4 - 1
- 야구 게임 제작하기
- 프로그램 시작 시 1 ~ 9 범위에 있는 숫자 중 4 개를 중복되지 않게 정답 선택
- 4 개의 숫자를 입력 받은 후 정답과 비교해서 결과 출력
- 입력 받은 숫자가 정답에 포함 되어있고 위치도 동일하면 스트라이크
- 숫자가 정답에 포함 되어있지만 위치가 동일하지 않다면 볼
- 4 스트라이크를 만들면 게임 종료
- 게임 종료 직후 몇번째 시도만에 성공했는지 결과 출력
"""


# Practice 4
class CP01Practice_04:
	# 초기화
	@classmethod
	def Start(cls, args):
		oAnswer = []
		cls.SetupAnswer(oAnswer)
		
		nTryTimes = 0
		print("정답 : {0}\n".format(oAnswer))
		
		while True:
			oListTokens = input("숫자 (4 개) 입력 : ").split()
			nTryTimes += 1
			
			nNumBalls = 0
			nNumStrikes = 0
			
			for i in range(0, len(oAnswer)):
				oDigit = str(oAnswer[i])
				
				nNumBalls += 1 if oDigit in oListTokens and i != oListTokens.index(oDigit) else 0
				nNumStrikes += 1 if oDigit in oListTokens and i == oListTokens.index(oDigit) else 0
			
			print("결과 : {0} Strike, {1} Ball\n".format(nNumStrikes, nNumBalls))
			
			# 게임이 종료 되었을 경우
			if nNumStrikes >= len(oAnswer):
				break
		
		print("{0} 번 시도 끝에 성공했습니다.".format(nTryTimes))
	
	# 정답을 설정한다
	@classmethod
	def SetupAnswer(cls, a_oAnswer: list):
		while len(a_oAnswer) < 4:
			nVal = random.randint(1, 9)
			
			# 중복 값이 없을 경우
			if nVal not in a_oAnswer:
				a_oAnswer.append(nVal)
