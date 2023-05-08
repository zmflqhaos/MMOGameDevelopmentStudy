using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	ServerSession _session = new ServerSession();

	//MyPlayer에서 받은 ArraySegment를 서버세션으로 전송한다.
	public void Send(ArraySegment<byte> sendBuff)
	{
		_session.Send(sendBuff);
	}

    void Start()
    {
		//클라이언트를 호스트에 연결한다.
		string host = Dns.GetHostName();
		IPHostEntry ipHost = Dns.GetHostEntry(host);
		IPAddress ipAddr = ipHost.AddressList[0];
		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

		Connector connector = new Connector();

		connector.Connect(endPoint,
			() => { return _session; },
			1);
	}

    void Update()
    {
		//list안에 있는 모두가 HandlePacket을 실행하게한다
		List<IPacket> list = PacketQueue.Instance.PopAll();
		foreach (IPacket packet in list)
			PacketManager.Instance.HandlePacket(_session, packet);
    }
}
