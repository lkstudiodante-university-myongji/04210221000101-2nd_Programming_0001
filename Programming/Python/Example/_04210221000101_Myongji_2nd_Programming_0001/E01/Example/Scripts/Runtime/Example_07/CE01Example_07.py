import os
import sys

"""
파일 시스템이란?
- 파일에 데이터를 입/출력 할 수 있는 기능을 의미한다. (즉, 파일 시스템을 활용하면 반영구적으로 데이터를 보관할 수 있다는 것을 알 수 있다.)
프로그램은 주 기억 장치 (RAM) 상에서 실행되기 때문에 프로그램이 사용하는 데이터는 해당 프로그램을 종료하는 순간 사라지는 단점이 존재한다.

따라서, 프로그램이 사용하던 데이터를 반영구적으로 보관하기 위해서는 해당 데이터를 다른 장소에 저장 할 필요가 있으며 이때 파일 시스템을 활용
할 수 있다.

파일은 보조 기억 장치 (HDD or SSD) 에 생성되기 때문에 해당 파일에 보관 된 데이터는 프로그램이 종료 되어도 계속 존재하는 특징이 있기 때문에
파일을 활용하면 반영구적으로 데이터를 저장 및 필요에 따라 읽어들이는 것이 가능하다. (즉, 이전 프로그램이 구동 중에 사용하던 데이터를 그대로
재사용 할 수 있다는 것을 알 수 있다.)
"""


# Example 7
class CE01Example_07:
	# 초기화
	@classmethod
	def Start(cls, args):
		CE07UserInfoStorage.GetInst().m_nNumGolds = 10
		CE07UserInfoStorage.GetInst().m_nNumEquipments = 20
		
		CE07UserInfoStorage.GetInst().SaveUserInfo()
		CE07UserInfoStorage.GetInst().LoadUserInfo()
		
		print("=====> 유저 정보 <=====")
		print("{0}, {1}".format(CE07UserInfoStorage.GetInst().m_nNumGolds, CE07UserInfoStorage.GetInst().m_nNumEquipments))


# 유저 정보 저장소
class CE07UserInfoStorage:
	m_oInst = None
	P_FILE_STORAGE_USER_INFO = f"{os.path.dirname(__file__)}/../../../Resources/Example_07/E01Storage_UserInfo.txt"
	
	# 초기화
	def __init__(self):
		self.m_nNumGolds = 0
		self.m_nNumEquipments = 0
	
	# 유저 정보를 로드한다
	def LoadUserInfo(self):
		"""
		스트림이란?
		- 데이터를 입/출력 할 수 있는 통로를 의미한다. (즉, 스트림을 활용하면 파일을 대상으로 데이터를 입/출력하는 것이 가능하다.)

		스트림은 컴퓨터에서 동작하는 모든 프로그램이 사용하는 공통 자원이기 때문에 스트림이 더이상 필요 없을 경우에는 반드시 해당 스트림을
		제거해 줄 필요가 있다. (즉, 사용이 완료 된 스트림을 제거하지 않을 경우 불필요한 스트림이 생성된다는 것을 의미하며 이는 곧 컴퓨터 자원이
		고갈 되어 정작 필요 할 때 스트림을 생성 할 수 없는 문제가 발생 할 수 있다.)
		"""
		# 파일이 존재 할 경우
		if os.path.isfile(CE07UserInfoStorage.P_FILE_STORAGE_USER_INFO):
			"""
			open 메서드란?
			- 파일을 대상으로 데이터를 입/출력 할 수 있게 스트림을 생성해주는 역할을 수행하는 메서드이다. 따라서, 해당 메서드는 반환 값으로
			스트림을 반환해주며 해당 스트림을 통해 원하는 데이터를 파일로부터 입력 받거나 출력하는 것이 가능하다.

			또한, 스트림은 close 메서드를 제공하며 해당 메서드는 스트림을 제거하는 역할을 수행한다. (즉, 스트림이 더이상 필요 없을 경우에는
			반드시 close 메서드를 통해 해당 스트림을 제거해줘야한다.)

			with 키워드란?
			- Python 에서 스트림을 좀 더 안전하게 사용 할 수 있도록 제공하는 키워드로서 해당 키워드를 활용하면 특정 스트림을 제거하기 위해서
			명시적으로 close 메서드를 호출 할 필요가 없다. (즉, with 키워드 영역을 벗어나면 Python 인터프리터가 자동으로 close 메서드를
			호출해준다는 것을 알 수 있다.)

			따라서, 해당 키워드를 활용하면 사용이 완료 된 스크림에 close 메서드를 호출하지 않는 실수를 최소화하는 것이 가능하다.
			"""
			with open(CE07UserInfoStorage.P_FILE_STORAGE_USER_INFO, "r") as oRStream:
				"""
				readline 또는 readlines 메서드는 스트림에서 데이터를 가져오는 역할을 수행한다.

				readline 메서드는 한 라인을 가져오는 역할을 수행하며 readlines 메서드는 모든 라인을 읽어들여서 리스트 형태로 데이터를
				가져오는 특징이 존재한다.

				따라서, 파일에 존재하는 모든 데이터를 가져오고 싶다면 readlines 메서드를 활용하는 것을 추천한다.

				Ex)
				oLineList = oRStream.readlines()

				for oLine in oLineList:
					oListTokens = oLine.split(",")

				위와 같이 readlines 메서드를 사용해서 모든 라인을 읽어들인 후 for 반복문을 사용하는 것으로 파일에 존재하는 모든 라인을
				처리하는 것이 가능하다.
				"""
				oListTokens = oRStream.readline().split(",")
				
				self.m_nNumGolds = int(oListTokens[0])
				self.m_nNumEquipments = int(oListTokens[1])
	
	# 유저 정보를 저장한다
	def SaveUserInfo(self):
		oPath_Dir = os.path.dirname(CE07UserInfoStorage.P_FILE_STORAGE_USER_INFO)

		"""
		open 메서드는 출력용으로 스트림을 생성 할 경우 파일을 생성하는 역할도 수행한다. (즉, 입력용으로 스트림을 생성 할 경우 파일이
		없으면 예외가 발생한다.)

		단, open 메서드는 디렉토리는 생성해주지 않기 때문에 명시 된 파일 경로에 존재하지 않는 디렉토리가 명시되어 있을 경우 스트림 생성에
		실패한다. (즉, 반환 값으로 None 데이터가 반환된다는 것을 알 수 있다.)

		따라서, 스트림 생성을 시도하기 전에 디렉토리 존재 여부를 검사 후 디렉토리 존재 여부에 따라 해당 디렉토리를 생성해주는 명령문을
		작성해줘야한다. (즉, os.makedirs 메서드를 통해 디렉토리를 생성할 수 있다.)
		"""
		# 디렉토리가 없을 경우
		if not os.path.isdir(oPath_Dir):
			os.makedirs(oPath_Dir)
		
		with open(CE07UserInfoStorage.P_FILE_STORAGE_USER_INFO, "w") as oWStream:
			oWStream.write("{0},{1}".format(self.m_nNumGolds, self.m_nNumEquipments))
	
	# 인스턴스를 반환한다
	@classmethod
	def GetInst(cls):
		# 인스턴스가 없을 경우
		if cls.m_oInst == None:
			cls.m_oInst = CE07UserInfoStorage()
		
		return cls.m_oInst
