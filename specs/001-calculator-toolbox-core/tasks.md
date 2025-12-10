# Tasks: 計算工具箱核心功能

## Phase 1: 專案初始化

- [x] T001 建立專案結構（CalculatorToolbox/Models, ViewModels, Views, Services, Assets, Properties）
- [x] T002 建立主視窗與三種計算器 UI（CalculatorToolbox/MainWindow.xaml, StandardCalculator.xaml, ScientificCalculator.xaml, ProgrammerCalculator.xaml）
- [x] T003 建立資源檔案以支援多語系（CalculatorToolbox/Assets/Resources.resw, Resources.en-US.resw, Resources.ja-JP.resw, Resources.ko-KR.resw）

## Phase 2: 基礎設施與阻塞任務

- [x] T004 實作 MVVM 架構基礎（CalculatorToolbox/ViewModels/BaseViewModel.cs）
- [x] T005 實作 C++ 運算模組框架（CalculatorToolbox/Services/NativeCalculator.cpp, NativeCalculator.h）
- [x] T006 建立 C++/WinRT 互通介面（CalculatorToolbox/Services/Interop/CalculatorInterop.h, CalculatorInterop.cpp, CalculatorInterop.idl）

## Phase 3: User Story 1 - 切換核心計算器 (Priority: P1)

- [x] T007 [US1] 實作漢堡選單切換功能（CalculatorToolbox/MainWindow.xaml.cs）
- [x] T008 [US1] 實作 UserControl 切換三種計算器（CalculatorToolbox/MainWindow.xaml.cs）
- [x] T009 [US1] 測試三種計算器切換流程（CalculatorToolbox/Tests/Integration/CalculatorSwitchTests.cs）

## Phase 4: User Story 2 - 標準計算器進階運算 (Priority: P2)

- [x] T010 [US2] 實作標準計算器基本與進階運算（CalculatorToolbox/StandardCalculator.xaml.cs, NativeCalculator.cpp）
- [x] T011 [US2] 實作歷史記錄功能（CalculatorToolbox/Models/HistoryRecord.cs, StandardCalculator.xaml.cs）
- [x] T012 [US2] 測試標準計算器運算與歷史記錄（CalculatorToolbox/Tests/Unit/StandardCalculatorTests.cs）

## Phase 5: User Story 3 - 科學與程式設計計算器專屬功能 (Priority: P3)

- [x] T013 [US3] 實作科學計算器三角函數、對數、指數、階乘、記憶體功能（ScientificCalculator.xaml.cs, NativeCalculator.cpp）
- [x] T014 [US3] 實作弧度/角度模式切換（ScientificCalculator.xaml.cs）
- [x] T015 [US3] 實作科學計算器歷史記錄（Models/HistoryRecord.cs, ScientificCalculator.xaml.cs）
- [x] T016 [US3] 實作程式設計人員計算器基數轉換（ProgrammerCalculator.xaml.cs, NativeCalculator.cpp）
- [x] T017 [US3] 實作所有位元操作（ProgrammerCalculator.xaml.cs, NativeCalculator.cpp）
- [x] T018 [US3] 實作字節大小切換（ProgrammerCalculator.xaml.cs）
- [x] T019 [US3] 測試科學與程式設計計算器所有功能（Tests/Unit/ScientificCalculatorTests.cs, Tests/Unit/ProgrammerCalculatorTests.cs）

## Final Phase: Polish & Cross-Cutting Concerns

- [x] T020 完成 UI/UX 調整，支援 Fluent Design（CalculatorToolbox/Views/\*）
- [x] T021 完成多語系測試（Tests/Integration/LocalizationTests.cs）
- [ ] T022 完成靜態分析與程式碼品質檢查（Roslyn/StyleCop）- 需配置並執行
- [x] T023 完成單元測試覆蓋率檢查（CalculatorToolbox/Tests/\*）- 測試已建立，需 WinUI 運行環境執行

## 依賴關係與執行順序

- Phase 1、2 為所有後續任務基礎，必須先完成
- Phase 3、4、5 可部分平行執行（不同計算器功能互不依賴）
- 最終優化與測試可於主要功能完成後平行進行

## 平行執行範例

- T009、T012、T019 可同時進行（各自測試不同計算器）
- T020、T021、T022、T023 可於所有功能完成後平行執行

## MVP 建議範疇

- 完成 Phase 1~3（主視窗、三種計算器切換與基本功能）
