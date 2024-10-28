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
            // GUILayout.Label("Sprite Sheet Preview:");

            // 스프라이트 시트에서 스프라이트들을 가져옴
            if (sprites == null && GUILayout.Button("Load Sprites"))
            {
                LoadSpritesFromSheet();
            }

            // 지정한 너비 내에서 스프라이트 정렬 및 줄바꿈 처리
            if (sprites != null)
            {
                HandleMouseEvents();  // 마우스 이벤트 처리

                float currentWidth = 0;  // 현재 줄의 너비
                float yOffset = 50;      // 첫 번째 줄의 Y 위치 설정

                var maxLength = sprites.Length < maxRowCount ? sprites.Length : maxRowCount;
                var displaycount = maxLength * spriteDisplaySize;

                var startXPosition = 10;
                var startYPosition = 50;

                int count = 0;
                
                GUILayout.BeginArea(new Rect(new Vector2(startXPosition, startYPosition), new Vector2(displaycount, displaycount + 5)));
                GUILayout.BeginHorizontal();
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
                    if (currentWidth + textureRect.width > maxWidth)
                    {
                        count++;
                        GUILayout.EndHorizontal();  // 현재 줄 종료
                        GUILayout.EndArea();
                        GUILayout.BeginArea(new Rect(new Vector2(startXPosition, startYPosition + count * spriteDisplaySize + 5), new Vector2(displaycount, displaycount)));
                        GUILayout.BeginHorizontal();  // 새 줄 시작
                        currentWidth = 0;  // 새 줄이므로 너비 초기화
                    }

                    // 스프라이트를 그릴 실제 위치 Rect 설정
                    Rect spriteRect = GUILayoutUtility.GetRect(spriteDisplaySize, spriteDisplaySize);
                    GUI.DrawTextureWithTexCoords(spriteRect, sprite.texture, uvRect);
                    //DrawSpriteOutline(spriteRect);  // 경계 표시

                    currentWidth += textureRect.width;  // 현재 줄 너비 업데이트
                }
                
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
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

            // 모든 스프라이트에 대해 클릭한 위치가 스프라이트 경계 안에 있는지 확인
            foreach (var sprite in sprites)
            {
                Rect textureRect = sprite.textureRect;

                // 마우스 좌표가 스프라이트 경계 내에 있는지 확인
                if (textureRect.Contains(mousePosition))
                {
                    selectedSprite = sprite;
                    Repaint();  // 선택된 스프라이트 정보를 표시하기 위해 창을 다시 그리기
                    break;
                }
            }
        }
    }
}