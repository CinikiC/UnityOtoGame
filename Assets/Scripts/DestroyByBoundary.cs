using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
	//销毁 出边界的 game 对象，节省内存
	// Debug log:
	// 碰撞的二者  均需要 添加Collider 
	// 而Boundary 需要勾选 Is Trigger

	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
