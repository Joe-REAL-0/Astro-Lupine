---
trigger: always_on
---

动态变量定义规范 (Dynamic Variable Standards)
在《杀戮尖塔2》Mod开发中，特别是在处理“星际‘狼’客”的寄存器交互时，务必严格遵循以下动态变量定义规范，以确保 `AstroLupineSystemPower.cs` 能够正确捕获和处理卡牌的【读取】和【写入】效果，避免系统逻辑冲突。

### 1. 魔法变量 (Magic)
- **绝对不要直接使用原生的 `new DynamicVar("Magic", X)` 或 `new IntVar("Magic", X)`。**
- **必须使用封装好的 `MagicVar`：** 在定义通用变量（如覆写层数、病毒层数等不属于伤害、格挡、抽牌的数值）时，请统一使用本 Mod 定义的 `new MagicVar(Xm)`。这不仅提供了语法糖，更能统一代码风格，避免拼写错误。

### 2. 抽牌变量 (Cards)
对于具有抽牌效果的卡牌，必须使用对应的专有变量，以保证在卡牌被临时赋予【写入】（Write）效果时，底层能够准确地将抽牌数量写入“抽牌寄存器”。
- **带有【读取】的抽牌卡：** 必须使用本 Mod 提供的 `new AstroReadCardsVar(X)`。该变量会自动加上抽牌寄存器的值，并在底层注册为 `"Cards"`。
- **纯粹的抽牌卡（无【读取】）：** 必须使用官方原生的 `new CardsVar(X)`。
- **严禁行为：** 绝对不要使用 `Magic` 相关的变量来表示抽牌数量。否则会导致 `AstroLupineSystemPower` 无法识别，造成写入处理错误。

### 3. 伤害与防御读取
- **带有读取效果的伤害变量：** 使用 `new AstroReadDamageVar(Xm)`
- **带有读取效果的防御变量：** 使用 `new AstroReadBlockVar(Xm)`