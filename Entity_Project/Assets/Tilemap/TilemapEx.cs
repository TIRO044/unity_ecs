using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace  TM
{
    public class TD
    {
        public Vector3Int coordinates;
        public Vector3 worldPoint;
        public Tile tile;
    }

    public static class Test
    {
        [MenuItem("TileMapTest/getT")]
        public static void T()
        {
            var grid = GameObject.Find("Grid_Test");
            var tileMap = grid.GetComponentInChildren<Tilemap>();
            if (tileMap == null)
            {
                Debug.LogError("not found tileMap");
                return;
            }

            var tiles = tileMap.GetTiles();
            //tileMap.MoveTileTest();
        }

        [MenuItem("TileMapTest/SetTileTest")]
        public static void TestTileMap()
        {
            var grid = GameObject.Find("Grid_Test");
            var tileMap = grid.GetComponentInChildren<Tilemap>();
            if (tileMap == null)
            {
                Debug.LogError("not found tileMap");
                return;
            }

            //var tiles = tileMap.GetTiles();
            tileMap.MoveTileTest();
        }

        [MenuItem("TileMapTest/GetCameraSize")]
        public static void GetCameraSize()
        {
            var mainCamera = Camera.main;
            var mCameraSize = mainCamera.orthographicSize;
            Debug.Log($"mCameraSize : {mCameraSize}");

            var screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            Debug.Log($"screenBounds : {screenBounds}");
        }
    }


    public static class TilemapEx
    {
        public static void MoveTileTest(this Tilemap tileMap)
        {
            var bounds = tileMap.cellBounds;
            var allTiles = tileMap.GetTilesBlock(bounds);
            var tileBase = allTiles[0];
            
            //tileBase.RefreshTile(new Vector3Int(2, 2, 2), tileMap);
            tileMap.SetTile(new Vector3Int(2, 2, 2), tileBase);
        }

        public static List<TD> GetTiles(this Tilemap tileMap)
        {
            List<TD> tileDataList = new List<TD>();
            BoundsInt bounds = tileMap.cellBounds;

            void GetTileTest()
            {
                var withIn = tileMap.cellBounds.allPositionsWithin;
                Debug.Log($"boundBox cellBounds: {bounds}");
                Debug.Log($"allPositionsWithin : {withIn}");

                foreach (var v in withIn)
                {
                    var tile = tileMap.GetTile(v);
                    if (tile != null)
                    {
                        Debug.Log($"get tile : {v}");
                    }
                }
            }

            //GetTileTest();

            void GetTileBlock()
            {
                var tilesWithinRange = tileMap.GetTilesBlock(bounds);
                foreach (var tileBase in tilesWithinRange)
                {
                    if (tileBase == null)
                        continue;

                    
                    if (tileBase is Tile defaultTile)
                    {
                        var coordinates = Vector3Int.FloorToInt(defaultTile.transform.GetPosition());
                        var sellToWorld = tileMap.CellToWorld(coordinates);
                        var tileData = new TD()
                        {
                            tile = defaultTile,
                            coordinates = coordinates,
                            worldPoint = sellToWorld
                        };

                        tileDataList.Add(tileData);
                        Debug.Log($"coordinates : {coordinates}");
                        Debug.Log($"sTow : {sellToWorld}");
                    } 
                    else if (tileBase is RuleTile ruleTile) {
                        var movePosition = 1;
                        
                        var position = ruleTile.m_DefaultGameObject.transform.position;
                        ruleTile.m_DefaultGameObject.transform.position = new Vector3(position.x + movePosition, position.y + movePosition, position.z + movePosition);
                        Debug.Log("Rule Tile");
                        //var coordinates = Vector3Int.FloorToInt(ruleTile.transform.GetPosition());
                        //var tileData = new TD()
                        //{
                        //    tile = tile,
                        //    coordinates = coordinates,
                        //    worldPoint = tileMap.CellToWorld(coordinates)
                        //};

                        //tileDataList.Add(tileData);
                    }
                    else
                    {
                        Debug.Log($"tileType _ {tileBase.GetType()}");
                    }
                }
            }
            
            //GetTileBlock();



            //foreach (var t in tilesWithinRange)
            //{
            //    var tile = tilemap.GetTile<Tile>();

            //    var tileData = new TD()
            //    {
            //        tile = tile,
            //        coordinates = coordinates,
            //        worldPoint = tilemap.CellToWorld(coordinates)
            //    };

            //    tileDataList.Add(tileData);
            //    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
            //}

            void Test2()
            {
                var startPositionX = bounds.position.x;
                var maxPositionX = startPositionX + bounds.size.x;

                var startPositionY = bounds.position.y;
                var maxPositionY = startPositionY + bounds.size.y;

                for (var x = startPositionX; x < maxPositionX; x++)
                {
                    for (var y = startPositionY; y < maxPositionY; y++)
                    {
                        //TileBase tile = allTiles[x + y * bounds.size.x];
                        var coordinates = new Vector3Int(x, y, 0);
                        Tile tile = tileMap.GetTile<Tile>(coordinates);
                        if (tile != null)
                        {
                            var tileData = new TD()
                            {
                                tile = tile,
                                coordinates = coordinates,
                                worldPoint = tileMap.CellToWorld(coordinates)
                            };

                            tileDataList.Add(tileData);
                            Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                        }
                        else
                        {
                            //Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                        }
                    }
                }
            }
            //Test2();

            void test3()
            {
                for (var y = tileMap.origin.y; y < (tileMap.origin.y + tileMap.size.y); y++)
                {
                    for (var x = tileMap.origin.x; x < (tileMap.origin.x + tileMap.size.x); x++)
                    {
                        var coordinates = new Vector3Int(x, y, 0);
                        var tile = tileMap.GetTile<Tile>(coordinates);
                        if (tile != null)
                        {
                            var tileData = new TD()
                            {
                                tile = tile,
                                coordinates = coordinates,
                                worldPoint = tileMap.CellToWorld(coordinates)
                            };

                            tileDataList.Add(tileData);
                        }
                    }
                }
            }

            test3();

            foreach (var td in tileDataList)
            {
                var tile = td.tile;
                var pos = tile.transform.GetPosition();


                //tile.gameObject.transform.position = new Vector3(pos.x + 1, pos.y + 1, pos.z + 1);
            }


            return tileDataList;
        }
    }
}