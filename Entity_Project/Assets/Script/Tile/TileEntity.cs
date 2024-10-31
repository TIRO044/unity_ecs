using UnityEditor;
using UnityEngine;

namespace Assets.Script.Tile
{
    public class TileEntity : MonoBehaviour
    {
        public Sprite Sprite;
        public SpriteRenderer SpriteRenderer;
        public BoxCollider2D BoxCollider;
        public float Width;
        public float Height;

        public void SetWH(float width, float height)
        {
            Width = width;
            Height = height;
            BoxCollider.size = new Vector2(Width, Height);
        }

        public void SetLocalPosition(Vector3 pos)
        {
            transform.localPosition = pos;
        }

        public void SetSprite(Sprite sprite)
        {
            Sprite = sprite;
            SpriteRenderer.sprite = sprite;
        }

        private void OnDrawGizmos()
        {            
            // // 프리팹의 위치와 회전 정보를 가져옵니다.
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            
            // 현재 게임 오브젝트의 회전을 적용합니다.
            Gizmos.matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
            
            // Gizmo 색상 설정 (원하는 색으로 변경 가능)
            Gizmos.color = Color.white;
            
            // 사각형을 중심을 기준으로 그립니다. (크기는 width, height)
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(Width, Height, 0));

        }
    }
}