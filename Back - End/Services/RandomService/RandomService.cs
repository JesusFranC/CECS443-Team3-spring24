﻿using System.Security.Cryptography;

namespace Team3.ThePollProject.Services;

public class RandomService : IRandomService
{
    public  uint GenerateUnsignedInt()
    {
        uint randUint = BitConverter.ToUInt32(RandomNumberGenerator.GetBytes(32));

        return randUint;
    }

    public  int GenerateSignedInt()
    {
        return RandomNumberGenerator.GetInt32(Int32.MaxValue);
    }

    public string GenerateRandomString(int size)
    {
        char character;
        string result ="";
        int characterValue;

        for (int i = 0; i < size; i++)
        {
            int Case = RandomNumberGenerator.GetInt32(0, 3);
            switch (Case)
            {
                case 0:
                    characterValue = RandomNumberGenerator.GetInt32(48,58);
                    character = (char)characterValue;
                    result += character;
                    break;
                case 1:
                    characterValue = RandomNumberGenerator.GetInt32(65, 91);
                    character = (char)characterValue;
                    result += character;
                    break;
                case 2:
                    characterValue = RandomNumberGenerator.GetInt32(97, 123);
                    character = (char)characterValue;
                    result += character;
                    break;
            }
        }
        return result;
    }
}
