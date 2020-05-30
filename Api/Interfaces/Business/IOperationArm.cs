using CommonLibrary.Entities.Angle;
using CommonLibrary.Entities.Arm;

namespace Api.Interfaces.Business
{
    public interface IOperationArm
    {
        Arm ManipulateArm();
        Arm AlterArm(Controls controls);
    }
}
