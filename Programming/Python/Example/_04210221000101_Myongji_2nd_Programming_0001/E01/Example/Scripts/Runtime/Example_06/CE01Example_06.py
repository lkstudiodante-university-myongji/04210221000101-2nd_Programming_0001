import os
import sys
import time
import random


# Example 6
class CE01Example_06:
	"""
	클래스 내부에 변수를 선언 할 경우 해당 변수는 클래스에 종속되는 클래스 변수가 된다.
	따라서, 특정 클래스로부터 생성 된 모든 객체가 공유 할 수 있는 변수가 필요 할 경우 전역 변수보다는 클래스 변수를 활용하는 것을 추천한다.
	"""
	MENU_NONE = -1
	MENU_ADD_CIRCLE = 0
	MENU_ADD_TRIANGLE = 1
	MENU_ADD_RECTANGLE = 2
	MENU_DRAW_ALL_SHAPES = 3
	MENU_EXIT = 4
	
	# 초기화
	@classmethod
	def Start(cls, args):
		# cls.Example_06_01(args)
		cls.Example_06_02(args)
	
	# Example 6 - 1
	@classmethod
	def E01Example_06_01(cls, args):
		oListTokens01 = input("플레이어 스탯 입력 (체력, 공격력, 방어력) : ").split()
		oListTokens02 = input("적 스탯 입력 (체력, 공격력, 방어력) : ").split()
		
		oPlayer = CE06Unit("플레이어", int(oListTokens01[0]), int(oListTokens01[1]), int(oListTokens01[2]))
		oEnemy = CE06Unit("적", int(oListTokens02[0]), int(oListTokens02[1]), int(oListTokens02[2]))
		
		while True:
			print("\n=====> 플레이어 공격 <=====")
			oPlayer.Attack(oEnemy)
			
			# 적 체력이 없을 경우
			if oEnemy.m_nHP <= 0:
				break
			
			print("\n=====> 적 공격 <=====")
			oEnemy.Attack(oPlayer)
			
			# 플레이어 체력이 없을 경우
			if oPlayer.m_nHP <= 0:
				break
			
			time.sleep(1)
		
		print("\n{0} 이(가) 이겼습니다.".format(oPlayer.m_oName if oPlayer.m_nHP > 0 else oEnemy.m_oName))
	
	# Example 6 - 2
	@classmethod
	def E01Example_06_02(cls, args):
		nSelMenu = cls.MENU_NONE
		oCanvas = CE06Canvas()
		
		while nSelMenu != cls.MENU_EXIT:
			cls.PrintMenu()
			nSelMenu = int(input("\n메뉴 선택 : ")) - 1
			
			# 모든 도형 그리기 일 경우
			if nSelMenu == cls.MENU_DRAW_ALL_SHAPES:
				oCanvas.DrawAllShapes()
			
			# 도형 추가 일 경우
			elif nSelMenu >= cls.MENU_ADD_CIRCLE and nSelMenu <= cls.MENU_ADD_RECTANGLE:
				oCanvas.AddShape(cls.CreateShape(nSelMenu))
			
			print()
	
	# 메뉴를 출력한다
	@classmethod
	def PrintMenu(cls):
		print("=====> 메뉴 <=====")
		print("1. 원 추가")
		print("2. 삼각형 추가")
		print("3. 사각형 추가")
		print("4. 모든 도형 그리기")
		print("5. 종료")
	
	"""
	특정 객체를 생성하는 메서드를 팩토리 메서드라고 하며 해당 메서드를 활용하면 객체를 생성하는 과정을 단일화 시키는 것이 가능하다.
	(즉, 해당 방법은 디자인 패턴 중에 팩토리 패턴 범위에 속하는 설계 방법이다.)
	"""
	
	# 도형을 생성한다
	@classmethod
	def CreateShape(cls, a_nSelMenu: int):
		nColor = random.randint(CE06Shape.COLOR_RED, CE06Shape.COLOR_BLUE)
		
		# 원 일 경우
		if a_nSelMenu == cls.MENU_ADD_CIRCLE:
			return CE06Circle(nColor)
		
		# 삼각형 일 경우
		elif a_nSelMenu == cls.MENU_ADD_TRIANGLE:
			return CE06Triangle(nColor)
		
		# 사각형 일 경우
		elif a_nSelMenu == cls.MENU_ADD_RECTANGLE:
			return CE06Rectangle(nColor)
		
		return None


