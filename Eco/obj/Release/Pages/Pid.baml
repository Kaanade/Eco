   M S B A M L   `   `   `  ���� >  :Eco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EcoEco      Eco.Pid   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     #localclr-namespace:Eco       N� Title$	  Pid�� � 	Resources    	dataTempl
 � Tag��%b   &	        �� .R��� -   $��70��$	��200��$��1q� �� .���� .+��� $��16(����� .��]� .+�� $	��Top=� � ColumnDefinitions �� $	��85* ��� $	��15* � � RowDefinitions �� $��80 ��� $��50 ��� $��1* �.+��� $��0 0 0 1q�$��0���� -   $��labelAvancementSysteme��$	��120��$
��Left=�$��Center=�$��100,10q�� -   $��progressBarAvSys��$	��100��$
��Left=�$��240,0q��� -   $��labelSysteme��$	��100��$��Center=�$��16(��� -   $��imgLogo��$��Right=�$	��200���� -     MetroCircleButtonStyle#���  $
��Left=�$��50��$��50��.���� $��20��$��20������ *
Foreground+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �� J� OpacityMask !�  j� Stretch$
 Fill=�  appbar_arrow_left	 !� Visual#	 [ �� -   
 � ToolTip$
 Minimiser l'application��#���  $��Right=�$��50��$��50��$
��50,0q�$��1��.���� $��20��$��7������ *
Foreground+ �� *FindAncestor+" �� !� $
 Fill=�  appbar_minus#	 [ �� -   $
 Fermer l'application��#���  $��Right=�$��50��$��50��$��1��.���� $��20��$��20������ *
Foreground+ �� *FindAncestor+" �� !� $
 Fill=�  appbar_close#	 [ �� -	   $	��200��$��50��$��1��$��0��$
��Left=�$��000,0q�.��Exporter FT�� -
   $	��200��$��50��$��1��$��0��$
��Left=�$��200,0q�.��Ouvrir Explorer�� -   $	��200��$��50��$��1��$��0��$
��Left=�$��200,0q�$y�	Collapsed=�.��
Voir FNC�� -   $	��200��$��50��$��1��$��0��$
��Left=�$��400,0q�$y�Hidden=�.��Plan qualité�� -    ��Name$ 	canvasPid��}�. $��3��$��0��$
��1720��$	��970��.+�   Eco.ZoomBorder  -     Name$ border��}�. 
������� �� Left$ 0��   Child. �� -   $��myPid��$��UniformToFill=��� -    ��Name$ LineRightButton��$��1��$��3��$��>�� �� LineRightCommand#��Z ����  �� ElementName$ scrollViewerActive���� -   $��ScrollListProc��$��Visible=�$��Disabled=�$��3��$��1����. .���� -   $��templ��#��[  ���� *
listProc+���� .R���  �� Orientation$ Vertical=�$��Disabled=