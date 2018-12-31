using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using System.Xml.Serialization;

public class RetrieveXMl : MonoBehaviour {

	//[SerializeField]
	//private Leaderboard board= null;

	[SerializeField]
	private OpenDRIVE openDrive = null;
	// Use this for initialization
	void Start () {

		string sub_path = (Application.dataPath);
		string path = sub_path + "/XMLFiles/roadSignal2.xml";
		XmlSerializer serializer = new XmlSerializer(typeof(OpenDRIVE));
		string xml = File.ReadAllText(path);
		using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
		{
			openDrive = (OpenDRIVE)serializer.Deserialize(stream);
			Debug.Log (openDrive.roads[2].signals.signal.s);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
