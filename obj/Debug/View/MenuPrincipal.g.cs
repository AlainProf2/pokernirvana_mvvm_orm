﻿#pragma checksum "..\..\..\View\MenuPrincipal.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A2087D1B1E259C6C18AAFFCCC4AD22FA"
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


namespace PokerNirvana_MVVM_ORM.View {
    
    
    /// <summary>
    /// MenuPrincipal
    /// </summary>
    public partial class MenuPrincipal : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\View\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInfoMembre;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\View\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConnexion;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\View\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreationTournoisParties;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\View\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReprise;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\View\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStatistiques;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\View\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnJouer;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\View\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTournoisListe;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\View\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTournoisParties;
        
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
            System.Uri resourceLocater = new System.Uri("/PokerNirvana_MVVM_ORM;component/view/menuprincipal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\MenuPrincipal.xaml"
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
            this.btnInfoMembre = ((System.Windows.Controls.Button)(target));
            
            #line 7 "..\..\..\View\MenuPrincipal.xaml"
            this.btnInfoMembre.Click += new System.Windows.RoutedEventHandler(this.InformationMembre);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnConnexion = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\..\View\MenuPrincipal.xaml"
            this.btnConnexion.Click += new System.Windows.RoutedEventHandler(this.Connexion);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnCreationTournoisParties = ((System.Windows.Controls.Button)(target));
            
            #line 9 "..\..\..\View\MenuPrincipal.xaml"
            this.btnCreationTournoisParties.Click += new System.Windows.RoutedEventHandler(this.CreationTournoisParties);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnReprise = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\View\MenuPrincipal.xaml"
            this.btnReprise.Click += new System.Windows.RoutedEventHandler(this.Reprise);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnStatistiques = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\View\MenuPrincipal.xaml"
            this.btnStatistiques.Click += new System.Windows.RoutedEventHandler(this.Statistiques);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnJouer = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\View\MenuPrincipal.xaml"
            this.btnJouer.Click += new System.Windows.RoutedEventHandler(this.Jouer);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnTournoisListe = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\View\MenuPrincipal.xaml"
            this.btnTournoisListe.Click += new System.Windows.RoutedEventHandler(this.TournoisListe);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnTournoisParties = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\View\MenuPrincipal.xaml"
            this.btnTournoisParties.Click += new System.Windows.RoutedEventHandler(this.TournoisParties);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

