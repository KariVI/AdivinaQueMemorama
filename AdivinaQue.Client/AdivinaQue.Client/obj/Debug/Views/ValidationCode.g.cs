﻿#pragma checksum "..\..\..\Views\ValidationCode.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7B1B493E15F51ADC4886F182BB9DF539066E3913D4D8D300AF4C0920729DA89C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using AdivinaQue.Client.Views;
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


namespace AdivinaQue.Client.Views {
    
    
    /// <summary>
    /// ValidationCode
    /// </summary>
    public partial class ValidationCode : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Views\ValidationCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCode;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Views\ValidationCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbCode;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Views\ValidationCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EnterBt;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Views\ValidationCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbEmail;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\ValidationCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbEmail;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Views\ValidationCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SendCodeBt;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Views\ValidationCode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbEmail_Copy;
        
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
            System.Uri resourceLocater = new System.Uri("/AdivinaQue.Client;component/views/validationcode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ValidationCode.xaml"
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
            this.tbCode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.lbCode = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.EnterBt = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\Views\ValidationCode.xaml"
            this.EnterBt.Click += new System.Windows.RoutedEventHandler(this.EnterBt_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lbEmail = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.tbEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.SendCodeBt = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Views\ValidationCode.xaml"
            this.SendCodeBt.Click += new System.Windows.RoutedEventHandler(this.SendCodeBt_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lbEmail_Copy = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

