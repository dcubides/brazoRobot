using CommonLibrary.Entities.Arm.GripperBase;
using CommonLibrary.Entities.Arm.Polygon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Entities.Arm
{
    public class Arm
    {
        #region ctor

        public Arm(Gripper _Gripper, List<UnitPolygon> _Joints)
        {
            this.Gripper = _Gripper;
            this.Joints = _Joints;
        }

        #endregion ctor

        #region fields

        private List<UnitPolygon> joints;
        private Gripper gripper;

        #endregion fields

        #region Gets & Sets

        public List<UnitPolygon> Joints { get => joints; set => joints = value; }
        public Gripper Gripper { get => gripper; set => gripper = value; }

        #endregion Gets & Sets
    }
}