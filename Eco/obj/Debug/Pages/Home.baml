   M S B A M L   `   `   `  ����>  :Eco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EcoEco  5          Eco.Home   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:Eco     5        N� Title$
  Home��5	       � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers	 P� Property
 P� Value��%	  '��       �� 5   
   " ��6   .�5      ��  � �� �#3737376+   �� 5       � ��
 �����6+   �� 5       � ��$ 20(�6)   �� 5       �@J�SnapsToDevicePixels . 64   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6"   
�������$��2q�6a   $ 8,8,3,3��6u   ._�5      � .+�5   "   �� $ MyContentPresenter��63   #��z��6O   $��Center=�6s   $��Center=�6�   $��0,0,0,0q�6�   5      5       5      P� 5      	 �@J�IsMouseOver
 . . �5   "   ��  � �� �#E594006?   �� 5   "    � ��
 �����6?   5       P� 5"      	 � ��
 . . �5#   "   ��  � ��
 � E��6?   �� 5$   "    � ��
 �����6?   5%      5'      5)      5+      5,      .��5.      ]� .+�5/   
   � $	��Top=�6   $��50��6$   .+�50      �� -   5         � ToolTip$ Minimiser l'application��50         MetroCircleButtonStyle#���   68   $��Right=�6i   $��50��6�   $��50��6�   $
��50,0q�6�   $��1��6�   .��51      �� $��20��6   $��7��6'   ��62   �� *
Foreground+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �� J� OpacityMask 52      !� 53       j� Stretch$
 Fill=�6&     appbar_minus !� Visual# [ 65   55      56      �� 57      -   5         ��Name$ button��57   4   $ Fermer l'application��6   #���   6D   $��Right=�6u   $��50��6�   $��50��6�   .��58      �� $��20��6   $��20��6'   ��63   �� *
Foreground+ �� *FindAncestor+" �� 59      !� 5:      $
 Fill=�6&     appbar_close# [ 65   5<      5=      5>      � 5?   
    � ColumnDefinitions 5@      �� 5A      $��5* �6#   �� 5B      $��5* �6#   .+�5D      `� $	��Top=�6   $��2��6/   5E      �� 5F      -   5        $	��Top=�5F      $��0��6-   $	��520��6=   $��Stretch=�6K   ��5G      �� 5H      .+�5I      �� $��	Préparer��6$   5J      5L      �� 5M      -   5        $	��Top=�5M      $��1��6-   $	��520��6>   $��Stretch=�6K   .��5N      �� .+�5O      �� $��	Réaliser��6    5P      5Q      5T      5U      5X      