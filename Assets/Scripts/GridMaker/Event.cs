using System;

[Serializable]
public class Event {

    public EventType type;
    private readonly Action _action;

    public Event(EventType type, Action action) {
        this.type = type;
        this._action = action;
    }

    public void Fire() {
        this._action();
    }
    
}