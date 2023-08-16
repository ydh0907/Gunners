using UnityEngine;

public enum GunType{
    a,b,c,d,e,f,g,h,i
}
public enum State{
    Live,
    Fire,
    Gun,
    Destroy
}
public class IPacket{
    public State state;
}
public class Packet{
    public short unique;
    public IPacket packet;
}
public class LivePacket : IPacket{
    public LivePacket(){
        state = State.Live;
    }
    public LivePacket(Vector2 pos, Vector2 dir){
        state = State.Live;
        this.pos = pos;
        this.dir = dir;
    }
    public Vector2 pos;
    public Vector2 dir;
}
public class FirePacket : IPacket{
    public FirePacket(){
        state = State.Fire;
    }
    public FirePacket(Vector2 pos, Vector2 dir){
        state = State.Fire;
        this.pos = pos;
        this.dir = dir;
    }
    public Vector2 pos;
    public Vector2 dir;
}
public class GunPacket : IPacket{
    public GunPacket(){
        state = State.Gun;
    }
    public GunPacket(GunType gunType){
        state = State.Gun;
        this.gunType = gunType;
    }
    public GunType gunType;
}
public class DestroyPacket : IPacket{
    public DestroyPacket(){
        state = State.Destroy;
    }
}
