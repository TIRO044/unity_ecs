using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TM
{
    public class InfiniteTileMap : MonoBehaviour
    {
        public bool InfiniteHorizontal = true;
        public bool InfiniteVertical;
        public float Choke;

        private Transform _cameraTransform;
        private Vector3 _lastCameraPosition;
        private Tilemap _tileMap;
        private List<TD> _tiles;
        private List<TD> _maxHorizontalTiles;
        private List<TD> _minHorizontalTiles;
        private List<TD> _minVerticalTiles;
        private List<TD> _maxVerticalTiles;
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        private int _minCoordX;
        private int _maxCoordX;
        private int _minCoordY;
        private int _maxCoordY;

        private int _width;
        private int _height;

        private Vector2 _screenBounds;

        private void Awake()
        {
            var mainCamera = Camera.main;
            _cameraTransform = mainCamera.transform;
            _lastCameraPosition = _cameraTransform.position;

            InitTileMap();
        }

        void InitTileMap()
        {
            var mainCamera = Camera.main;
            _tileMap = GetComponent<Tilemap>();
            _tileMap.CompressBounds();
            _tiles = _tileMap.GetTiles();

            if (_tiles.Count == 0)
            {
                Debug.Log("tiles count is zero");
                return;
            }

            
            _screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

            _maxHorizontalTiles = new List<TD>();
            _minHorizontalTiles = new List<TD>();
            
            _maxVerticalTiles = new List<TD>();
            _minVerticalTiles = new List<TD>();
            
            CalculateSize();

            _width = _maxCoordX - _minCoordX;
            _height = _maxCoordY - _minCoordY;
        }

        private void CalculateSize()
        {
            _maxHorizontalTiles.Clear();
            _maxHorizontalTiles.Clear();
            _minHorizontalTiles.Clear();
            _maxVerticalTiles.Clear();

            _minX = _tiles[0].worldPoint.x;
            _maxX = _tiles[^1].worldPoint.x;
            _minY = _tiles[0].worldPoint.y;
            _maxY = _tiles[^1].worldPoint.y;

            foreach (var tileData in _tiles)
            {
                if (tileData.worldPoint.x <= _minX)
                {
                    _minX = tileData.worldPoint.x;
                    _minCoordX = tileData.coordinates.x;
                }

                if (tileData.worldPoint.x >= _maxX)
                {
                    _maxX = tileData.worldPoint.x;
                    _maxCoordX = tileData.coordinates.x;
                }

                if (tileData.worldPoint.y <= _minY)
                {
                    _minY = tileData.worldPoint.y;
                    _minCoordY = tileData.coordinates.y;
                }

                if (tileData.worldPoint.y >= _maxY)
                {
                    _maxY = tileData.worldPoint.y;
                    _maxCoordY = tileData.coordinates.y;
                }
            }

            foreach (var tileData in _tiles)
            {
                if (tileData.worldPoint.x <= _minX)
                {
                    _minHorizontalTiles.Add(tileData);
                }

                if (tileData.worldPoint.x >= _maxX)
                {
                    _maxHorizontalTiles.Add(tileData);
                }

                if (tileData.worldPoint.y <= _minY)
                {
                    _minVerticalTiles.Add(tileData);
                }

                if (tileData.worldPoint.y >= _maxY)
                {
                    _maxVerticalTiles.Add(tileData);
                }
            }
        }

        private void Update()
        {
        }

        private void LateUpdate()
        {
            UpdateTile();
        }

        private void UpdateTile()
        {
            if (_minHorizontalTiles == null) return;

            Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
            if (deltaMovement.Equals(Vector3.zero))
            {
                return;
            }

            if (InfiniteHorizontal)
            {
                if (_cameraTransform.position.x + _screenBounds.x + Choke > _maxX)
                {
                    MoveHorizontalTiles(_minHorizontalTiles, _width);
                }
                else if (_cameraTransform.position.x - _screenBounds.x - Choke < _minX)
                {
                    MoveHorizontalTiles(_maxHorizontalTiles, -_width);
                }
            }

            if (InfiniteVertical)
            {
                if (_cameraTransform.position.y + _screenBounds.y + Choke > _maxY)
                {
                    MoveVerticalTiles(_minVerticalTiles, _height);
                }
                else if (_cameraTransform.position.y - _screenBounds.y - Choke < _minY)
                {
                    MoveVerticalTiles(_maxVerticalTiles, -_height);
                }
            }

            _lastCameraPosition = _cameraTransform.position;
        }

        private void MoveVerticalTiles(List<TD> tiles, int amount)
        {
            foreach (TD tile in tiles)
            {
                SetTile(tile, new Vector3Int(tile.coordinates.x, tile.coordinates.y + amount, tile.coordinates.z));
            }

            CalculateSize();
        }

        private void MoveHorizontalTiles(List<TD> tiles, int amount)
        {
            foreach (TD tile in tiles)
            {
                SetTile(tile, new Vector3Int(tile.coordinates.x + amount, tile.coordinates.y, tile.coordinates.z));
            }

            CalculateSize();
        }

        private void SetTile(TD tile, Vector3Int newCoordinates)
        {
            _tileMap.SetTile(tile.coordinates, null);
            Debug.Log($"{tile.coordinates} -> {newCoordinates}");
            tile.coordinates = newCoordinates;
            _tileMap.SetTile(tile.coordinates, tile.tile);
            var ctw = _tileMap.CellToWorld(tile.coordinates);
            Debug.Log($"{tile.worldPoint} -> {ctw}");
            tile.worldPoint = ctw;
        }
    }
}