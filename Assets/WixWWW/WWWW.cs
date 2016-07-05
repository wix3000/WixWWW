using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WWWW : CustomYieldInstruction {

	public WWW www{ get; private set;}

	public WWWWState state { get; private set; }
	public string text { get { return www.text; } }
	public Texture2D texture{ get { return www.texture; } }
	public Texture2D textureNonReadable{ get { return www.textureNonReadable; } }
	public string error { get { return www.error; } }

	public override bool keepWaiting {
		get {
			return CheckWWWState();
		}
	}

	DateTime? limitTime;
	Action<WWWW> onSuccess;
	Action<WWWW> onError;
	Action<WWWW> onTimeup;
	Action<WWWW> onDone;

	// 建構子

	private WWWW(){
	}

	private WWWW(WWW www){
		this.www = www;
	}

	public WWWW(string url){
		www = new WWW (url);
	}

	public WWWW(string url, WWWForm form){
		www = new WWW (url, form);
	}

	public WWWW(string url, byte[] postData){
		www = new WWW(url, postData);
	}

	public WWWW(string url, byte[] postData, Dictionary<string, string> headers){
		www = new WWW(url, postData, headers);
	}

	bool CheckWWWState(){
		if (www.isDone) {
			if (error == null) {
				state = WWWWState.Complete;
				if (onSuccess != null)
					onSuccess (this);
			} else {
				state = WWWWState.Error;
				if (onError != null)
					onError (this);
			}
			if (onDone != null)
				onDone (this);
			return false;
		} else if (limitTime.HasValue && DateTime.Now > limitTime.Value) {
			state = WWWWState.Timeup; 
			if (onTimeup != null)
				onTimeup (this);
			if (onDone != null)
				onDone (this);
			return false;
		}
		return true;
	}


	// 外部方法

	public WWWW SetTimeLimit(float sec){
		limitTime = DateTime.Now.AddSeconds (sec);
		return this;
	}

	public WWWW OnSuccess(Action<WWWW> callBack){
		onSuccess = callBack;
		return this;
	}

	public WWWW OnError(Action<WWWW> callBack){
		onError = callBack;
		return this;
	}

	public WWWW OnTimeup(Action<WWWW> callBack){
		onTimeup = callBack;
		return this;
	}

	public WWWW OnDone(Action<WWWW> callBack){
		onDone = callBack;
		return this;
	}

	public enum WWWWState{
		Connecting,
		Complete,
		Error,
		Timeup
	}

	public static implicit operator WWWW(WWW www) {
		return new WWWW(www);
	}
}
