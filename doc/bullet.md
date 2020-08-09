# 子弹
子弹 = 轨迹 + 运行状态

## 轨迹
轨迹是一元参数方程，输入时间t，输出位置pos和旋转朝向rot。

## 运行状态
运行状态描述了子弹位于哪个轨迹、处于轨迹的什么位置。包括轨迹实例参数和子弹实例参数。

- ProjectileState
  - TrajectoryParams
    - rotation
    - position
  - BulletParams
    - speed
    - lifetime