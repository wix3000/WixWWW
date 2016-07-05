using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestSample : MonoBehaviour {

	[SerializeField]
	Slider bar;
	[SerializeField]
	Text chat, wwwwlabel;
	[SerializeField]
	InputField bas, pow;

	bool isStart;
	float barValue;

	// Use this for initialization
	void Start () {
	}

	void Update(){
		if(isStart){
			barValue += 10f * Time.deltaTime;
			bar.value = Mathf.PingPong(barValue, bar.maxValue);
		}
	}

	public void OnStart1(){
		StartCoroutine(RunBar());
	}

	IEnumerator RunBar(){
		isStart = true;
		yield return new WaitWhile(() => Input.GetMouseButton(0));
		isStart = false;
	}

	public void OnStart2(){
		StartCoroutine(RunChat());
	}

	IEnumerator RunChat(){
		chat.text = "小明成績很不好\n媽媽一口氣給他報名10家補習班\n結果段考考完 媽媽問:感覺如何\n小明說:時間有點不夠";

		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

		chat.text = "小明成績很不好\n媽媽一口氣給他報名10家補習班\n結果段考考完 媽媽問:感覺如何\n小明說:時間有點不夠\n媽媽就給他報了第11間";

		yield return null;
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

		chat.text = "";
	}

	public void OnStart3(){
		string baseValeu = (string.IsNullOrEmpty(bas.text)) ? "0" : bas.text;
		string powerValeu = (string.IsNullOrEmpty(pow.text)) ? "0" : pow.text;
		wwwwlabel.text = "";

		StartCoroutine(LinkToTest(baseValeu, powerValeu));
	}

	IEnumerator LinkToTest(string basValue, string powValue){
		yield return new WaitForSeconds(1f);
		WWWForm form = new WWWForm();
		form.AddField("value", basValue);
		form.AddField("power", powValue);

		WWWW www = new WWWW("http://54.200.100.5/carpool/power.php", form);
		yield return www;

		wwwwlabel.text = www.text;
	}
}
