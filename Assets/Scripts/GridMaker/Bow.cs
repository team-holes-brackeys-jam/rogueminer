using System.Numerics;
using UnityEngine;

public class Bow {

    public Vector2Int position;
    public Vector2Int direction;

    public Bow(Vector2Int position, Vector2Int direction) {
        this.position = position;
        this.direction = direction;
    }
}