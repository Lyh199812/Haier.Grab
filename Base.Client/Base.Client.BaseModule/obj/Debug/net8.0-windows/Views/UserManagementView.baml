   M S B A M L   `   `   `  ����Q  MBase.Client.BaseModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Base.Client.BaseModule.ViewsBase.Client.BaseModule.Views  5      5    /Base.Client.BaseModule.Views.UserManagementView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:Base.Client.BaseModule.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $= 7/Base.Client.Assets;component/Styles/BaseResources.xaml?�6%   �� 5      $= 7/Base.Client.Assets;component/Styles/TextBoxStyles.xaml?�6%   �� 5      $< 6/Base.Client.Assets;component/Styles/ButtonStyles.xaml?�6%   �� 5      $F @/Base.Client.Assets;component/Styles/DefaultToolBarTemplate.xaml?�6%   %    5      ��5      �� 5   
    �� ResourceKey$ ToolBarTemplate��6   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      .+�5   
   �� ���#EEE6   � 5   
    � ColumnDefinitions 5       �� 5!      $��40 �6#   �� 5"      $	��120 �6#   �� 5#      $��2* �6#   �� 5$      �� 5%      $	��150 �6#   .+�5'      �� $��序号��6   $��Center=�6"   $��Center=�6=   �� 5(      $��头像��6   $��1��6"   $��Center=�62   $��Center=�6M   �� 5)      $��用户信息��6   $��2��6$   $��Center=�64   �� 5*      $��角色��6   $��3��6"   $��Center=�62   �� 5+      $��操作��6   $��4��6"   $��Center=�62   $��Center=�6M   5-      �� 5/   
   $��1��6   $
��Auto=�6$   .��50      �� ��6   �� *Users+��51      �� 52      .R�53      � $
��root��68   
������  54      �� 55   "   $��40 �63   �� 56   "   $	��120 �63   �� 57   "   $��2* �63   �� 58   "   �� 59   "   $	��150 �63   .+�5<      �� ���#EEE$��0,0,0,1q�68   $��1��6R   $��Bottom=�6]   $��10��6x    J� SnapsToDevicePixels$
 True��6�   �� 5>      $��Center=�6?   $��Center=�6Z   ��6(   �� *Index+6Z   �� 5?      $��1��6%   $��40��65   $��40��6@    �� CornerRadius$ 20��6L   
�������$��2q�6r    J� Effect 5@   "   3  -System.Windows.Media.Effects.DropShadowEffect  5A   &   	   
BlurRadius$	 10Q�67   
   ShadowDepth$
 0Q�6G      	Direction$ 0Q�6W      Color$
 Gray��6e      Opacity$	 0.3Q�6r   ��5C   "   �� 5D   &    �� ImageSource 61   �� *
UserIcon+5F      �� 5I      $��2��6)   $
��0,10q�69   .+�5J   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5K   &   �� $��Center=�6J   ��60   �� *
RealName+6J   �� 5L   &   $��状态：有效��60   $��11(�6=   ���#888$��Center=�6]   $
��10,0q�6x   5M   #   �� 5N   "   $ 
Horizontal=�6-   .+�5O   &   �� ���#888��60   �� *
UserName+6J   �� 5P   &   ���#888$
��10,0q�6W   ��60   �� *Age+6W   5Q   #   �� 5R   "   �� 5S   &   $�[��6*    �� 5T   &    �� Command 60   �� *
PwdCommand+ �� CommandParameter 6O   �� .�6l   重置密码6r    �� 5U   &   $�]��6*   5V   #   5W      �� 5Y      $��3��6)   $��Center=�69   .+�5Z   "   �� ��6/   �� *Roles+��5[   &   �� 5\   *   .R�5]   .   � 5^   +   ��5`   &   �� 5a   *   .R�5b   .   �� $��3q�6R   ��68   �� *
RoleName+6R   5c   +   5e   #   �� 5f   "   $
��Left=�6,   $��0,5,0,0q�6G   �� 5g   &   $�[��6*    �� 5h   &    60   �� *RoleCommand+ 6P   �� .�6m   权限分配6s    �� 5i   &   $�]��6*   5j   #   5k      �� 5m      $��4��6)   $��Center=�69   $��Center=�6T   $ 
Horizontal=�6q   .+�5n   "   �� $	��3,0q�6,   �� 5o   &    60   �� *EditCommand+ 6P   �� .�6m   编辑6q   5p   #   �� 5q   "   $	��3,0q�6,   �� 5r   &    60   �� *DeleteCommand+ 6R   �� .�6o   删除6s   5s   #   5t      5u       �� Triggers 5v      P� 5w       P� Property �@J�IsMouseOver P� Value$
 True��6=   . �5x   "   ��  �� 
TargetName$
 root��6)    �� Property � �� �� Value �#F7F9FA6Q   5y      5{      5}      5~      5      5�      