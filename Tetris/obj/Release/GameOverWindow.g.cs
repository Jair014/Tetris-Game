﻿#pragma checksum "..\..\GameOverWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CAA71F0859629D45FAC7BD867B6EF5146F3A5E517A5EDE3E939A683BAE9E89D8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using Tetris;


namespace Tetris {
    
    
    /// <summary>
    /// GameOverWindow
    /// </summary>
    public partial class GameOverWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\GameOverWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label GameOverLabel;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\GameOverWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HomeButton;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\GameOverWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PlayAgainButton;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\GameOverWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitButton;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\GameOverWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label HighScoreText;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\GameOverWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InstructionText;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\GameOverWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InitialsTextBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\GameOverWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Tetris;component/gameoverwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GameOverWindow.xaml"
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
            this.GameOverLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.HomeButton = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\GameOverWindow.xaml"
            this.HomeButton.Click += new System.Windows.RoutedEventHandler(this.HomeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PlayAgainButton = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\GameOverWindow.xaml"
            this.PlayAgainButton.Click += new System.Windows.RoutedEventHandler(this.PlayAgainButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ExitButton = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\GameOverWindow.xaml"
            this.ExitButton.Click += new System.Windows.RoutedEventHandler(this.ExitButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.HighScoreText = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.InstructionText = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.InitialsTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.SubmitButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\GameOverWindow.xaml"
            this.SubmitButton.Click += new System.Windows.RoutedEventHandler(this.SubmitButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

