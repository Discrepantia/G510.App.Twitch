   M S B A M L   `   `   `  ����C  ?G510.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:G510.AppG510.App  5   	   7 clr-namespace:G510.App.ConverterG510.App.Converter  5   	       G510.App.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   (localclr-namespace:G510.App     5   	   5cnverter clr-namespace:G510.App.Converter     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � DataContext 5      #   G510.App.MainWindow_ViewModel  5   
    � 	Resources 5      /   )G510.App.Converter.MultiBinding_Converter   MultiBinding_Converter��%   &	          5   
   6*   5      .��5      � .+�5   
   �� .@�5      �� $
��G510��6   .��5      � $��5q�6   .+�5      �� .+�5      �� $��	UserName:��6    66   � 5      $��Stretch=�6   $	��Top=�6=    � ColumnDefinitions 5      �� 5   "   $��* �64   �� 5   "   $
��Auto �63   .+�5      �� $��0��6&   $��Stretch=�66   ��6T   �� *UserToWatch+ �� UpdateSourceTrigger$ 	LostFocus=�.��6�    J� InputBindings 5   "   �� 5   &   
 �� Key$ Enter��61    �� CommandParameter$ UPDATETARGET��6\    �� Command 6=   �� *
UserAction+6\   .��5   9   5      �� 5       $��UPDATETARGET��6%   $��1��6E   $��Update target��6U   $
��Left=�6m   ��6�   �� *
UserAction+5!      5"      �� 5$      $��CHANGESTATE��6<   $��80��6�   $	��250��6�   $��Center=�6�   $��Bottom=�6�   ��6   �� *
UserAction+��6[   �� *	StateText+$ PropertyChanged=�6�   5%      5&      �� 5(      $��Search Channels��6   .��5)      � $��5q�6    5*      �� 5+      $��0 �6+   �� 5,      $��0 �6+   �� 5-      $��* �6+   .+�5/      � $��0��6   	 � RowDefinitions	 50      �� 51      $
��Auto �6,   �� 52      $
��Auto �6,   �� 53      $��* �6,   .+�55      �� $��0��6%   
 �� Orientation$
 
Horizontal=�62   .+�56      �� $��Games��6$    � ContextMenu 57   "   �� 58   &   .@�59   *   m� $��LoadNext��63    m� CommandParameter$ GAMESLOADNEXT��6d    m� Command 6E   �� *
UserAction+6d   5:   '   5<      �� 5=      $��ResultCount:��6$   �� 5>      ��6$   �� *GamesDisplay.Streams.Count+$ PropertyChanged=��� 5?      $��/��6$   �� 5@      ��6$   �� *GamesDisplay.Total+$ PropertyChanged=��� 5A      $	��5,0q�6M   ��6$   �� *SteamsStringAddition+6M   5B      � 5C      $��1��6    5D      �� 5E   "   $��* �63   �� 5F   "   $
��Auto �63   .+�5H      �� ��6&   �� *SearchStringGames+$ PropertyChanged=���6n   ��  �� ElementName$ __games�� �� Path$ ActualWidth�.��6�    5I   "   �� 5J   &   $ Enter��61   $ SEARCHGAMES��6\    6=   �� *
UserAction+6\   .��5K   9   5L      �� 5M      $��1��6%   $��SEARCHGAMES��6T   ��65   �� *
UserAction+.��5N   "   F� $��20��6'   $��20��63    �� Stretch$ UniformToFill=�6>   ���	#FF000000��������O������O�;Gp��;G�m��;G��w����)����)�Ӱkd�)���BL%� Q%�X�Ѵ���<�$����M8�l��M��Z�<�|�H/*|�H3��Z�R��l/��q�����>T�����2����mӀ��p�p(��O����O������ �@����� �����mӀ� V �@� ���� �� !�� ��2
 V�2
�m��2
 �� !���������6g   5O      5P      �� 5Q      -   5         ��Name$ __games��5Q   0   $��2��6#   ��6d   �� *GamesDisplay.Games+$ PropertyChanged=� �� SelectedItem 6�   �� *SelectedGame+$ PropertyChanged=�5R      5S      �� 5T      $��1��6#   $��5��63   $��Stretch=�6=   $	��2,0q�6\   � 5U      $��2��6   	 5V      �� 5W      $
��Auto �6,   �� 5X      $
��Auto �6,   �� 5Y      $��* �6,   .+�5[      �� $��0��6%   $
 
Horizontal=�62   .+�5\      �� $��Streams��6$    5]   "   �� 5^   &   .@�5_   *   m� $��LoadNext��63   $ STREAMSLOADNEXT��6d    6E   �� *
UserAction+6d   5`   '   5b      �� 5c      $��ResultCount:��6$   �� 5d      ��6$   �� *StreamsDisplay.Streams.Count+$ PropertyChanged=��� 5e      $��/��6$   �� 5f      ��6$   �� *StreamsDisplay.Total+$ PropertyChanged=��� 5g      $	��5,0q�6M   ��6$   �� *SteamsStringAddition+6M   5h      � 5i      $��1��6    5j      �� 5k   "   $��* �63   �� 5l   "   $
��Auto �63   .+�5n      �� ��6&   �� *SearchStringStreams+$ PropertyChanged=���6p   �� $ 	__streams��$ ActualWidth�.��6�    5o   "   �� 5p   &   $ Enter��61   $ SEARCHSTREAMS��6\    6=   �� *
UserAction+6\   .��5q   9   5r      �� 5s      $��1��6%   $��SEARCHSTREAMS��6T   ��65   �� *
UserAction+.��5t   "   F� $��20��6'   $��20��63   $ UniformToFill=�6>   ���	#FF000000��������O������O�;Gp��;G�m��;G��w����)����)�Ӱkd�)���BL%� Q%�X�Ѵ���<�$����M8�l��M��Z�<�|�H/*|�H3��Z�R��l/��q�����>T�����2����mӀ��p�p(��O����O������ �@����� �����mӀ� V �@� ���� �� !�� ��2
 V�2
�m��2
 �� !���������6g   5u      5v      &   System.Windows.Controls.DataGrid  5x      -   5          Name$ 	__streams��5x   �      AutoGenerateColumns .  $��2��6c      CanUserAddRows .     
IsReadOnly . ��6�   �� *StreamsDisplay.Streams+$ PropertyChanged=�   RowStyle 5y      �� 5z   "    �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6(    �� BasedOn# [B 6J   .�5{   &   ��  �� Property �@�ContextMenu �� Value 5|   *   �� 5}   .   .@�5~   2   m� $��View on G510��6;      stUserAction# Z 6Q    5   6   `� 5�   :    `� 	Converter# [  6G    `� ConverterParameter$ CHANGEG510STALKEDCHANNEL��6{   .1�5�   >   �� 5�   ;   5�   3   5�   /   5�   '   5�   #      Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�   "   ,  &System.Windows.Controls.DataGridColumn    Header$  GameName��65   1  +System.Windows.Controls.DataGridBoundColumn!   Binding! 6G   �� *Game+  5�   "   $  StreamerName��65   ! 6K   �� *Channel.DisplayName+  5�   "   $  
StreamName��65   ! 6I   �� *Channel.Status+  5�   "   $  Viewers��65   ! 6F   �� *	Viewers+5�      5�      5�      5�      5�      5�      5�      