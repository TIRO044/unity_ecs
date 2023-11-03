using Unity.Transforms;

namespace Cube.System
{
    using Unity.Entities;

    public partial struct RotationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Excute.IJobEntity>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var jobEntity = new RotateAndScaleJobEntity();
        }
    }

    partial struct RotateAndScaleJobEntity : IJobEntity
    {

        void Execute(ref LocalTransform localTransform, ref PostTransformMatrix postTransformMatrix)
        {
            
        }
    }
}
