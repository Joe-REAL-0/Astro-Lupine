# 🐺 星际"狼"客 · The Astro-Lupine

> A custom character mod for **Slay the Spire 2**, built with Godot + C# and the [BaseLib](https://github.com/Alchyr/BaseLib-StS2) modding framework.

**星际"狼"客**是《杀戮尖塔 2》的全新可选角色 Mod。他是一名游荡于星海与尖塔之间的灰狼兽人，以赛博朋克废土美学为设计基调，通过精密的**寄存器运算系统**碾压敌人。

---

## ✨ 特色一览

- 🔧 **寄存器系统 (Registers)** — 全新的角色专属 Buff 机制，三枚常驻寄存器控制攻击、防御与抽牌
- 📖 **读取 (Read)** — 将寄存器数值叠加到卡牌基础值之上，且不受虚弱/破甲等 Debuff 影响
- ✏️ **写入 (Write)** — 强制覆盖赋值寄存器，实现"左脚踩右脚"式的无限成长
- 🎴 **30+ 张卡牌** — 包含 12 张初始牌与 18 张普通卡池牌（持续扩展至 70-75 张）
- 🌐 **多语言支持** — 中文 / English 本地化

---

## 🎮 核心机制

### 寄存器 (Registers)

战斗开始时，角色自动获得三枚寄存器 Buff：

| 寄存器       | 初始值 | 作用         |
| ------------ | :----: | ------------ |
| 攻击寄存器 ⚔️ |   6    | 控制输出上限 |
| 防御寄存器 🛡️ |   5    | 控制生存下限 |
| 抽牌寄存器 🃏 |   2    | 控制运转效率 |

> 所有寄存器的数值最低为 **1**，不能降至 0。

### 关键词

| 关键词                      | 结算公式                                 | 说明                                   |
| --------------------------- | ---------------------------------------- | -------------------------------------- |
| **读取 (Read)**             | `最终数值 = 卡牌基础值 + 寄存器层数`     | 在所有 Buff/Debuff 结算之后再叠加      |
| **写入 (Write)**            | `寄存器层数 := 面板指定值`               | 覆盖赋值，非增减叠层                   |
| **自增/自减 (Inc/Dec)**     | `寄存器层数 += 增量`                     | 基于当前值的相对增量运算               |

### 初始卡组 (12 张)

| 卡牌     | ×  | 费用 | 效果概要                                         |
| -------- | -- | :--: | ------------------------------------------------ |
| 打击     | 5  |  1   | 0 基础伤害 + **读取**                            |
| 防御     | 5  |  1   | 0 基础格挡 + **读取**                            |
| RA++     | 1  |  1   | 攻击寄存器 **+1**（增量运算）                     |
| RD++     | 1  |  1   | 防御寄存器 **+1**（增量运算）                     |

---

## 🏗️ 项目结构

```
Astro Lupine/
├── Scripts/
│   ├── Cards/
│   │   ├── BaseAstroLupineCard.cs      # 卡牌基类（读取/写入通用逻辑）
│   │   ├── AstroLupineDynamicVars.cs   # 动态变量（描述文本数值绑定）
│   │   ├── AstroLupineKeywords.cs      # 关键词定义
│   │   ├── Starter/                    # 初始卡牌 (打击/防御/攻击++/防御++)
│   │   └── Common/                     # 普通卡池 (18 张)
│   ├── Characters/
│   │   ├── AstroLupineCharacter.cs     # 角色定义（HP/金币/牌组/遗物池）
│   │   ├── AstroLupineCardPool.cs      # 卡池注册
│   │   └── AstroLupineRelicPool.cs     # 遗物池注册
│   ├── Powers/
│   │   ├── BaseRegisterPower.cs        # 寄存器 Buff 基类
│   │   ├── AttackRegisterPower.cs      # 攻击寄存器
│   │   ├── DefenseRegisterPower.cs     # 防御寄存器
│   │   └── DrawRegisterPower.cs        # 抽牌寄存器
│   └── Relics/
│       └── AstroLupineStarterRelic.cs  # 初始遗物
├── localization/
│   ├── zhs/                            # 简体中文本地化
│   └── en-US/                          # 英文本地化 (WIP)
├── docs/
│   ├── development_plan.md             # 开发规划书
│   ├── common_card_stack.md            # 普通卡池设计文档
│   └── export_and_run_tutorial.md      # 打包与运行教程
├── AstroLupine.json                    # Mod 清单 (BaseLib)
├── AstroLupine.csproj                  # .NET 项目配置
└── project.godot                       # Godot 项目配置
```

---

## 🛠️ 技术栈

| 层级     | 技术                        |
| -------- | --------------------------- |
| 引擎     | Godot 4.5 (Forward+)       |
| 语言     | C# / .NET 9.0              |
| 模组框架 | BaseLib (NuGet: `Alchyr.Sts2.BaseLib`) |
| Patching | Harmony 2.3.3              |
| 游戏本体 | `sts2.dll` (反编译参考)     |

---

## 🚀 构建与安装

### 前置要求

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Godot 4.5+](https://godotengine.org/) (.NET / C# 版本)
- 《杀戮尖塔 2》本体 + [BaseLib](https://github.com/Alchyr/BaseLib-StS2) 已安装

### 第一步：编译 DLL

```bash
cd "Astro Lupine"
dotnet build -c Release
```

编译产物位于 `bin/Release/net9.0/AstroLupine.dll`。

### 第二步：导出 PCK

1. 用 Godot 4.5+ 打开项目
2. **项目 → 导出 → 添加 Windows Desktop 导出配置**
3. 点击 **导出 PCK/ZIP**，保存为 `AstroLupine.pck`

### 第三步：部署到游戏

将以下文件复制到游戏 Mods 目录：

```
<STS2安装目录>/Mods/AstroLupine/
├── AstroLupine.dll
├── AstroLupine.pck
└── AstroLupine.json
```

启动游戏后，在角色选择界面即可看到新角色。

---

## 🗺️ 开发路线图

- [x] **Phase 1** — 底层框架搭建：角色注册、三枚寄存器 Buff、卡牌基类
- [x] **Phase 2** — 初始测试环境：12 张初始卡牌跑通战斗循环
- [/] **Phase 3** — 扩充卡组与流派分支：实装 70-75 张完整卡池
- [ ] **Phase 4** — 美术资产与最终打磨：赛博朋克原画、UI、战斗 VFX

---

## 📁 参考资源

| 目录 | 说明 |
| --- | --- |
| `BaseLib-StS2/` | BaseLib 框架源码（git submodule，已 gitignore） |
| `sts2/` | 反编译的 `sts2.dll` 源码（仅供 API 参考，已 gitignore） |
| `docs/` | 设计文档与教程 |

---

## 📜 许可证

本项目为《杀戮尖塔 2》的社区 Mod，仅供学习和娱乐用途。所有游戏资产版权归 [MegaCrit](https://www.megacrit.com/) 所有。

---

<p align="center">
  <em>以数据为刃，以运算为盾。</em><br>
  <strong>⚡ Compute to Conquer. ⚡</strong>
</p>
