import os
import sys
import random

from .CP01Utility_08 import *
from .CP01UserInfoStorage_08 import *
from .CP01StoreInfoStorage_08 import *

"""
중간 평가
- 상점 시뮬레이션 제작
- 프로그램 시작 시 초기 비용을 입력
- 상점에서는 장비 구입, 장비 판매, 나가기 3 개의 메뉴가 존재
- 장비 구입 메뉴 선택 시 구입 가능한 장비 목록 출력 (+ 각 장비는 판매 비용과 재고가 존재)
- 장비 판매 메뉴 선택 시 판매 가능한 장비 목록 출력 (+ 사용자가 현재 소지 중인 장비)
- 장비 구입 시 비용이 부족하거나 재고가 모두 소진 되었을 경우에는 판매 불가
- 장비 구입에 성공했을 경우 해당 장비 구입 비용만큼 사용자가 소지한 비용 차감
- 장비 판매 시 판매 비용은 구입 비용의 절반
- 판매한 장비는 다시 상점에 입고 (+ 즉, 상점이 보유하고 있는 상품 재고가 증가)
- 해당 시뮬레이션 결과는 프로그램을 종료한 후 다시 실행해도 계속 유지 (+ 즉, 상점의 재고 상태와 사용자가 보유하고 있는 비용과 장비가 모두 유지 되어야한다.)

Ex)
초기 비용 입력 : 1000

PS)
초기 비용은 시뮬레이션 결과 파일이 없을 경우에만 입력 받는다. (+ 즉, 이미 한번 프로그램을 실행해서 시뮬레이션 결과가 존재한다면 이후 프로그램 시작 시에는 초기 비용 입력을 생략한다.)

=====> 메인 메뉴 <=====
1. 상점 방문
2. 종료

메인 메뉴 선택 : 1

=====> 상점 메뉴 <=====
1. 장비 구입
2. 장비 판매
3. 나가기

상점 메뉴 선택 : 1
=====> 구입 가능 장비 <=====
1. 숏 소드 (비용 : 100, 재고 : 3)
2. 롱 소드 (비용 : 250, 재고 : 2)
3. 그레이트 소드 (비용 : 1000, 재고 : 1)
4. 그만두기

구입 장비 선택 (소지 비용 : 1000) : 1

Case 1. 구입 가능 할 경우
숏 소드를 구입했습니다.

Case 2. 구입 불가능 할 경우
비용이 부족하거나 숏 소드 재고가 없습니다.

PS)
장비를 구입했으면 상점에서는 해당 장비의 재고와 사용자가 소지하고 있는 비용은 차감되고 구입한 장비는 사용자에게 추가된다.

상점 메뉴 선택 : 2
=====> 판매 가능 장비 <=====
1. 숏 소드 (비용 : 500, 보유 수량 : 1)
2. 그만두기

판매 장비 선택 (소지 비용 : 900) : 1
숏 소드를 판매했습니다.

PS)
판매한 장비는 다시 상점에 입고되며 사용자가 보유하고 있던 비용은 판매 비용만큼 증가한다. (+ 또한, 판매한 장비는 사용자로부터 제거된다.)
장비 구입 또는 장비 판매에서 그만두기를 선택했을 경우에는 다시 상점 메뉴로 이동하며 잘못된 메뉴를 선택했을 경우에는 잘못된 메뉴를 선택했다고 출력한다.
"""

