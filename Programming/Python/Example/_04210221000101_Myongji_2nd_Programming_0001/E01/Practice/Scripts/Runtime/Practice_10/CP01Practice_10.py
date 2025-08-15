import os
import sys

from PyQt5 import *
from PyQt5 import uic
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *

"""
과제 10 - 1
- PyQt 를 이용한 그림판 제작하기
- 도형의 종류는 4 가지 (+ 펜, 직선, 타원, 직사각형)
- 윈도우 창에는 도형 종류를 변경 할 수 있게 4 개의 버튼 배치 (+ 위치 및 크기는 자유롭게 설정)
- 마우스로 윈도우 영역의 특정 위치를 누른 후 드래그 할 경우 마우스의 위치에 맞게 선택한 도형이 그려져야한다
"""

oPath_Dir = os.path.dirname(__file__)
oPath_Resources = f"{oPath_Dir}/../../../Resources/Practice_10/P01MainWindow_10.ui"

# Practice 10
class CP01Practice_10(QMainWindow, uic.loadUiType(oPath_Resources)[0]):
	# 도구
	class EP10Tool:
		NONE = -1
		PEN = 0
		LINE = 1
		ELLIPSE = 2
		RECTANGLE = 3
		MAX_VAL = RECTANGLE + 1
	
	# 생성자
	def __init__(self):
		# 멤버 변수를 설정한다
		self.m_eSelTool = CP01Practice_10.EP10Tool.PEN
		self.m_oPosList = []
		
		super().__init__()
		self.__init__practice_10__()
	
	# 초기화
	def __init__practice_10__(self):
		self.show()
		self.setupUi(self)
		self.setWindowTitle("Practice 10")
		
		# 영역을 설정한다
		nHeight = self.frameGeometry().height() - self.geometry().height()
		self.setGeometry(10, 10 + nHeight, self.geometry().width(), self.geometry().height())
		
		# 타이머를 설정한다
		self.m_oTimer = QTimer(self)
		self.m_oTimer.timeout.connect(self.OnUpdate)
		
		self.m_oTimer.start(1)
		
		# 메뉴 바를 설정한다
		self.menuBar().setNativeMenuBar(False)
		self.actionAbout.triggered.connect(self.OnClickAboutMenu)
		
		# 버튼을 설정한다
		self.m_oPenBtn = QPushButton("펜", self.centralWidget())
		self.m_oPenBtn.setGeometry(10, 10, 150, 35)
		self.m_oPenBtn.clicked.connect(lambda: self.OnClickToolBtn(CP01Practice_10.EP10Tool.PEN))
		
		self.m_oLineBtn = QPushButton("직선", self.centralWidget())
		self.m_oLineBtn.setGeometry(170, 10, 150, 35)
		self.m_oLineBtn.clicked.connect(lambda: self.OnClickToolBtn(CP01Practice_10.EP10Tool.LINE))
		
		self.m_oEllipseBtn = QPushButton("타원", self.centralWidget())
		self.m_oEllipseBtn.setGeometry(330, 10, 150, 35)
		self.m_oEllipseBtn.clicked.connect(lambda: self.OnClickToolBtn(CP01Practice_10.EP10Tool.ELLIPSE))
		
		self.m_oRectangleBtn = QPushButton("사각형", self.centralWidget())
		self.m_oRectangleBtn.setGeometry(490, 10, 150, 35)
		self.m_oRectangleBtn.clicked.connect(lambda: self.OnClickToolBtn(CP01Practice_10.EP10Tool.RECTANGLE))
	
	# 상태를 갱신한다
	def OnUpdate(self):
		self.update()
	
	# 그리기 이벤트를 수신했을 경우
	def paintEvent(self, a_oEvent: QPaintEvent):
		oPainter = QPainter(self)
		
		try:
			nNumPositions = len(self.m_oPosList)
			
			# 위치 정보가 없을 경우
			if nNumPositions < 2:
				return
			
			# 펜 일 경우
			if self.m_eSelTool == CP01Practice_10.EP10Tool.PEN:
				for i in range(0, nNumPositions - 1):
					oPainter.drawLine(self.m_oPosList[i], self.m_oPosList[i + 1])
			
			# 직선 일 경우
			elif self.m_eSelTool == CP01Practice_10.EP10Tool.LINE:
				oPainter.drawLine(self.m_oPosList[0], self.m_oPosList[nNumPositions - 1])
			
			# 타원 일 경우
			elif self.m_eSelTool == CP01Practice_10.EP10Tool.ELLIPSE:
				oPainter.drawEllipse(QRect(self.m_oPosList[0], self.m_oPosList[nNumPositions - 1]))
			
			# 사각형 일 경우
			elif self.m_eSelTool == CP01Practice_10.EP10Tool.RECTANGLE:
				oPainter.drawRect(QRect(self.m_oPosList[0], self.m_oPosList[nNumPositions - 1]))
		
		finally:
			oPainter.end()
	
	# 닫기 이벤트를 수신했을 경우
	def closeEvent(self, a_oEvent: QCloseEvent):
		nResult = QMessageBox.question(self, "알림", "앱을 종료하시겠습니까?", QMessageBox.Yes | QMessageBox.No, QMessageBox.Yes)
		a_oEvent.accept() if nResult == QMessageBox.Yes else a_oEvent.ignore()
	
	# 키 눌림 이벤트를 수신했을 경우
	def keyPressEvent(self, a_oEvent: QKeyEvent):
		# Esc 키를 눌렀을 경우
		if a_oEvent.key() == Qt.Key_Escape:
			self.close()
	
	# 마우스 이동 이벤트를 수신했을 경우
	def mouseMoveEvent(self, a_oEvent: QMouseEvent):
		self.m_oPosList.append(QPoint(a_oEvent.x(), a_oEvent.y()))
	
	# 마우스 버튼 눌림 이벤트를 수신했을 경우
	def mousePressEvent(self, a_oEvent: QMouseEvent):
		self.m_oPosList.clear()
		self.m_oPosList.append(QPoint(a_oEvent.x(), a_oEvent.y()))
	
	# 마우스 버튼 눌림 종료 이벤트를 수신했을 경우
	def mouseReleaseEvent(self, a_oEvent: QMouseEvent):
		self.mouseMoveEvent(a_oEvent)
	
	# 정보 메뉴를 눌렀을 경우
	def OnClickAboutMenu(self):
		QMessageBox.aboutQt(self, "알림")
	
	# 툴 버튼을 눌렀을 경우
	def OnClickToolBtn(self, a_eTool: EP10Tool):
		self.m_eSelTool = a_eTool
		self.m_oPosList.clear()
	
	# 초기화
	@classmethod
	def Start(cls, args):
		oApp = QApplication(args)
		oPractice = CP01Practice_10()
		
		sys.exit(oApp.exec())
