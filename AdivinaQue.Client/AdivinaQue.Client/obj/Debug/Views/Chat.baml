   M S B A M L   `   `   `  ����L  HAdivinaQue.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AdivinaQue.Client.ViewsAdivinaQue.Client.Views  5   	   "    AdivinaQue.Client.Views.Chat   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AdivinaQue.Client.Views     5   	   -   5          � Title$
  Chat��5   	   $	��450��6   $	��600��6#   .��5
      � 
������� � ColumnDefinitions 5   
   �� 5      $	��200 �6   6-   �� 5      $��* �6   6+   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $��* �6    6-   .+�5      � $��0��6    5      �� 5      $��31 �6$   62   �� 5      $��* �6$   61   .+�5      � $��0��6   .+�5      �� -   5        $��
TitleUsers��5         lbTitleUsers#���   6.   ���#dda0dd$��Right=�6m   $��Bubble Bobble)�6�   $��14(�6�   $	��190��6�   $��0,0,10,0q�6�   5      � 5      $��1��6   $
��Left=�6$   $	��200��6?    5      �� 5       $	��36* �6+   �� 5!      $	��13* �6+   �� 5"      $	��51* �6+   .+�5$      �� -   5        $��UsersConnected��5$      ���#c0aee0$��3��6I   $��0,0,10,0q�6]   5%      5&      5(      � 5*   
   $��1��6    5+      �� 5,      $��31 �6    6.   �� 5-      $��* �6    6-   �� 5.      $��60 �6    6.   .+�50      � $��0��6   .+�51      �� -   5        $��Title��51      
�������$��Right=�6<   $��Bubble Bobble)�6X   $��18(�6s   $	��401��6�   $��-9,0,0,0q�6�   53      �� 54      -   5        $��lbUser��54      $�� ��6&   $��220,0,-220,0q�61   56      � 57      $��1��6    58      �� 59      $
��203* �6'   �� 5:      $	��16* �6'   �� 5;      $	��26* �6'   �� 5<      $
��147* �6'   .+�5>      �� -   5        $��ContentScroller��5>      $��Disabled=�66   $
��Auto=�6_   $��4��6�   6�   �� 5?      -   5         ��Name$ listMessages��5?      
�������$��4��6G   $��
-10,0,10,0q�6[   5A      5D      � 5E      $��2��6   ���#efaeb9$��	-10,0,0,0q�65   .+�5F      ��  �� Orientation$ 
Horizontal=�6   .+�5G      �� -   5         ��Name$ MessageContent��5G      ���#e4b6b3$	��264��6L    �� TextWrapping$
 Wrap=�6X    �� AcceptsReturn . $��10q�6�   �� 5H      -	   5         ��Name$ 
SendButton��5H       
 btSend#���  61   $��14q�6T   $��Center=�6`   $��0q�6}   ���#cf7c77$��80��6�   $��30��6�   6�   5I      5L      5M      5N      5O      