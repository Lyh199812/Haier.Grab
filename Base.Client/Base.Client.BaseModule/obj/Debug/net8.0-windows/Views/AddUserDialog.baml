   M S B A M L   `   `   `  ����Q  MBase.Client.BaseModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Base.Client.BaseModule.ViewsBase.Client.BaseModule.Views  5      0    *Base.Client.BaseModule.Views.AddUserDialog   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:Base.Client.BaseModule.Views     5      \ XPrism.Unity.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59V RPrism.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5        � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $< 6/Base.Client.Assets;component/Styles/ButtonStyles.xaml?�6%   �� 5      $= 7/Base.Client.Assets;component/Styles/TextBoxStyles.xaml?�6%   %    5        Prism.Dialogs.Dialog   WindowStyle 5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value$	 500��6&   �� 5       � ��$	 350��6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      �� 5      $��70 �6   .+�5   
   �� $��20(�6+   $��Center=�69   ���#888$
��10,0q�6f   ��6   �� *Title+6f   � 5    
   $��1��6   $��20,10q�6    5!      �� 5"      �� 5#      �� 5$      �� 5%      $
��auto �6     � ColumnDefinitions 5'      �� 5(      $��60 �6#   �� 5)      $
��129* �6#   �� 5*      $
��271* �6#   .+�5-      �� $��	用户名��6   $��Center=�6#   ���#8886>   �� 5.      $��	姓   名��6   $��1��6%   $��Center=�62   ���#8886M   �� 5/      $��	年   龄��6   $��2��6%   $��Center=�62   ���#8886M   �� 50      $��	头   像��6   $��3��6%   $��Center=�62   ���#8886M   �� 53      $��1��6      NormalTextBoxStyle#��[  6d   $��2��6�   ��6&   �� *UserInfo.UserName+	 �� ValidatesOnDataErrors$
	 True��
 �� 
IsReadOnly
 6�   �� *IsReadOnlyUserName+6�   �� 54      $��1��6   #��[  6I   $��2��6u   $��	0,43,0,43q�6�   $��3��6�   ��6&   �� *UserInfo.RealName+6�   �� 55      $��1��6   $��2��6D   #��[  6Q   $��2��6}   ��6&   �� *UserInfo.Age+6}   �� 56      $��50��6   $��50��6     �� CornerRadius$ 10��6,   $��3��6>   $��1��6K   $��5,5,0,5q�6[   $
��Left=�6l   ���#DDD$��1q�6�   ��57      �� 58       �� ImageSource 6!   �� *UserInfo.UserIcon+ J� InputBindings 5:      d� 5;       d� MouseAction$ 	LeftClicke�6#    �� Command 5<   #   �� *
SelectIcon+5>      5?      �� 5B   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��2��6J   $
��20,0q�6W   .+�5C      �� $��确定��6   $	��100��6"   $��30��6.   $	��5,0q�6:     NormalButtonStyle#��[ 5D      ���	#FF0ABEFF��5F      �� *ConfirmCommand+�� 5G      $��取消��6   $	��100��6"   $��30��6.   #��[ 5H      ���#DDD���#666��5J      �� *CancelCommand+5K      5L      5M      