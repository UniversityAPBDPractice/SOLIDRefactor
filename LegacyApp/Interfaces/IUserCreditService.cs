using System;
using System.Collections.Generic;

namespace LegacyApp;

public interface IUserCreditService
{
    int GetCreditLimit(string lastname);
    int CalcNewCreditLimit(string priority, int prevLimit);
    int InferCreditCoeff(string priority);
}