﻿#pragma checksum "..\..\..\Pages\CreationIntervenant.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AE4125F42F1313CFBFF9AEF4424B4DDD58FAF8C8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Eco;
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


namespace Eco {
    
    
    /// <summary>
    /// CreationIntervenant
    /// </summary>
    public partial class CreationIntervenant : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPrenomUtilisateur;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNomUtilisateur;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFonction;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMail;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLogin;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPassword;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Pages\CreationIntervenant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLogin;
        
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
            System.Uri resourceLocater = new System.Uri("/Eco;component/pages/creationintervenant.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\CreationIntervenant.xaml"
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
            
            #line 16 "..\..\..\Pages\CreationIntervenant.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnRetour);
            
            #line default
            #line hidden
            return;
            case 2:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Pages\CreationIntervenant.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.btnClose);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtPrenomUtilisateur = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtNomUtilisateur = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtFonction = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtMail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\Pages\CreationIntervenant.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnLogin = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\Pages\CreationIntervenant.xaml"
            this.btnLogin.Click += new System.Windows.RoutedEventHandler(this.btnValid);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

