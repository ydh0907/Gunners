using Do.Net;
using System;

namespace GunnersServer.Packets
{
    public enum PacketID
    {
        C_ConnectPacket, // 처음 접속
        C_DisconnectPacket, // 접속 해제
        C_MatchingPacket, // 매칭 시작
        C_ReadyPacket, // 준비 정보
        C_MovePacket, // 이동 정보
        C_FirePacket, // 발사 정보
        C_HitPacket, // 맞췄는지 확인
        C_GameEndPacket, // 게임이 끝났는지

        S_ConnectPacket, // 번호 부여
        S_MatchedPacket, // 매칭 성공
        S_GameStartPacket, // 둘다 준비되면 시작
        S_MovePacket, // 이동 정보
        S_FirePacket, // 발사 정보
        S_HitPacket, // 적중 했으면 보내기 (모두에게)
        S_GameEndPacket, // 둘다 끝났으면 방 폭파
    }
}