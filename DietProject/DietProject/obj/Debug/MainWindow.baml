   M S B A M L   `   `   `  ����F  BDietProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DietProjectDietProject  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 61       DietProject.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DietProject     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     61   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6r   -   5           MaterialDesignPaper#���   5	   	     MaterialDesignBody#���  5
   	     � Title$  
MainWindow��5   	   $	��526��6   $��882.4��6)   .��5      � .+�5   
   �� .@�5      �� $��Add Meal��6   .��5      �  � ColumnDefinitions 5      �� 5      $��2* �6+   �� 5      $��2* �6+   �� 5      $��2* �6+    � RowDefinitions 5      .+�5      )  #MaterialDesignThemes.Wpf.DialogHost   $  MaterialDesignEmbeddedDialogHost#��[ 60   $��Stretch=�5   $   $��Stretch=�5   $      DialogMargin$ 0q�5   $   $��1��5   $      DialogContent 5      � 5      $��Center=�6#   $��Center=�6>   .+�5    "   � $
��Left=�6'   $	��Top=�6B   $	��118��6Z   $��125,118,0,0q�6f    5!   &   �� 5"   *   $��1* �6;   �� 5#   *   $��3 �6;   �� 5$   *   $��1* �6;   �� 5%   *   $��1* �6;   .+�5'   &   �� -   5         ��Name$ hour_tbx��5'   .   $��0��6@   �� 5(   &   $��1��60   $��:��6@   �� 5)   &   -   5        $ 
minute_tbx��5)   .   $��2��6B   �� 5*   &   -   5         ��Name$ time_cbx��5*   /   $��3��6A   $��5,-5,-13.6,0q�6Q   $��Bottom=�6g   $��25��6�   .@�5+   *   ��  �� 
IsSelected . .��6I   AM6M   �� 5,   *   .��67   PM6;   5-   '   5.   #   �� 5/   "   $
��Left=�6,   $��
102,27,0,0q�6G   $
��Wrap=�6[   $	��Top=�6o    J� RenderTransformOrigin$ -0.468,0.652&�6�   $
��Meal��6�   $��22(�6�   �� 50   "   $
��Left=�6,   $��
23,210,0,0q�6G   $
��Wrap=�6[   $��Carbohydrates��6o   $	��Top=�6�   �� 51   "   $
��Left=�6,   $��
58,165,0,0q�6G   $
��Wrap=�6[   $��Calories��6o   $	��Top=�6   $ 2.552,-0.288&�6�   �� 52   "   $
��Left=�6,   $��	68,75,0,0q�6G   $
��Wrap=�6Z   $
��Name��6n   $	��Top=�6z   �� 53   "   $
��Left=�6,   $��
58,247,0,0q�6G   $
��Wrap=�6[   $	��Top=�6o   $ -0.167,0.327&�6�   $��16��6�   $��42��6�   �� 6�   $�Protein��6�   �� 6�   �� 6�   6�   �� 54   "   $
��Left=�6,   $��
75,291,0,0q�6G   $
��Wrap=�6[   $	��Top=�6o   $ -0.167,0.327&�6�   $	��Fat��6�   �� 55   "   -   5        $ MealName_tbx��55   *   $
��Left=�6@   $��23��6[   $��
125,67,0,0q�6g   	 �� TextWrapping$
	 Wrap=�6{   $	��Top=�6�   $	��134��6�   �� 56   "   -   5        $ MealCalories_tbx��56   *   $
��Left=�6D   $��23��6_   $��125,158,0,0q�6k   $
	 Wrap=�6�   $	��Top=�6�   $	��134��6�   �� 57   "   -   5        $ MealCarb_tbx��57   *   $
��Left=�6@   $��23��6[   $��125,204,0,0q�6g   $
	 Wrap=�6|   $	��Top=�6�   $	��134��6�   �� 58   "   -   5        $ MealProtein_tbx��58   *   $
��Left=�6C   $��23��6^   $��125,244,0,0q�6j   $
	 Wrap=�6   $	��Top=�6�   $	��134��6�   �� 59   "   -	   5        $ MealFat_tbx��59   *   $
��Left=�6?   $��23��6Z   $��125,284,0,0q�6f   $
	 Wrap=�6{   $	��Top=�6�   $	��134��6�   �� 5:   "   $
��Left=�6,   $��
74,119,0,0q�6G   $
��Wrap=�6[   $
��Time��6o   $	��Top=�6{   $��16��6�   $ -0.116,0.55&�6�   �� 5=   "   $��Cancel��6)   $��1��6:   
   CloseDialogCommand#��Z
 6K   $
��Auto��6�   $��9.6,343,150.8,93.8q�6�   �� 5>   "   -
   5         ��Name$ AddMeal_btn��5>   )   $��Add Meal��6>   #��Z
 6Q   $��150.6,343,9.8,93.8q�6�   $
��Auto��6�   5?      .��5A      � .+�5B      �� -   5         ��Name$ 	Meals_lbx��5B   &   $
��Left=�69   $	��152��6T   $��	10,78,0,0q�6a   $	��Top=�6t   $	��271��6�   6�   �� 5C      $
��Left=�6(   $
��Wrap=�6C   $	��Top=�6W   $ -0.468,0.652&�6o   $��22(�6�   $��Meals��6�   $��	10,44,0,0q�6�   �� 5D      $��0��6%   $��Add a new meal��65      OpenDialogCommand#��Z 6N   $��10,295,86.4,0q�5E      $��35��65   $	��Top=�6A   �� 5F      -   5        $ DeleteMeal_btn��5F   %   $��0��6=   $��Delete selected meal��6M   $��10,245,86.4,0q�5G   !   $��35��68   $	��Top=�6D   6\   5H      5I      �� 5K      -   5        $ Days_lbx��5K      $
��Left=�60   $	��246��6K   $��	10,78,0,0q�6X   $	��Top=�6k   $	��270��6�   6�   �� 5M      -   5        $ Day_btn��5M      $	��Add��6.   $
��Left=�6<   $��
205,39,0,0q�6W   $	��Top=�6k   $��75��6�   $��30��6�   (  "System.Windows.Controls.DatePicker  5N      -   5          Name$ day_dp��5N   !   $
��Left=�61   $��	20,39,0,0q�6L   $	��Top=�6_   $��30��6w   $	��165��6�   �� 5O      $��	Day info:��6   $��2��60   $
��Left=�6@   $��25.4,39,0,0q�6[   $	��Top=�6p   $��16(�6�   �� 5P      $��	Calories:��6   $��2��60   $
��Left=�6@   $��25.4,78,0,0q�6[   $	��Top=�6p   �� 5Q      $��Protein:��6   $��2��6/   $
��Left=�6?   $��25.4,140,0,0q�6Z   $	��Top=�6p   �� 5R      $��Carbs:��6   $��2��6-   $
��Left=�6=   $��25.4,109,0,0q�6X   $	��Top=�6n   �� 5S      $
��Fat:��6   $��2��6+   $
��Left=�6;   $��25.4,171,0,0q�6V   $	��Top=�6l   �� 5T      $��
Meal info:��6   $��2��61   $
��Left=�6A   $��25.4,216,0,0q�6\   $	��Top=�6r   $��16(�6�   �� 5U      $��	Calories:��6   $��2��60   $
��Left=�6@   $��25.4,255,0,0q�6[   $	��Top=�6q   �� 5V      $��Protein:��6   $��2��6/   $
��Left=�6?   $��25.4,317,0,0q�6Z   $	��Top=�6p   �� 5W      $��Carbs:��6   $��2��6-   $
��Left=�6=   $��25.4,286,0,0q�6X   $	��Top=�6n   �� 5X      $
��Fat:��6   $��2��6+   $
��Left=�6;   $��25.4,348,0,0q�6V   $	��Top=�6l   �� 5Y      -   5         ��Name$ Cal_tblk��5Y       $��2��62   $
��Left=�6B   $��79.4,260,0,0q�6]   $
��Wrap=�6s   $	��Top=�6�   �� 5Z      -   5        $ 
Carbs_tblk��5Z       $��2��64   $
��Left=�6D   $��79.4,291,0,0q�6_   $
��Wrap=�6u   $	��Top=�6�   �� 5[      -   5        $ Protein_tblk��5[       $��2��66   $
��Left=�6F   $��79.4,322,0,0q�6a   $
��Wrap=�6w   $	��Top=�6�   �� 5\      -   5        $ Fat_tblk��5\       $��2��62   $
��Left=�6B   $��79.4,353,0,0q�6]   $
��Wrap=�6s   $	��Top=�6�   �� 5]      -   5        $ totalCal_tblk��5]       $��2��67   $
��Left=�6G   $��79.4,83,0,0q�6b   $
��Wrap=�6w   $	��Top=�6�   �� 5^      -   5        $ totalCarbs_tblk��5^       $��2��69   $
��Left=�6I   $��79.4,114,0,0q�6d   $
��Wrap=�6z   $	��Top=�6�   �� 5_      -   5        $ totalProtein_tblk��5_       $��2��6;   $
��Left=�6K   $��79.4,144,0,0q�6f   $
��Wrap=�6|   $	��Top=�6�   �� 5`      -   5        $ totalFat_tblk��5`       $��2��67   $
��Left=�6G   $��79.4,176,0,0q�6b   $
��Wrap=�6x   $	��Top=�6�   5a      5b      �� 5d      $��Stats��6   .��5e      �  5f      �� 5g      $��1* �6+   �� 5h      $��8* �6+   �� 5i      $��1* �6+    5k      �� 5l      $��1* �6(   �� 5m      $��8* �6(   �� 5n      $��1* �6(   .+�5r      �� $"��Daily calories for the past ��6   $��1��6C   $��0��6S   $��Bottom=�6`   $��9.8,0,485.2,10.2q�6{   $��16(�6�   �� 5u      -   5        $ PastDays_cbx��5u      $��1��65   $��0��6E   $��215.8,0,337.2,8.2q�6R   $��Bottom=�6m   $��32��6�   $��16(�6�   .@�5v      ��  . .��69   Week6?   �� 5w      .��6'   Month6.   5x      #  LiveCharts.Wpf.CartesianChart  5z      -   5          Name$ 
Stat_Graph��5z   )   &   LiveCharts.Wpf.Charts.Base.Chart   Zoom$
 None=�6=      LegendLocation$
 Left=�6m   $��1��6�   $��1��6�      Series 6I   �� *SeriesCollection+   AxisX 5|        LiveCharts.Wpf.Axis     Title$
 Date��6'      Labels 64   �� *Labels+6P      AxisY 5           MinValue$ 0Q�6'   $ Calories��64      LabelFormatter 6E   �� *	Formatter+6l   5�      5�      5�      5�      5�      5�      