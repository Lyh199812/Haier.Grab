   M S B A M L   `   `   `  ����Q  MBase.Client.BaseModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Base.Client.BaseModule.ViewsBase.Client.BaseModule.Views  5      3    -Base.Client.BaseModule.Views.SelectUserDialog   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:Base.Client.BaseModule.Views     5      \ XPrism.Unity.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59V RPrism.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5        � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $= 7/Base.Client.Assets;component/Styles/BaseResources.xaml?�6%   �� 5      $< 6/Base.Client.Assets;component/Styles/ButtonStyles.xaml?�6%   �� 5      $= 7/Base.Client.Assets;component/Styles/TextBoxStyles.xaml?�6%   %    5        Prism.Dialogs.Dialog   WindowStyle 5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value$	 500��6&   �� 5       � ��$	 450��6'   5      .��5      � $��0,20,0,0q�6    � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      �� 5      $��70 �6   .+�5   
   �� $��20(�6+   $��Center=�69   ���#888$
��10,0q�6f   ��6   �� *Title+6f   � 5!   
   $��1��6   $��20,10q�6    5"      �� 5#      $��30 �6    �� 5$      .+�5&      �� ���#EEE6   � 5'       � ColumnDefinitions 5(      �� 5)      $��40 �6'   �� 5*      �� 5+      .+�5-      �� $��选择��6   $��Center=�6&   $��Center=�6A   �� 5.      $��用户名称��6   $��1��6(   $��Center=�68   $
��Left=�6S   �� 5/      $��用户姓名��6   $��2��6(   $��Center=�68   $
��Left=�6S   50      �� 51      $��1��69   ��6   �� *Users+��52      �� 53      .R�54      � $��30��6    55      �� 56   "   $��40 �63   �� 57   "   �� 58   "   .+�5:      �� $��Center=�6H   $��Center=�6c   	 d� 	IsChecked	 6'   �� *
IsSelected+6c   �� 5;      $��1��6B   $��Center=�6R   ��6(   �� *
UserName+6R   �� 5<      $��2��6B   $��Center=�6R   ��6(   �� *
RealName+6R   5=      5>      5@      5A      �� 5C   
   
 �� Orientation$
 
Horizontal=�6   $��Right=�6.   $��2��6J   $
��20,0q�6W   .+�5D      �� $��确定��6   $	��100��6"   $��30��6.   $	��5,0q�6:      NormalButtonStyle#��[  5E      ���	#FF0ABEFF��5G      �� *ConfirmCommand+�� 5H      $��取消��6   $	��100��6"   $��30��6.   #��[  5I      ���#DDD���#666��5K      �� *CancelCommand+5L      5M      5N      