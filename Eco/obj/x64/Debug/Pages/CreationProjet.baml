   M S B A M L   `   `   `  ����>  :Eco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EcoEco  5          Eco.CreationProjet   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:Eco     5        N� Title$  CreationProjet��5	       � 	Resources 5
       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers	 P� Property
 P� Value��%.  '��       �� 5   
   " ��6   .�5      ��  � �� �#3737376+   �� 5       � ��
 �����6+   �� 5       � ��$ 20(�6)   �� 5       �@J�SnapsToDevicePixels . 64   �� 5       � ��$	 110��6&   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6"   
�������$��2q�6a   $ 3,0,0,0��6u   ._�5      � .+�5   "   �� $ MyContentPresenter��63   #��z��6O   $��Center=�6s   $��Center=�6�   $��0,0,0,0q�6�   5      5       5      P� 5      	 �@J�IsMouseOver
 . . �5   "   ��  � �� �#E594006?   �� 5   "    � ��
 �����6?   5      P� 5!      	 � ��
 . . �5"   "   ��  � ��
 � E��6?   �� 5#   "    � ��
 �����6?   5$      5&      5(      5)      5*      .��5+      �  � ColumnDefinitions 5,   
   �� 5-      $	��100 �6   �� 5.      $	��120 �6   �� 5/      $	��240 �6   �� 50      $��* �6    � RowDefinitions 52   
   �� 53      $	��120 �6   �� 54      $��50 �6   �� 55      $��50 �6   �� 56      $��50 �6   �� 57      .+�59   
   �� $��1��6   $��0��6!   $��3��6.   $��18(�6C   $��30q�6Q   .��6]   Création de Projet6r   �� 5:   
   $��1��6   $��1��6"   .��6/   Nom de l'installation6F   �� 5;   
   -   5        $��txtNomInstallation��5;   [   $��2��6   $��1��6#    � ToolTip$% Entrez le nom de l'installation��60   $��23��6u   $	��Top=�6�   �� 5<   
   $��2��6   $��3��6&   $��10q�64   $��Center=�6A    �� Orientation$ 
Horizontal=�6_   .+�5=      �� -   5        $��	btnCancel��5=       �� IsCancel . $��Annuler��68    J� Effect 5>      3  -System.Windows.Media.Effects.DropShadowEffect  5?         Color$
 Gray��6'      Opacity$	 .50Q�64      ShadowDepth$ 8Q�6C   5A      �� 5B      -   5        $��btnLogin��5B       �� 	IsDefault . $��Valider��68    5C        5D      $
 Gray��6'   $	 .50Q�5E      $ 8Q�5F      5H      5I      5J      5K      