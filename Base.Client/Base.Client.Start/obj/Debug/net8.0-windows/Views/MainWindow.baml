   M S B A M L   `   `   `  ����L  HBase.Client.Start, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Base.Client.ViewsBase.Client.Views  5   	   "    Base.Client.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Base.Client.Views     5   	   \ XPrism.Unity.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59V RPrism.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   
������   � WindowStartupLocation$  CenterScreen=�5   3   $��Microsoft YaHei)�5	   	   $��13(�6&   ���#333$��
ExtraLight"�6F    � Title$ XX管理��5
   	   $	��750��6   $
��1200��6#   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ -1q�6      ResizeBorderThickness$ 4q�60    � 	Resources 5       �� 
TargetType   TabCloseButtonStyle �� Property �� Value	 �� 
TargetType
 �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName � ColumnDefinitions �� Value �� Binding �� Triggers��%�  &	        '�� �    7   �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      "	 ��6&   .R�5      �� $
��back��5   !   $��Center=�5   !   $��Center=�6<   $
 3��6Y   
������ ._�5      F� +����        � z  z �     z � z     	$��5q�6;   #��z��5   #   $��1��6I   5       5      P� 5       �@J�IsMouseOver$
 True��6=   . �5   "   �� $
 back��6Q    � �� �	#190000005      5      5      5       �� 5"   
   " ��6   .�5#      ��  � �� 6'   �� *DataContext.PageTitle+�� 5$       � ��
 ���� 6+   �� 5%       � ��$	 2,0q�6'   �� 5&       � �� �#4446+   �� 5'       � �� 5(      �� 5)      "	 ��6&   .R�5*      � #��z��6   $��30��6I    5+      �� 5,   "   �� 5-   "   $
��auto �63   $��30��6@   $��10��6N   .+�5/      �� #��z��6(   $��Center=�6H   $��10,5,5,5q�6c   � 50      $��
close_grid��63   $��1��6#   $y�	Collapsed=�6E   $��30��6\   $��0,0,3,0q�6g   .+�51   "   �� $��	close_btn��52   <   $��1��51   )   8��  #��z��6f   $y�	Collapsed=�52   %   ��53   %   �� *DataContext.CloseCommand+��54   %   �� *DataContext.NavUri+55      56       57      �� 58      $
 True��6U    6*   �� *DataContext.IsCanClose+.a�59   "   �� $ 
close_grid��6)    � y�$ Visible=�6W   5:      P� 5;       �@J�IsMouseOver$
 True��6=   . �5<   "   �� $ 	close_btn��6O    � y�$ Visible=�6?   6O   5=      P� 5>       �@��
IsSelected$
 True��6<   . �5?   "   �� $ 	close_btn��6O    � y�$ Visible=�6?   6O   5@      5B      5D       5E      P� 5F       �@J�IsMouseOver$
 True��61   . �5G      ��  � �� �#EEE63   5H      P� 5I       �@��
IsSelected$
 True��60   . �5J      ��  � �� �	#FF0ABEFF63   �� 5K       � ��
 �����63   5L      5N      5O      .��5P      ]� .+�5R   
   � $
��Left=�6   $	��230��6%   ���#0b3d90 � RowDefinitions 5S      �� 5T      $
��auto �6    �� 5U      .+�5W      �� .+�5X      �� $I��Cpack://application:,,,/Base.Client.Assets;component/Images/Logo.png��6   $��70��6e   $��0,30,0,0q�5Y      �� 5Z      $��Test Platform��6   $��20(�61   $
��Bold"�6?   $��Center=�5[      $
��0,30q�69   
�������6G   5\      �� 5^      $��1��6   ,  &Prism.Navigation.Regions.RegionManager   
RegionName$ LeftMenuTreeRegion��6*   5_      �� 5a   
   $ MainHeaderRegion��6   $��70��6K   $	��Top=�6W   �� 5c   
   $ MainViewRegion��6   $��5q�6E   $��0,1,0,0q�6P   5d      5e      