# Practice 8
class CP01Practice_08:
	MENU_NONE = -1
	MENU_VISIT_STORE = 0
	MENU_EXIT = 1
	
	# 실행한다
	@classmethod
	def Start(cls, args):
		cls.LoadInfos()
		nSelMenu = cls.MENU_NONE
		
		# 유저 정보가 없을 경우
		if not CP01UserInfoStorage_08.GetInst().GetUserInfo(0):
			oUserInfo = CP01UserInfo_08()
			oUserInfo.m_nNumGolds = int(input("초기 비용 입력 : "))
			
			CP01UserInfoStorage_08.GetInst().AddUserInfo(oUserInfo)
		
		while nSelMenu != cls.MENU_EXIT:
			cls.PrintMenu()
			nSelMenu = int(input("\n메인 메뉴 선택 : ")) - 1
			
			# 상점 방문 일 경우
			if nSelMenu == cls.MENU_VISIT_STORE:
				CP08Store.Visit()
			
			print("")
		
		cls.SaveInfos()
	
	# 메누를 출력한다
	@classmethod
	def PrintMenu(cls):
		print("=====> 메인 메뉴 <=====")
		print("1. 상점 방문")
		print("2. 종료")
	
	# 정보를 로드한다
	@classmethod
	def LoadInfos(cls):
		CP01UserInfoStorage_08.GetInst().LoadUserInfos()
		CP01StoreInfoStorage_08.GetInst().LoadStoreInfos()
	
	# 정보를 저장한다
	@classmethod
	def SaveInfos(cls):
		CP01UserInfoStorage_08.GetInst().SaveUserInfos()
		CP01StoreInfoStorage_08.GetInst().SaveStoreInfos()


