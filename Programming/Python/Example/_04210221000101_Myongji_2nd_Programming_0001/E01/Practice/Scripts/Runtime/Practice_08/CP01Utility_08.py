import os
import sys

from .CP01EquipInfo_08 import *


# 유틸리티
class CP01Utility_08:
	# 비용을 반환한다
	@classmethod
	def GetPrice(cls, a_oEquipInfo: CP01EquipInfo_08, a_bIsBuy: bool):
		oPriceList = [100, 250, 500]
		oPercentList = [0.0, 0.1, 0.15, 0.3]
		
		nPrice = oPriceList[a_oEquipInfo.m_nKinds] if a_oEquipInfo.m_nKinds > CP01EquipInfo_08.KINDS_NONE and a_oEquipInfo.m_nKinds < CP01EquipInfo_08.KINDS_MAX_VAL else 0
		nTradePrice = nPrice if a_bIsBuy else nPrice // 2
		
		fPercent = oPercentList[a_oEquipInfo.m_nGrade] if a_oEquipInfo.m_nGrade > CP01EquipInfo_08.GRADE_NONE and a_oEquipInfo.m_nGrade < CP01EquipInfo_08.GRADE_MAX_VAL else 0.0
		return int(nTradePrice + (nPrice * fPercent))
	
	# 장비 정보 => 메뉴 장비 정보로 변환한다
	@classmethod
	def ConvertToMenuEquipInfos(cls, a_oEquipInfoList: list):
		oMenuEquipInfoList = []
		
		for i in range(CP01EquipInfo_08.KINDS_NONE, CP01EquipInfo_08.KINDS_MAX_VAL):
			for j in range(CP01EquipInfo_08.GRADE_NONE, CP01EquipInfo_08.GRADE_MAX_VAL):
				oEquipInfoList = list(filter(lambda a_oEquipInfo: a_oEquipInfo.m_nKinds == i and a_oEquipInfo.m_nGrade == j, a_oEquipInfoList))
				
				# 장비 정보가 존재 할 경우
				if oEquipInfoList:
					oMenuEquipInfoList.append(CP01MenuEquipInfo_08(i, len(oEquipInfoList), j))
		
		return oMenuEquipInfoList
