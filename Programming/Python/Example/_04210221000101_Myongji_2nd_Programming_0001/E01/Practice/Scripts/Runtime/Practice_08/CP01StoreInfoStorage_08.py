import os
import sys

from .CP01EquipInfo_08 import *


# 상점 정보
class CP01StoreInfo_08:
	# 초기화
	def __init__(self, a_oStoreInfo: str = None):
		self.__init__storage_info__()
		oListTokens = a_oStoreInfo.split(",")
		
		for i in range(1, len(oListTokens), 2):
			self.m_oEquipList.append(CP01EquipInfo_08(int(oListTokens[i]), int(oListTokens[i + 1])))
	
	# 초기화
	def __init__storage_info__(self):
		self.m_oEquipList = []
	
	# 장비 정보를 추가한다
	def AddEquipInfo(self, a_oEquipInfo: CP01EquipInfo_08):
		self.m_oEquipList.append(a_oEquipInfo)
	
	# 장비 정보를 제거한다
	def RemoveEquipInfo(self, a_oEquipInfo: CP01EquipInfo_08):
		for i in range(0, len(self.m_oEquipList)):
			# 종류가 동일 할 경우
			if a_oEquipInfo.m_nKinds == self.m_oEquipList[i].m_nKinds:
				del self.m_oEquipList[i]
				break
	
	# 초기화
	def SaveStoreInfo(self, a_oOutStoreInfoList: list):
		oStoreInfo = "{0}".format(len(self.m_oEquipList))
		
		for oEquipInfo in self.m_oEquipList:
			oStoreInfo += ",{0}".format(oEquipInfo.GetEquipInfo())
		
		a_oOutStoreInfoList.append(oStoreInfo)


# 상점 정보 저장소
class CP01StoreInfoStorage_08:
	m_oInst = None
	P_FILE_STORAGE_STORE_INFO = f"{os.path.dirname(__file__)}/../../../Resources/Practice_08/P01Storage_StoreInfo_08.txt"
	
	# 초기화
	def __init__(self):
		self.__init__store_info_storage__()
	
	# 초기화
	def __init__store_info_storage__(self):
		self.m_oStoreInfoList = []
	
	# 상점 정보를 반환한다
	def GetStoreInfo(self, a_nIdx: int):
		return self.m_oStoreInfoList[a_nIdx] if a_nIdx >= 0 and a_nIdx < len(self.m_oStoreInfoList) else None
	
	# 상점 정보를 로드한다
	def LoadStoreInfos(self):
		# 상점 정보가 존재 할 경우
		if os.path.isfile(CP01StoreInfoStorage_08.P_FILE_STORAGE_STORE_INFO):
			with open(CP01StoreInfoStorage_08.P_FILE_STORAGE_STORE_INFO, "r") as oRStream:
				for oStoreInfo in oRStream.readlines():
					self.m_oStoreInfoList.append(CP01StoreInfo_08(oStoreInfo))
	
	# 상점 정보를 저장한다
	def SaveStoreInfos(self):
		# 디렉토리가 없을 경우
		if not os.path.isdir(os.path.dirname(CP01StoreInfoStorage_08.P_FILE_STORAGE_STORE_INFO)):
			os.makedirs(os.path.dirname(CP01StoreInfoStorage_08.P_FILE_STORAGE_STORE_INFO))
		
		with open(CP01StoreInfoStorage_08.P_FILE_STORAGE_STORE_INFO, "w") as oWStream:
			oStoreInfoList = []
			
			for oStoreInfo in self.m_oStoreInfoList:
				oStoreInfo.SaveStoreInfo(oStoreInfoList)
			
			oWStream.writelines(oStoreInfoList)
	
	# 인스턴스를 반환한다
	@classmethod
	def GetInst(cls):
		# 인스턴스가 없을 경우
		if cls.m_oInst == None:
			cls.m_oInst = CP01StoreInfoStorage_08()
		
		return cls.m_oInst
