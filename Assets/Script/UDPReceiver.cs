using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using Unity.Collections;
using TMPro;

public class UDPReceiver : MonoBehaviour
{
    private SystemController systemController;
    public string UDP_LISTEN_IP;
    public int UDP_LISTEN_PORT_IS_KNOCKING;
    private bool isKnocking;
    private int material;
    private float pitch;
    private UdpClient isKnockingUdpClient;
    private Thread receiveIsKnockingThread;

	public TMP_Text BuildDebugText_isKnocking;
	public TMP_Text BuildDebugText_Material;
	public TMP_Text BuildDebugText_FixedUpdate;

	// Start is called before the first frame update
	void Start()
    {
        //systemController = GameObject.Find("System").GetComponent<SystemController>();
        systemController = SystemController.instance;

        UDP_LISTEN_IP = "172.20.10.3"; // self wifi
        //UDP_LISTEN_IP = "172.20.10.4"; // wama wifi
        //UDP_LISTEN_IP = "192.168.50.112"; // 孟 wifi
        //UDP_LISTEN_IP = "0.0.0.0";


		Debug.Log("Starting to Receive UDP Material on " + UDP_LISTEN_IP + "...");
        BuildDebugText_isKnocking.text = "Starting to Receive UDP Material on " + UDP_LISTEN_IP + "...";

		isKnocking = false;
        material = 0;
        pitch = 0f;
		UDP_LISTEN_PORT_IS_KNOCKING = 5005;
        isKnockingUdpClient = new UdpClient(UDP_LISTEN_PORT_IS_KNOCKING, AddressFamily.InterNetwork); // 監聽端口
        receiveIsKnockingThread = new Thread(new ThreadStart(ReceiveIsKnockingData));
        receiveIsKnockingThread.IsBackground = true;
        receiveIsKnockingThread.Start();
        Debug.LogWarning("Starting to Receive UDP isKnocking at port " + UDP_LISTEN_PORT_IS_KNOCKING + "... Done");
    }
	private void FixedUpdate() {
        if(systemController == null)
        {
            BuildDebugText_FixedUpdate.text = "no systemController !";
            return;
		}
        systemController.isKnocking = isKnocking;
        systemController.material = material;
        systemController.pitch = pitch;
		BuildDebugText_FixedUpdate.text = "\nend  FixedUpdate, isKnocking : " + isKnocking;
	}

	void OnDestroy() {
        if (receiveIsKnockingThread != null) {
            receiveIsKnockingThread.Abort();
        }
        isKnockingUdpClient.Close();
    }

    private void ReceiveIsKnockingData() {
        while (true) {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(UDP_LISTEN_IP), UDP_LISTEN_PORT_IS_KNOCKING);
            var receivedResults = isKnockingUdpClient.Receive(ref remoteEndPoint);
            string message = Encoding.UTF8.GetString(receivedResults);
			isKnocking = System.String.Equals(message.Split(",")[0], "Yes");
			Debug.LogWarning("Received isKnocking UDP message: " + message + " , isKnocking" + isKnocking);
			material = int.Parse(message.Split(",")[1]);
			pitch = float.Parse(message.Split(",")[2]);
			BuildDebugText_isKnocking.text = "Received isKnocking UDP message: " + isKnocking;
        }
    }
}
