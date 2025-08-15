import os
import sys

from PyQt5 import *
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *

from ......Scripts.Runtime.Global.Utility.Manager.CTimeManager import *


# 볼
class CP01Ball_15:
	# 생성자
	def __init__(self, a_oMainWnd: QMainWindow):
		self.m_fRadius = 15.0
		self.m_oMainWnd = a_oMainWnd
		
		self.m_oPos = QPointF(0.0, 0.0)
		self.m_oVelocity = QPointF(0.0, 0.0)
		
	# 상태를 갱신한다
	def OnUpdate(self):
		fMinPosX = self.m_fRadius
		fMaxPosX = self.m_oMainWnd.geometry().width() - self.m_fRadius
		
		fMinPosY = self.m_oMainWnd.menuBar().geometry().height() + self.m_fRadius
		fMaxPosY = self.m_oMainWnd.geometry().height() - self.m_fRadius
		
		self.m_oPos += self.m_oVelocity * CTimeManager.GetInst().GetDeltaTime()
		self.m_oPos.setX(min(fMaxPosX, max(fMinPosX, self.m_oPos.x())))
		self.m_oPos.setY(min(fMaxPosY, max(fMinPosY, self.m_oPos.y())))
		
		# 수직 방향 변경이 필요 할 경우
		if self.m_oPos.x() <= fMinPosX or self.m_oPos.x() >= fMaxPosX:
			self.m_oVelocity.setX(self.m_oVelocity.x() * -1.0)
			
		# 수평 방향 변경이 필요 할 경우
		if self.m_oPos.y() <= fMinPosY or self.m_oPos.y() >= fMaxPosY:
			self.m_oVelocity.setY(self.m_oVelocity.y() * -1.0)
	
	# 터렛을 그린다
	def OnPaint(self, a_oPainter: QPainter):
		a_oPainter.drawEllipse(self.m_oPos, self.m_fRadius, self.m_fRadius)
		