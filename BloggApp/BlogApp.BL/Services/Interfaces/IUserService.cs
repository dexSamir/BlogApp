using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
namespace BlogApp.BL.Services.Interfaces;
public interface IUserService
{
}


//public static bool VerifyHashedPassword(string hashedPassword, string password)
//{
//    byte[] buffer4;
//    if (hashedPassword == null)
//    {
//        return false;
//    }
//    if (password == null)
//    {
//        throw new ArgumentNullException("password");
//    }
//    byte[] src = Convert.FromBase64String(hashedPassword);
//    if ((src.Length != 0x31) || (src[0] != 0))
//    {
//        return false;
//    }
//    byte[] dst = new byte[0x10];
//    Buffer.BlockCopy(src, 1, dst, 0, 0x10);
//    byte[] buffer3 = new byte[0x20];
//    Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
//    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
//    {
//        buffer4 = bytes.GetBytes(0x20);
//    }
//    return ByteArraysEqual(buffer3, buffer4);
//}
