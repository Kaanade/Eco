   M S B A M L   `   `   `  ���� >  :Eco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EcoEco      Eco.Affecter   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     #localclr-namespace:Eco       N� Title$  Affecter��.��]� .+�� $	��Top=�$��50��.+��� -      MetroCircleButtonStyle#���   $
��Left=�$��50��$��50��.���� $��20��$��20������ *
Foreground+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �� J� OpacityMask !�  j� Stretch$
 Fill=�  appbar_arrow_left !� Visual# [ �� -    ��Name$ button�� � ToolTip$ Fermer l'application��#���   $��Right=�$��50��$��50��.���� $��20��$��20������ *
Foreground+ �� *FindAncestor+" �� !� $
 Fill=�  appbar_close# [ � -    �Name$ GridSlideMenu��$	��200��.+�&   System.Windows.Controls.DataGrid  -   $��ListProjects��	   AutoGenerateColumns	 .  
   CanUserAddRows
 .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Installation��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *nomInstallation+����  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .���  �� Property � �� �� Value �#000000��  � �� �#000000��  � ��$ 50��-    �� Triggers P�  P� Property �@ 
IsSelected P� Value . . ���  � �� �#FF2626P�  �@��AlternationIndex$ 0��. ���  � �� �#FF9933P�  �@��AlternationIndex$ 1��. ���  � �� �#FFB266���� "  .���   �@ AlternatingRowBackground
 ��  ��  � RowDefinitions �� $��* � � ColumnDefinitions �� -    �Name$ slidebutton��$��15 �.+��� -   $��1��� .+�  -   $��	ListUsers��	 .  
 .     $	 Nom��$	 200  �� *nomUtilisateur+  $ Prenom��$	 200  �� *prenomUtilisateur+  $ Fonction��$	 200  �� *
fonction+���� "  .���  � �� �#000000��  � �� �#000000��  � ��$ 50��-	    P�  �@ 
IsSelected . . ���  � �� �#FF2626P�  �@��AlternationIndex$ 0��. ���  � �� �#66B2FFP�  �@��AlternationIndex$ 1��. ���  � �� �#99CCFF���� "  .���   �@ AlternatingRowBackground
 ��  �� $��Bottom=�$��40�����#3399FF J� RenderTransformOrigin$ 0.79,-1.578&�$��
3,990,3,10q�.+��� $��
-10,0,10,0q��� -
   $	��170��$��Center=�.��Créer Intervenant