﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Services
{
    public interface IValidatorService
    {
        void ValidateToken(string token);
    }
}
