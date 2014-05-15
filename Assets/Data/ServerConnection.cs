using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

public class ServerConnection{
	
	public UIInput username;
	public UIInput password;
	public UIButton login;

	private string loginURL = "http://mjbcdn.com/nxt/login/login.php";
	private string movesURL = "http://mjbcdn.com/nxt/login/getMoveInfo.php";

	// Use this for initialization
	void Start () {
		Debug.Log ("Here");
	}
	
	// Update is called once per frame
	IEnumerator OnClick () {
		Debug.Log ("Clicked");
		Debug.Log (username.value);

		WWWForm login = new WWWForm ();
		login.AddField ("username", username.value);
		login.AddField ("password", password.value);

		WWW submitLogin = new WWW (loginURL, login);

		yield return submitLogin;

		Debug.Log (submitLogin.text);

		Debug.Log ("AtMoves");
		WWW moves = new WWW (movesURL);
		
		yield return moves;
		
		Debug.Log (moves.text);

		JSONObject moveJSON = new JSONObject (moves.text);

		List<MoveData> moveList = new List<MoveData>{};
		for (int i = 0; i<moveJSON.Count; i++) {
						JSONObject obj = moveJSON [i];
						moveList.Add (new MoveData{
				Id = float.Parse(obj["move_id"].str),
				Name = obj["name"].str,
				ShortEffect = obj["short_effect"].str,
				LongText = obj["text"].str,
				PP = float.Parse(obj["pp"].str),
				Accuracy = float.Parse(obj["accuracy"].str),
				DamageType = obj["identifier"].str,
				Power = float.Parse(obj["power"].str)
				});
				}

		//foreach (var i in moveList) {
		//	Debug.Log (i.Id.ToString() + " : " + i.Name + " : " + i.PP.ToString());
		//		}
		//getMoves ();
		//doSocket();
		doSocket ();

	}

	IEnumerator getMoves(){
		Debug.Log ("AtMoves");
		WWW moves = new WWW (movesURL);

		yield return moves;

		Debug.Log (moves.text);

		}

	void doSocket(){
		TcpClient tcpclnt = new TcpClient();
		Debug.Log("Connecting.....");
		
		tcpclnt.Connect("pokemonnxt.com",23323);
		// use the ipaddress as in the server program
		
		Debug.Log("Connected");
		//Debug.Log("Enter the string to be transmitted : ");
		
		var str = "{\"header\":{\"PTYPE\":\"LOGIN\"},\"payload\":{\"Username\":\"User\",\"Password\":\"Password\"}}\r\n";
		var stm = tcpclnt.GetStream();
		
		var asen= new ASCIIEncoding();
		byte[] ba=asen.GetBytes(str);
		Debug.Log("Transmitting.....");
		
		stm.Write(ba,0,ba.Length);
		
		byte[] bb=new byte[100];
		int k=stm.Read(bb,0,100);
		string result = "";
		for (int i=0; i<k; i++) {
			result+=char.ConvertFromUtf32(bb[i]);
				}
			Debug.Log(result);

		var movesStr = "{\"header\":{\"PTYPE\":\"DATA_REQUEST\"},\"payload\":{\"Type\":\"ALL\"}";
		byte[] bam =asen.GetBytes(movesStr);

		stm.Write(bam,0,bam.Length);

		byte[] bbm=new byte[100];
		int km=stm.Read(bbm,0,100);

	
		for (int i=0; i<km; i++) {
			result += char.ConvertFromUtf32(bbm[i]);
				}
			Debug.Log(result);
		tcpclnt.Close();

	}
}
