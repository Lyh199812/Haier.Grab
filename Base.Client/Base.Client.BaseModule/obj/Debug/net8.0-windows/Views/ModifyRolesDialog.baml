   M S B A M L   `   `   `  ����Q  MBase.Client.BaseModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Base.Client.BaseModule.ViewsBase.Client.BaseModule.Views  5      4    .Base.Client.BaseModule.Views.ModifyRolesDialog   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:Base.Client.BaseModule.Views     5      \ XPrism.Unity.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59V RPrism.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5        � 	Resources  5
       �� MergedDictionaries �� Source �� 
TargetType   Key �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value �� 
TargetName �� Triggers�� 5   
    5      �� 5      $= 7/Base.Client.Assets;component/Styles/BaseResources.xaml?�6%   �� 5      $< 6/Base.Client.Assets;component/Styles/ButtonStyles.xaml?�6%   �� 5      �� 5      " ��6   RoleItemButtonStyle  .�5      ��  � �� 5      �� 5   "   " ��62   .R�5   &   � 
������ .+�5   *   �� $
��back��6i   ���#EEE$��1q�6D   $ 3��6X   ._�5   .   �� #��z��68   $��Center=�6Y   $��Center=�6t   $	��8,5q�6�   5   +   5   '    5   &   P� 5   *   	 �@d�	IsChecked$

 TrueY�6G   . �5   .   �� $
 back��6Z    � ��
 � ���5   +   5   #   5!       5"      P� 5#      	 �@d�	IsChecked$

 TrueY�6;   . �5$   "   ��  � ��
 �����6?   5%      5'      5(      %
   '��       5*        Prism.Dialogs.Dialog   WindowStyle 5,      �� 5-   
   " �6   .�5.      ��  � ��$	 500��6&   �� 5/       � ��$	 450��6'   50      .��52      � $��0,20,0,0q�6    � RowDefinitions 53   
   �� 54      $��70 �6   �� 55      �� 56      $��70 �6   .+�58   
   �� $��20(�6+   $��Center=�69   ���#888$
��10,0q�6f   ��6   �� *Title+6f   �� 5:   
   $��1��65   $��20q�6B   ��6   �� *Roles+��5;      �� 5<      .R�5=      � 5>      ��5@      �� 5A      .R�5B      �� $	��8,5q�5C   @     RoleItemButtonStyle#��[ 5D      ��5B      �� *
RoleName+ � DataContext 6<   ��  d� 	IsChecked 5C      �� *
IsSelected+5D      5E      5G      �� 5I   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��2��6J   $
��20,0q�6W   .+�5J      �� $��确定��6   $	��100��6"   $��30��6.   $	��5,0q�6:     NormalButtonStyle#��[ 5K      ���	#FF0ABEFF��5M      �� *ConfirmCommand+�� 5N      $��取消��6   $	��100��6"   $��30��6.   #��[ 5O      ���#DDD���#666��5Q      �� *CancelCommand+5R      5S      5T      