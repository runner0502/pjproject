﻿#pragma checksum "..\..\GroupWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "473896767479848C6F7BE7A08CAF0FA5E07E5153"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ClientDemo {
    
    
    /// <summary>
    /// GroupWindow
    /// </summary>
    public partial class GroupWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 19 "..\..\GroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddGroup;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\GroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView GroupTree;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\GroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu m1;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\GroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miEditDgna;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\GroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miAddDev;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\GroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miDelGroup;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\GroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miDelDev;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\GroupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miMonitor;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClientDemo;component/groupwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GroupWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnAddGroup = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\GroupWindow.xaml"
            this.btnAddGroup.Click += new System.Windows.RoutedEventHandler(this.btnAddGroup_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GroupTree = ((System.Windows.Controls.TreeView)(target));
            
            #line 20 "..\..\GroupWindow.xaml"
            this.GroupTree.PreviewMouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.GroupTree_PreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.m1 = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 5:
            this.miEditDgna = ((System.Windows.Controls.MenuItem)(target));
            
            #line 37 "..\..\GroupWindow.xaml"
            this.miEditDgna.Click += new System.Windows.RoutedEventHandler(this.miEditDgna_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.miAddDev = ((System.Windows.Controls.MenuItem)(target));
            
            #line 38 "..\..\GroupWindow.xaml"
            this.miAddDev.Click += new System.Windows.RoutedEventHandler(this.miAddDev_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.miDelGroup = ((System.Windows.Controls.MenuItem)(target));
            
            #line 39 "..\..\GroupWindow.xaml"
            this.miDelGroup.Click += new System.Windows.RoutedEventHandler(this.miDelGroup_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.miDelDev = ((System.Windows.Controls.MenuItem)(target));
            
            #line 40 "..\..\GroupWindow.xaml"
            this.miDelDev.Click += new System.Windows.RoutedEventHandler(this.miDelDev_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.miMonitor = ((System.Windows.Controls.MenuItem)(target));
            
            #line 41 "..\..\GroupWindow.xaml"
            this.miMonitor.Click += new System.Windows.RoutedEventHandler(this.miMonitor_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 3:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.PreviewMouseRightButtonDownEvent;
            
            #line 31 "..\..\GroupWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.TreeViewItem_PreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}
