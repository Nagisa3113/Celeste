
# Celeste
- in C#, Unity
- using Finite state Machine
  
## 状态
- 使用了一个较为规范的状态机框架
- 优化手感

## 人物移动
- 使用AnimationCurve,分别记录加速和减速
- 射线检测碰撞
- 协程控制冲刺等动作

## 跳跃优化
- 输入缓冲,可以在未落地时响应跳跃命令
- 使用AnimationCurve控制垂直速度
- 通过起跳前速度决定跳跃水平速度
- 使用计时器记录控制每帧速度实现蓄力跳跃

## 摄像机
- 单场景固定摄像机，在切换场景时用Smoothdamp



