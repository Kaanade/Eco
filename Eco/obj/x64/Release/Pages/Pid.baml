   M S B A M L   `   `   `  ���� >  :Eco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EcoEco      Eco.Pid   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     #localclr-namespace:Eco       N� Title$	  Pid�� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers	 P� Property
 P� Value��%�  '��       �� " ��.���  � �� �#373737��  � ��
 �������  � ��$ 20(���  �@J�SnapsToDevicePixels . ��  � �� �� " ��.R��� #��z��
�������$��2q�$ 8,8,3,3��._�� .+��� $ MyContentPresenter��#��z��$��Center=�$��Center=�$��0,0,0,0q� P� 	 �@J�IsMouseOver
 . . ���  � �� �#E59400��  � ��
 �����P� 	 � ��
 . . ���  � ��
 � E����  � ��
 �����.��]� .+�� $	��Top=�$��40�����#3399FF.+��� -   $	��100��$
��Left=�.��Retour�� -   $	��100��$��Right=�.��Fermer� $	��Top=�$	��120��
������� � ColumnDefinitions �� $��* �.+��� $��Disabled=�$��Visible=�.���� -    ��Name$ procGrid��� -    �Name$ pidGrid��$
��Left=�$	��Top=�$
��Left=�$	��920�� �� $
��1700 ��� $	��220 � � RowDefinitions �� $	��850 ��� $��70 �.+��� -   $ 	canvasPid��$
��1700��$	��850��}�. $��0��$��0��.+�   Eco.ZoomBorder  -     Name$ border��}�. 
������� �� Left$ 0��   Child. �� -   $��myPid��$��UniformToFill=�$
��1700��$
��1200���� $��1��$��0������  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ #99CCFF�� � Offset$ 0Q�� $ #CCE5FF��$	 1.0Q��� $��0��$��1��$��2������ $	 0,0&�$	 0,1&�.��� $ #99CCFF��$ 0Q�� $ #CCE5FF��$	 1.0Q��� -   $	��200��$��1��$
��Left=�.��Ajouter Procédure