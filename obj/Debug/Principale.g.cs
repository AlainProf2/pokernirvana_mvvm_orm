﻿#pragma checksum "..\..\Principale.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "82386A51BC7BA8C7C9FD29BA39A61CDA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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


namespace PokerNirvana_MVVM_ORM {
    
    
    /// <summary>
    /// Principale
    /// </summary>
    public partial class Principale : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\Principale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRetour;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Principale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInfo;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Principale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnParam;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\Principale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAide;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\Principale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock nbrNotif;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\Principale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ToolBar toolBarNotif;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\Principale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentPresenter presenteurContenu;
        
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
            System.Uri resourceLocater = new System.Uri("/PokerNirvana_MVVM_ORM;component/principale.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Principale.xaml"
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
            
            #line 6 "..\..\Principale.xaml"
            ((PokerNirvana_MVVM_ORM.Principale)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnRetour = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\Principale.xaml"
            this.btnRetour.Click += new System.Windows.RoutedEventHandler(this.btnRetour_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnInfo = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\Principale.xaml"
            this.btnInfo.Click += new System.Windows.RoutedEventHandler(this.btnAPropos_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnParam = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\Principale.xaml"
            this.btnParam.Click += new System.Windows.RoutedEventHandler(this.btnParam_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnAide = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\Principale.xaml"
            this.btnAide.Click += new System.Windows.RoutedEventHandler(this.btnAide_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.nbrNotif = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.toolBarNotif = ((System.Windows.Controls.ToolBar)(target));
            return;
            case 8:
            this.presenteurContenu = ((System.Windows.Controls.ContentPresenter)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

