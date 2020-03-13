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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ClientDemo
{
    /// <summary>
    /// GroupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GroupWindow : Window
    {
        List<GroupEntry> categories;     //声明动态分组对象
        public GroupWindow()
        {
            InitializeComponent();
        }
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;

                if (this.GroupTree.SelectedItem != null)
                {
                    GroupEntry ge = (GroupEntry)this.GroupTree.SelectedItem;
                    if (string.IsNullOrEmpty(ge.PID))
                    {
                        this.miDelDev.Visibility = Visibility.Collapsed;
                        this.miMonitor.Visibility = Visibility.Visible;
                        this.miAddDev.Visibility = Visibility.Visible;
                        this.miDelGroup.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.miDelDev.Visibility = Visibility.Visible;
                        this.miMonitor.Visibility = Visibility.Collapsed;
                        this.miAddDev.Visibility = Visibility.Collapsed;
                        this.miDelGroup.Visibility = Visibility.Collapsed;
                    }
                }

                //this.m1.Visibility = Visibility.Visible;
                //Point pp = Mouse.GetPosition(e.Source as FrameworkElement);//WPF方法
                //m1.Margin = new Thickness(pp.X, pp.Y, 0, 0);
            }
            else
            {
                //this.m1.Visibility = Visibility.Collapsed;
            }
        }

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }


        private List<GroupEntry> GetCategories(XElement element)
        {
            return (from GroupEntry in element.Elements("dgna")
                    select new GroupEntry()
                    {
                        Value = GroupEntry.Element("dgna_name").Value,
                        Name = GroupEntry.Element("dgna_name").Value,
                        SystemID = GroupEntry.Element("systemId").Value,
                        SubGroupEntry = GetCategoriesDeviceEntry(GroupEntry.Element("member_list"), GroupEntry.Element("dgna_name").Value)
                        //SubGroupEntry = this.GetCategories(GroupEntry)

                    }).ToList();
        }

        private List<GroupEntry> GetCategoriesDeviceEntry(XElement element, string PID)
        {
            List<XElement> list = element.Elements("member").ToList<XElement>();
            return (from GroupEntry in element.Elements("member")
                    select new GroupEntry()
                    {
                        PID = PID,
                        Value = GroupEntry.Element("number").Value,
                        Name = GroupEntry.Element("number").Value,
                        //SubGroupEntry = GetCategoriesDeviceEntry(GroupEntry.Element("member_list"))
                    }).ToList();
        }


        private void GroupTree_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            if (treeViewItem != null)
            {
                GroupTree.ContextMenu = m1;
            }
            else
            {
                GroupTree.ContextMenu = null;
            }
        }

        /// <summary>
        /// 新增组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            //BusinessCenter.Instance.add_dgna();
            string systemID = string.Empty;
            string groupName = string.Empty;
            var addGroup = new AddGroupWindow("ADD");
            addGroup.Owner = this;
            addGroup.ShowDialog();
            if (addGroup.DialogResult == true)
            {
                systemID = addGroup.txtSystemId.Text;
                groupName = addGroup.txtName.Text;
                //新增动态重组
                //BusinessCenter.Instance.add_dgna(systemID, groupName);
                //Thread.Sleep(3000);
                ////重新加载动态重组
                //BusinessCenter.Instance.dgna_request();

                if (categories != null)
                {
                    GroupEntry ge = new GroupEntry();
                    ge.Name = groupName;
                    ge.Value = groupName;
                    ge.SubGroupEntry = null;
                    categories.Add(ge);
                    GroupTree.ItemsSource = categories;
                    GroupTree.Items.Refresh();

                }


            }
        }

        /// <summary>
        /// 新增成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAddDev_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 删除动态重组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDelGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDelDev_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 动态重组监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miMonitor_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 动态添加节点
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="treeViewItem"></param>
        /// <param name="areaList"></param>
        private void AddTreeNode(string parentID, TreeViewItem treeViewItem, List<GroupEntry> areaList)
        {
            List<GroupEntry> tree = (from li in areaList
                                     where li.PID == parentID
                                     select li
                                  ).ToList<GroupEntry>();
            if (tree.Count > 0)
            {
                foreach (GroupEntry area in tree)
                {
                    TreeViewItem objTreeNode = new TreeViewItem();
                    objTreeNode.Header = area.Name;
                    objTreeNode.DataContext = area;
                    objTreeNode.IsExpanded = true;
                    if (treeViewItem == null)
                    {
                        GroupTree.Items.Add(objTreeNode);
                    }
                    else
                    {
                        treeViewItem.Items.Add(objTreeNode);
                    }
                    AddTreeNode(area.PID, objTreeNode, areaList);
                }
            }
        }

        /// <summary>
        /// 编辑动态重组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEditDgna_Click(object sender, RoutedEventArgs e)
        {
            string systemID = string.Empty;
            string groupName = string.Empty;
            var addGroup = new AddGroupWindow("EDIT");
            addGroup.Owner = this;
            addGroup.ShowDialog();
            if (addGroup.DialogResult == true)
            {

            }
        }
    }
}
