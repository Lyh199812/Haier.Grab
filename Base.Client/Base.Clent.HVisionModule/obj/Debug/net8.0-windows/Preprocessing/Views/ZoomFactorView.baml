   M S B A M L   `   `   `  ����S  OBase.Clent.HVisionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Base.Clent.HVisionModule.Preprocessing.Views,Base.Clent.HVisionModule.Preprocessing.Views  5      T Phalcondotnet, Version=22.5.0.0, Culture=neutral, PublicKeyToken=4973bed59ddbf2b8A0clr-namespace:HalconDotNet;assembly=HalconDotNetHalconDotNet 5      M IHVisionLibs.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulleEclr-namespace:HVisionLibs.Shared.Controls;assembly=HVisionLibs.SharedHVisionLibs.Shared.Controls 5
      A    ;Base.Clent.HVisionModule.Preprocessing.Views.ZoomFactorView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Llocal:clr-namespace:Base.Clent.HVisionModule.Preprocessing.Views     5      Chalcon0clr-namespace:HalconDotNet;assembly=HalconDotNet     5      ZcontrolsEclr-namespace:HVisionLibs.Shared.Controls;assembly=HVisionLibs.Shared     5
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
������� � Cursor$  ��5L   :   ��5M   &   �� 5N   *   .+�5O   .   �� $��
ScaleWidth��68   �� 5P   .   $��0,5,5,5q�66   $��5q�5Q   2    !  MaterialDesignOutlinedTextBox#��[  5S   2   
���������5R   2   �� *!Service.RunParameter.ScaleWidth+.��5T   F   5U   /   �� 5V   .   $��ScaleHeight��68   �� 5W   .   $��0,5,5,5q�66   $��5q�5X   6   #��[  5Z   6   
���������5Y   6   �� *" Service.RunParameter.ScaleHeight+.��5[   J   5\   /   �� 5]   .   $��Interpolation ��68   �� 5^   .   $��0,5,5,5q�67   $��5q�5_   7    " MaterialDesignOutlinedComboBox#��[ 5a   7   
������� �� Text 5`   7   �� *$"Service.RunParameter.Interpolation+.@�5c   2   �� .��6?   	bicubic6H   �� 5d   2   .��6?   
bilinear6I   �� 5e   2   .��6?   
constant6I   �� 5f   2   .��6?   nearest_neighbor6Q   �� 5g   2   .��6?   
weighted6I   5i   /   5j   +   5l   #   5m      5n      �� 5p      $��1��6%   $��0,5,5,5q�5q   !   .+�5r      �� $��执行��6%   $	��0,5q�62   ��5s   !   �� *
RunCommand+�� 5t      $��保存图像��6%   ��5u   %   �� *SaveImageCommand+5v      5w      � 5y      $��1��6    5z      �� 5{      �� 5|      $
��auto �6,   .+�5~      /  )HVisionLibs.Shared.Controls.ImageEditView     Image 5      �� *Image+   HWindow 5�      �� *Service.HWindow+	 �� Mode$	 TwoWay=�
   DrawObjectList
 5�      �� *DrawObjectList+�� 5�      $��2��6$   $��5q�5�       ��5�       �� *OperateResult.Message+5�      5�      5�      �� 5�      $��参数说明��6   .��5�      �� $��Hidden=�6   .��5�      �� $
��15,5q�6#   ��5�      �� *Service.info.Parmeters+��5�      �� 5�      .R�5�   "   �� 5�      ��5�      �� 5�      .R�5�   "   �� $��0,5,0,10q�6-   .+�5�   &   �� 
�������$
��Bold"�5�   ,   $��14(�5�   ,   ��5�   0   �� *Name+5�   ,   �� 5�   &   
�������$��20,5,0,0q�5�   ,   ��5�   0   �� *Description+5�   ,   5�   #   5�      5�      5�      5�      �� 5�      $��关联算子��6   .��5�      �� $
��Auto=�6   .��5�      �� $
��15,5q�6#   ��5�      �� *Service.info.Predecessors+��5�      �� 5�      .R�5�   "   �� 5�      ��5�      �� 5�      .R�5�   "   �� $��0,5,0,10q�6-   .+�5�   &   �� 
���������60   �� 5�   ,   5�   #   5�      5�      5�      5�      5�      5�      5�      