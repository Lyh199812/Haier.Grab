   M S B A M L   `   `   `  ����U  QProject.Modules.GrabLocate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Project.Modules.GrabLocate.Views Project.Modules.GrabLocate.Views  5      ;    5Project.Modules.GrabLocate.Views.ProductionConfigView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:Project.Modules.GrabLocate.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source   TextBoxWithPlaceholderStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius	 ��Name

 � Tag J� IsHitTestVisible ��Name �� Triggers P� Property P� Value �� 
TargetName�� 5
   
    5      �� 5      $< 6/Base.Client.Assets;component/Styles/ButtonStyles.xaml?�6%   �� 5      $> 8/Base.Client.Assets;component/Styles/DataGridStyle2.xaml?�6%   %S  &	        �� 5      " ��68   .�5      ��  � ��$ Center=�6=   �� 5       � ��$
 10,0q�6,   �� 5       � ��$ 1q�64   �� 5       � ��
 �����60   �� 5       � ��
 �����6/   �� 5       � �� 5      �� 5      " ��6*   .R�5      � .+�5   "   �� $ Border��6)   #��z��5      #��z��5      #��z��5      $ 10��5       �� 5"   "   $	 Placeholder��6,   #��z
 5#       
�������$��12,0,0,0q�5%       $��Center=�5&       $y�	Collapsed=�5'       $ False��5(       �� 5*   "   $ PART_ContentHost��6/   $��2q�5+   #   $��Center=�5,   #   5-       5.      P� 50   "    � ��$  ��6:   . �51   &   �� $ Placeholder��6-    � y�$ Visible=�6\   52   #   P� 54   "    �@J�IsMouseOver$
 True��6A   . �55   &   �� $ Border��6-    � �� �#007ACC6X   56   #   P� 58   "    �@J�	IsFocused$
 True��6?   . �59   &   �� $ Border��6-    � �� �#005A9E6X   �� 5:   &   $ Placeholder��6-    � y�$ 	Collapsed=�6\   5;   #   5=      5?      5@      5C      .��5E      �  � RowDefinitions 5G   
   �� 5I      $
��Auto �6   �� 5K      $
��Auto �6   �� 5M      $��* �6   �� 5N      $
��Auto �6   .+�5R   
   ��  �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $	��Top=�6I   $��
10,10,10,0q�6a   .+�5T      �� -   5        $��ProductConfigIDInput��5V      $	��200��6)   $��5q�65   #��[  5W      $
 请输入产品配置 ID��5X      ��5U      �� *ProductConfigIDInput+ �� UpdateSourceTrigger$ PropertyChanged=�5X      �� 5[      $��启用��5]      $��80��6   $��5q�6%     NormalButtonStyle2#��[ 5^      ��5\      �� *EnableCommand+5^      �� 5a      $��停用��5c      $��80��6   $��5q�6%   #��[ 5d      ��5b      �� *DisableCommand+5d      �� 5h      $��选中��5j      $��80��6   $��5q�6%   #��[ 5k      ��5i      �� *SelectCommand+5k      �� 5n      $��刷新��5p      $��80��6   $��5q�6%   #��[ 5q      ��5o      �� *RefreshCommand+5q      �� 5t      $��删除��5v      $��80��6   $��5q�6   #��[ 5w      ��5u      �� *DeleteCommand+5w      5x      �� 5z   
   $ 
Horizontal=�6   $��	10,0,10,5q�6.   $��1��5{      .+�5}      �� $	��200��5      $��5q�6   #��[  5�      $'
 !请输入新建产品配置名称��5�      ��5~      �� *NewProductConfigName+$ PropertyChanged=�5�      �� 5�      $��新建��5�      $��80��6   $��5q�6%   #��[ 5�      ��5�      �� *CreateCommand+5�      5�      �� 5�   
   $��1��6   $��1��6   $��Bottom=�5�      
�������$��5q�5�   #   &   System.Windows.Controls.DataGrid  5�   
   -   5        $��ProductConfigDataGrid��5�   	   $��2��5�   	      AutoGenerateColumns$
 True��5�   	   $
��10,5q�5�   	      
IsReadOnly$
 True��5�   	   $��Stretch=�5�   	   $��Stretch=�5�   	     NormalDataGridStyle3#��[ 5�   	   ��5�   	   �� *ProductConfigs+ �� SelectedItem 5�   	   �� *CurProductConfig+5�      �� 5�   
   $��3��5�   	   
���  ��$
��10,5q�5�   	   $��Center=�5�   	   ��5�   	   �� *SearchResult+5�   	   5�      5�      