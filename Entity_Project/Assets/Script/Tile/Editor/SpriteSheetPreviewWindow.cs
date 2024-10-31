using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SpriteSheetPreviewWindow : EditorWindow
{
    private Texture2D _spriteSheet;  // 스프라이트 시트 원본 텍스처
    private SpriteInfo _selectedSpriteInfo;  // 선택된 스프라이트

    private List<SpriteInfo> _spriteInfos = new();
    private Vector3 _scrollViewPos = Vector3.zero;
    
    public class SpriteInfo
    {
        public Sprite Sprite { private set; get; }
        public Texture2D ConvertedTexture2dBySprite { private set; get; }

        public void Set(Sprite sprite)
        {
            Sprite = sprite;
            SetTexture2DVia(Sprite);
        }

        private void SetTexture2DVia(Sprite sprite)
        {
            var rect = sprite.rect;
            var tex = new Texture2D((int)rect.width, (int)rect.height);
            var data = sprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
            tex.SetPixels(data);
            tex.Apply(true);
            ConvertedTexture2dBySprite = tex;
        }
    }
    
    [MenuItem("TMEditor/SpriteSheet Previewer")]
    public static void ShowWindow()
    {
        GetWindow<SpriteSheetPreviewWindow>("SpriteSheet Previewer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Drag and Drop a Sprite Sheet", EditorStyles.boldLabel);

        // 스프라이트 시트를 끌어다 놓을 수 있는 필드
        _spriteSheet = (Texture2D)EditorGUILayout.ObjectField("Sprite Sheet", _spriteSheet, typeof(Texture2D), false);

        // 스프라이트 시트가 있을 때
        if (_spriteSheet != null)
        {
            if (_spriteInfos.Count == 0 && GUILayout.Button("Load Sprites"))
            {
                LoadSpritesFromSheet();
            }

            if (_spriteInfos.Count > 0)
            {
                // 선택된 스프라이트 정보 표시
                if (_selectedSpriteInfo != null)
                {
                    GUILayout.BeginArea(new Rect(10, 60, 200, 60));
                        GUILayout.BeginVertical();
                            GUILayout.Label($"Selected Sprite: {_selectedSpriteInfo.Sprite.name}");
                            GUILayout.Label($"Position: {_selectedSpriteInfo.Sprite.rect.position}");
                            GUILayout.Label($"Size: {_selectedSpriteInfo.Sprite.rect.size}");
                        GUILayout.EndVertical();
                    GUILayout.EndArea();
                }
                
                GUILayout.BeginArea(new Rect(10, 130, 200, 120));
                    GUILayout.BeginVertical();
                        _scrollViewPos = GUILayout.BeginScrollView(_scrollViewPos);
                            GUILayout.BeginHorizontal(); 
                                for (var index = 0; index < _spriteInfos.Count; index++)
                                {
                                    if (index % 4 == 0)
                                    {
                                        GUILayout.EndHorizontal();
                                        GUILayout.BeginHorizontal();
                                    }
                                    
                                    var spriteInfo = _spriteInfos[index];
                                    if (GUILayout.Button(spriteInfo.ConvertedTexture2dBySprite
                                            , GUILayout.Width(40)
                                            , GUILayout.Height(40)))
                                    {
                                        _selectedSpriteInfo = spriteInfo;
                                    }
                                }
                            GUILayout.EndHorizontal();
                        GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                GUILayout.EndArea();
            }
        }
    }
    // 스프라이트 시트에서 잘라진 스프라이트들을 로드
    private void LoadSpritesFromSheet()
    {
        string path = AssetDatabase.GetAssetPath(_spriteSheet);  // 스프라이트 시트의 경로
        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);  // 스프라이트 시트에서 모든 에셋을 로드

        foreach (var asset in assets)
        {
            if (asset is Sprite sprite)
            {
                var spriteInfo = new SpriteInfo();
                spriteInfo.Set(sprite);
                _spriteInfos.Add(spriteInfo);
            }
        }
    }
}