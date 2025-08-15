import os
import sys

from PyQt5 import *
from PyQt5 import uic
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *

from .CE01Shape_12 import *
from ......Scripts.Runtime.Global.Utility.Manager.CTimeManager import *
from ......Scripts.Runtime.Global.Utility.Manager.CInputManager import *


oPath_Dir = os.path.dirname(__file__)
oPath_Resources = f"{oPath_Dir}/../../../Resources/Example_12/E01Example_12.ui"

# Example 12
class CE01Example_12(QMainWindow, uic.loadUiType(oPath_Resources)[0]):
	# 생성자
	def __init__(self):
		# 멤버 변수를 설정한다
		self.m_oCircle = CE01Circle_12(50.0)
		
		super().__init__()
		self.__init__example_12__()
	
	# 초기화
	def __init__example_12__(self):
		self.show()
		self.setupUi(self)
		self.setWindowTitle("Example 12")
		
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
		
		# 도형을 설정한다
		self.m_oCircle.SetPos(QPointF(self.geometry().width() / 2.0, self.geometry().height() / 2.0))
		self.m_oCircle.SetDirection(QPointF(1.0, 0.0))
	
	# 상태를 갱신한다
	def OnUpdate(self):
		self.update()
		
		# 관리자를 갱신한다
		CTimeManager.GetInst().Update()
		CInputManager.GetInst().Update()
		
		# 라벨을 갱신한다
		self.labelDeltaTime.setText("Delta Time: {0:0.5f} sec".format(CTimeManager.GetInst().GetDeltaTime()))
		self.labelRunningTime.setText("Running Time: {0:0.5f} sec".format(CTimeManager.GetInst().GetRunningTime()))
		
		# 위치를 갱신한다
		oNextPos = self.m_oCircle.GetPos() + (self.m_oCircle.GetDirection() * 350.0) * CTimeManager.GetInst().GetDeltaTime()
		oNextPos.setX(max(oNextPos.x(), self.m_oCircle.GetRadius()))
		oNextPos.setX(min(oNextPos.x(), self.geometry().width() - self.m_oCircle.GetRadius()))
		
		# 왼쪽 or 오른쪽 영역을 벗어났을 경우
		if oNextPos.x() <= self.m_oCircle.GetRadius() or oNextPos.x() >= self.geometry().width() - self.m_oCircle.GetRadius():
			self.m_oCircle.SetDirection(QPointF(self.m_oCircle.GetDirection().x() * -1.0, self.m_oCircle.GetDirection().y()))
		
		self.m_oCircle.SetPos(oNextPos)
	
	# 그리기 이벤트를 수신했을 경우
	def paintEvent(self, a_oEvent: QPaintEvent):
		oPainter = QPainter(self)
		
		try:
			self.m_oCircle.Draw(oPainter)
		
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
		
		CInputManager.GetInst().AddKey(a_oEvent.key())
	
	# 키 눌림 종료 이벤트를 수신했을 경우
	def keyReleaseEvent(self, a_oEvent: QKeyEvent):
		CInputManager.GetInst().RemoveKey(a_oEvent.key())
	
	# 정보 메뉴를 눌렀을 경우
	def OnClickAboutMenu(self):
		QMessageBox.aboutQt(self, "알림")
	
	# 초기화
	@classmethod
	def Start(cls, args):
		oApp = QApplication(args)
		oExample = CE01Example_12()
		
		sys.exit(oApp.exec())
