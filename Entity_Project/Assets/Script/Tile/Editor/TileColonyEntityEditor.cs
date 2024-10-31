using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.Tile.Editor
{
    [CustomEditor(typeof(TileColonyEntity))]    
    public class TileColonyEntityEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            EditorApplication.update += OnSceneGui;
        }

        private void OnDisable()
        {
            EditorApplication.update -= OnSceneGui;
        }

        private void OnSceneGui()
        {

                // 마우스 왼쪽 클릭 감지
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 worldClickPosition = HandleUtility.GUIPointToWorldRay(Input.mousePosition).origin;

                    Collider2D hitCollider = Physics2D.OverlapPoint(worldClickPosition);
                    if (hitCollider != null)
                    {
                        if (hitCollider.gameObject.GetComponent<TileEntity>()?.GetType() == typeof(TileEntity))
                        {
                            Debug.Log("TileEntity가 클릭되었습니다: " + hitCollider.gameObject.name);
                        }
                    }
                }
        }

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