# 유닛
class CE06Unit:
	# 초기화
	def __init__(self, a_oName: str, a_nHP: int, a_nATK: int, a_nDEF: int):
		self.m_nHP = a_nHP
		self.m_nATK = a_nATK
		self.m_nDEF = a_nDEF
		self.m_oName = a_oName
	
	# 공격한다
	def Attack(self, a_oTarget):
		nDamage = max(0, self.m_nATK - a_oTarget.m_nDEF)
		a_oTarget.m_nHP -= nDamage
		
		print("{0} 에게 {1} 피해를 입혔습니다.".format(a_oTarget.m_oName, nDamage))


# 도형
class CE06Shape:
	COLOR_RED = 0
	COLOR_GREEN = 1
	COLOR_BLUE = 2
	
	"""
	Python 은 변수 선언 시 자료형을 명시하는 것이 가능하다.
	따라서, 특정 메서드의 매개 변수에 자료형을 명시함으로서 에디터가 지원하는 인텔리센스 기능에 도움을 받는 것이 가능하다.

	즉, 자료형이 명시되지 않은 매개 변수는 어떤 데이터가 전달 될 지 알 수 없기 때문에 에디터가 지원하는 인텔리센스 기능이 동작하지 않는다는
	것을 알 수 있다.
	"""
	
	# 초기화
	def __init__(self, a_nColor: int):
		self.m_nColor = a_nColor
	
	# 색상 문자열을 반환한다
	def GetColorStr(self):
		oColorStrs = ["빨간색", "녹색", "파란색"]
		return oColorStrs[self.m_nColor]
	
	"""
	Python 은 순수 가상 메서드 (추상 메서드) 를 지원하지 않기 떄문에 특정 메서드를 오버라이드 전용으로 구현하기 위해서는 pass 키워드를
	활용하면 된다. (즉, 완전한 순수 가상 메서드는 아니지만 구현부가 존재하지 않는 메서드를 구현하는 것이 가능하다.)

	순수 가상 메서드란?
	- 일반적인 메서드와 달리 메서드의 구현부가 존재하지 않는 메서드를 의미한다. (즉, 미완성 된 메서드를 의미한다.)
	따라서, 특정 클래스에 순수 가상 메서드가 하나라도 존재 할 경우 해당 메서드는 객체화 시킬 수 없는 추상 클래스가 되는 특징이 존재한다.
	"""
	
	# 도형을 그린다
	def Draw(self):
		pass


# 원
class CE06Circle(CE06Shape):
	# 초기화
	def __init__(self, a_nColor: int):
		super().__init__(a_nColor)
	
	# 도형을 그린다
	def Draw(self):
		print("{0} 원을 그렸습니다.".format(self.GetColorStr()))


# 삼각형
class CE06Triangle(CE06Shape):
	# 초기화
	def __init__(self, a_nColor: int):
		super().__init__(a_nColor)
	
	# 도형을 그린다
	def Draw(self):
		print("{0} 삼각형을 그렸습니다.".format(self.GetColorStr()))


# 사각형
class CE06Rectangle(CE06Shape):
	# 초기화
	def __init__(self, a_nColor: int):
		super().__init__(a_nColor)
	
	# 도형을 그린다
	def Draw(self):
		print("{0} 사각형을 그렸습니다.".format(self.GetColorStr()))


# 캔버스
class CE06Canvas:
	# 초기화
	def __init__(self):
		self.m_oShapeList = list()
	
	# 도형을 추가한다
	def AddShape(self, a_oShape: CE06Shape):
		self.m_oShapeList.append(a_oShape)
	
	# 모든 도형을 그린다
	def DrawAllShapes(self):
		for oShape in self.m_oShapeList:
			oShape.Draw()
