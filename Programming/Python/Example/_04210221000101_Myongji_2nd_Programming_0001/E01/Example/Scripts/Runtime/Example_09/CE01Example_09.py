import os
import sys

from PyQt5 import *
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *


# Example 9
class CE01Example_09(QMainWindow):
	# 생성자
	def __init__(self):
		super().__init__()
		self.__init__example_09__()
	
	# 초기화
	def __init__example_09__(self):
		"""
		show 메서드는 QMainWindow 를 화면 상에 출력하는 역할을 수행한다. (즉, 해당 메서드를 호출하지 않으면 윈도우가 화면 상에 보이지
		않는다는 것을 알 수 있다.)
		
		setWindowTitle 메서드는 윈도우 제목을 변경하는 역할을 수행한다. (즉, 해당 메서드를 활용하면 자유롭게 윈도우 이름을 프로그램
		목적에 맞게 변경하는 것이 가능하다.)
		"""
		self.show()
		self.setWindowTitle("Example 9")
		
		"""
		Geometry vs Frame Geometry
		- Geometry 는 윈도우의 타이틀 바를 제외한 영역을 의미하며 이를 클라이언트 영역이라고 한다. 반면, Frame Geometry 는 윈도우의
		타이틀 바를 포함한 영역을 의미하기 때문에 이를 윈도우 영역이라고 부른다. (즉, 일반적인 GUI 위젯은 클라이언트 영역에 배치 된다는 것을
		알 수 있다.)

		QDesktopWidget 이란?
		- 윈도우즈 플랫폼의 바탕화면 (Mac 플랫폼에서는 Finder) 을 제어 할 수 있는 클래스를 의미한다. (즉, 해당 클래스를 활용하면 현재
		디스플레이의 해상도 등을 가져오는 것이 가능하다.)
		"""
		# 영역을 설정한다
		nHeight = self.frameGeometry().height() - self.geometry().height()
		oDesktopGeometry = QDesktopWidget().availableGeometry()
		
		self.setGeometry(10, 10 + nHeight, oDesktopGeometry.width() // 2, oDesktopGeometry.height() // 2)
		
		"""
		메뉴란?
		- 윈도우 바 하단에 위치하는 버튼 위젯을 의미한다. (즉, 메뉴를 활용하면 윈도우에서 공통적으로 사용 되는 기능들을 버튼 형태로 표현하는
		것이 가능하다.)

		PyQt 에서는 특정 메뉴가 선택 되었을 경우 해당 메뉴를 처리하기 위해서 액션 객체가 필요하다. (즉, 액션 객체는 메뉴 버튼과 이를 처리하기
		위한 메서드를 연결해주는 객체라는 것을 알 수 있다.)
		"""
		# 메뉴를 설정한다
		oAction = QAction("About", self)
		oAction.triggered.connect(self.OnClickAboutMenu)
		
		"""
		setNativeMenuBar 메서드는 운영체제가 기본적으로 제공하는 메뉴 바를 비활성 시키는 역할을 수행한다. (즉, 기본적인 메뉴 바를
		비활성 시킬 수 있다는 것을 알 수 있다.)
		"""
		oMenuBar = self.menuBar()
		oMenuBar.setNativeMenuBar(False)
		
		"""
		addMenu 메서드는 새로운 메뉴를 추가 시키는 역할을 수행한다. (즉, 해당 메서드를 활용하면 프로그램 목적에 맞게 다양한 메뉴를
		손쉽게 추가하는 것이 가능하다.)
		"""
		oFileMenu = oMenuBar.addMenu("File")
		oFileMenu.addAction(oAction)
	
	# 정보 메뉴를 눌렀을 경우
	def OnClickAboutMenu(self):
		"""
		QMessageBox 클래스는 알림 창을 제어하는 역할을 수행한다. (즉, 해당 클래스를 활용하면 특정 동작의 진행 여부를 사용자로부터 입력받거나
		반대로 사용자에게 특정 연산 결과를 알리는 것이 가능하다.)
		
		해당 클래스는 aboutQt 이외에도 다양한 형태의 알림 창을 지원하기 때문에 프로그램의 목적에 맞는 적절한 알림 창을 생성 및 출력하는
		것이 가능하다.
		"""
		QMessageBox.aboutQt(self, "알림")
	
	# 초기화
	@classmethod
	def Start(cls, args):
		"""
		QApplication 클래스는 PyQt 를 사용해서 제작 된 앱을 관리하는 역할을 수행한다. (즉, 해당 클래스는 단순히 앱을 생성하고 실행하는
		용도 이외에는 거의 사용되지 않는다는 것을 알 수 있다.)
		"""
		oApp = QApplication(args)
		
		"""
		try ~ except ~ finally 구문은 Python 으로 제작 된 프로그램이 실행 중에 발생하는 예외 (의도치 않은 동작) 를 처리하는 것이
		가능하다.
		
		try 구문은 예외를 감지하기 위한 구문을 의미하며 해당 구문 내부에서 발생한 예외는 except 구문에서 처리하는 것이 가능하다. 또한,
		예외는 raise 키워드를 통해서 명시적으로 발생시키는 것이 가능하다. (즉, 예외를 명시적으로 발생시킴으로서 프로그램을 제작하는 과정
		중에 발생하는 의도칙 않은 동작을 사전에 방지 할 수 있다는 것을 알 수 있다.)
		
		finally 구문은 try 구문 내부에서 예외가 발생하더라도 반드시 실행되는 구문을 의미한다. (즉, 해당 구문을 활용하면 스트림 해제와 같은
		컴퓨터 자원 처리 구문을 좀 더 안전하게 작성하는 것이 가능하다.)
		"""
		try:
			oExample = CE01Example_09()
		
		finally:
			"""
			sys.exit 메서드는 프로그램을 종료시키는 역할을 수행한다. (즉, 해당 메서드를 호출하면 프로그램은 처리 중인 작업을 모두 종료한
			후 즉시 프로그램이 종료시킨다는 것을 알 수 있다.)
			
			QApplication 클래스를 통해 생성 된 객체는 exec 메서드를 통해서 구동시키는 것이 가능하다. (즉, 해당 메서드를 호출하지 않으면
			PyQt 를 통해 제작 된 프로그램이 동작하지 않는다는 것을 알 수 있다.)
			"""
			sys.exit(oApp.exec())
