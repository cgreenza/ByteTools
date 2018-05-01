# Greensoft.Encodings

A library to convert between byte arrays and strings using hex (base 16) encoding i.e. each byte is represented by 2 hex characters.

The implementation makes use of an efficient table lookup that is approx 16x faster than using System.BitConverter:
```c#
System.BitConverter.ToString(x).Replace("-", "")
```

## Examples in C#

To convert from hex string to byte array:
```c#
var b = HexEncoding.GetBytes("2FA5"); // returns new byte [] { 0x2f, 0xa5 }
```

To convert from byte array to hex string:
```c#
var s = HexEncoding.GetString(new byte[] { 0x2f, 0xa5 }); // returns "2FA5"
```

## NuGet Package
Available from NuGet:
https://www.nuget.org/packages/Greensoft.Encodings
