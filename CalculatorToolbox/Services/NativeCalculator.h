#pragma once
#include <string>
#include <cstdint>

#ifndef M_PI
#define M_PI 3.14159265358979323846
#endif

namespace CalculatorToolbox::Services
{
    /// <summary>
    /// 原生 C++ 計算器引擎
    /// 提供高性能的數學運算功能
    /// </summary>
    class NativeCalculator
    {
    public:
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
        static double Sin(double angle, bool isRadians = true);
        static double Cos(double angle, bool isRadians = true);
        static double Tan(double angle, bool isRadians = true);
        static double Asin(double value);
        static double Acos(double value);
        static double Atan(double value);
        static double Log10(double value);
        static double Ln(double value);
        static double Exp(double value);
        static double Factorial(int n);
        
        // 程式設計人員計算器功能
        static int64_t BitwiseAnd(int64_t a, int64_t b);
        static int64_t BitwiseOr(int64_t a, int64_t b);
        static int64_t BitwiseXor(int64_t a, int64_t b);
        static int64_t BitwiseNot(int64_t value);
        static int64_t LeftShift(int64_t value, int shift);
        static int64_t RightShift(int64_t value, int shift);
        static int64_t RotateLeft(int64_t value, int rotation, int bitSize);
        static int64_t RotateRight(int64_t value, int rotation, int bitSize);
        
        // 基數轉換
        static int64_t ConvertFromBase(const std::string& value, int fromBase);
        static std::string ConvertToBase(int64_t value, int toBase);
        
        // 位元遮罩操作
        static int64_t ApplyByteMask(int64_t value, int byteSize);
    };
}