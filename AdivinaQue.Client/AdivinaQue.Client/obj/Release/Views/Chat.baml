   M S B A M L   `   `   `  ���� L  HAdivinaQue.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AdivinaQue.Client.ViewsAdivinaQue.Client.Views  "    AdivinaQue.Client.Views.Chat   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7local%clr-namespace:AdivinaQue.Client.Views     -     � Title$
  Chat��$	��450��$	��600��.��� 
������� � ColumnDefinitions �� $	��200 ��� $��* �.+�� $��0�� � RowDefinitions �� $��* �.+�� $��0�� �� $��31 ��� $��* �.+�� $��0��.+��� -   $��
TitleUsers��   lbTitleUsers#���   ���#dda0dd$��Right=�$��Bubble Bobble)�$��14(�$	��190��$��0,0,10,0q�� $��1��$
��Left=�$	��200�� �� $	��36* ��� $	��13* ��� $	��51* �.+��� -   $��UsersConnected�����#c0aee0$��3��$��0,0,10,0q�� $��1�� �� $��31 ��� $��* ��� $��60 �.+�� $��0��.+��� -   $��Title��
�������$��Right=�$��Bubble Bobble)�$��18(�$	��401��$��-9,0,0,0q��� -   $��lbUser��$�� ��$��220,0,-220,0q�� $��1�� �� $
��203* ��� $	��16* ��� $	��26* ��� $
��147* �.+��� -   $��ContentScroller��$��Disabled=�$
��Auto=�$��4���� -    ��Name$ listMessages��
�������$��4��$��
-10,0,10,0q�� $��2�����#efaeb9$��	-10,0,0,0q�.+���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ MessageContent�����#e4b6b3$	��264�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . $��10q��� -	    ��Name$ 
SendButton�� 
 btSend#���  $��14q�$��Center=�$��0q����#cf7c77$��80��$��30��