   M S B A M L   `   `   `  ����O  KBase.Client.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Base.Client.ControlsBase.Client.Controls  5      R Nnetstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd514)clr-namespace:System;assembly=netstandardSystem 5      a ]PresentationFramework.Aero, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35gJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroMicrosoft.Windows.Themes 5	      %    Base.Client.Controls.Pagination   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Base.Client.Controls     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  5      3sys)clr-namespace:System;assembly=netstandard  5      WThemesJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero  5	        � 	Resources  5         
movingPath 
 counts �� Type  MouseOver.Background �� EndPoint �� 
StartPoint � Color � Offset �� 
TargetType  FlipButtonStyle �� Property �� Value	 �� 
TargetType
 �� CornerRadius �� RelativeSource �� Mode �� Triggers P� Property P� Value  PageNumberButtonStyle  ComboBoxToggleButton ��Name J� SnapsToDevicePixels F�Name �� Triggers ^� 
Conditions �� Binding �� Value �� AncestorType �� 
TargetName  ComboBoxTemplate �Name � ColumnDefinitions �Name � AllowsTransparency �� Mode5  /Microsoft.Windows.Themes.SystemDropShadowChrome  Name   Color  �� MaxDropDownHeight! �� ElementName!  System.Windows.SystemColors"   WindowBrushKey# ��Name$ �� ClearTypeHint% ��Name& ��Name' ��Name-  'System.Windows.Input.KeyboardNavigation(   DirectionalNavigation) d�Name* d� 	IsChecked+ ��Name#, �� SelectionBoxItemStringFormat- �� ContentStringFormat. J� IsHitTestVisible/ P� 
SourceName  PagesComboBoxStyle0 �� Path��%b  &	        &	 '     &	 �     &	 m    7  &	      &	 �    7  &	 �
    7  &	 �    7  '�� �    B� 5   
   $��M5,0 0 5 5 10C�6*   �� 5   
   " ��6!   .s�5      ��10��6   ��5      15��6   ��5      20��6   ��5      30��6   ��5      50��6   5      �� 5   
   $	 0,1&�5      $	 0,0&�6-   .��5      � $ 	#FFECF4FC��6   $	 0.0Q�6-   � 5      $ 	#FFDCECFC��6   $	 1.0Q�6-   5      �� 5   
   " ��6   .�5      ��  � �� �#5556+   �� 5       � ��
 ���� 6+   �� 5        � ��$ 50��6&   �� 5!       � �� 5"      �� 5#      "	 ��6&   .R�5$      �� $
��back��5%      #��z��5$   !   
 6K   �� *Tag+ �� $ TemplatedParent=�._�5&      �� $��content��6g   $��Center=�6/   $��Center=�6J   6g   5'      5(      5*       5+      P� 5,       �@J�IsMouseOver$
 True��61   . �5-      ��  � ��8   63   �� 5.       � �� �	#FF7EB4EA64   5/      P� 50       � {�$ False��6/   . �51      ��  � �� �#CCC63   52      54      �� 57   
   " 	�6   .�58      ��  �@	�	GroupName$ number_button��6*   �� 59       � ��$	 1,0q�6'   �� 5:       � �� �#8886+   �� 5;       � ��
 ���� 6+   �� 5<       � �� 5=      �� 5>      "	 	�6&   .R�5?      �� $
��root��6K   #��z��6!   $
 5��6W   $��25��5@   !   $��25��6-   ._�5A      �� $��Center=�6?   $��Center=�6Z   ��6(   �� *Index+6Z   5B      5C      5E       5F      P� 5G       �@J�IsMouseOver$
 True��61   . �5H      ��  � �� �#EEE63   5I      P� 5J       �@d�	IsChecked$
 TrueY�6/   . �5K      ��  � �� �	#FF0ABEFF63   �� 5L       � ��
 �����63   5M      5O      �� 5Q   
   " d�6-   .�5R      ��  �@�OverridesDefaultStyle$
 true��66   �� 5S       � ��$ false��6*   �� 5T       � |�$ false��6*   �� 5U       �@��	ClickMode$ Press=�6*   �� 5V       � �� 5W      �� 5X      "	 d�6&   .R�5Y      �� $ templateRoot��6!   $
 true��67   
������ $��0q�5Z   !   $
 	0,10,10,0��65   ._�5[      �� $ splitBorder��6%   $��25��6:   $
 true��6E   $��0q�6`   $��Right=�6k   $��1q�6�   
������ ._�5\   "   F� $ arrow��6'   $��Center=�66   $��0q�6Q   $��Center=�6\   
�������$��1��5]   5   "����        �@KL @KL р��     	5^   '   5_      5`       5a      ^� 5b       5c   "   �� 5d   &    60   �� *IsMouseOver+ �� *Self+ true6v   �� 5e   &    60   �� *
IsEditable+ �� " ��	 false6�   .0�5g   "   �� $ templateRoot��6?   8    � ��6Y   �� 5h   "   $ templateRoot��6@    � �� �	#FF7EB4EA6Z   5i      5k      5m      5n      �� 5p   
   "	 ��63   .R�5q      �� 
������ $
 	0,10,10,0��6.   ._�5r      � $ templateRoot��6   $
 true��6-    5s      �� 5t      $��* �6+   �� 5u      $��0 �6+   .+�5w      � $ 
