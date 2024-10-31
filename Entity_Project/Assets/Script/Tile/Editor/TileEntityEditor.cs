using Assets.Script.Tile;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileEntity))]
public class TileEntityEditor : Editor
{
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnScene;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnScene;
    }

    private void OnScene(SceneView sceneView)
    {
        TileEntity myScript = (TileEntity)target;

        Handles.color = Color.green; // 선택된 상태일 때 초록색

        // 박스 그리기
        Handles.DrawWireCube(myScript.transform.position, new Vector3(myScript.Width, myScript.Height, 1));
        
        // 씬 뷰 업데이트
        SceneView.RepaintAll();
    }
}
