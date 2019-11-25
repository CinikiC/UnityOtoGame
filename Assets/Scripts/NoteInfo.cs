using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;


// Note 序列 的基类
public class NoteInfo : MonoBehaviour{
	
	// 文件名 ， note 文件必须放在 StreamingAssets 目录下
	public string NoteFileName;

	// bpm 为 整型
	private int bpm;	
	private List < List<float > >  note_seq = new List< List<float>>();
	private int[] note_seq_len = new int[4];	

	//Note 文件 总 行 数目
	private int totLine = 0;

	// 加载本地 Note 序列 // 以后添加其他加载途径
	public void LoadNote(){
		for(int i = 0 ;i < 4; ++i ){
			note_seq_len[i] = 0;
			note_seq.Add(new List<float>{});
		}
		
		try{
			// 从streamingAssets 目录下得到 Note 文件 
			// #if UNITY_EDITOR
			// string fileAddress = Application.dataPath + "/streamingAssets/" + NoteFileName;
			// #endif
			//  var fileAddress = System.IO.Path.Combine(Application.streamingAssetsPath,NoteFileName);
			var fileAddress = System.IO.Path.Combine(Application.streamingAssetsPath,NoteFileName);
			// FileInfo fInfo0 = new FileInfo(fileAddress);
			StreamReader rd = new StreamReader(fileAddress);

			// 按行读
			string curline = rd.ReadLine();
			while(curline != null){
				CheckCurLine(curline);
				curline = rd.ReadLine();
			}
			rd.Close();
		}
		catch(IOException e){
			Debug.Log("An IOexception has been thrown!");
			Debug.Log(e.ToString());
			return;
		}

	}
	
	//
	private void CheckCurLine(string str){
		if(totLine == 0){
			if(!int.TryParse(str,out bpm))
				Debug.Log("Note txt first Line has problem.");
			totLine ++;
		}
		else{
			// 用正则读取数字
			string pattern = @"(\d*\.?\d)";
			int cnt = 0;
			int trackind = 0;
			float seqind = 0f;
			foreach (Match match in Regex.Matches(str,pattern))
			{
				if(cnt == 0) trackind = int.Parse(match.Value);
				else if(cnt == 1) seqind = float.Parse(match.Value);
				cnt ++;
			}

			// 当且仅当 是 track 对应 note 拍时 时才算真正的一行数据
			if(cnt == 2)
				if(0 < trackind && trackind <= 4){
					// // 临时增加 -4f ，弥补 Note 
					// // 之后一定要删除191125
					// if(seqind - 4f > 4f){
					// 	note_seq[trackind-1].Add(seqind - 4f);
					// 	note_seq_len[trackind-1]++;
					// }

					//尝试延后 4f
					note_seq[trackind-1].Add(seqind+8);
					note_seq_len[trackind-1]++;

					//// 原本逻辑
					// note_seq[trackind-1].Add(seqind);
					// note_seq_len[trackind-1]++;
				}
					
		}
	}

	public int GetBpm(){
		return this.bpm;
	}

	// 根据 Hit 对应轨道 的Note 下标 算出时间差
	public float HitDif(int trackInd,int obIndex,float HitPosInBeat){
		return Mathf.Abs(note_seq[trackInd][obIndex] - HitPosInBeat);
	}

	//决定 Hit 对应轨道 最近的 Note
	public int GetNearestIndex(int trackInd,int nextIndex,float HitPosInBeat){
		if(nextIndex == 0)
			return nextIndex;
		else if(nextIndex >= note_seq_len[trackInd])
			return nextIndex - 1;
		else{
			float leftdif = HitPosInBeat - note_seq[trackInd][nextIndex - 1];
			float rightdif = note_seq[trackInd][nextIndex] - HitPosInBeat;
			if(leftdif <= rightdif)
				return nextIndex - 1;
			else
				return nextIndex;
		}
	}

	// 通过 音乐节拍位置(拍子时间) 决定当前是否更新检索的 Note 的下标
	public bool ReadyToMove(int trackInd,int nextIndex,float songPosInBeat){
		if( nextIndex < this.note_seq_len[trackInd] && note_seq[trackInd][nextIndex] < songPosInBeat){
			return true;
		}
		else{
			return false;
		}
	}

	// 通过 音乐节拍位置(拍子时间) 决定是否准备生成 下一个音符
	public bool ReadyToSpawn(int trackInd,int nextIndex,float songPosInBeat,float beatAdvance){
		if( nextIndex < this.note_seq_len[trackInd] && note_seq[trackInd][nextIndex] < songPosInBeat + beatAdvance){
			return true;
		}
		else{
			return false;
		}
	}

}
