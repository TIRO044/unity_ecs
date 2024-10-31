using System;
using System.Collections.Generic;

namespace Assets.Script.Tile
{
    using UnityEngine;
    
    public class TileColonyEntity : MonoBehaviour
    {
        public List<TileEntity> Entities { private set; get; }=  new List<TileEntity>();
        public GameObject cubePrefab;  // 큐브 프리팹
        public int rows = 10;          // 행의 개수
        public int columns = 10;       // 열의 개수
        public float cubeSize = 1.0f;  // 각 큐브의 크기
        
        void CleanUp()
        {
            foreach (var entity in Entities)
            {
                DestroyImmediate(entity.gameObject);
            }
            
            Entities.Clear();
            Entities = null;
        }

        public void Init()
        {
            if (Entities != null)
            {
                CleanUp();
            }
            
            Vector3 totalPosition = Vector3.zero;  // 큐브들의 총합 위치 계산
            Transform parentTransform = transform;  // 부모 Transform

            // 큐브 생성 및 배치
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    // 큐브 생성
                    GameObject cube = Instantiate(cubePrefab, parentTransform);

                    var tileEntity = cube.GetComponent<TileEntity>();
                    
                    // 큐브의 위치 계산 (부모의 로컬 공간에서)
                    float xPos = (col - (columns - 1) / 2.0f) * cubeSize;
                    float yPos = (row - (rows - 1) / 2.0f) * cubeSize;

                    Vector3 cubePosition = new Vector3(xPos, yPos, 0);

                    // 총합 위치에 현재 큐브의 위치 더하기
                    totalPosition += cube.transform.position;
                    tileEntity.SetWH(cubeSize, cubeSize);
                    tileEntity.SetLocalPosition(cubePosition);
                }
            }

            // 자식들의 중심 계산 (평균값)
            Vector3 centerPosition = totalPosition / (rows * columns);

            // 부모의 위치를 자식들의 중심으로 이동
            parentTransform.position = centerPosition;

            // 부모 위치를 기준으로 모든 자식들의 로컬 위치 재조정
            foreach (Transform child in parentTransform)
            {
                child.localPosition -= parentTransform.position;
            }
        }
    }
}