using System;
using System.Collections.Generic;

[Serializable]
public class Tile {
    
    public Terrain terrain;
    public List<Layer> layers;

    public Tile(Terrain terrain, List<Layer> layers) {
        this.terrain = terrain;
        this.layers = layers;
    }

    public Tile(Terrain terrain) {
        this.terrain = terrain;
        this.layers = new List<Layer>();
    }

    public void AddLayer(Layer layer) {
        this.layers.Add(layer);
    }

    public void RemoveLayer(Layer layer) {
        this.layers.Remove(layer);
    }

}