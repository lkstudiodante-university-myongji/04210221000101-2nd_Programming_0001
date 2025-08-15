import os
import sys

from .CP01EquipInfo_08 import *


# 유저 정보
class CP01UserInfo_08:
	# 초기화
	def __init__(self, a_oUserInfo: str = None):
		self.__init__user_info__()
		
		# 유저 정보가 유효 할 경우
		if a_oUserInfo:
			oListTokens = a_oUserInfo.split(",")
			self.m_nNumGolds = int(oListTokens[0])
			
			for i in range(2, len(oListTokens), 2):
				self.m_oEquipList.append(CP01EquipInfo_08(int(oListTokens[i]), int(oListTokens[i + 1])))
	
	# 초기화
	def __init__user_info__(self):
		self.m_nNumGolds = 0
		self.m_oEquipList = []
	
	# 장비 정보를 추가한다
	def AddEquipInfo(self, a_oEquipInfo: CP01EquipInfo_08):
		self.m_oEquipList.append(a_oEquipInfo)
	
	# 장비 정보를 제거한다
	def RemoveEquipInfo(self, a_oEquipInfo: CP01EquipInfo_08):
		for i in range(0, len(self.m_oEquipList)):
			# 종류와 등급이 동일 할 경우
			if a_oEquipInfo.m_nKinds == self.m_oEquipList[i].m_nKinds and a_oEquipInfo.m_nGrade == self.m_oEquipList[i].m_nGrade:
				del self.m_oEquipList[i]
				break
	
	# 유저 정보를 저장한다
	def SaveUserInfo(self, a_oOutUserInfoList: list):
		oStoreInfo = "{0},{1}".format(self.m_nNumGolds, len(self.m_oEquipList))
		
		for oEquipInfo in self.m_oEquipList:
			oStoreInfo += ",{0}".format(oEquipInfo.GetEquipInfo())
		
		a_oOutUserInfoList.append(oStoreInfo)


# 유저 정보 저장소
class CP01UserInfoStorage_08:
	m_oInst = None
	P_FILE_STORAGE_USER_INFO = f"{os.path.dirname(__file__)}/../../../Resources/Practice_08/P01Storage_UserInfo_08.txt"
	
	# 초기화
	def __init__(self):
		self.__init__user_info_storage__()
	
	# 초기화
	def __init__user_info_storage__(self):
		self.m_oUserInfoList = []
	
	# 유저 정보를 반환한다
	def GetUserInfo(self, a_nIdx: int):
		return self.m_oUserInfoList[a_nIdx] if a_nIdx >= 0 and a_nIdx < len(self.m_oUserInfoList) else None
	
	# 유저 정보를 추가한다
	def AddUserInfo(self, a_oUserInfo: CP01UserInfo_08):
		self.m_oUserInfoList.append(a_oUserInfo)
	
	# 유저 정보를 로드한다
	def LoadUserInfos(self):
		# 유저 정보가 존재 할 경우
		if os.path.isfile(CP01UserInfoStorage_08.P_FILE_STORAGE_USER_INFO):
			with open(CP01UserInfoStorage_08.P_FILE_STORAGE_USER_INFO, "r") as oRStream:
				for oUserInfo in oRStream.readlines():
					self.m_oUserInfoList.append(CP01UserInfo_08(oUserInfo))
	
	# 유저 정보를 저장한다
	def SaveUserInfos(self):
		# 디렉토리가 없을 경우
		if not os.path.isdir(os.path.dirname(CP01UserInfoStorage_08.P_FILE_STORAGE_USER_INFO)):
			os.makedirs(os.path.dirname(CP01UserInfoStorage_08.P_FILE_STORAGE_USER_INFO))
		
		with open(CP01UserInfoStorage_08.P_FILE_STORAGE_USER_INFO, "w") as oWStream:
			oUserInfoList = []
			
			for oUserInfo in self.m_oUserInfoList:
				oUserInfo.SaveUserInfo(oUserInfoList)
			
			oWStream.writelines(oUserInfoList)
	
	# 인스턴스를 반환한다
	@classmethod
	def GetInst(cls):
		# 인스턴스가 없을 경우
		if cls.m_oInst == None:
			cls.m_oInst = CP01UserInfoStorage_08()
		
		return cls.m_oInst
