# UnityOtoGame
(This is a project for classes) An music game based on Unity. 

## 版本20191125
已经完成游戏所有基本逻辑，现在需要增加的功能是：选歌界面，结算界面，游戏中的得分效果，游戏进度显示。是否增加网络下载功能待定。

下一步先完成 游戏中 得分效果，游戏进度显示。此二者比较简单的，得分效果需要美工给UI。另外，结算界面与得分效果等美工给图给资源。

另外技术层面上，对于封装 玩家输入操作 成为一个游戏对象的工作还没有开始，这一部分是日后可能移植其他平台的基础。同时还有NoteInfo加载Note文件也面临不同平台的改写需求。

### 游玩测试须知
键盘 D F J K ，分别对应从左至右四个轨道的击打。关于命中 Perfect Good 的判定时间延迟，待测试。

Note 与 音乐同步的问题逻辑上已经确定，但仍需要集体进行测试。

如果需要更换Note File 与 音乐资源，请Import音乐资源(mp3文件)于 Assets目录下，Note文件于streamingAssets目录下，并于MainWeb 场景下更换 Main Camera 的组件Audio Source 的AudioClip的内容为 指定的音乐资源，更换 NoteInfo的 Note File Name 为Note文件名(含后缀)。

另外，已经做好的场景请勿随意改动 GameObject 的位置与组件内容，如有需要及意见请在群中提出。