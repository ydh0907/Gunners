using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type{
    Player,
    Bullet,
}
public enum State{
    Init,
    Live,
    Disable
}
public class Socket{
    public Type type;
    public State state;
    public Vector2 pos;
    public Vector2 dir;
    public float speed;
    public int unique;
}
