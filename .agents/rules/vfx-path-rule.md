# 特效路径规范 (VFX Path Rules)

在为本项目的卡牌、技能或任何游戏行为分配动画特效 (VFX) 时，**严禁自行杜撰或假设《杀戮尖塔2》引擎中存在某些特定名称的特效文件**。

## 问题背景
此前在开发过程中，为攻击卡牌分配了如 `"vfx/vfx_attack_heavy"`, `"vfx/vfx_attack_bite"`, `"vfx/vfx_attack_slash_fast"`, `"vfx/vfx_attack_blunt_fast"` 等自定义特效路径。
由于游戏本体内部其实**并不存在**这些路径，当卡牌执行至 `PlayVfx` 时，Godot 引擎在加载该不存在的场景时会返回 `Null`，进而抛出 `System.NullReferenceException` 崩溃，导致卡牌动作中断。
这种中断使得卡牌无法完成“造成伤害”、“回到弃牌堆”的生命周期结算，最终在游戏中卡死。

## 强制要求
1. **只能使用已经验证的官方特效路径**，绝对不要凭借直觉去猜测特效名称。
2. 常用的有效近战攻击特效路径如下：
   - **斩击/挥砍**：`"vfx/vfx_attack_slash"`
   - **钝击/重击**：`"vfx/vfx_attack_blunt"`
   - **雷电/法术**：`"vfx/vfx_attack_lightning"`
3. 若需要其它特效，请务必先通过反编译的 `sts2.dll` 源码验证该字符串真实存在并被官方代码使用过。
