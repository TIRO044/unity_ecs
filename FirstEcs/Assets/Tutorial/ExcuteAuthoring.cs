
using Unity.Entities;
using UnityEngine;


namespace Cube.Excute
{
    public class ExcuteAuthoring : MonoBehaviour
    {
        public bool _mainThread;
        public bool _iJobSystem;
    
        class Baker : Baker<ExcuteAuthoring>
        {
            public override void Bake(ExcuteAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                if (authoring._iJobSystem)
                {
                    AddComponent<IJobEntity>(entity);
                }
            }
        }
    }

    public class IJobEntity : IComponentData
    {
    
    }
}
