﻿#pragma checksum "..\..\..\Views\GameConfiguration.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7B0621CE242466E24A39467ED543E2DBB0B0A1D4C06033F6EB3F699A6B86EC26"
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
    /// GameConfiguration
    /// </summary>
    public partial class GameConfiguration : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Views\GameConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSizeBoard;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Views\GameConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConfirmBt;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\GameConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbxTopics;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\GameConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiFirstTopic;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Views\GameConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiSecondTopic;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\GameConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiThirdTopic;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\GameConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBoxItem lbiFourthTopic;
        
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
            System.Uri resourceLocater = new System.Uri("/AdivinaQue.Client;component/views/gameconfiguration.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\GameConfiguration.xaml"
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
            
            #line 8 "..\..\..\Views\GameConfiguration.xaml"
            ((AdivinaQue.Client.Views.GameConfiguration)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbSizeBoard = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.ConfirmBt = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Views\GameConfiguration.xaml"
            this.ConfirmBt.Click += new System.Windows.RoutedEventHandler(this.ConfirmBt_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lbxTopics = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.lbiFirstTopic = ((System.Windows.Controls.ListBoxItem)(target));
            return;
            case 6:
            this.lbiSecondTopic = ((System.Windows.Controls.ListBoxItem)(target));
            return;
            case 7:
            this.lbiThirdTopic = ((System.Windows.Controls.ListBoxItem)(target));
            return;
            case 8:
            this.lbiFourthTopic = ((System.Windows.Controls.ListBoxItem)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

