using SciChart.Charting.Visuals.Annotations;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace fx.Charting.HewFibonacci
{
    [TemplatePart( Name = "PART_InputTextBlockArea", Type = typeof( TextBlock ) )]    
    public partial class SRlevelTextAnnotation : TextAnnotation
    {
        private TextBlock _inputTextBlock;        

        public SRlevelTextAnnotation( )
        {
            InitializeComponent( );
        }

        public override void OnApplyTemplate( )
        {
            _inputTextBlock = GetAndAssertTemplateChild<TextBlock>( "PART_InputTextBlockArea" );
            AnnotationRoot = GetAndAssertTemplateChild<TextBlock>( "PART_InputTextBlockArea" );
            PerformFocusOnInputTextArea( );
        }

        protected override void FocusInputTextArea( )
        {
            if ( _inputTextBlock == null )
            {
                return;
            }
            _inputTextBlock.IsEnabled = true;
            _inputTextBlock.Focus( );
        }

        protected override void RemoveFocusInputTextArea( )
        {
            if ( _inputTextBlock == null )
            {
                return;
            }
            _inputTextBlock.IsEnabled = false;
        } 
        
        public SRlevelLineAnnotationBase ParentLine { get; set; }

        public bool LineSelected { get; set; }
    }
}
