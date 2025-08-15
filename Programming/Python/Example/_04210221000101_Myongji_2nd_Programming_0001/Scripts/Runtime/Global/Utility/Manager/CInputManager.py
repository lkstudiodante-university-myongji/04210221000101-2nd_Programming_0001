import os
import sys


# 입력 관리자
class CInputManager:
	m_oInst = None
	
	# 생성자
	def __init__(self):
		self.m_oKeySet = set()
		self.m_oPrevKeySet = set()
	
	# 상태를 갱신한다
	def Update(self):
		self.m_oPrevKeySet = set(self.m_oKeySet)
	
	# 키 눌림 여부를 검사한다
	def IsKeyDown(self, a_nKey: int):
		return a_nKey in self.m_oKeySet
	
	# 키 눌림 시작 여부를 검사한다
	def IsKeyPress(self, a_nKey: int):
		return a_nKey in self.m_oKeySet and a_nKey not in self.m_oPrevKeySet
	
	# 키 눌림 종료 여부를 검사한다
	def IsKeyRelease(self, a_nKey: int):
		return a_nKey in self.m_oPrevKeySet and a_nKey not in self.m_oKeySet
	
	# 키를 추가한다
	def AddKey(self, a_nKey: int):
		self.m_oKeySet.add(a_nKey)
	
	# 키를 제거한다
	def RemoveKey(self, a_nKey: int):
		# 키가 존재 할 경우
		if a_nKey in self.m_oKeySet:
			self.m_oKeySet.remove(a_nKey)
	
	# 인스턴스를 반환한다
	@classmethod
	def GetInst(cls):
		# 인스턴스가 없을 경우
		if cls.m_oInst == None:
			cls.m_oInst = CInputManager()
		
		return cls.m_oInst
