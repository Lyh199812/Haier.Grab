   M S B A M L   `   `   `  ����S  OBase.Clent.HVisionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Base.Clent.HVisionModule.Preprocessing.Views,Base.Clent.HVisionModule.Preprocessing.Views  5      T Phalcondotnet, Version=22.5.0.0, Culture=neutral, PublicKeyToken=4973bed59ddbf2b8A0clr-namespace:HalconDotNet;assembly=HalconDotNetHalconDotNet 5      M IHVisionLibs.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulleEclr-namespace:HVisionLibs.Shared.Controls;assembly=HVisionLibs.SharedHVisionLibs.Shared.Controls 5
      I    CBase.Clent.HVisionModule.TemplateMatch.Views.NCCTemplateCreatorView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Llocal:clr-namespace:Base.Clent.HVisionModule.Preprocessing.Views     5      Chalcon0clr-namespace:HalconDotNet;assembly=HalconDotNet     5      ZcontrolsEclr-namespace:HVisionLibs.Shared.Controls;assembly=HVisionLibs.Shared     5
      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      
���   �
�������  � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $M Gpack://application:,,,/HVisionLibs.Shared;component/Themes/Generic.xaml?�6%   �� 5      $d ^pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   �� $��15q�6   $��0��5       .+�5!      �� $��22(�6   $
��Bold"�5"      
���������5#      �� *Service.info.Name+5$      �� 5%      $
��Wrap=�5&      
���������5%      �� *Service.info.Description+5'      5(      �� 5*   
   $��1��6   
�������$��0,1,0,0q�5,      $��0,0,0,15q�5-      �� 5/   
   $��1��6   .@�50      �� $��开始使用��6   .��51      �  � ColumnDefinitions 52      �� 53      $
��0.3* �6+   �� 54      .+�58      �  59      �� 5:      �� 5;      $
��auto �6,   .+�5>      �� $��Hidden=�6'   .��5?      �� $	��5,0q�6)   .+�5@   "   9� $��	图像源��6+   
���������5B   &   �� 5C   *   $��0,5,5,5q�65   .+�5D   .   �� $��加载图像��65   $��0,0,0,5q�6D   ��5E   1   �� *LoadImageCommand+5G   +   5I   #   9� 5K   "   $��运行参数��6+   
������� � Cursor$  ��5L   :   ��5M   &   �� 5N   *   .+�5O   .   �� $��	NumLevels��68   �� 5P   .   $��0,5,5,5q�67   $��5q�5Q   7    "  MaterialDesignOutlinedComboBox#��[  5S   7   
������� �� Text 5R   7   �� * Service.RunParameter.NumLevels+.@�5U   2   �� .��6?   auto6E   �� 5V   2   .��6?   16B   �� 5W   2   .��6?   26B   �� 5X   2   .��6?   36B   �� 5Y   2   .��6?   46B   �� 5Z   2   .��6?   56B   �� 5[   2   .��6?   66B   �� 5\   2   .��6?   76B   �� 5]   2   .��6?   86B   �� 5^   2   .��6?   96B   �� 5_   2   .��6?   106C   5a   /   �� 5c   .   $��
AngleStart��68   �� 5d   .   $��0,5,5,5q�66   $��5q�5e   2    ! MaterialDesignOutlinedTextBox#��[ 5g   2   
���������5f   2   �� *!Service.RunParameter.AngleStart+.��5h   F   5i   /   �� 5k   .   $��AngleExtent��68   �� 5l   .   $��0,5,5,5q�66   $��5q�5m   6   #��[ 5o   6   
���������5n   6   �� *" Service.RunParameter.AngleExtent+.��5p   J   5q   /   �� 5s   .   $��	AngleStep��68   �� 5t   .   $��0,5,5,5q�66   $��5q�5u   6   #��[ 5w   6   
���������5v   6   �� * Service.RunParameter.AngleStep+.��5x   J   5y   /   �� 5{   .   $��Metric ��68   �� 5|   .   $��0,5,5,5q�67   $��5q�5}   7   #��[  5   7   
������� 5~   7   �� *Service.RunParameter.Metric+.@�5�   2   �� .��6?   use_polarity6M   �� 5�   2   .��6?   ignore_global_polarity6W   5�   /   5�   +   5�   #   5�      5�      �� 5�      $��1��6%   $��0,5,5,5q�5�   !   .+�5�      �� $��生成模板��6%   $	��0,5q�64   ��5�   !   �� *
RunCommand+�� 5�      $��保存模板��6%   ��5�   %   �� *SaveImageCommand+5�      5�      � 5�      $��1��6    5�      �� 5�      �� 5�      $
��auto �6,   .+�5�      /  )HVisionLibs.Shared.Controls.ImageEditView     Image 5�      �� *Image+   HWindow 5�      �� *Service.HWindow+	 �� Mode$	 TwoWay=�
   DrawObjectList
 5�      �� *DrawObjectList+�� 5�      $��2��6$   $��5q�5�       ��5�       �� *OperateResult.Message+5�      5�      5�      �� 5�      $��参数说明��6   .��5�      �� $��Hidden=�6   .��5�      �� $
��15,5q�6#   ��5�      �� *Service.info.Parameters+��5�      �� 5�      .R�5�   "   �� 5�      ��5�      �� 5�      .R�5�   "   �� $��0,5,0,10q�6-   .+�5�   &   �� 
�������$
��Bold"�5�   ,   $��14(�5�   ,   ��5�   0   �� *Name+5�   ,   �� 5�   &   
�������$��20,5,0,0q�5�   ,   ��5�   0   �� *Description+5�   ,   5�   #   5�      5�      5�      5�      �� 5�      $��关联算子��6   .��5�      �� $
��Auto=�6   .��5�      �� $
��15,5q�6#   ��5�      �� *Service.info.Predecessors+��5�      �� 5�      .R�5�   "   �� 5�      ��5�      �� 5�      .R�5�   "   �� $��0,5,0,10q�6-   .+�5�   &   �� 
���������60   �� 5�   ,   5�   #   5�      5�      5�      5�      5�      5�      5�      