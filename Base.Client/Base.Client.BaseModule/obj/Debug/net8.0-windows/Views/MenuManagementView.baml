   M S B A M L   `   `   `  ����Q  MBase.Client.BaseModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Base.Client.BaseModule.ViewsBase.Client.BaseModule.Views  5      5    /Base.Client.BaseModule.Views.MenuManagementView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:Base.Client.BaseModule.Views     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5        � 	Resources  5
       �� MergedDictionaries �� Source �� 
TargetType   Key �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName	 �� Property
 �� Value�� 5   
    5      �� 5      $= 7/Base.Client.Assets;component/Styles/BaseResources.xaml?�6%   �� 5      $= 7/Base.Client.Assets;component/Styles/TextBoxStyles.xaml?�6%   �� 5      $< 6/Base.Client.Assets;component/Styles/ButtonStyles.xaml?�6%   �� 5      $F @/Base.Client.Assets;component/Styles/DefaultToolBarTemplate.xaml?�6%   �� 5      �� 5      " d�6&   ExpandeButtonTemplate  .R�5      �� ���	#FF0ABEFF$ 3��68   $��15��6I   $��15��6T   ._�5      F� $
��path��6^   +����     	= � z  	= � 	=     � 	=  z 	
�������$��1��6J   $��Center=�5   #   $��Center=�6>   5       5      P� 5       �@d�	IsChecked$
 TrueY�6;   . �5   "   �� $
 path��6J   	 � ��
 ��        � z     	5      5      5       %    5"      ��5$      �� 5%   
    �� ResourceKey$ ToolBarTemplate��6   .��5(      � $��1��6    � IsSharedSizeScope$
 True��6    � RowDefinitions 5)   
   �� 5*      $��40 �6   �� 5+      .+�5-   
   �� ���#F7F9FA6   � 5.   
    � ColumnDefinitions 5/      �� 50      $��30 �6#   �� 51      $��2* �6#   �� 52      $��* �6#    z� SharedSizeGroup$ column1��6-   �� 53      $	��150 �6#   �� 54      $	��100 �6#   .+�56      �� $��菜单名称��6   $��Center=�6$   $��1��6?   �� 57      $��目标视图��6   $��Center=�6$   $
��Left=�6?   $��2��6Z   �� 58      $��图标��6   $��Center=�6"   $��Center=�6=   $��3��6Z   �� 59      $��操作��6   $��Center=�6"   $��Center=�6=   $��4��6Z   5:      �� 5;   
   $��1��6   $
��Auto=�6$   .��5<      R� $��0q�65   
������ ��6   �� *Menus+  5=       �� 
TargetType J� 	AllowDrop ��Name �� Padding J� SnapsToDevicePixels*  $Microsoft.Xaml.Behaviors.Interaction   Triggers+  %Microsoft.Xaml.Behaviors.EventTrigger   	EventName*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction   PassEventArgsToCommand   Command ��Name ��Name �� Value �� Binding��%
  'Q�       �� 5?      " Q�6   .�5@      �� 	 �@Q�
IsExpanded
 67   �� *
IsExpanded+�� 5A      	 � ��$
 Stretch=�6G   �� 5B      	 � ��
 5C      �� 5D   "   " Q�62   .R�5E   &   � $
��root��6D   
������ $
 True��6P    5F   *   �� 5G   .   �� 5H   .   $��30��6?    5J   *   �� 5K   .   $
��Auto �6<   $��36��6J   �� 5L   .   $
��auto �6<   .+�5O   *   �� $
��borH��6�   $��1��61   $��0,0,0,1q�6<   
�������$
��Left=�6n   $��20��6�   $��Center=�6�   6�   � 5P   *   $��canvas��6J   $
��Left=�6/   $��2��6X   .+�5Q   .   �� $
��borV��5R   E   $��1��5Q   5   
�������$��1,0,0,0q�6W   $
��Left=�5R   )   6E   5S   +   �� 5V   *   $ Bd��61   
��� ���$��0q�6S   #��z��5W   1   $ 0��5X   1   $��2��6B   # z��6V   $
 true��5Y   1   $
 True��5Z   1    5[   .     5\   2   $ 	MouseMove��6A   . 5]   6     $
 True��6q    6L   �� *MouseMoveCommand+6q   5^   3     5_   2   $
 Drop��6A   . 5`   6     $
 True��6l    6L   �� *DropCommand+6l   5a   3     5b   2   $ DragOver��6A   . 5c   6     $
 True��6p    6L   �� *DragOverCommand+6p   5d   3     5e   2   $ 	DragLeave��6A   . 5f   6     $
 True��6q    6L   �� *DragLeaveCommand+6q   5g   3   ._�5i   .   �� $ PART_Header��6?   $��Header��6T   #��z��5j   ?   #��z��5k   ?   # z 5l   ?   5m   /   5n   +   �� 5p   *   $ 	ItemsHost��69   $��20,0,0,0q�6L   $��2��6^   $��1��6r   5q   '    5s   &   P� 5t   *    �@Q�
