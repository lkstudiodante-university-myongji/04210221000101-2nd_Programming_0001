import os
import sys

from PyQt5 import *
from PyQt5 import uic
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *


oPath_Dir = os.path.dirname(__file__)
oPath_Resources = f"{oPath_Dir}/../../../Resources/Example_10/E01Example_10.ui"

# Example 10
class CE01Example_10(QMainWindow, uic.loadUiType(oPath_Resources)[0]):
	# 생성자
	def __init__(self):
		# 멤버 변수를 설정한다
		self.m_oSrcPos = QPoint(0, 0)
		self.m_oDestPos = QPoint(0, 0)
		
		super().__init__()
		self.__init__example_10__()
	
	# 초기화
	def __init__example_10__(self):
		self.show()
		self.setupUi(self)
		self.setWindowTitle("Example 10")
		
		# 영역을 설정한다
		nHeight = self.frameGeometry().height() - self.geometry().height()
		self.setGeometry(10, 10 + nHeight, self.geometry().width(), self.geometry().height())
		
		"""
		QTimer 는 특정 메서드를 일정 시간 간격으로 지속적으로 호출해주는 역할을 수행한다. (즉, 해당 클래스를 이용하면 실시간으로 처리가
		필요한 프로그램을 제작하는 것이 가능하다.)
		
		일반적인 프로그램은 사용자가 아무런 입력을 하지 않을 경우 프로그램 또한 아무런 작업도 수행하지 않지만 게임과 같은 실시간 프로그램은
		사용자의 입력 여부와 상관 없이 프로그램이 항상 특정 작업을 수행 할 필요가 있다.
		
		따라서, PyQt 는 QTimer 클래스를 제공하며 해당 클래스를 활용하면 일정 시간마다 특정 메서드를 호출함으로서 실시간 프로그램을
		제작하는 것이 가능하다.
		"""
		# 타이머를 설정한다
		self.m_oTimer = QTimer(self)
		self.m_oTimer.timeout.connect(self.OnUpdate)
		
		self.m_oTimer.start(1)
		
		# 메뉴 바를 설정한다
		self.menuBar().setNativeMenuBar(False)
		self.actionAbout.triggered.connect(self.OnClickAboutMenu)
	
	# 상태를 갱신한다
	def OnUpdate(self):
		"""
		update 메서드는 내부적으로 paintEvent 를 발생시키는 역할을 수행한다. (즉, 해당 메서드를 호출함으로서 실시간으로 QMainWindow
		상에 특정 그래픽을 그리는 것이 가능하다는 것을 알수 있다.)
		"""
		self.update()
	
	"""
	paintEvent 메서드는 PyQt 위젯 상태가 변경 되었을 경우 호출된다. (즉, 해당 메서드는 변경 된 위젯 상태에 맞게 특정 위젯을 다시
	화면 상에 그리는 역할을 수행한다는 것을 알 수 있다.)
	"""
	
	# 그리기 이벤트를 수신했을 경우
	def paintEvent(self, a_oEvent: QPaintEvent):
		"""
		QPainter 클래스는 PyQt 위젯 상에 특정 그래픽을 그리는 역할을 수행한다. (Ex. 선, 이미지 등등...)
		
		PyQt 위젯에 특정 그래픽을 그리기 위해서는 반드시 해당 클래스를 활용해야하며 해당 클래스는 paintEvent 메서드 내부에서
		사용 가능하다.
		
		또한, 더이상 그릴 그래픽이 없을 경우 end 메서드를 호출해줘야한다. (즉, 해당 메서드를 호출함으로서 특정 그래픽 출력을 완료했다는
		사실을 PyQt 에게 알려 줄 필요가 있다.)
		"""
		oPainter = QPainter(self)
		
		try:
			"""
			QPainter 클래스는 drawLine 과 같은 특정 그래픽을 그릴 수 있는 메서드를 제공한다. 따라서, QPainter 클래스가 제공하는
			여러 메서드를 활용하면 다양한 그래픽 처리가 가능하다는 것을 알 수 있다.
			"""
			oPainter.drawLine(self.m_oSrcPos, self.m_oDestPos)
		
		finally:
			oPainter.end()
	
	"""
	closeEvent 는 QMainWindow 가 닫힐 경우 호출되는 메서드이다. 단, 해당 메서드에 전달되는 QCloseEvent 를 통해 윈도우 창의 닫힘
	이벤트를 무시하는 것이 가능하다. (즉, QCloseEvent 의 accept 메서드를 호출하면 닫힘 이벤트를 수락하겠다는 것을 의미하며 이는
	곧 윈도우 창이 화면 상에서 사라진다는 것을 알 수 있다.)
	"""
	
	# 닫기 이벤트를 수신했을 경우
	def closeEvent(self, a_oEvent: QCloseEvent):
		nResult = QMessageBox.question(self, "알림", "앱을 종료하시겠습니까?", QMessageBox.Yes | QMessageBox.No, QMessageBox.Yes)
		a_oEvent.accept() if nResult == QMessageBox.Yes else a_oEvent.ignore()
	
	"""
	keyPressEvent 및 keyReleaseEvent 메서드는 키보드의 특정 키가 눌렸을 경우 호출되는 메서드이다.
	"""
	
	# 키 눌림 이벤트를 수신했을 경우
	def keyPressEvent(self, a_oEvent: QKeyEvent):
		# Esc 키를 눌렀을 경우
		if a_oEvent.key() == Qt.Key_Escape:
			"""
			close 메서드는 QMainWindow 를 닫는 역할을 수행한다. (즉, 해당 메서드를 호출하면 내부적으로 closeEvent 메서드가 호출
			된다는 것을 알 수 있다.)
			"""
			self.close()
	
	"""
	mouseMoveEvent, mousePressEvent 및 mouseReleaseEvent 메서드는 마우스가 이동 되었거나 버튼이 눌렸을 경우 호출되는 메서드이다.
	"""
	
	# 마우스 이동 이벤트를 수신했을 경우
	def mouseMoveEvent(self, a_oEvent: QMouseEvent):
		self.m_oDestPos = QPoint(a_oEvent.x(), a_oEvent.y())
	
	# 마우스 버튼 눌림 이벤트를 수신했을 경우
	def mousePressEvent(self, a_oEvent: QMouseEvent):
		self.m_oSrcPos = self.m_oDestPos = QPoint(a_oEvent.x(), a_oEvent.y())
	
	# 마우스 버튼 눌림 종료 이벤트를 수신했을 경우
	def mouseReleaseEvent(self, a_oEvent: QMouseEvent):
		self.mouseMoveEvent(a_oEvent)
	
	# 정보 메뉴를 눌렀을 경우
	def OnClickAboutMenu(self):
		QMessageBox.aboutQt(self, "알림")
	
	# 초기화
	@classmethod
	def Start(cls, args):
		oApp = QApplication(args)
		oExample = CE01Example_10()
		
		sys.exit(oApp.exec())
