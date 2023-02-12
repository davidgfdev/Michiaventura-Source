using UnityEngine;
using UnityEngine.Tilemaps;

public class scr_gemPopulate : MonoBehaviour
{
    Tilemap tilemap;
    [SerializeField] GameObject gem;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
    void Start()
    {
        if (tilemap != null)
        {
            SwapTilesForGems();
        }
    }

    void SwapTilesForGems()
    {
        for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
        {
            for (int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
            {
                Vector3Int localPlace = new Vector3Int(i, j, (int)tilemap.transform.position.z);
                if (tilemap.HasTile(localPlace))
                {
                    Vector3 tilePlace = tilemap.CellToWorld(localPlace);
                    Instantiate(gem, localPlace, gem.transform.rotation);
                }
            }
        }
        GetComponent<TilemapRenderer>().enabled = false;
    }
}
