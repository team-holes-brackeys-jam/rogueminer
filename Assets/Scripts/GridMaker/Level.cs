using System;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

[Serializable]
public class Level {

    public Tile[,] tiles;
    public Vector2Int playerPosition;
    public List<Bow> bows;

    public Level(Tile[,] tiles, Vector2Int playerPosition, List<Bow> bows) {
        this.tiles = tiles;
        this.playerPosition = playerPosition;
        this.bows = bows;
    }

    public Vector2Int GetDimensions() {
        return new Vector2Int(tiles.GetLength(0), tiles.GetLength(1));
    }

    public void FireEvents(EventType eventType) {
        foreach (Tile tile in this.tiles) {
            tile.layers.ForEach(layer => {
                layer.events.ForEach(Event => {
                    if (Event.type == eventType) {
                        Event.Fire();
                    }
                });
            });
        }
    }

    public bool IsLevelValid() {
        // TODO: add more constraints in the future
        // CONSTRAINT 1: Player spawns in an empty tile.
        return tiles[playerPosition.x, playerPosition.y].terrain == Terrain.Empty;
    }

}