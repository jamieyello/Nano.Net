﻿using System;
using Nano.Net.Extensions;
using Xunit;
using static Nano.Net.Utils;

namespace Nano.Net.Tests
{
    public class UtilsTest
    {
        private const string Seed = "949DD42FB17350D7FDDEDFFBD44CB1D4DF977026E715E0C91C5A62FB6CA72716";
        private const string FirstPrivateKey = "A066701E0641E524662E3B7F67F98A248C300017BAA8AA0D91A95A2BCAF8D4D8";
        private const string FirstPublicKey = "EA2F47E903C768AA0D73E3810D1A1692021BB3C7DBF876138473AF7950529715";
        private const string FirstAddress = "nano_3tjhazni9juaoa8q9rw33nf3f6i45gswhpzrgrbrawxhh7a777ror9okstch";

        [Fact]
        public void HexToBytesTest()
        {
            "ABcd".HexToBytes();
            Assert.Throws<ArgumentException>(() => "ABCDE".HexToBytes()); // invalid hex string size, must be a multiple of 2
        }

        [Fact]
        public void DerivePrivateKeyTest()
        {
            Assert.Throws<ArgumentException>(() => DerivePrivateKey(string.Empty, 0));

            Assert.Equal("A066701E0641E524662E3B7F67F98A248C300017BAA8AA0D91A95A2BCAF8D4D8",
                DerivePrivateKey(Seed, 0).BytesToHex(),
                true);

            Assert.Equal("814CE3DB9F40AB48537D342E3FE34B8954A6A351FD89D0D5A5EFD2A25019FE04",
                DerivePrivateKey(Seed, 468).BytesToHex(),
                true);
        }

        [Fact]
        public void PublicKeyFromPrivateKeyTest()
        {
            Assert.Equal(FirstPublicKey,
                PublicKeyFromPrivateKey(FirstPrivateKey.HexToBytes()).BytesToHex(),
                true);
        }

        [Fact]
        public void AddressFromPublicKeyTest()
        {
            Assert.Equal(FirstAddress,
                AddressFromPublicKey(FirstPublicKey.HexToBytes()),
                true);
        }

        [Fact]
        public void PublicKeyFromAddressTest()
        {
            Assert.Equal(FirstPublicKey, PublicKeyFromAddress(FirstAddress).BytesToHex(), true);
        }

        [Fact]
        public void IsAddressValidTest()
        {
            Assert.False(IsAddressValid("nano_1fed6j3ihjxs5ohrk9j56smyxoj4wirc5ja4ru5spqfkpue1xnxc1hk"));
            Assert.False(IsAddressValid("nano_1aaaasfed6j3ihjxs5ohrk9j56smyxoj4wirc5ja4ru5spqfkpue1xnxc1hk"));
            Assert.False(IsAddressValid("nan_1aq4tsfed6j3ihjxs5ohrk9j56smyxoj4wirc5ja4ru5spqfkpue1xnxc1hk"));
            Assert.True(IsAddressValid("nano_1aq4tsfed6j3ihjxs5ohrk9j56smyxoj4wirc5ja4ru5spqfkpue1xnxc1hk"));
            Assert.False(IsAddressValid("ban_1aq4tsfed6j3ihjxs5ohrk9j56smyxoj4wirc5ja4ru5spqfkpue1xnxc1hk"));
            Assert.True(IsAddressValid("ban_1aq4tsfed6j3ihjxs5ohrk9j56smyxoj4wirc5ja4ru5spqfkpue1xnxc1hk", new string[] { "ban" }));
        }

        [Fact]
        public void ValidateWorkTest()
        {
            Assert.True(IsWorkValid("4E5004CA14899B8F9AABA7A76D010F73E6BAE54948912588B8C4FE0A3B558CA5", "ae51f0fe1604558c"));
            Assert.True(IsWorkValid("E1D6E04A3BC2C00A6E7F282ACC84C622E1A4C9B8BDBB087E73496CF8999E5494", "3de4af5abb7e4931"));
            Assert.False(IsWorkValid("4E5004CA14899B8F9AABA7A76D010F73E6BAE54948912588B8C4FE0A3B558CA5", "ae51f0fe1604558b"));
            Assert.False(IsWorkValid("E1D6E04A3BC2C00A6E7F282ACC84C622E1A4C9B8BDBB087E73496CF8999E5494", "3de4af5abb7e4930"));
        }
    }
}
