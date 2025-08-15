import os
import sys

from PyQt5 import *
from PyQt5 import uic
from PyQt5.QtGui import *
from PyQt5.QtCore import *
from PyQt5.QtWidgets import *


# 도구
class CE01Tool_11:
	NONE = -1
	PEN = 0
	LINE = 1
	ELLIPSE = 2
	RECTANGLE = 3
	MAX_VAL = RECTANGLE + 1


# 색상
class CE01Color_11:
	NONE = -1
	DEF = 0
	RED = 1
	GREEN = 2
	BLUE = 3
	MAX_VAL = BLUE + 1


# 도형
class CE01Shape_11:
	"""
	QColor 는 색상 정보를 나타내는 자료형이다. 따라서, 해당 자료형을 활용하면 다양한 색상을 표현하는 것이 가능하며 PyQt 는 색상을
	표현하기 위해서 빛의 삼원색을 이용한 가산법을 사용한다. (즉, R/G/B 3 개의 채널을 이용해서 색상을 표현하고 A 채널을 이용해서 투명도를
	조절하는 것이 가능하다.)

	또한, QColor 는 0 ~ 1 의 범위를 지니는 정규 값이 아닌 0 ~ 255 의 범위를 지니는 정수 형태로 색상을 표현한다. (즉, 총 4 바이트를
	활용해서 색상을 표현한다는 것을 알 수 있다.)
	"""
	PEN_COLOR_LIST = [
		QColor(0, 0, 0, 255), QColor(255, 0, 0, 255), QColor(0, 255, 0, 255), QColor(0, 0, 255, 255)
	]
	
	BRUSH_COLOR_LIST = [
		QColor(0, 0, 0, 0), QColor(255, 0, 0, 255), QColor(0, 255, 0, 255), QColor(0, 0, 255, 255)
	]
	
	# 생성자
	def __init__(self, a_nPenColor: int, a_nBrushColor: int):
		self.m_oPosList = []
		self.m_nPenColor = a_nPenColor
		self.m_nBrushColor = a_nBrushColor
	
	# 위치를 추가한다
	def AddPos(self, a_oPos: QPoint):
		self.m_oPosList.append(a_oPos)
	
	# 도형을 그린다
	def Draw(self, a_oPainter: QPainter):
		"""
		QPen 클래스는 선의 상태를 제어하는 역할을 수행한다. (즉, QPen 을 활용하면 선의 색상이나 두께 등을 변경하는 것이 가능하다.)

		QBrush 클래스는 특정 영역을 색상으로 채우는 역할을 수행한다. (즉, QBrush 를 활용하면 색상을 지니는 사각형 등을 그리는 것이
		가능하다.)
		"""
		a_oPainter.setPen(QPen(CE01Shape_11.PEN_COLOR_LIST[self.m_nPenColor]))
		a_oPainter.setBrush(QBrush(CE01Shape_11.BRUSH_COLOR_LIST[self.m_nBrushColor]))


# 펜
class CE01Pen_11(CE01Shape_11):
	# 도형을 그린다
	def Draw(self, a_oPainter: QPainter):
		super().Draw(a_oPainter)
		
		# 위치 정보가 존재 할 경우
		if len(self.m_oPosList) >= 2:
			for i in range(0, len(self.m_oPosList) - 1):
				a_oPainter.drawLine(self.m_oPosList[i], self.m_oPosList[i + 1])


# 직선
class CE01Line_11(CE01Shape_11):
	# 도형을 그린다
	def Draw(self, a_oPainter: QPainter):
		super().Draw(a_oPainter)
		
		# 위치 정보가 존재 할 경우
		if len(self.m_oPosList) >= 2:
			a_oPainter.drawLine(self.m_oPosList[0], self.m_oPosList[len(self.m_oPosList) - 1])


# 타원
class CE01Ellipse_11(CE01Shape_11):
	# 도형을 그린다
	def Draw(self, a_oPainter: QPainter):
		super().Draw(a_oPainter)
		
		# 위치 정보가 존재 할 경우
		if len(self.m_oPosList) >= 2:
			a_oPainter.drawEllipse(QRect(self.m_oPosList[0], self.m_oPosList[len(self.m_oPosList) - 1]))


# 사각형
class CE01Rectangle_11(CE01Shape_11):
	# 도형을 그린다
	def Draw(self, a_oPainter: QPainter):
		super().Draw(a_oPainter)
		
		# 위치 정보가 존재 할 경우
		if len(self.m_oPosList) >= 2:
			a_oPainter.drawRect(QRect(self.m_oPosList[0], self.m_oPosList[len(self.m_oPosList) - 1]))
