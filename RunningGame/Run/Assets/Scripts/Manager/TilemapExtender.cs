using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapExtender : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase groundTile;
    [SerializeField] private Transform player;
    [SerializeField] private int extendBuffer = 10; // 플레이어 앞에 몇 칸 미리 생성할지
    [SerializeField] private int groundY = -5; // y축 고정

    private int lastTileX = 0;

    private void Start()
    {
        lastTileX = Mathf.FloorToInt(player.position.x);
        ExtendTilesIfNeeded();
    }

    private void Update()
    {
        ExtendTilesIfNeeded();
    }

    private void ExtendTilesIfNeeded()
    {
        int playerTileX = Mathf.FloorToInt(player.position.x);
        int targetTileX = playerTileX + extendBuffer;

        while (lastTileX < targetTileX)
        {
            tilemap.SetTile(new Vector3Int(lastTileX, groundY, 0), groundTile);
            lastTileX++;
        }
    }
}