using brazoRobot.ControllerLayer;
using brazoRobot.ModelLayer;
using System;
using System.Windows.Forms;

namespace brazoRobot
{
    public partial class View : Form
    {
        private Model model;
        private Controller controller;

        public Controller Controller
        {
            get
            {
                if (controller == null)
                {
                    controller = new Controller(this);
                }
                return controller;
            }
        }

        public Model Model
        {
            get
            {
                return model;
            }
        }

        public View(Model _Model)
        {
            this.model = _Model;
            InitializeComponent();
            this.model.LblAxis1 = this.lblValueAxis1;
            this.model.LblAxis2 = this.lblValueAxis2;
            this.model.LblAxis3 = this.lblValueAxis3;
            this.model.LblAxis4 = this.lblValueAxis4;
            this.model.LblAxis5 = this.lblValueAxis5;
            this.model.box = this.pbGraph;
            this.model.btnGripper = this.btnGripper;
            AssingEvents();
        }

        public void AssingEvents()
        {
            tbAxis1.ValueChanged += new EventHandler(Controller.AlterArm1);
            tbAxis1.Scroll += new EventHandler(Controller.AssingToolTipAxis1);
            tbAxis2.ValueChanged += new EventHandler(Controller.AlterArm2);
            tbAxis2.Scroll += new EventHandler(Controller.AssingToolTipAxis2);
            tbAxis3.ValueChanged += new EventHandler(Controller.AlterArm3);
            tbAxis3.Scroll += new EventHandler(Controller.AssingToolTipAxis3);
            tbAxis4.ValueChanged += new EventHandler(Controller.AlterArm4);
            tbAxis4.Scroll += new EventHandler(Controller.AssingToolTipAxis4);
            tbAxis5.ValueChanged += new EventHandler(Controller.AlterArm5);
            tbAxis5.Scroll += new EventHandler(Controller.AssingToolTipAxis5);
            btnGripper.Click += new EventHandler(Controller.ActionGripper);
            cbShowPoints.Click += new EventHandler(Controller.ShowPointMap);
            this.Shown += new EventHandler(Controller.MainInit);
            this.FormClosed += new FormClosedEventHandler(Controller.MainEnd);
        }
    }
}