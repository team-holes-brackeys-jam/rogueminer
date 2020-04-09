using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class GridManager : MonoBehaviour {
    
    private float tileSize = 2.5f;

    void Start() {
        RenderLevel(GenerateLevel());
    }

    private GameObject GetGameObjectFromTileState(Terrain terrain) {
        Debug.Log("Terrain:");
        Debug.Log(terrain);
        return Instantiate(Resources.Load($"Prefabs/{terrain}")) as GameObject;
    }

    private Level GenerateLevel() {
        Tile emptyTile = new Tile(Terrain.Empty);
        Tile dirtTile = new Tile(Terrain.Dirt);
        Tile wallTile = new Tile(Terrain.Wall);
        Tile[,] board = new Tile[5, 5] {
            {wallTile, wallTile, wallTile, wallTile, wallTile},
            {wallTile, emptyTile, dirtTile, emptyTile, wallTile},
            {wallTile, dirtTile, emptyTile, dirtTile, wallTile},
            {wallTile, emptyTile, dirtTile, emptyTile, wallTile},
            {wallTile, wallTile, wallTile, wallTile, wallTile}
        };
        Vector2Int playerPosition = new Vector2Int(2, 2);
        Bow bowOne = new Bow(new Vector2Int(1, 1), Vector2Int.right);
        Bow bowTwo = new Bow(new Vector2Int(3, 3), Vector2Int.left);
        List<Bow> bows = new List<Bow>();
        bows.Add(bowOne);
        bows.Add(bowTwo);
        return new Level(board, playerPosition, bows);
    }

    private void RenderLevel(Level level) {
        Vector2Int dimensions = level.GetDimensions();
        Debug.Log("Dimensions: " + dimensions.x + ", " + dimensions.y);
        for (int i = 0; i < dimensions.x; i++) {
            for (int j = 0; j < dimensions.y; j++) {
                Tile currentTile = level.tiles[i, j];
                Debug.Log("currentTile: " + currentTile.terrain);
                GameObject terrainPrefab = GetGameObjectFromTileState(currentTile.terrain);
                GameObject tile = Instantiate(terrainPrefab, transform);
                Destroy(terrainPrefab);
                float posX = j * tileSize;
                float posY = i * -tileSize;
                Vector2 currentPosition = new Vector2(posX, posY);
                tile.transform.position = currentPosition;
                RenderLayers(currentTile.layers, currentPosition);
            }
        }
        // Draw player
        GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        GameObject playerInTile = Instantiate(player, transform);
        float playerX = level.playerPosition.y * tileSize;
        float playerY = level.playerPosition.x * -tileSize;
        Vector3 playerPosition = new Vector3(playerX, playerY, -4);
        playerInTile.transform.position = playerPosition;
        Destroy(player);
        foreach (Bow bow in level.bows) {
            RenderBow(bow);
        }
    }

    private void RenderBow(Bow bow) {
        GameObject bowPrefab = Instantiate(Resources.Load("Prefabs/Bow")) as GameObject;
        GameObject bowInTile = Instantiate(bowPrefab, transform);
        float bowX = bow.position.y * tileSize;
        float bowY = bow.position.x * -tileSize;
        Vector3 playerPosition = new Vector3(bowX, bowY, -4);
        bowInTile.transform.position = playerPosition;
        int rotation = 0;
        if (bow.direction.Equals(Vector2Int.right)) {
            rotation = 1;
        } else if (bow.direction.Equals(Vector2Int.down)) {
            rotation = 2;
        } else if (bow.direction.Equals(Vector2Int.left)) {
            rotation = 3;
        }
        bowInTile.transform.Rotate(0, 0, -90 * rotation);
        Destroy(bowPrefab);
    }

    private void RenderLayers(List<Layer> layers, Vector2 currentPosition) {
        for (int i = 0; i < layers.Count; i++) {
            Layer layer = layers[i];
            // Layers need to be refactored to ScriptableObject instead of GameObject
        }
    }
    
}