   M S B A M L   `   `   `  ���� >  :Eco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EcoEco      Eco.Connexion   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     #localclr-namespace:Eco       N� Title$  	Connexion�� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers	 P� Property
 P� Value��%�  '��       �� " ��.���  � �� �#373737��  � ��
 �������  � ��$ 20(���  �@J�SnapsToDevicePixels . ��  � ��$	 110����  � �� �� " ��.R��� #��z��
�������$��2q�$ 3,0,0,0��._�� .+��� $ MyContentPresenter��#��z��$��Center=�$��Center=�$��0,0,0,0q� P� 	 �@J�IsMouseOver
 . . ���  � �� �#E59400��  � ��
 �����P� 	 � ��
 . . ���  � ��
 � E����  � ��
 �����.���� $ 10��
�������$��3q�
�������$��24q� �� Padding$ 4q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$
 Gray��   Opacity$	 .50Q�   ShadowDepth$ 16Q�._��  � ColumnDefinitions �� $	��780 ��� $	��120 ��� $	��240 ��� $��* � � RowDefinitions �� $	��120 ��� $��50 ��� $��50 ��� $��50 ��� .+��� $��1��$��0��$��3��$��18(�$��30q�.��86Veuillez vous connecter pour accéder à l'application�� $��1��$��1��.��	User Name�� -   $��txtUserName��$��2��$��1�� � ToolTip$ Enter Your User Name��$��23��$	��Top=��� $��1��$��2��.��
PasswordG� -   $��txtPassword��$��2��$��2��$ Enter Your Password��$��23��$	��Top=��� $��2��$��3��$��10q�$��Center=� �� Orientation$ 
Horizontal=�.+��� -   $��	btnCancel�� �� IsCancel . $��Annuler��   $
 Gray��$	 .50Q�$ 8Q��� -   $��btnLogin�� �� 	IsDefault . $��	Connexion��   $
 Gray��$	 .50Q�$ 8Q