using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections.Specialized;

public class SpriteSheetPreviewWindow : EditorWindow
{
    private Texture2D spriteSheet;  // 스프라이트 시트 원본 텍스처
    private Sprite[] sprites;       // 잘려진 스프라이트 목록
    private Sprite selectedSprite;  // 선택된 스프라이트
    private const float maxWidth = 100f;  // 한 줄의 최대 너비
    private const float spriteDisplaySize = 30f; // 스프라이트 표시 크기 고정 (10x10)
    private const float maxRowCount = 5;

    [MenuItem("TMEditor/SpriteSheet Previewer")]
    public static void ShowWindow()
    {
        GetWindow<SpriteSheetPreviewWindow>("SpriteSheet Previewer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Drag and Drop a Sprite Sheet", EditorStyles.boldLabel);

        // 스프라이트 시트를 끌어다 놓을 수 있는 필드
        spriteSheet = (Texture2D)EditorGUILayout.ObjectField("Sprite Sheet", spriteSheet, typeof(Texture2D), false);

        // 스프라이트 시트가 있을 때
        if (spriteSheet != null)
        {
            if (sprites == null && GUILayout.Button("Load Sprites"))
            {
                LoadSpritesFromSheet();
            }

            if (sprites != null)
            {
                float currentWidth = 0;  // 현재 줄의 너비
                float yOffset = 50;      // 첫 번째 줄의 Y 위치 설정

                foreach (var sprite in sprites)
                {
                    Rect textureRect = sprite.textureRect;
                    Rect uvRect = new Rect(
                        textureRect.x / sprite.texture.width,
                        textureRect.y / sprite.texture.height,
                        textureRect.width / sprite.texture.width,
                        textureRect.height / sprite.texture.height
                    );

                    // 현재 줄의 너비가 maxWidth를 넘으면 줄바꿈
                    if (currentWidth + spriteDisplaySize > maxWidth)
                    {
                        currentWidth = 0;
                        yOffset += spriteDisplaySize + 5;
                    }

                    // 스프라이트를 10x10 크기로 고정하여 그릴 위치 Rect 설정
                    Rect spriteRect = new Rect(10 + currentWidth, yOffset, spriteDisplaySize, spriteDisplaySize);
                    GUI.DrawTextureWithTexCoords(spriteRect, sprite.texture, uvRect);

                    // 클릭 이벤트 처리
                    if (Event.current.type == EventType.MouseDown && spriteRect.Contains(Event.current.mousePosition))
                    {
                        selectedSprite = sprite;
                        Repaint();
                    }

                    currentWidth += spriteDisplaySize + 5;  // 다음 스프라이트의 X 위치 업데이트
                }

                // 선택된 스프라이트 정보 표시
                if (selectedSprite != null)
                {
                    GUILayout.Space(20 + yOffset);
                    GUILayout.Label($"Selected Sprite: {selectedSprite.name}");
                    GUILayout.Label($"Position: {selectedSprite.rect.position}");
                    GUILayout.Label($"Size: {selectedSprite.rect.size}");
                }
            }
        }
    }
    // 스프라이트 시트에서 잘라진 스프라이트들을 로드
    private void LoadSpritesFromSheet()
    {
        string path = AssetDatabase.GetAssetPath(spriteSheet);  // 스프라이트 시트의 경로
        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);  // 스프라이트 시트에서 모든 에셋을 로드

        List<Sprite> spriteList = new List<Sprite>();
        foreach (var asset in assets)
        {
            if (asset is Sprite sprite)
            {
                spriteList.Add(sprite);
            }
        }

// 배열로 변환
        sprites = spriteList.ToArray();
    }

    // 스프라이트의 경계를 그리는 함수
    private void DrawSpriteOutline(Rect rect)
    {
        // GUI에서 경계 그리기
        Handles.color = Color.red;
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMin), new Vector3(rect.xMax, rect.yMin));
        Handles.DrawLine(new Vector3(rect.xMax, rect.yMin), new Vector3(rect.xMax, rect.yMax));
        Handles.DrawLine(new Vector3(rect.xMax, rect.yMax), new Vector3(rect.xMin, rect.yMax));
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMax), new Vector3(rect.xMin, rect.yMin));
    }

    // 마우스 이벤트 처리 함수
    private void HandleMouseEvents()
    {
        Event e = Event.current;

        // 마우스 클릭 이벤트 감지
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            Vector2 mousePosition = e.mousePosition;

        }
    }
}