using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisualTreeHelperDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// 遍历TopGrid中的Button信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_One_Click(object sender, RoutedEventArgs e)
        {
            string btnName = "";
            List<Button> btnList = FindVisualChild<Button>(TopGrid);
            foreach (Button item in btnList)
            {
                btnName += string.IsNullOrEmpty(btnName) ? item.Name.ToString() : "," + item.Name.ToString();
            }
            MessageBox.Show(string.Format(TopGrid.Name.ToString()+"共有{0}个Button,名称分别为{1}", btnList.Count, btnName));
        }

        /// <summary>
        /// 遍历Button2父级对象信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Two_Click(object sender, RoutedEventArgs e)
        {
            string parentName = "";
            List<Grid> gridList = FindVisualParent<Grid>(btn_Two);
            foreach (Grid item in gridList)
            {
                parentName += string.IsNullOrEmpty(parentName) ? item.Name.ToString() : "," + item.Name.ToString();
            }
            MessageBox.Show(string.Format(btn_Two.Name.ToString() + "共有{0}个逻辑父级,名称分别为{1}", gridList.Count, parentName));
        }

        /// <summary>
        /// 利用visualtreehelper寻找窗体中的子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<T> FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        TList.Add((T)child);
                    }
                    else
                    {
                        List<T> childOfChildren = FindVisualChild<T>(child);
                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                }
                return TList;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return null;
            }
        }

        /// <summary>
        /// 利用VisualTreeHelper寻找指定依赖对象的父级对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                DependencyObject parent = VisualTreeHelper.GetParent(obj);
                if (parent != null && parent is T)
                {
                    TList.Add((T)parent);
                    List<T> parentOfParent = FindVisualParent<T>(parent);
                    if (parentOfParent != null)
                    {
                        TList.AddRange(parentOfParent);
                    }
                }
                else 
                {
                }
                return TList;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return null;
            }
        }

    }


}
