using System;
using Assets.Script.Tile;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileEntity))]
public class TileEntityForEditor : Editor
{
    private void OnSceneGUI()
    {
        TileEntity myScript = (TileEntity)target;

        Handles.color = Color.green;
        
        Handles.DrawWireCube(myScript.transform.position, new Vector3(1, 1, 1));
        
        SceneView.RepaintAll();
    }
}
