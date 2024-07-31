using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct ConfigSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ConfigData>();
    }
    
    public void OnUpdate(ref SystemState state)
    {
        // player 캐릭터 생성
        {
            if (SystemAPI.HasSingleton<ConfigData>())
            {
                var configData = SystemAPI.GetSingleton<ConfigData>();
                var playerPrefab = state.EntityManager.Instantiate(configData.playerEntityPrefab);
                LocalTransform newLocalTransform = LocalTransform.FromRotation(Quaternion.Euler(0, 180, 0));
                state.EntityManager.SetComponentData(playerPrefab, newLocalTransform);
            }
            else
            {
                // 없거나 여러개 인 경우? 
            }
        }
        
        state.Enabled = false;
    }
}
