﻿using brazoRobot.ModelLayer;
using System;
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

        public void AlterArm1(object sender, System.EventArgs e)
        {
            this.model.Angle = ((TrackBar)sender).Value;
            this.model.Render();
        }

        internal void AlterArm2(object sender, EventArgs e)
        {
            this.model.Angle2 = ((TrackBar)sender).Value;
            this.model.Render();
        }

        internal void AlterArm3(object sender, EventArgs e)
        {
            this.model.Angle3 = ((TrackBar)sender).Value;
            this.model.Render();
        }

        internal void ActionGripper(object sender, EventArgs e)
        {
            MessageBox.Show("Hola pinza ");
        }

        internal void MainInit(object sender, EventArgs e)
        {
            this.model.Angle = 0;
            this.model.Render();
            MessageBox.Show("Hola estoy cargando ");
        }
    }
}