   M S B A M L   `   `   `  ����Q  MBase.Client.BaseModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Base.Client.BaseModule.ViewsBase.Client.BaseModule.Views  5      .    (Base.Client.BaseModule.Views.FeeModeView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:Base.Client.BaseModule.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source   Key � ColumnDefinitions z� SharedSizeGroup �� Orientation �� Command �� CommandParameter �� Triggers	 P� Property
 P� Value �� 
TargetName �� Property �� Value�� 5
   
    5      �� 5      $= 7/Base.Client.Assets;component/Styles/BaseResources.xaml?�6%   �� 5      $= 7/Base.Client.Assets;component/Styles/TextBoxStyles.xaml?�6%   �� 5      $< 6/Base.Client.Assets;component/Styles/ButtonStyles.xaml?�6%   �� 5      $F @/Base.Client.Assets;component/Styles/DefaultToolBarTemplate.xaml?�6%   �� 5      $= 7/Base.Client.Assets;component/Styles/DataGridStyle.xaml?�6%   �� 5      �� 5      ColorItemTemplate  .R�5      � $
��root��6+   $��33��6   
������  5      �� 5   "   $��50 �63   $ A��6>   �� 5   "   $��* �63   �� 5   "   $��90 �63   .+�5      �� $��0��6?   $��Center=�6O   $��Center=�6j   ���#888��6(   �� *Index+5   (   �� 5      $��1��6C   $��Center=�6S   ��6(   �� *	ColorName+6S   �� 5      $��6��6)   $��Center=�69   $��Center=�6T   $ 
Horizontal=�6q   .+�5   "   �� $	��3,0q�6,   �� 5    &    60   �� *DeleteCommand+ 6R   �� .�6o   删除6s   5!   #   5"      5#       5$      P� 5%      	 �@J�IsMouseOver$

 True��6=   . �5&   "   �� $
 root��6L    � �� �#EEE5'      5)      5*      %    5,      ��5.      �� 5/   
    �� ResourceKey$ ToolBarTemplate��6   .��51      � $��20q�6    � RowDefinitions 52   
   �� 53      $��35 �6   �� 54      .+�57   
   �� ���#F7F9FA6   � 58   
    59      �� 5:      $��50 �6#   �� 5;      $��* �6#   �� 5<      $��70 �6#   .+�5>      �� $��序号��6   $��Center=�6"   $��Center=�6=   $��0��6Z   �� 5?      $��收费模式��6   $��Center=�6$   $
��Left=�6?   $��1��6Z   �� 5@      $��操作��6   $��Center=�6"   $��Center=�6=   $��2��6Z   5A      �� 5B   
   $��1��6   $��Hidden=�6%   .��5C      ��   ColorItemTemplate#��[ 5D      ��5C      �� *
FeeModes+5E      5F      5H      5I      