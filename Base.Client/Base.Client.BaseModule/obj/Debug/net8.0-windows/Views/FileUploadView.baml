   M S B A M L   `   `   `  ����  Base.Client.BaseModule1    +Base.Client.BaseModule.Views.FileUploadView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $= 7/Base.Client.Assets;component/Styles/BaseResources.xaml?�6%   �� 5      $< 6/Base.Client.Assets;component/Styles/ButtonStyles.xaml?�6%   �� 5      $= 7/Base.Client.Assets;component/Styles/TextBoxStyles.xaml?�6%   �� 5      $F @/Base.Client.Assets;component/Styles/DefaultToolBarTemplate.xaml?�6%   %    5      ��5      �� 5   
    �� ResourceKey$ ToolBarTemplate��6   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� ���#F7F9FA6   � 5   
    � ColumnDefinitions 5       �� 5!      $��50 �6#   �� 5"      $��2* �6#   �� 5#      $��* �6#   �� 5$      $��* �6#   �� 5%      $	��100 �6#   .+�5'      �� $��序号��6   $��Center=�6"   $��Center=�6=   �� 5(      $��文件名称��6   $��Center=�6$   $��1��6?   �� 5)      $��更新地址��6   $��Center=�6$   $��Center=�6?   $��2��6\   �� 5*      $��上传时间��6   $��Center=�6$   $��Center=�6?   $��3��6\   �� 5+      $��操作��6   $��Center=�6"   $��Center=�6=   $��4��6Z   5,      �� 5-   
   $��1��65   ��6   �� *Files+��5.      �� 5/      .R�50      � $
��root��6'   $��35��6   
������  51      �� 52      $��50 �6/   �� 53      $��2* �6/   �� 54      $��* �6/   �� 55      $��* �6/   �� 56      $	��100 �6/   .+�58      �� $��Center=�6;   $��Center=�6V   ��6$   �� *Index+6V   �� 59      $��Center=�6>   $��1��6Y   ��6$   �� *
FileName+6Y   �� 5:      $��Center=�6>   $��Center=�6Y   $��2��6v   ��6$   �� *
FilePath+6v   �� 5;      $��Center=�6@   $��Center=�6[   $��3��6x   ��6$   �� *
UploadTime+6x   �� 5<      $��Center=�6$   $��Center=�6?   $��4��6\   �� 5=   "    �� Command 6,   �� *DeleteCommand+ �� CommandParameter 5>   ,   �� .�6I   删除6M   5?      5@       �� Triggers 5A      P� 5B      	 P� Property	 �@J�IsMouseOver
 P� Value$

 True��69   . �5C      ��  �� 
TargetName$
 root��6K    �� Property � �� �� Value �#F7F9FA5D      5F      5H      5I      5J      