#pragma once
#include "pch.h"
#include "CalculatorInterop.g.h"
#include "../NativeCalculator.h"

namespace winrt::CalculatorToolbox::Services::Interop::implementation
{
    /// <summary>
    /// C++/WinRT 互通層
    /// 提供 C# 可呼叫的 WinRT 元件介面
    /// </summary>
    struct CalculatorInterop : CalculatorInteropT<CalculatorInterop>
    {
        CalculatorInterop() = default;

        // 基本運算
        static double Add(double a, double b);
        static double Subtract(double a, double b);
        static double Multiply(double a, double b);
        static double Divide(double a, double b);

        // 進階運算
        static double Modulo(double a, double b);
        static double Power(double base, double exponent);
        static double SquareRoot(double value);
        static double Square(double value);
        static double Reciprocal(double value);
        static double Negate(double value);

        // 科學計算功能
        static double Sin(double angle, bool isRadians);
        static double Cos(double angle, bool isRadians);
        static double Tan(double angle, bool isRadians);
        static double Asin(double value);
        static double Acos(double value);
        static double Atan(double value);
        static double Log10(double value);
        static double Ln(double value);
        static double Exp(double value);
        static double Factorial(int32_t n);

        // 程式設計人員計算器功能
        static int64_t BitwiseAnd(int64_t a, int64_t b);
        static int64_t BitwiseOr(int64_t a, int64_t b);
        static int64_t BitwiseXor(int64_t a, int64_t b);
        static int64_t BitwiseNot(int64_t value);
        static int64_t LeftShift(int64_t value, int32_t shift);
        static int64_t RightShift(int64_t value, int32_t shift);
        static int64_t RotateLeft(int64_t value, int32_t rotation, int32_t bitSize);
        static int64_t RotateRight(int64_t value, int32_t rotation, int32_t bitSize);

        // 基數轉換
        static int64_t ConvertFromBase(hstring const& value, int32_t fromBase);
        static hstring ConvertToBase(int64_t value, int32_t toBase);

        // 位元遮罩操作
        static int64_t ApplyByteMask(int64_t value, int32_t byteSize);
    };
}

namespace winrt::CalculatorToolbox::Services::Interop::factory_implementation
{
    struct CalculatorInterop : CalculatorInteropT<CalculatorInterop, implementation::CalculatorInterop>
    {
    };
}