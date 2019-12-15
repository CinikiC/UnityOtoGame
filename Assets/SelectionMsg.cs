using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMsg{
	private static SelectionMsg ins = null;

	// 关卡数据暂不封装，用文件名代替
	public string NoteTxtName;
	public string MusicName;

    private SelectionMsg()
    {
        //将构造函数置为私有
		//避免外部创建类的实例,通过Instance()来获取数据管理类的实例
    }

    public static SelectionMsg Instance()
    {
        if(ins == null)
        { 
            ins = new SelectionMsg();
        }
        return ins;
    }
}
