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
    }
}
