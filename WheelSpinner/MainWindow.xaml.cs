using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace WheelSpinner {
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window {
        int _totalDelta = 0;
        Timer timer = new Timer();
        Storyboard sb;
        public MainWindow() {
            InitializeComponent();
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
            sb = (Storyboard)this.SpinnerImage.FindResource("spin");
        }

        private void SpinnerImage_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void SpinnerImage_MouseWheel(object sender, MouseWheelEventArgs e) {

            if (_totalDelta < 0) {
                _totalDelta = 0;
            }
            else if (_totalDelta > 750) {
                _totalDelta = 750;
            }
            else {
                if (!(e.Delta < 0)) {
                    _totalDelta += e.Delta;
                }

            }
            try {

                if (timer.Enabled == false) {
                    sb.Begin();
                    if (!(_totalDelta < 0)) {
                        sb.SetSpeedRatio(_totalDelta);//sample data
                    }
                }

            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show(ex.ToString());
            }

            //lbl_debugger.Content = _totalDelta.ToString();
            if (timer.Enabled == false) {
                timer.Start();
            }

        }
        private void timer_Tick(object sender, EventArgs e) {
            if (_totalDelta <= 0) {
                _totalDelta = 0;
                sb.Stop();
                timer.Stop();
                return;
            }
            sb.SetSpeedRatio(_totalDelta--);
            //lbl_debugger.Content = _totalDelta.ToString();

        }

        private void SpinnerImage_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            timer.Interval = 10;
        }

        private void SpinnerImage_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            timer.Interval = 200;
        }
    }



}
