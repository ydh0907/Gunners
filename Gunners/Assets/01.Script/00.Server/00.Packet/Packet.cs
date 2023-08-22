using UnityEngine;

public class Packet{
    public Packet(int state, Vector2 pos, Vector2 dir){
        this.state = state;
        this.pos = pos;
        this.dir = dir;
    }
    public int state;
    public Vector2 pos;
    public Vector2 dir;
}