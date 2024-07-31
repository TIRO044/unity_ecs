using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public class PlayerEntityBaker : Baker<PlayerEntity>
    {
        public override void Bake(PlayerEntity authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerEntityData());
        }
    }
}
