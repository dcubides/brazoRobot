using brazoRobot.ModelLayer;
using System;
using System.Threading;
using System.Windows.Forms;

namespace brazoRobot.ControllerLayer
{
    public class Controller
    {
        #region Vars

        private View view;
        private Model model;

        #endregion Vars

        public Controller(View _view)
        {
            this.view = _view;
            this.model = this.view.Model;
        }

        internal void AlterArm1(object sender, System.EventArgs e)
        {
            Thread.Sleep(20);
            this.model.Angle = ((TrackBar)sender).Value;
        }

        internal void AlterArm2(object sender, EventArgs e)
        {
            Thread.Sleep(20);
            this.model.Angle2 = ((TrackBar)sender).Value;
        }

        internal void AlterArm3(object sender, EventArgs e)
        {
            Thread.Sleep(20);
            this.model.Angle3 = ((TrackBar)sender).Value;
        }

        internal void AlterArm4(object sender, EventArgs e)
        {
            Thread.Sleep(20);
            this.model.Angle4 = ((TrackBar)sender).Value;
        }

        internal void AlterArm5(object sender, EventArgs e)
        {
            Thread.Sleep(20);
            this.model.Angle5 = ((TrackBar)sender).Value;
        }

        internal void AssingToolTipAxis1(object sender, EventArgs e)
        {
            this.view.tTpAxis1.SetToolTip(this.view.tbAxis1, this.view.tbAxis1.Value.ToString());
        }

        internal void AssingToolTipAxis5(object sender, EventArgs e)
        {
            this.view.tTpAxis5.SetToolTip(this.view.tbAxis5, this.view.tbAxis5.Value.ToString());
        }

        internal void AssingToolTipAxis2(object sender, EventArgs e)
        {
            this.view.tTpAxis2.SetToolTip(this.view.tbAxis2, this.view.tbAxis2.Value.ToString());
        }

        internal void AssingToolTipAxis3(object sender, EventArgs e)
        {
            this.view.tTpAxis3.SetToolTip(this.view.tbAxis3, this.view.tbAxis3.Value.ToString());
        }

        internal void ShowPointMap(object sender, EventArgs e)
        {
            this.model.ShowPoint = ((CheckBox)sender).Checked;
        }

        internal void ActionGripper(object sender, EventArgs e)
        {
            this.model.StatusGripper = !this.model.StatusGripper;
        }

        internal void AssingToolTipAxis4(object sender, EventArgs e)
        {
            this.view.tTpAxis4.SetToolTip(this.view.tbAxis4, this.view.tbAxis4.Value.ToString());
        }

        internal void MainInit(object sender, EventArgs e)
        {
            try
            {
                //this.model.Angle = 0;
                //this.model.Angle2 = 0;
                //this.model.Angle3 = 0;
                //this.model.Angle4 = 0;
                //this.model.Angle5 = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void MainEnd(object sender, EventArgs e)
        {
            try
            {
                this.model.StartRender = !this.model.StartRender;
                this.model.StopThread();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}