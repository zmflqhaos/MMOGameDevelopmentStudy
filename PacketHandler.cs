using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class PacketHandler
{
	public static void S_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
	{
		//게임에 처음 접속하면 실행한다.
		S_BroadcastEnterGame pkt = packet as S_BroadcastEnterGame;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.EnterGame(pkt);
	}

	public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
	{
		//게임을 종료할 때 실행한다.
		S_BroadcastLeaveGame pkt = packet as S_BroadcastLeaveGame;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.LeaveGame(pkt);
	}

	public static void S_PlayerListHandler(PacketSession session, IPacket packet)
	{
		//새로운 플레이어를 생성한다.
		S_PlayerList pkt = packet as S_PlayerList;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.Add(pkt);
	}

	public static void S_BroadcastMoveHandler(PacketSession session, IPacket packet)
	{
		//플레이어를 이동시킨다.
		S_BroadcastMove pkt = packet as S_BroadcastMove;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.Move(pkt);
	}
}
