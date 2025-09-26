using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapExtender : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase groundTile;
    [SerializeField] private Transform player;
    [SerializeField] private int extendBuffer = 10;
    [SerializeField] private int removeBuffer = 20;
    [SerializeField] private int groundY = -5;
    
    private int prevPlayerTileX = int.MinValue;
    private int prevStartX = int.MinValue;
    private int prevEndX = int.MinValue;

    private void Update()
    {
        // 플레이어의 월드 좌표를 타일맵 셀 좌표로 변환
        int playerTileX = tilemap.WorldToCell(player.position).x;

        if (playerTileX == prevPlayerTileX)
            return;

        int startX = playerTileX - removeBuffer;
        int endX = playerTileX + extendBuffer;

       // Debug.Log($"플레이어 월드X: {player.position.x}, 셀X: {playerTileX}, startX: {startX}, endX: {endX}");

        // 1. 현재 프레임에서 유지해야 할 구간의 타일은 모두 생성
        for (int x = startX; x < endX; x++)
        {
            tilemap.SetTile(new Vector3Int(x, groundY, 0), groundTile);
        }

        // 2. 이전 프레임에서 유지했던 구간 중, 이번 프레임에 벗어난 타일은 모두 제거
        if (prevStartX != int.MinValue && prevEndX != int.MinValue)
        {
            for (int x = prevStartX; x < startX; x++)
                tilemap.SetTile(new Vector3Int(x, groundY, 0), null);
            for (int x = endX; x < prevEndX; x++)
                tilemap.SetTile(new Vector3Int(x, groundY, 0), null);
        }

        prevPlayerTileX = playerTileX;
        prevStartX = startX;
        prevEndX = endX;
    }
}