using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Layer {
    public GameObject prefab; // TODO: change this with ScriptableObject
    public LayerPriority priority;
    public List<Event> events;

    public Layer(GameObject prefab, LayerPriority priority, List<Event> events) {
        this.prefab = prefab;
        this.priority = priority;
        this.events = events;
    }

    public Layer(GameObject prefab, LayerPriority priority) {
        this.prefab = prefab;
        this.priority = priority;
        this.events = new List<Event>();
    }

    public Layer(GameObject prefab) {
        this.prefab = prefab;
        this.priority = LayerPriority.Default;
        this.events = new List<Event>();
    }

    public void FireEvents(EventType eventType) {
        this.events.ForEach((Event) => {
            if (Event.type == eventType) {
                Event.Fire();
            }
        });
    }
    
}