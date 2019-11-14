using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Note 序列 的基类
public class NoteInfo : MonoBehaviour{

	// 简单地 用 List< float > 表示 Note 序列;
	private List< float > note_seq = new List<float>();
	private int note_seq_len = 0 ;

	// 加载 Note 序列 // 以后添加其他加载途径
	// 这里先写成静态
	public void LoadNote(){
		note_seq_len = 6;
		note_seq.Add(5f);
		note_seq.Add(6f);
		note_seq.Add(6.5f);
		note_seq.Add(7f);
		note_seq.Add(7.5f);
		note_seq.Add(8.5f);
	}
	



	// 根据 Hit 对应Note 下标 算出时间差
	public float HitDif(int obIndex,float HitPosInBeat){
		return Mathf.Abs(note_seq[obIndex] - HitPosInBeat);
	}

	//决定 Hit 对应最近的 Note
	public int GetNearestIndex(int nextIndex,float HitPosInBeat){
		if(nextIndex == 0)
			return nextIndex;
		else if(nextIndex >= note_seq_len)
			return nextIndex - 1;
		else{
			float leftdif = HitPosInBeat - note_seq[nextIndex - 1];
			float rightdif = note_seq[nextIndex] - HitPosInBeat;
			if(leftdif <= rightdif)
				return nextIndex - 1;
			else
				return nextIndex;
		}
	}

	// 通过 音乐节拍位置(拍子时间) 决定当前是否更新检索的 Note 的下标
	public bool ReadyToMove(int nextIndex,float songPosInBeat){
		if( nextIndex < this.note_seq_len && note_seq[nextIndex] < songPosInBeat){
			return true;
		}
		else{
			return false;
		}
	}

	// 通过 音乐节拍位置(拍子时间) 决定是否准备生成 下一个音符
	public bool ReadyToSpawn(int nextIndex,float songPosInBeat,float beatAdvance){
		if( nextIndex < this.note_seq_len && note_seq[nextIndex] < songPosInBeat + beatAdvance){
			return true;
		}
		else{
			return false;
		}
	}

}
