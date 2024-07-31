using Unity.Entities;
using UnityEngine;
public class ConfigEntity : MonoBehaviour
{
    public class ConfigEntityBaker : Baker<ConfigEntity>
    {
        public override void Bake(ConfigEntity authoring)
        {
            var player = Resources.Load("PlayerCube2") as GameObject;
            var entity =  GetEntity(TransformUsageFlags.None);
            AddComponent(
                entity,
                new ConfigData()
                {
                    playerEntityPrefab = GetEntity(player, TransformUsageFlags.Dynamic)
                }
            );
        }
    }
}
