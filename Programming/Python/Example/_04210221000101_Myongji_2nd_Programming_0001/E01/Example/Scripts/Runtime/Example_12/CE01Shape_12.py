import os
import sys

from PyQt5 import *
from PyQt5 import uic
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *


# 도형
class CE01Shape_12:
	# 생성자
	def __init__(self):
		self.m_oPos = QPointF(0.0, 0.0)
		self.m_oDirection = QPointF(0.0, 0.0)
	
	# 위치를 반환한다
	def GetPos(self):
		return self.m_oPos
	
	# 방향을 반환한다
	def GetDirection(self):
		return self.m_oDirection
	
	# 위치를 변경한다
	def SetPos(self, a_oPos: QPointF):
		self.m_oPos = a_oPos
	
	# 방향을 변경한다
	def SetDirection(self, a_oDirection: QPointF):
		self.m_oDirection = a_oDirection
	
	# 도형을 그린다
	def Draw(self, a_oPainter: QPainter):
		pass


# 원
class CE01Circle_12(CE01Shape_12):
	# 생성자
	def __init__(self, a_fRadius: float):
		super().__init__()
		self.__init__circle__(a_fRadius)
	
	# 초기화
	def __init__circle__(self, a_fRadius: float):
		self.m_fRadius = a_fRadius
	
	# 반지름을 반환한다
	def GetRadius(self):
		return self.m_fRadius
	
	# 도형을 그린다
	def Draw(self, a_oPainter: QPainter):
		super().Draw(a_oPainter)
		a_oPainter.drawEllipse(self.m_oPos, self.m_fRadius, self.m_fRadius)
