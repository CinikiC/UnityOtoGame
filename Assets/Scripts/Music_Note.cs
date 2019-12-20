using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	private float BeatAdvance ;

	private float velocity;
	// 音乐对应的 NoteInfo  
	NoteInfo song_note;
	// 在Spawn 时 下一个被检索的 Note 的 Index
	private int[] nextSpawnIndex = new int[4];

	// 当前拍子时间 songPosInBeat 将在 note 序列中检索位置
	private int[] nextIndex = new int[4];
	// 上一个命中的 Note 在序列中的下标
	private int[] PreHitIndex = new int[4];
	// ScoreManager
	ScoreManager score_manager;
	// 玩家输入 封装体 
	PlayerInput play_input;

	ProgressDegree progress_degree;

	void Awake() {
		// 四个轨道的 三种Index 初始化
		for(int i = 0;i<4;++i){
			nextSpawnIndex[i] = 0;
			PreHitIndex[i] = -1; 
			nextIndex[i] = 0;
		}

		song_note = GameObject.Find("NoteInfo").GetComponent<NoteInfo>();
		score_manager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		play_input = GameObject.Find("Main Camera").GetComponent<PlayerInput>();
		progress_degree = GameObject.Find("ProgressDegree").GetComponent<ProgressDegree>();
		progress_degree.Load(GetComponent<AudioSource>().clip.length);
// 如果要 不build调试 就注释掉下面用这个实例加载 NoteFileName 与 MusicName五行
		// song_note.NoteFileName = SelectionMsg.Instance().NoteTxtName;
		// GetComponent<AudioSource>().clip = 
		// 			(AudioClip)Resources.Load(
		// 				"Audios/" + SelectionMsg.Instance().MusicName,
		// 				typeof(AudioClip));

		song_note.LoadNote();
	}

	// Use this for initialization
	void Start () {
		
		secPerBeat = 60f/song_note.GetBpm();
		dspTimeSong = (float)AudioSettings.dspTime;
		BeatAdvance = 4f;
		velocity = 28.0f / BeatAdvance * (1.0f * song_note.GetBpm()/60.0f) ;

		// Debug.Log(dspTimeSong);
		//开始播放音乐
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		// 更新song 的时间位置 与 节拍位置
		songPosition = (float)(AudioSettings.dspTime - dspTimeSong);
		songPosInBeat = songPosition / secPerBeat;

		// Debug.Log(songPosition);
		for(int trackind = 0;trackind<4;++trackind){
			// 判定每个轨道是否需要生成新的 音符
			if(song_note.ReadyToSpawn(trackind,nextSpawnIndex[trackind],songPosInBeat,BeatAdvance)){
				//Debug.Log("ReadyToSpawn!");
				// 实例化位置
				int posX = 0;
				switch (trackind)
				{
					case 0: posX = -9 ; break;
					case 1: posX = -3 ; break;
					case 2: posX = 3  ; break;
					case 3: posX = 9  ; break;
					default: break;
				}
				//实例化 并为其改名				
				GameObject mk_GP = Instantiate(Resources.Load("mk_GP"),
							new Vector3(posX,-10,30),Quaternion.identity)as GameObject;
				mk_GP.GetComponent<Rigidbody>().velocity = new Vector3(0,0,-velocity);
				
				// 多轨道tag , Hit 时根据tag 销毁
				mk_GP.name = trackind.ToString() +'-'+ nextSpawnIndex[trackind].ToString();	
				
				nextSpawnIndex[trackind]++;
			}
			// 判定当前Hit位置是否需要检索下一个Note
			if(song_note.ReadyToMove(trackind,nextIndex[trackind],songPosInBeat)){
				nextIndex[trackind]++;
			}
		}
		
		// 是否 按键 Hit 轨道
		bool[] HitTrackInd = new bool[4]; 
		// Input GetKey 这个操作以后需要封装到 另一个GameObject player 里统一得到 输入 信息. ezio 191113
		// HitTrackInd[0] = Input.GetKey(KeyCode.D)?true:false;
		// HitTrackInd[1] = Input.GetKey(KeyCode.F)?true:false;
		// HitTrackInd[2] = Input.GetKey(KeyCode.J)?true:false;
		// HitTrackInd[3] = Input.GetKey(KeyCode.K)?true:false;
		play_input.DoseHit(HitTrackInd);
		
		for(int trackind = 0;trackind < 4;++trackind){
			if(HitTrackInd[trackind]){
				//根据 当前拍子时间计算 最近的Note下标
				int nearestInd = song_note.GetNearestIndex(trackind,nextIndex[trackind],songPosInBeat);
				if(PreHitIndex[trackind] == nearestInd){
					// 命中过则不在计算
				}
				else{
					float TimeDif = song_note.HitDif(trackind,nearestInd,songPosInBeat);
					if( score_manager.DoesGetHit(TimeDif)){
						// 更改PreHitIndex信息 , 并立刻销毁之前实例化的对象
						PreHitIndex[trackind] = nearestInd;
	
						GameObject obj = GameObject.Find(trackind.ToString() 
									+'-'+PreHitIndex[trackind].ToString());
						DestroyImmediate(obj);
							
						// Debug.Log("Hit !!");
					}
				}
			}
		}

		progress_degree.UpdateText(GetComponent<AudioSource>().time);
		if(progress_degree.IsOver()){
			
			//给下一个场景传递得分
			SelectionMsg.Instance().cntPerfect = score_manager.GetCntPerfect();
			SelectionMsg.Instance().cntGood = score_manager.GetCntGood();
			SelectionMsg.Instance().cntMiss = score_manager.GetCntMiss();
			SelectionMsg.Instance().score = score_manager.GetTotalScore();
			
			float ScoreMole = 1f * score_manager.GetTotalScore();
			float ScoreDeno = 1f * score_manager.Perfect_Score *song_note.GetCntTotNote();
			//为分数定级
			if( ScoreMole / ScoreDeno >0.95f){
				SelectionMsg.Instance().scoreDegree = 0;
			}else if( ScoreMole / ScoreDeno >0.8f){
				SelectionMsg.Instance().scoreDegree = 1;
			}else{
				SelectionMsg.Instance().scoreDegree = 2;
			}
			StartCoroutine(Delay());
		}
	}


	IEnumerator Delay()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadSceneAsync(5);
	}
}
