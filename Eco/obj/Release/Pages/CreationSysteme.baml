   M S B A M L   `   `   `  ���� >  :Eco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EcoEco      Eco.CreationSysteme   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     #localclr-namespace:Eco       N� Title$  CreationSysteme��.��]� .+�� $	��Top=�$��50��.+��� -      MetroCircleButtonStyle#���   $
��Left=�$��50��$��50��.���� $��20��$��20������ *
Foreground+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �� J� OpacityMask !�  j� Stretch$
 Fill=�  appbar_arrow_left !� Visual# [ �� -    ��Name$ button�� � ToolTip$ Fermer l'application��#���   $��Right=�$��50��$��50��.���� $��20��$��20������ *
Foreground+ �� *FindAncestor+" �� !� $
 Fill=�  appbar_close# [ �  � ColumnDefinitions �� $	��640 ��� $	��280 ��� $	��208 ��� $	��152 ��� 	 � RowDefinitions	 �� $	��120 ��� $	��120 ��� $��50 ��� $��50 ��� $��50 ��� $��70 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� .+��� $��0��$��0��$��50������ 
 �� 
StartPoint$	
 0,0&� �� EndPoint$	 0,1&�.���  � Color$ #99CCFF�� � Offset$ 0Q�� $ #CCE5FF��$	 1.0Q��� $��4��$��0��$��50������ $	
 0,0&�$	 0,1&�.��� $ #99CCFF��$ 0Q�� $ #CCE5FF��$	 1.0Q��� 
���   �$��1q�$��1��$��0��$��3��$��13���� $��1��$��1��$��4��$��18(�$��30q�.��Création du Système�� $��1��$��2��$
��20,0q�.��Nom système :�� -   $��txtNomSysteme��$��2��$��2��$  Entrez le nom sus système��$��23��$	��Top=��� $��1��$��3��$
��20,0q�.��Doc système :�� -   $��
txtPathDoc��$��2��$��3��$��23��$	��Top=��� -   $��3��$��3��$H BChoisissez un fichier ou dossier pour la documentation du système��$��28��$	��Top=�.��Parcourir...�� $��1��$��4��$
��20,0q�.��PID système :�� -   $��
txtPathPID��$��2��$��4��$��23��$	��Top=��� -   $	��btn��$��3��$��4��$3 -Choisissez un fichier pour le PID du système��$��28��$	��Top=�.��Parcourir...�� $��1��$��11��$��30,10,10,10q�$��Center=� �� Orientation$ 
Horizontal=�$	��240��.+��� -   $��	btnCancel�� �� IsCancel . $��Annuler��$	��100�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$
 Gray��   Opacity$	 .50Q�   ShadowDepth$ 8Q��� -	   $��btnLogin�� �� 	IsDefault . $��Valider��$��2��$	��100��   $
 Gray��$	 .50Q�$ 8Q