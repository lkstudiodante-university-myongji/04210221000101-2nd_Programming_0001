import os
import sys


# 장비 정보
class CP01EquipInfo_08:
	KINDS_NONE = -1
	KINDS_SHORT_SWORD = 0
	KINDS_LONG_SWORD = 1
	KINDS_GREATE_SWORD = 2
	KINDS_MAX_VAL = 3
	
	GRADE_NONE = -1
	GRADE_COMMON = 0
	GRADE_MAGIC = 1
	GRADE_LENGEND = 2
	GRADE_HERO = 3
	GRADE_MAX_VAL = 4
	
	# 초기화
	def __init__(self, a_nKinds: int, a_nGrade: int = GRADE_NONE):
		self.__init__equip_info__(a_nKinds, a_nGrade)
	
	# 초기화
	def __init__equip_info__(self, a_nKinds: int, a_nGrade: int):
		self.m_nKinds = a_nKinds
		self.m_nGrade = a_nGrade
	
	# 종류를 반환한다
	def GetKinds(self):
		oKindsList = [
			"숏 소드", "롱 소드", "그레이트 소드"
		]
		
		return oKindsList[self.m_nKinds] if self.m_nKinds > CP01EquipInfo_08.KINDS_NONE and self.m_nKinds < CP01EquipInfo_08.KINDS_MAX_VAL else "알수 없음"
	
	# 등급을 반환한다
	def GetGrade(self):
		oGradeList = [
			"일반", "매직", "전설", "영웅"
		]
		
		return oGradeList[self.m_nGrade] if self.m_nGrade > CP01EquipInfo_08.GRADE_NONE and self.m_nGrade < CP01EquipInfo_08.GRADE_MAX_VAL else "알수 없음"
	
	# 장비 정보를 반환한다
	def GetEquipInfo(self):
		return "{0},{1}".format(self.m_nKinds, self.m_nGrade)


# 메뉴 장비 정보
class CP01MenuEquipInfo_08(CP01EquipInfo_08):
	# 초기화
	def __init__(self, a_nKinds: int, a_nNumEquips: int, a_nGrade: int = CP01EquipInfo_08.GRADE_NONE):
		super().__init__(a_nKinds, a_nGrade)
		self.__init__menu_equip_info__(a_nNumEquips)
	
	# 초기화
	def __init__menu_equip_info__(self, a_nNumEquips: int):
		self.m_nNumEquips = a_nNumEquips
