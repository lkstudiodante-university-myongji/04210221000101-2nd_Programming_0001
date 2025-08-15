import os
import sys

from datetime import *


# 시간 관리자
class CTimeManager:
	m_oInst = None
	
	# 생성자
	def __init__(self):
		self.m_fDeltaTime = 0.0
		self.m_fRunningTime = 0.0
		
		self.m_oPrevTime = datetime.now()
		self.m_oStartTime = datetime.now()
	
	# 상태를 갱신한다
	def Update(self):
		self.m_fDeltaTime = (datetime.now() - self.m_oPrevTime).total_seconds()
		self.m_fRunningTime = (datetime.now() - self.m_oStartTime).total_seconds()
		
		self.m_oPrevTime = datetime.now()
		
	# 시간 간격을 반환한다
	def GetDeltaTime(self):
		return self.m_fDeltaTime
	
	# 구동 시간을 반환한다
	def GetRunningTime(self):
		return self.m_fRunningTime
	
	# 인스턴스를 반환한다
	@classmethod
	def GetInst(cls):
		# 인스턴스가 없을 경우
		if cls.m_oInst == None:
			cls.m_oInst = CTimeManager()
		
		return cls.m_oInst
