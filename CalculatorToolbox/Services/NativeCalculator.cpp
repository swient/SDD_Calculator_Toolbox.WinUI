#include "pch.h"
#include "NativeCalculator.h"
#include <cmath>
#include <stdexcept>

namespace CalculatorToolbox::Services
{
    // 基本運算實作
    double NativeCalculator::Add(double a, double b)
    {
        return a + b;
    }

    double NativeCalculator::Subtract(double a, double b)
    {
        return a - b;
    }

    double NativeCalculator::Multiply(double a, double b)
    {
        return a * b;
    }

    double NativeCalculator::Divide(double a, double b)
    {
        if (b == 0.0)
        {
            throw std::invalid_argument("Division by zero");
        }
        return a / b;
    }

    // 進階運算實作
    double NativeCalculator::Modulo(double a, double b)
    {
        if (b == 0.0)
        {
            throw std::invalid_argument("Modulo by zero");
        }
        return std::fmod(a, b);
    }

    double NativeCalculator::Power(double base, double exponent)
    {
        return std::pow(base, exponent);
    }

    double NativeCalculator::SquareRoot(double value)
    {
        if (value < 0.0)
        {
            throw std::invalid_argument("Cannot calculate square root of negative number");
        }
        return std::sqrt(value);
    }

    double NativeCalculator::Square(double value)
    {
        return value * value;
    }

    double NativeCalculator::Reciprocal(double value)
    {
        if (value == 0.0)
        {
            throw std::invalid_argument("Cannot calculate reciprocal of zero");
        }
        return 1.0 / value;
    }

    double NativeCalculator::Negate(double value)
    {
        return -value;
    }

    // 科學計算功能
    double NativeCalculator::Sin(double angle, bool isRadians)
    {
        double radians = isRadians ? angle : angle * M_PI / 180.0;
        return std::sin(radians);
    }

    double NativeCalculator::Cos(double angle, bool isRadians)
    {
        double radians = isRadians ? angle : angle * M_PI / 180.0;
        return std::cos(radians);
    }

    double NativeCalculator::Tan(double angle, bool isRadians)
    {
        double radians = isRadians ? angle : angle * M_PI / 180.0;
        return std::tan(radians);
    }

    double NativeCalculator::Asin(double value)
    {
        if (value < -1.0 || value > 1.0)
        {
            throw std::invalid_argument("Asin domain error: value must be in [-1, 1]");
        }
        return std::asin(value);
    }

    double NativeCalculator::Acos(double value)
    {
        if (value < -1.0 || value > 1.0)
        {
            throw std::invalid_argument("Acos domain error: value must be in [-1, 1]");
        }
        return std::acos(value);
    }

    double NativeCalculator::Atan(double value)
    {
        return std::atan(value);
    }

    double NativeCalculator::Log10(double value)
    {
        if (value <= 0.0)
        {
            throw std::invalid_argument("Logarithm domain error: value must be positive");
        }
        return std::log10(value);
    }

    double NativeCalculator::Ln(double value)
    {
        if (value <= 0.0)
        {
            throw std::invalid_argument("Natural logarithm domain error: value must be positive");
        }
        return std::log(value);
    }

    double NativeCalculator::Exp(double value)
    {
        return std::exp(value);
    }

    double NativeCalculator::Factorial(int n)
    {
        if (n < 0)
        {
            throw std::invalid_argument("Factorial domain error: value must be non-negative");
        }
        if (n > 170)
        {
            throw std::overflow_error("Factorial overflow: value too large");
        }
        if (n == 0 || n == 1)
        {
            return 1.0;
        }
        
        double result = 1.0;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }

    // 程式設計人員計算器功能
    int64_t NativeCalculator::BitwiseAnd(int64_t a, int64_t b)
    {
        return a & b;
    }

    int64_t NativeCalculator::BitwiseOr(int64_t a, int64_t b)
    {
        return a | b;
    }

    int64_t NativeCalculator::BitwiseXor(int64_t a, int64_t b)
    {
        return a ^ b;
    }

    int64_t NativeCalculator::BitwiseNot(int64_t value)
    {
        return ~value;
    }

    int64_t NativeCalculator::LeftShift(int64_t value, int shift)
    {
        if (shift < 0 || shift >= 64)
        {
            throw std::invalid_argument("Shift amount must be between 0 and 63");
        }
        return value << shift;
    }

    int64_t NativeCalculator::RightShift(int64_t value, int shift)
    {
        if (shift < 0 || shift >= 64)
        {
            throw std::invalid_argument("Shift amount must be between 0 and 63");
        }
        return value >> shift;
    }

    int64_t NativeCalculator::RotateLeft(int64_t value, int rotation, int bitSize)
    {
        if (bitSize != 8 && bitSize != 16 && bitSize != 32 && bitSize != 64)
        {
            throw std::invalid_argument("Bit size must be 8, 16, 32, or 64");
        }
        
        rotation %= bitSize;
        if (rotation == 0) return value;
        
        int64_t mask = (bitSize == 64) ? -1LL : ((1LL << bitSize) - 1);
        value &= mask;
        
        return ((value << rotation) | (value >> (bitSize - rotation))) & mask;
    }

    int64_t NativeCalculator::RotateRight(int64_t value, int rotation, int bitSize)
    {
        if (bitSize != 8 && bitSize != 16 && bitSize != 32 && bitSize != 64)
        {
            throw std::invalid_argument("Bit size must be 8, 16, 32, or 64");
        }
        
        rotation %= bitSize;
        if (rotation == 0) return value;
        
        int64_t mask = (bitSize == 64) ? -1LL : ((1LL << bitSize) - 1);
        value &= mask;
        
        return ((value >> rotation) | (value << (bitSize - rotation))) & mask;
    }

    // 基數轉換輔助函數
    int64_t NativeCalculator::ConvertFromBase(const std::string& value, int fromBase)
    {
        if (fromBase < 2 || fromBase > 36)
        {
            throw std::invalid_argument("Base must be between 2 and 36");
        }
        
        try
        {
            return std::stoll(value, nullptr, fromBase);
        }
        catch (const std::exception&)
        {
            throw std::invalid_argument("Invalid number format for the specified base");
        }
    }

    std::string NativeCalculator::ConvertToBase(int64_t value, int toBase)
    {
        if (toBase < 2 || toBase > 36)
        {
            throw std::invalid_argument("Base must be between 2 and 36");
        }
        
        if (value == 0)
        {
            return "0";
        }
        
        const char* digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        std::string result;
        bool isNegative = value < 0;
        int64_t absValue = isNegative ? -value : value;
        
        while (absValue > 0)
        {
            result = digits[absValue % toBase] + result;
            absValue /= toBase;
        }
        
        if (isNegative)
        {
            result = "-" + result;
        }
        
        return result;
    }

    // 位元遮罩操作
    int64_t NativeCalculator::ApplyByteMask(int64_t value, int byteSize)
    {
        switch (byteSize)
        {
        case 8:
            return value & 0xFF;
        case 16:
            return value & 0xFFFF;
        case 32:
            return value & 0xFFFFFFFF;
        case 64:
            return value;
        default:
            throw std::invalid_argument("Byte size must be 8, 16, 32, or 64");
        }
    }
}