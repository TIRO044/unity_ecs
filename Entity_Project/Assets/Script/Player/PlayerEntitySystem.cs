using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct PlayerEntitySystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerEntityData>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var yAxis = SystemAPI.Time.DeltaTime * 2f;
        var xAxis = SystemAPI.Time.DeltaTime * 2f;

        foreach (var (playerLocalTransform, playerEntityData) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerEntityData>>())
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerLocalTransform.ValueRW.Position.y += yAxis;
            } 
            else if (Input.GetKey(KeyCode.S))
            {
                playerLocalTransform.ValueRW.Position.y -= yAxis;
            } 
            
            if (Input.GetKey(KeyCode.D))
            {
                playerLocalTransform.ValueRW.Position.x += xAxis;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerLocalTransform.ValueRW.Position.x -= xAxis;
            }
        }
    }
}
