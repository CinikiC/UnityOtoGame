using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 同步 Music 与 Note
// 按拍子时间定位Note , 并根据 Note 生成音符Point
// 按拍子时间记录Hit
/*
	Note序列 和 Hit序列 都是以拍子时间记录，以 Hit Line 为参照点
	（这样利于编写 Note 拍谱 与 Hit 序列记录，下面以 拍时 代指 拍子时间的单位）
	
	由于 从 Spawn Line 到 Hit Line 需要一段距离，设为 distance（=28）,
	另外设音符运动速度 velocity(=7) ， bpm(=60).
	所以 音符需要提前 distance/velocity (seconds)时间生成，将其换算成 拍子时间
	则是 BeatAdvance = distance / velocity * (bpm/60) .

	注意： 编写 Note 的时候，第一个音符的拍子时间必须 大于 BeatAdvance ！！！
	否则 NoteInfo 类中的成员函数ReadyToSpawn() 可能会将多个音符在几帧的时间内
	同时判定为可生成。
	
	另外，如改变 bpm 时，需要同时改变 velocity , 才能保持 BeatAdvance不变。
*/
public class Music_Note : MonoBehaviour {
	// beats per minute , public 提供接口
	public float bpm;
	// 节拍时间 , 由 bpm 计算
	private float secPerBeat;

	// 音乐开始时间
	private float dspTimeSong;
	// 音乐目前时间位置，真实时间
	private float songPosition;
	// 音乐目前节拍位置 , 拍子时间。由songPosition 计算
	private float songPosInBeat;

	// 根据 Note 生成 音符 有一个拍子时间差,这个常量需要估计
	// 根据实际情况修改
	public float BeatAdvance ;

	
	// 音乐对应的 NoteInfo  
	NoteInfo song_note;
	// 在Spawn 时 下一个被检索的 Note 的 Index
	private int nextSpawnIndex;

	// 当前拍子时间 songPosInBeat 将在 note 序列中检索位置
	private int nextIndex;
	// 上一个命中的 Note 在序列中的下标
	private int PreHitIndex;
	// ScoreManager
	ScoreManager score_manager;

	// Use this for initialization
	void Start () {
		secPerBeat = 60f/bpm;
		dspTimeSong = (float)AudioSettings.dspTime;
		
		nextSpawnIndex = 0;

		PreHitIndex = -1; 
		nextIndex = 0;

		song_note = GameObject.Find("HitPlane_mid").GetComponent<NoteInfo>();
		score_manager = GameObject.Find("HitPlane_mid").GetComponent<ScoreManager>();

		song_note.LoadNote();
		// Debug.Log(dspTimeSong);
		//开始播放音乐
		//GetComponent().Play();
	}
	
	// Update is called once per frame
	void Update () {
		// 更新song 的时间位置 与 节拍位置
		songPosition = (float)(AudioSettings.dspTime - dspTimeSong);
		songPosInBeat = songPosition / secPerBeat;

		// Debug.Log(songPosition);
		// 判定是否需要生成新的 音符
		if(song_note.ReadyToSpawn(nextSpawnIndex,songPosInBeat,BeatAdvance)){
			//暂时生成
			Instantiate(Resources.Load("mk_GP"));
			nextSpawnIndex++;
		}
		// 判定当前是否需要检索下一个Note
		if(song_note.ReadyToMove(nextIndex,songPosInBeat)){
			nextIndex++;
		}

		// 判定是否按下 S 键 
		// 这个操作以后需要封装到 另一个GameObject player 里统一得到 输入 信息. ezio 191113
		if(  Input.GetKey(KeyCode.S)){
			//根据 当前拍子时间计算 最近的Note下标
			int nearestInd = song_note.GetNearestIndex(nextIndex,songPosInBeat);
			if(PreHitIndex == nearestInd){
				// 命中过则不在计算
			}
			else{
				float TimeDif = song_note.HitDif(nearestInd,songPosInBeat);
				if( score_manager.DoesGetHit(TimeDif)){
					PreHitIndex = nearestInd;
					Debug.Log("Hit !!");
				}
			}
		}
	}
}
