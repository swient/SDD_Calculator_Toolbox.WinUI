#include "pch.h"
#include "CalculatorInterop.h"
#include "CalculatorInterop.g.cpp"

using namespace CalculatorToolbox::Services;

namespace winrt::CalculatorToolbox::Services::Interop::implementation
{
    // 基本運算
    double CalculatorInterop::Add(double a, double b)
    {
        return NativeCalculator::Add(a, b);
    }

    double CalculatorInterop::Subtract(double a, double b)
    {
        return NativeCalculator::Subtract(a, b);
    }

    double CalculatorInterop::Multiply(double a, double b)
    {
        return NativeCalculator::Multiply(a, b);
    }

    double CalculatorInterop::Divide(double a, double b)
    {
        return NativeCalculator::Divide(a, b);
    }

    // 進階運算
    double CalculatorInterop::Modulo(double a, double b)
    {
        return NativeCalculator::Modulo(a, b);
    }

    double CalculatorInterop::Power(double base, double exponent)
    {
        return NativeCalculator::Power(base, exponent);
    }

    double CalculatorInterop::SquareRoot(double value)
    {
        return NativeCalculator::SquareRoot(value);
    }

    double CalculatorInterop::Square(double value)
    {
        return NativeCalculator::Square(value);
    }

    double CalculatorInterop::Reciprocal(double value)
    {
        return NativeCalculator::Reciprocal(value);
    }

    double CalculatorInterop::Negate(double value)
    {
        return NativeCalculator::Negate(value);
    }

    // 科學計算功能
    double CalculatorInterop::Sin(double angle, bool isRadians)
    {
        return NativeCalculator::Sin(angle, isRadians);
    }

    double CalculatorInterop::Cos(double angle, bool isRadians)
    {
        return NativeCalculator::Cos(angle, isRadians);
    }

    double CalculatorInterop::Tan(double angle, bool isRadians)
    {
        return NativeCalculator::Tan(angle, isRadians);
    }

    double CalculatorInterop::Asin(double value)
    {
        return NativeCalculator::Asin(value);
    }

    double CalculatorInterop::Acos(double value)
    {
        return NativeCalculator::Acos(value);
    }

    double CalculatorInterop::Atan(double value)
    {
        return NativeCalculator::Atan(value);
    }

    double CalculatorInterop::Log10(double value)
    {
        return NativeCalculator::Log10(value);
    }

    double CalculatorInterop::Ln(double value)
    {
        return NativeCalculator::Ln(value);
    }

    double CalculatorInterop::Exp(double value)
    {
        return NativeCalculator::Exp(value);
    }

    double CalculatorInterop::Factorial(int32_t n)
    {
        return NativeCalculator::Factorial(n);
    }

    // 程式設計人員計算器功能
    int64_t CalculatorInterop::BitwiseAnd(int64_t a, int64_t b)
    {
        return NativeCalculator::BitwiseAnd(a, b);
    }

    int64_t CalculatorInterop::BitwiseOr(int64_t a, int64_t b)
    {
        return NativeCalculator::BitwiseOr(a, b);
    }

    int64_t CalculatorInterop::BitwiseXor(int64_t a, int64_t b)
    {
        return NativeCalculator::BitwiseXor(a, b);
    }

    int64_t CalculatorInterop::BitwiseNot(int64_t value)
    {
        return NativeCalculator::BitwiseNot(value);
    }

    int64_t CalculatorInterop::LeftShift(int64_t value, int32_t shift)
    {
        return NativeCalculator::LeftShift(value, shift);
    }

    int64_t CalculatorInterop::RightShift(int64_t value, int32_t shift)
    {
        return NativeCalculator::RightShift(value, shift);
    }

    int64_t CalculatorInterop::RotateLeft(int64_t value, int32_t rotation, int32_t bitSize)
    {
        return NativeCalculator::RotateLeft(value, rotation, bitSize);
    }

    int64_t CalculatorInterop::RotateRight(int64_t value, int32_t rotation, int32_t bitSize)
    {
        return NativeCalculator::RotateRight(value, rotation, bitSize);
    }

    // 基數轉換
    int64_t CalculatorInterop::ConvertFromBase(hstring const& value, int32_t fromBase)
    {
        std::wstring wstr{ value };
        std::string str(wstr.begin(), wstr.end());
        return NativeCalculator::ConvertFromBase(str, fromBase);
    }

    hstring CalculatorInterop::ConvertToBase(int64_t value, int32_t toBase)
    {
        std::string result = NativeCalculator::ConvertToBase(value, toBase);
        return hstring(std::wstring(result.begin(), result.end()));
    }

    // 位元遮罩操作
    int64_t CalculatorInterop::ApplyByteMask(int64_t value, int32_t byteSize)
    {
        return NativeCalculator::ApplyByteMask(value, byteSize);
    }
}