using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
public class CSVParsing : MonoBehaviour
{
	public TextAsset csvFile; // Reference of CSV file
	public Text contentArea; // Reference of contentArea where records are displayed
	private char lineSeperater = '\n'; // It defines line seperate character
	private char fieldSeperator = ','; // It defines field seperate chracter
	DataArray panelArray = new DataArray();

	DataArray solarPanel = new DataArray ();
	DataArray bioMass    = new DataArray ();
	DataArray nuclear    = new DataArray ();
	DataArray saving     = new DataArray ();
	string temp;

	class DataArray{
		private string _dataName;
		public string DataName{
			get{ return _dataName; }
			set{_dataName = value;}
		}

     	public ArrayList data = new ArrayList();
		public void setArray(int amount)
		{
			data.Capacity = amount;
		}
	};


	void Start ()
	{
		//readData ();	
		panelArray.DataName = "Panel";

		panelArray.setArray (20);
		//Debug.Log (panelArray);
		//Debug.Log (panelArray.DataName);
		panelArray.data.Add (23);
		panelArray.data.Add (3);
		panelArray.data.Add (123);
		panelArray.data.Add (23);
		panelArray.data.Add (223);
		panelArray.data.Add (-3);
		panelArray.data.Add (33);
		panelArray.data.Add (33);
		panelArray.data.Add (13);
		panelArray.data.Add (3);
		//Debug.Log (panelArray.data.Count);
		//Debug.Log (panelArray.data.Capacity);

		for(int i=0; i<panelArray.data.Count; i++){
		//	Debug.Log(panelArray.data[i]);
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
		
			ShowData ();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			DisplayArray ();
		}
	}

	void ShowData(){
		readData ();
			
	}
	// Read data from CSV file
	private void readData()
	{
		string[] records = csvFile.text.Split (lineSeperater);
		foreach (string record in records)
		{
		//string[] fields = record.Split(fieldSeperator);

			string[] fields = record.Split (new[]{','},System.StringSplitOptions.None);
			foreach(string field in fields)
			{
				if (field == "Panel") {
					solarPanel.DataName = "Solar Panel";
					temp = "Panel";
				} else if (field == "Nuclear") {
					nuclear.DataName = "Nuclear reactor";
					temp = "Nuclear";
				} else if (field == "Bio") {
					bioMass.DataName = "Bio";
					temp = "Bio";
				} else if (field == "Saving") {
					saving.DataName = "Saving";
					temp = "Saving";
				}
				if (field != "END") {
					if (temp == "Panel") {
						solarPanel.data.Add (field);
						contentArea.text += field + ", ";
					} else if (temp == "Nuclear") {
						nuclear.data.Add (field);
						contentArea.text += field + ", ";
					} else if (temp == "Bio") {
						bioMass.data.Add (field);
						contentArea.text += field + ", ";
					} else if (temp == "Saving") {
						saving.data.Add (field);
						contentArea.text += field + ", ";
					}
				} 
				else {
					contentArea.text += '\n';
				}
			}
		}
	}

	// Add data to CSV file
	public void addData()
	{
		// Following line adds data to CSV file
		//File.AppendAllText(getPath() + "/Assets/StudentData.csv",lineSeperater + rollNoInputField.text + fieldSeperator +nameInputField.text);
		// Following lines refresh the edotor and print data
		//rollNoInputField.text = "";
		//nameInputField.text = "";
		contentArea.text = "";
		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif
		readData();
	}
	// Get path for given CSV file
	private static string getPath()
	{
		#if UNITY_EDITOR
		return Application.dataPath;
		#elif UNITY_ANDROID
		return Application.persistentDataPath;// +fileName;
		#elif UNITY_IPHONE
		return GetiPhoneDocumentsPath();// +"/"+fileName;
		#else
		return Application.dataPath;// +"/"+ fileName;
		#endif
	}
		// Get the path in iOS device
	private static string GetiPhoneDocumentsPath()
	{
		string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		path = path.Substring(0, path.LastIndexOf('/'));
		return path + "/Documents";
	}

	
		void DisplayArray(){
		for (int i = 0; i < solarPanel.data.Count-1; i++) {

			Debug.Log (solarPanel.data [i]);
		}

		for (int i = 0; i < nuclear.data.Count-1; i++) {
		Debug.Log (nuclear.data [i]);
		}

		for (int i = 0; i < bioMass.data.Count-1; i++) {
		Debug.Log (bioMass.data [i]);
		}

		for (int i = 0; i < saving.data.Count-1 ; i++) {
		Debug.Log (saving.data [i]);
		}
			
		}
}


