   M S B A M L   `   `   `  ����L  HAdivinaQue.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AdivinaQue.Client.ViewsAdivinaQue.Client.Views  5   	   #    AdivinaQue.Client.Views.Podio   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AdivinaQue.Client.Views     5   	   -   5           lbScore  � Title#  �   5   	   $	��550��6+   $	��600��68   .��5	      � 
������� � ColumnDefinitions 5
   
   �� 5      $	��42* �6   �� 5      $��7* �6   �� 5      $
��543* �6   .+�5   
   � ���#c0aee0$��30,70,107,70q�6%   $��2��6;    5      �� 5      $	��200 �6#   61   �� 5      $��* �6#   6/   �� 5      $��10 �6#   60   .+�5      � $��0��6    � RowDefinitions 5      �� 5      $��30 �6$   63   �� 5      $��* �6$   61   .+�5      � $��0��6   .+�5      ��   	lbPlayers#���  6   
���ݠ��$��Tw Cen MT Condensed)�6T   $��18(�6u   6�   5      � 5      $��1��6   .+�5      �� -   5         ��Name$ lvPlayer��5      ���#c0aee06H   5      5       � 5!      $��1��6    5"      �� 5#      $��30 �6$   62   �� 5$      $��* �6$   61   .+�5&      � $��0��6   .+�5'      ��  
 lbWins#���  6   
���ݠ��$��Tw Cen MT Condensed)�6S   $��18(�6t   6�   5(      � 5)      $��1��6   .+�5*      �� -   5        $ 	lvVictory��5*      ���#c0aee06I   5+      � 5,      $��2��6    5-      �� 5.      $��* �6(   65   .+�50      � $��0��6   .+�51      �� -   5        $��ContentScroller��51   '   $��Disabled=�6>   $
��Auto=�6g   $��Tw Cen MT Condensed)�6�   $��18(�6�   6�   52      53      54      56      �� 57   
   -   5          btReturn#���  57      $
��Left=�66   $��Tw Cen MT Condensed)�6R   $��16(�6s   
���ݠ��$��445,477,0,0q�6�   $	��Top=�6�   $��75��6�   $��2��6�   6�   �� 58   
   #���   6   $
��Left=�64   $��
199,14,0,0q�6O   $	��Top=�6c   $��OCR A Extended)�6{   $��20(�6�   $��2��6�   5:      5;      