IsExpanded$ false��6H   . �5u   .   �� $ 	ItemsHost��6K   	 � y�$
 	Collapsed=�6b   5v   +   P� 5w   *    �@Q�
IsSelected$
 true��6H   . �5x   .   �� $ Bd��6K   	 � ��
 �	#110000006[   5y   +   �� 5z   *   $ 0��6Q    66   �� *MenuId+.a�5{   .   �� $
 borH��65   	 � y�$
 	Collapsed=�6]   �� 5|   .   $ canvas��65   	 � y�$
 	Collapsed=�6_   5}   +   �� 5~   *   $
 true��6V    66   �� *IsLastChild+.a�5   .   �� $ canvas��65   	 � ��$
 1��6a   �� 5�   .   $
 borV��65   	 � ��$
 0,0,0,18q�6Y   5�   +   �� 5�   *   $ 1��6W    66   �� *OverLocation+.a�5�   .   �� $ Bd��65   	 � ��$
 0,1,0,0q�6`   5�   +   �� 5�   *   $ 3��6W    66   �� *OverLocation+.a�5�   .   �� $ Bd��65   	 � ��$
 0,0,0,1q�6`   5�   +   �� 5�   *   $ 2��6W    66   �� *OverLocation+.a�5�   .   �� $ Bd��65   	 � ��
 �	#AA0ABEFF6[   5�   +   5�   #   5�      5�      5�      ��5�      �� 5�       �� ItemsSource 6/   �� *
Children+.R�5�      � $
��back��68   
������  5�      �� 5�   "   $��30 �63   �� 5�   "   $��2* �63   �� 5�   "   $ column1��63   �� 5�   "   $	��150 �63   �� 5�   "   $	��100 �63   .+�5�      d� $��	expandBox��6L   $��15��6]   $��15��6h   $y�	Collapsed=�6t     ExpandeButtonTemplate#��[ 5�   +    d� 	IsChecked 5�   +   �� *
IsExpanded+5�   +   �� 5�      $��Center=�6D   $��1��6_   ��6(   �� *
MenuHeader+6_   �� 5�      $��Center=�6D   $
��Left=�6_   $��2��6z   ��6(   �� *
TargetView+6z   �� 5�      $��Center=�6B   $��Center=�6]   $��3��6z     Iconfont#��[ 6�   ��6(   �� *
MenuIcon+6�   �� 5�      $��operationPanel��6�     �� Orientation$  
Horizontal=�6)   $��Center=�6B   $��Center=�6]   $��4��6z   .+�5�   "   �� $��Center=�6,   $��Center=�6G   $��4��6d   $��0,0,3,0q�6t   �� 5�   *   ! �� Command! 64   �� *EditCommand+" �� CommandParameter" 5�   4   �� .�6Q   编辑6U   5�   #   �� 5�   "   $��Center=�6,   $��Center=�6G   $��4��6d   $��3,0,0,0q�6t   �� 5�   *   ! 64   �� *DeleteCommand+" 5�   4   �� .�6Q   删除6U   5�   #   5�      5�      # �� Triggers# 5�      �� 5�      $ 1��6G    6*   �� *
MenuType+.a�5�   "   �� $ 	expandBox��6O   	 � y�$
 Visible=�6?   6O   5�      5�      5�      5�      5�      5�      