   M S B A M L   `   `   `  ����L  HAdivinaQue.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AdivinaQue.Client.ViewsAdivinaQue.Client.Views  5   	   "    AdivinaQue.Client.Views.Game   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AdivinaQue.Client.Views     5   	   -   5          � Title$
  Game��5	   	   $	��700��6   $
��1000��6#   .��5
      � 
������� � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��300 �6   .+�5   
   � $��0��6   .+�5      �� $��Visible=�6   
�������.��5      � -   5         �Name$ wpCards��5      $
��Left=�6.   $	��624��6I   $��	24,22,0,0q�6V   $	��Top=�6i   $	��639��6�   5      5      � 5   
   $��1��6   .+�5      �� $��10,5,0,0q�6   $��Tw Cen MT Condensed)�6&   $��13(�6G   5      �� 5      -   5         ��Name$ 	lbMessage��5      $��48,268,27,304q�6,   $��Center=�6D   $��20(�6a   $��Tw Cen MT Condensed)�6o    J� RenderTransformOrigin$ 0.483,-0.224&�6�   �� 5      -   5         ��Name$ 
lbUserName��5      $
�� AAA��6(   $��10,25,181,600q�67   $��Tw Cen MT Condensed)�6O   $��17(�6p   �� 5      -   5        $ lbPlayerScore��5      $��
Player One��6+   
�������$��Tw Cen MT Condensed)�6S   $��13(�6t   $
��Left=�6�   $	��100��6�   $��
50,100,0,0q�6�   $	��Top=�6�   �� 5      -   5         ��Name$ tbPlayerScore��5      $��0��6.   $��Tw Cen MT Condensed)�67   $��27(�6X    �� TextAlignment$ Center=�6f   ���#efaeb9$
��Left=�6�   $��36��6�   $��
50,120,0,0q�6�   $	��Top=�6�   $	��100��6�   �� 5      -   5        $ lbRivalScore��5      $��	PlayerTwo��6*   
�������$��Tw Cen MT Condensed)�6P   $��13(�6q   $
��Left=�6   $	��100��6�   $��150,100,0,0q�6�   $	��Top=�6�   �� 5      -   5        $ tbRivalScore��5      $��0��6-   $��Tw Cen MT Condensed)�66   $��27(�6W   $ Center=�6e   ���#efaeb9$
��Left=�6�   $��36��6�   $��150,120,0,0q�6�   $	��Top=�6�   $	��100��6�   5      5!      5"      