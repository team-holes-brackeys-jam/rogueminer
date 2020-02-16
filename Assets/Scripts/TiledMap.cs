using System;
using System.Collections.Generic;

[Serializable]
public class TiledMap
{
    public long compressionlevel;
    public long height;
    public bool infinite;
    public List<Layer> layers;
    public long nextlayerid;
    public long nextobjectid;
    public string orientation;
    public string renderorder;
    public string tiledversion;
    public long tileheight;
    public Tileset[] tilesets;
    public long tilewidth;
    public string type;
    public double version;
    public long width;
}

[Serializable]
public class Layer
{
    public List<long> data;
    public long height;
    public long id;
    public string name;
    public long opacity;
    public string type;
    public bool visible;
    public long width;
    public long x;
    public long y;
}

[Serializable] 
public class Tileset
{
    public long firstgid;
    public string source;
}