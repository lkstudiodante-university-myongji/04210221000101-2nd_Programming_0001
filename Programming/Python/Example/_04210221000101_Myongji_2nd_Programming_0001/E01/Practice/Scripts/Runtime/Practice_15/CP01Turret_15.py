import os
import sys
import math

from PyQt5 import *
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *

from ......Scripts.Runtime.Global.Utility.Manager.CTimeManager import *
from ......Scripts.Runtime.Global.Utility.Manager.CInputManager import *


# 터렛
class CP01Turret_15:
	# 생성자
	def __init__(self, a_oMainWnd: QMainWindow):
		self.m_fAngle = 0.0
		self.m_fRadius = 50.0
		
		self.m_oPos = QPointF(0.0, 0.0)
		self.m_oMainWnd = a_oMainWnd
	
	# 상태를 갱신한다
	def OnUpdate(self):
		self.m_oPos = QPointF(15.0, self.m_oMainWnd.geometry().height() - 15.0)
		
		# 회전 키를 눌렀을 경우
		if CInputManager.GetInst().IsKeyDown(Qt.Key_Up) or CInputManager.GetInst().IsKeyDown(Qt.Key_Down):
			fExtraAngle = -90.0 if CInputManager.GetInst().IsKeyDown(Qt.Key_Up) else 90.0
			
			self.m_fAngle = self.m_fAngle + (fExtraAngle * CTimeManager.GetInst().GetDeltaTime())
			self.m_fAngle = min(0.0, max(-90.0, self.m_fAngle))
	
	# 터렛을 그린다
	def OnPaint(self, a_oPainter: QPainter):
		oDirection = QPointF(math.cos(math.radians(self.m_fAngle)), math.sin(math.radians(self.m_fAngle)))
		oSrcPos = self.m_oPos + (oDirection * self.m_fRadius)
		
		a_oPainter.drawLine(oSrcPos, self.GetShootPos())
		a_oPainter.drawEllipse(self.m_oPos, self.m_fRadius, self.m_fRadius)
	
	# 발사 위치를 반환한다
	def GetShootPos(self):
		oDirection = self.GetShootDirection()
		return self.m_oPos + (oDirection * self.m_fRadius) + oDirection * self.m_fRadius / 2.0
	
	# 발사 방향을 반환한다
	def GetShootDirection(self):
		return QPointF(math.cos(math.radians(self.m_fAngle)), math.sin(math.radians(self.m_fAngle)))
