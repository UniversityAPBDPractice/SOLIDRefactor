using System;

namespace LegacyApp;

public interface IAgeCalculator
{
    int GetAge(DateTime before, DateTime now);
}