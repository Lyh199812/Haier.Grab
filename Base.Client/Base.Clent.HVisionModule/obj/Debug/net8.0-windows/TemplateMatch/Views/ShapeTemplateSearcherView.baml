   M S B A M L   `   `   `  ����S  OBase.Clent.HVisionModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Base.Clent.HVisionModule.Preprocessing.Views,Base.Clent.HVisionModule.Preprocessing.Views  5      T Phalcondotnet, Version=22.5.0.0, Culture=neutral, PublicKeyToken=4973bed59ddbf2b8A0clr-namespace:HalconDotNet;assembly=HalconDotNetHalconDotNet 5      M IHVisionLibs.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulleEclr-namespace:HVisionLibs.Shared.Controls;assembly=HVisionLibs.SharedHVisionLibs.Shared.Controls 5
      L    FBase.Clent.HVisionModule.TemplateMatch.Views.ShapeTemplateSearcherView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Llocal:clr-namespace:Base.Clent.HVisionModule.Preprocessing.Views     5      Chalcon0clr-namespace:HalconDotNet;assembly=HalconDotNet     5      ZcontrolsEclr-namespace:HVisionLibs.Shared.Controls;assembly=HVisionLibs.Shared     5
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
���������5%      �� *Service.info.Description+5'      �� 5(      �� 5)      $�Current Mode:��6    �� 5*      �6   �� *Service.ModeName+5+      5,      �� 5.   
   $��1��6   
�������$��0,1,0,0q�50      $��0,0,0,15q�51      �� 53   
   $��1��6   .@�54      �� $��开始使用��6   .��55      �  � ColumnDefinitions 56      �� 57      $
��0.3* �6+   �� 58      .+�5<      � .+�5=      �� $
��10,0q�6%   .@�5>      �� $��基本参数��6&   .��5?   "   �  5@   &   �� 5A   *   �� 5B   *   $
��auto �68   .+�5E   &   �� $��Hidden=�63   .��5F   *   �� .+�5G   .   9� $��	图像源��67   
���������5I   2   �� 5J   6   $��0,5,5,5q�6A   .+�5K   :   �� $��加载图像��6A   ��5L   9   �� *LoadImageCommand+5M   7   5O   /   9� 5Q   .   $��	模板源��67   
���������5R   2   �� 5S   6   $��0,5,5,5q�6A   .+�5T   :   �� $��加载模板��6A   ��5U   A   �� *LoadModeCommand+5V   7   5X   /   9� 5Z   .   $��	ROI范围��67   
���������5\   2   �� 5]   6   $��0,5,5,5q�6A   .+�5^   :   �� $��设置识别范围��6A   ��5_   9   �� *SetRangeCommand+5`   7   5b   /   9� 5e   .   $��运行参数��67   
���������5g   2   �� 5h   6   .+�5i   :   �� $��
AngleStart��6D   �� 5j   :   $��0,5,5,5q�6B   $��5q�5k   :    !  MaterialDesignOutlinedTextBox#��[  5m   :   
���������5l   :   �� *!Service.RunParameter.AngleStart+5n   :   �� 5o   :   $��AngleExtent��6D   �� 5p   :   $��0,5,5,5q�6B   $��5q�5q   :   #��[  5s   :   
���������5r   :   �� *" Service.RunParameter.AngleExtent+5t   :   �� 5u   :   $��MinScore��6D   �� 5v   :   $��0,5,5,5q�6B   $��5q�5w   :   #��[  5y   :   
���������5x   :   �� *Service.RunParameter.MinScore+5z   :   �� 5{   :   $��
NumMatcher��6D   �� 5|   :   $��0,5,5,5q�6B   $��5q�5}   :   #��[  5   :   
���������5~   :   �� *!Service.RunParameter.NumMatches+5�   :   �� 5�   :   $��
MaxOverlap��6D   �� 5�   :   $��0,5,5,5q�6B   $��5q�5�   :   #��[  5�   :   
���������5�   :   �� *!Service.RunParameter.MaxOverlap+5�   :   �� 5�   :   $��SubPixel��6D   �� 5�   :   $��0,5,5,5q�6C   $��5q�5�   ;    " MaterialDesignOutlinedComboBox#��[ 5�   ;   
������� �� Text 5�   ;   �� *Service.RunParameter.SubPixel+.@�5�   >   �� .��6K   true6Q   �� 5�   >   .��6K   false6R   5�   ;   �� 5�   :   $��	NumLevels��6D   �� 5�   :   $��0,5,5,5q�6C   $��5q�5�   ;   #��[ 5�   ;   
������� 5�   ;   �� * Service.RunParameter.NumLevels+.@�5�   >   �� .��6K   06N   �� 5�   >   .��6K   16N   �� 5�   >   .��6K   26N   �� 5�   >   .��6K   36N   �� 5�   >   .��6K   46N   �� 5�   >   .��6K   56N   �� 5�   >   .��6K   66N   �� 5�   >   .��6K   76N   �� 5�   >   .��6K   86N   �� 5�   >   .��6K   96N   �� 5�   >   .��6K   106O   5�   ;   5�   7   5�   /   5�   +   5�   '   �� 5�   &   $��1��61   $��0,5,5,5q�5�   )   .+�5�   *   �� $��执行��61   ��5�   )   �� *
RunCommand+5�   '   5�   #   5�      �� 5�      $��结果显示��6&   .��5�   "   �� $��10q�6-   .+�5�   &   ]� $��False��60   .+�5�   *   �� $��16(�64   $��显示文本��5�   ,   d� 5�   *   $��Right=�67    d� 	IsChecked 5�   /   �� *#!Service.Setting.IsShowDisplayText+5�   '   ]� 5�   &   $��False��60   $
��0,10q�5�   (   .+�5�   *   �� $��16(�64   $��显示中点��5�   ,   d� 5�   *   $��Right=�67    5�   /   �� *Service.Setting.IsShowCenter+5�   '   ]� 5�   &   $��False��60   .+�5�   *   �� $��16(�64   $��显示检测范围��5�   ,   d� 5�   *   $��Right=�67    5�   /   �� *" Service.Setting.IsShowMatchRange+5�   '   5�   #   5�      5�      5�      � 5�      $��1��6    5�      �� 5�      �� 5�      $
��0.3* �6,   �� 5�      $
��auto �6,   .+�5�      /  )HVisionLibs.Shared.Controls.ImageEditView     Image 61   �� *Image+   DrawObjectList 5�   )   �� *DrawObjectList+	   MatchResult	 5�   )   �� *MatchResult+
   
