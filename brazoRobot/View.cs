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
            this.model.box = this.pbGraph;
            AssingEvents();
        }

        public void AssingEvents()
        {
            tbAxis1.ValueChanged += new EventHandler(Controller.AlterArm1);
            tbAxis2.ValueChanged += new EventHandler(Controller.AlterArm2);
            tbAxis3.ValueChanged += new EventHandler(Controller.AlterArm3);
            btnGripper.Click += new EventHandler(Controller.ActionGripper);
            this.Shown += new EventHandler(Controller.MainInit);
        }
    }
}