PART_Popup��6   $
 true��60   $��2��6J   $��1q�6�   #��� .�6�   $��Bottom=�6"  ��6^   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.��5x        $ shadow��68   $ Transparent��6H   #��z  6\   ��6�   �� *ActualWidth+$! templateRoot��._�5y      �� $ dropDownBorder��6%   ���#EEE$��1q�6P   #��� " 6d   ._�5z   "   �� $# DropDownScrollViewer��6/   .��5{   &   � $
 grid��6+   $$ Enabled=�69   .+�5|   *   �� $% canvas��61   $
��Left=�6A   $��0��6\   $	��Top=�6g   $��0��6   .+�5}   .   �� $& 
opaqueRect��68   ��6L   �� *
Background+$! dropDownBorder����6�   �� *ActualHeight+$! dropDownBorder����6�   �� *ActualWidth+$! dropDownBorder��5~   +   �� 5   *   $' ItemsPresenter��69   $( 	Contained=�6Q   # z 6�   5�   '   5�   #   5�      5�      5�      d� 5�      $) toggleButton��6#   $��0q�6:   
������ $��2��5�      8��  * 63   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+5�      �� 5�      $+ contentPresenter��6%   $	��5,0q�6?   #��z��5�   #   #- z, 5�   #   #��z��5�   #   $. false��5�   #   # z 5�   #   $��Center=�5�   #   .��5�      �� �� 5�      �6"   �� *SelectionBoxItem+ ��  ComboBox$ FindAncestor=�$ OneWay=� �� 5�      $�条/页��6"   5�      5�      5�      5�       5�      P� 5�      $/ 
PART_Popup��63    �@�HasDropShadow$
 true��6K   . �5�      �� $ shadow��6/    � ��$ 0,5,5,5q�6C   �� 5�      $ shadow��6.    �@ Color$ 	#22000000��6B   5�      P� 5�       �@��HasItems$ false��6.   . �5�      �� $ dropDownBorder��6/    � ��$ 95��6K   5�      P� 5�      $/ DropDownScrollViewer��6C    � ��$ false��6e   . �5�      �� $ 
opaqueRect��63    6K   �� *VerticalOffset+$! DropDownScrollViewer�� �@��Top�� 5�      $ 
opaqueRect��64    6L   �� *HorizontalOffset+$! DropDownScrollViewer�� �@��Left5�      5�      �� 5�   
   " ��6+   .�5�      ��  �@�FocusVisualStyle 61   Z� �� 5�       � ��
 ���� 6+   �� 5�       � �� �#7776+   �� 5�       � ��$ 0q�60   �� 5�       � ��$
 Auto=�6K   �� 5�       � ��$
 Auto=�6I   �� 5�       � ��$ 6,3,5,3q�6(   �� 5�       � ��$
 true��6>   �� 5�       �@��PanningMode$
 Both=�69   �� 5�       �@��IsFlicksEnabled$ False��67   �� 5�       � ��8   6)   5�      �� 5�   
   " ��6   .�5�      ��  � ��$ 30��6'   �� 5�       � �� 5�      �� 5�      "	 ��6&   .R�5�      �� #��z��6!   ._�5�      �� $��Center=�6(   $	��5,0q�6C   �� 5�   "   �6&   �� $0 .�$ OneWay=� �� 5�   "   $�条/页��6&   5�      5�      5�      5�       5�      P� 5�       �@J�IsMouseOver$
 True��61   . �5�      ��  � �� �	#220ABEFF63   5�      P� 5�       �@��
IsSelected$
 True��60   . �5�      ��  � �� �	#FF0ABEFF63   �� 5�       � ��
 �����63   5�      5�      5�      .��5�      �  5�   
   �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $
��auto �6   .+�5�   
   �� #��[ 6   
1 � Tag$1 	10,0,0,10��6:   {�5�      �� *IsCanPrevious+��5�      �� *
NavCommand+��64   �� *PreviousIndex+.��5�      (� $��12��6   $��12��6"   .��5�      F� #��[  6   $��1��6�   2 �� StrokeStartLineCap$2 Round=�5�      3 �� StrokeEndLineCap$3 Round=�66   ��5�   :   �� *
Foreground+ �� 
 Button5�   6   5�      5�      �� 5�   
   $��2��6   
������ ��6@   �� *PageNumbers+��5�      �� 5�      .R�5�      A� 4 A� Rows$4 1��6"   5�      ��5�      �� 5�      .R�5�      	� #��[ 6"   ��5�   &   �� *DataContext.NavCommand+ ��  UserControl$ FindAncestor=���5�   &   �� *Index+{�5�   &   �� *	IsEnabled+* 5�   &   �� *	IsCurrent+5�      5�      �� 5�   
   $��4��6   #��[ 6!   {�5�      �� *	IsCanNext+��5�      �� *
NavCommand+��64   �� *	NextIndex+.��5�      (� $��12��6   $��12��6"   .��5�      F� #��[  6   $��1��6�   $2 Round=�5�      $3 Round=�66   5 J� RenderTransformOrigin$5 0.5,0.5&�6O   ��5�   :   �� *
Foreground+ �� 
 Buttonz�5�      �� 5�      6 �� Angle$	6 180Q�6*   5�      5�      5�      �� 5�   
   $��5��6   $��80��6#   #���  6.   7 �� SelectedIndex$7 0��5     8 �� SelectedValue8 5�      �� *CountPerPage+��5      �� 9 �� Source#9 [ *  $Microsoft.Xaml.Behaviors.Interaction:   Triggers: 5     +  %Microsoft.Xaml.Behaviors.EventTrigger  5     ;   	EventName$; SelectionChanged��6!   *  $Microsoft.Xaml.Behaviors.TriggerBase<   Actions.< 5     2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  =   Command= 6,   �� *CountPerPageChangeCommand+5     5     5     5	     