MaskObject
 5�   )   �� *
MaskObject+ �� Mode$ TwoWay=�&   System.Windows.Controls.DataGrid  5�      $��1��6#      AutoGenerateColumns$ False��5�         CanUserAddRows$ False��5�         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5�         
IsReadOnly$
 True��5�      ��5�      �� *MatchResult.Results+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�   "   ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�   -   �� *Index+  5�   "   $ 中点坐标X��65    5�   -   �� *Row+  5�   "   $ 中点坐标Y��65    5�   -   �� *Column+  5�   "   $ 角度��65    5�   -   �� *Angle+  5�   "   $ 分数��65    5�   -   �� *Score+5�      �� 5�      $��2��6$   $��5q�5�      ��5�      �� *OperateResult.Message+5�      5�      5�      �� 5�      $��参数说明��6   .��5�      �� $��Hidden=�6   .��5�      �� $
��15,5q�6#   ��5�      �� *Service.info.Parameters+��5�      �� 5�      .R�5�   "   �� 5�      ��5�      �� 5�      .R�5�   "   �� $��0,5,0,10q�6-   .+�5�   &   �� 
�������$
��Bold"�5�   (   $��14(�5�   (   ��5�   0   �� *Name+5�   (   �� 5   &   
�������$��20,5,0,0q�5  (   ��5   0   �� *Description+5  (   5  #   5     5     5     5     �� 5	     $��关联算子��6   .��5
     �� $
��Auto=�6   .��5     �� $
��15,5q�6#   ��5     �� *Service.info.Predecessors+��5     �� 5     .R�5  "   �� 5     ��5     �� 5     .R�5  "   �� $��0,5,0,10q�6-   .+�5  &   �� 
���������60   �� 5  (   5  #   5     5     5     5     5      5!     5"     