# 상점
class CP08Store:
	MENU_NONE = -1
	MENU_BUY_EQUIP = 0
	MENU_SELL_EQUIP = 1
	MENU_EXIT = 2
	
	# 방문한다
	@classmethod
	def Visit(cls):
		nSelMenu = cls.MENU_NONE
		
		while nSelMenu != cls.MENU_EXIT:
			cls.PrintMenu()
			nSelMenu = int(input("\n상점 메뉴 선택 : ")) - 1
			
			# 장비 구입 일 경우
			if nSelMenu == cls.MENU_BUY_EQUIP:
				cls.BuyEquip()
			
			# 장비 판매 일 경우
			elif nSelMenu == cls.MENU_SELL_EQUIP:
				cls.SellEquip()
			
			print("")
	
	# 메뉴를 출력한다
	@classmethod
	def PrintMenu(cls):
		print("=====> 상점 메뉴 <=====")
		print("1. 장비 구입")
		print("2. 장비 판매")
		print("3. 나가기")
	
	# 장비 구입 메뉴를 출력한다
	@classmethod
	def PrintBuyEquipMenu(cls, a_oMenuEquipInfoList: list):
		print("=====> 구입 가능 장비 <=====")
		
		for i, oMenuEquipInfo in enumerate(a_oMenuEquipInfoList):
			print("{0}. {1} (비용 : {2}, 재고 : {3})".format(i + 1, oMenuEquipInfo.GetKinds(), CP01Utility_08.GetPrice(oMenuEquipInfo, True), oMenuEquipInfo.m_nNumEquips))
		
		print("{0}. 그만두기".format(len(a_oMenuEquipInfoList) + 1))
	
	# 장비 판매 메뉴를 출력한다
	@classmethod
	def PrintSellEquipMenu(cls, a_oMenuEquipInfoList: list):
		print("=====> 판매 가능 장비 <=====")
		
		for i, oMenuEquipInfo in enumerate(a_oMenuEquipInfoList):
			print("{0}. {1} (등급 : {2}, 비용 : {3}, 재고 : {4})".format(i + 1, oMenuEquipInfo.GetKinds(), oMenuEquipInfo.GetGrade(), CP01Utility_08.GetPrice(oMenuEquipInfo, False), oMenuEquipInfo.m_nNumEquips))
		
		print("{0}. 그만두기".format(len(a_oMenuEquipInfoList) + 1))
	
	# 장비를 구입한다
	@classmethod
	def BuyEquip(cls):
		nSelEquip = -1
		oMenuEquipInfoList = CP01Utility_08.ConvertToMenuEquipInfos(CP01StoreInfoStorage_08.GetInst().GetStoreInfo(0).m_oEquipList)
		
		while nSelEquip < len(oMenuEquipInfoList):
			cls.PrintBuyEquipMenu(oMenuEquipInfoList)
			nSelEquip = int(input("\n구입 장비 선택 (소지 금액 : {0}) : ".format(CP01UserInfoStorage_08.GetInst().GetUserInfo(0).m_nNumGolds))) - 1
			
			# 장비를 선택했을 경우
			if nSelEquip < len(oMenuEquipInfoList):
				nNumEquips = int(input("구입 수량 입력 : "))
				
				# 재고가 부족 할 경우
				if oMenuEquipInfoList[nSelEquip].m_nNumEquips < nNumEquips:
					print("재고가 부족합니다.")
				
				# 비용이 부족 할 경우
				elif CP01UserInfoStorage_08.GetInst().GetUserInfo(0).m_nNumGolds * nNumEquips < CP01Utility_08.GetPrice(oMenuEquipInfoList[nSelEquip], True):
					print("비용이 부족합니다.")
				
				else:
					for _ in range(0, nNumEquips):
						oEquipInfo = CP01EquipInfo_08(oMenuEquipInfoList[nSelEquip].m_nKinds, random.randint(CP01EquipInfo_08.GRADE_COMMON, CP01EquipInfo_08.GRADE_HERO))
						print("{0} {1} 을(를) 구입했습니다.".format(oEquipInfo.GetGrade(), oEquipInfo.GetKinds()))
						
						CP01UserInfoStorage_08.GetInst().GetUserInfo(0).m_nNumGolds -= CP01Utility_08.GetPrice(oMenuEquipInfoList[nSelEquip], True)
						
						CP01UserInfoStorage_08.GetInst().GetUserInfo(0).AddEquipInfo(oEquipInfo)
						CP01StoreInfoStorage_08.GetInst().GetStoreInfo(0).RemoveEquipInfo(oEquipInfo)
				
				nSelEquip = -1
			
			print("")
			oMenuEquipInfoList = CP01Utility_08.ConvertToMenuEquipInfos(CP01StoreInfoStorage_08.GetInst().GetStoreInfo(0).m_oEquipList)
	
	# 장비를 판매한다
	@classmethod
	def SellEquip(cls):
		nSelEquip = -1
		oMenuEquipInfoList = CP01Utility_08.ConvertToMenuEquipInfos(CP01UserInfoStorage_08.GetInst().GetUserInfo(0).m_oEquipList)
		
		while nSelEquip < len(oMenuEquipInfoList):
			cls.PrintSellEquipMenu(oMenuEquipInfoList)
			nSelEquip = int(input("\n판매 장비 선택 (소지 금액 : {0}) : ".format(CP01UserInfoStorage_08.GetInst().GetUserInfo(0).m_nNumGolds))) - 1
			
			# 장비를 선택했을 경우
			if nSelEquip < len(oMenuEquipInfoList):
				nNumEquips = int(input("판매 수량 입력 : "))
				
				# 재고가 부족 할 경우
				if oMenuEquipInfoList[nSelEquip].m_nNumEquips < nNumEquips:
					print("재고가 부족합니다.")
				
				else:
					print("{0} {1} 을(를) 판매했습니다.".format(oMenuEquipInfoList[nSelEquip].GetGrade(), oMenuEquipInfoList[nSelEquip].GetKinds()))
					
					for _ in range(0, nNumEquips):
						CP01UserInfoStorage_08.GetInst().GetUserInfo(0).m_nNumGolds += CP01Utility_08.GetPrice(oMenuEquipInfoList[nSelEquip], False)
						
						CP01UserInfoStorage_08.GetInst().GetUserInfo(0).RemoveEquipInfo(CP01EquipInfo_08(oMenuEquipInfoList[nSelEquip].m_nKinds, oMenuEquipInfoList[nSelEquip].m_nGrade))
						CP01StoreInfoStorage_08.GetInst().GetStoreInfo(0).AddEquipInfo(CP01EquipInfo_08(oMenuEquipInfoList[nSelEquip].m_nKinds, CP01EquipInfo_08.GRADE_NONE))
				
				nSelEquip = -1
			
			print("")
			oMenuEquipInfoList = CP01Utility_08.ConvertToMenuEquipInfos(CP01UserInfoStorage_08.GetInst().GetUserInfo(0).m_oEquipList)
