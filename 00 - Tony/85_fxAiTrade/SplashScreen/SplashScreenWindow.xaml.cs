using System;
using System.Linq;
using System.Windows.Controls;

namespace FreemindAITrade.View
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : UserControl
    {
        public SplashScreenWindow()
        {
            InitializeComponent();
        }

        //#region ISplashScreen
        //public void Progress( double value )
        //{
        //    //progressBar.Value = value;
        //}

        //public void CloseSplashScreen( )
        //{
        //    this.board.Begin( this );
        //}

        //public void SetProgressState( bool isIndeterminate )
        //{
        //    //progressBar.IsIndeterminate = isIndeterminate;
        //}
        //#endregion

        //#region Event Handlers
        //void OnAnimationCompleted( object sender, EventArgs e )
        //{
        //    this.board.Completed -= OnAnimationCompleted;
        //    this.Close( );
        //}
        //#endregion
    }
}
