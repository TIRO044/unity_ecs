using UnityEditor;
using UnityEngine;

namespace Assets.Script.Tile.Editor
{
    [CustomEditor(typeof(TileColonyEntity))]    
    public class TileColonyEntityEditor : UnityEditor.Editor
    {
        // public override VisualElement CreateInspectorGUI()
        // {
        //     // Create a new VisualElement to be the root of our inspector UI
        //     VisualElement myInspector = new VisualElement();
        //
        //     // Add a simple label
        //     myInspector.Add(new Label("This is a custom inspector"));
        //
        //     // Return the finished inspector UI
        //     return myInspector;
        // }
        // toolkit으로 하는 것도 있구나
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var myScript = (TileColonyEntity)target;
            
            if (GUILayout.Button("Create"))
            {
                myScript.Init();
            }
        }
        
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
            TileColonyEntity myScript = (TileColonyEntity)target;
            
            if(myScript.Entities.Count == 0)
                return;
            
            foreach (var entity in myScript.Entities)
            {
                Handles.color = Color.white; // 선택된 상태일 때 초록색
                // 박스 그리기
                Handles.DrawWireCube(entity.transform.position, new Vector3(entity.Width, entity.Height, 1));
            }            
            SceneView.RepaintAll();
        }
    }
}
