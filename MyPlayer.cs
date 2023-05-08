using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player // PlayerID를 가지고있는 Player 상속
{
	NetworkManager _network;

	Vector3 vec = Vector3.zero;
	float speed = 5f;

	void Start()
    {
		StartCoroutine("CoSendPacket");

		//C_Move를 보내기 위해 NetworkManager를 찾는다.
		_network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
	}

    void Update()
    {
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");

		vec -= new Vector3(h, 0, v) * Time.deltaTime * speed;
		

		//Q 눌러서 바로 연결 종료
		if(Input.GetKeyDown(KeyCode.Q))
        {
			S_BroadcastLeaveGame leaveGame = new S_BroadcastLeaveGame();
			leaveGame.playerId = PlayerId;
			PlayerManager.Instance.LeaveGame(leaveGame);
        }
	}

	IEnumerator CoSendPacket()
	{
		while (true)
		{
			//일정 주기마다 C_Move를 생성해서 플레이어의 위치를 네트워크매니저에 전송한다.
			yield return new WaitForSeconds(0.25f);

			C_Move movePacket = new C_Move();
			movePacket.posX = vec.x;
			movePacket.posY = vec.y;
			movePacket.posZ = vec.z;
			_network.Send(movePacket.Write());
		}
	}
}
