import os
import sys
import math

from PyQt5 import *
from PyQt5 import uic
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *

from ......Scripts.Runtime.Global.Utility.Manager.CTimeManager import *
from ......Scripts.Runtime.Global.Utility.Manager.CInputManager import *


# 플레이어
class CE01Player_13:
	# 생성자
	def __init__(self):
		self.m_fSpeed = 0.0
		self.m_fRadius = 0.0
		
		self.m_oPos = QPointF(0.0, 0.0)
	
	# 상태를 갱신한다
	def Update(self):
		oDirection = QPointF(0.0, 0.0)
		
		# 위쪽/아래쪽 방향 키를 눌렀을 경우
		if CInputManager.GetInst().IsKeyDown(Qt.Key_Up) or CInputManager.GetInst().IsKeyDown(Qt.Key_Down):
			oDirection.setY(-1.0 if CInputManager.GetInst().IsKeyDown(Qt.Key_Up) else 1.0)
		
		# 왼쪽/오른쪽 방향 키를 눌렀을 경우
		if CInputManager.GetInst().IsKeyDown(Qt.Key_Left) or CInputManager.GetInst().IsKeyDown(Qt.Key_Right):
			oDirection.setX(-1.0 if CInputManager.GetInst().IsKeyDown(Qt.Key_Left) else 1.0)
		
		try:
			fLength = math.sqrt(math.pow(oDirection.x(), 2.0) + math.pow(oDirection.y(), 2.0))
			oDirection = QPointF(oDirection.x() / fLength, oDirection.y() / fLength)
			
			self.m_oPos += (oDirection * self.m_fSpeed) * CTimeManager.GetInst().GetDeltaTime()
		except Exception as oException:
			pass
	
	# 플레이어를 그린다
	def Draw(self, a_oPainter: QPainter):
		a_oPainter.drawEllipse(self.m_oPos, self.m_fRadius, self.m_fRadius)


# 총알
class CE01Bullet_13:
	# 생성자
	def __init__(self):
		self.m_fSpeed = 0.0
		self.m_fRadius = 0.0
		
		self.m_oPos = QPointF(0.0, 0.0)
		self.m_oDirection = QPointF(0.0, 0.0)
	
	# 상태를 갱신한다
	def Update(self):
		fLength = math.sqrt(math.pow(self.m_oDirection.x(), 2.0) + math.pow(self.m_oDirection.y(), 2.0))
		oDirection = QPointF(self.m_oDirection.x() / fLength, self.m_oDirection.y() / fLength)
		
		self.m_oPos += (oDirection * self.m_fSpeed) * CTimeManager.GetInst().GetDeltaTime()
	
	# 총알을 그린다
	def Draw(self, a_oPainter: QPainter):
		a_oPainter.setBrush(QBrush(QColor(255, 0, 0, 255)))
		a_oPainter.drawEllipse(self.m_oPos, self.m_fRadius, self.m_fRadius)
