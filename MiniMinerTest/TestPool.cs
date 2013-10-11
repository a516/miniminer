using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMiner;

namespace MiniMinerTest
{
    public class TestPool : Pool
    {
        public TestPool(string login)
            : base(login)
        {
        }

        protected override string InvokeMethod(string method, string paramString = null)
        {
            return
                "jason {\"error\": null, \"id\": 0, \"result\": {\"hash1\": \"00000000000000000000000000000000000000000000000000000000000000000000008000000000000000000000000000000000000000000000000000010000\", \"data\": \"00000002a239c6bf13785854a0687bb06b026c4b55b138843c5ea2ad0000000000000000ad526866cc8fb56f66d073a9d6f3ec2e89f6dfd26b27a2160f74f57227e8f73a5258530b1916b0ca00000000000000800000000000000000000000000000000000000000000000000000000000000000000000000000000080020000\", \"target\": \"0000000000000000000000000000000000000000000000000000ffff00000000\", \"midstate\": \"5336366fa6ed490f5af90837ccfbbf27077e4fc12a4a14f2ad9b7a232609fbb6\"}}";
        }

        public override bool SendShare(string paddedData, uint nonce)
        {

            switch (nonce)
            {
                case 100000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199f860100",
                        paddedData);
                    break;
                case 200000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193f0d0300",
                        paddedData);
                    break;
                case 300000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619df930400",
                        paddedData);
                    break;
                case 400000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197f1a0600",
                        paddedData);
                    break;
                case 500000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191fa10700",
                        paddedData);
                    break;
                case 600000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bf270900",
                        paddedData);
                    break;
                case 700000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195fae0a00",
                        paddedData);
                    break;
                case 800000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619ff340c00",
                        paddedData);
                    break;
                case 900000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199fbb0d00",
                        paddedData);
                    break;
                case 1000000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193f420f00",
                        paddedData);
                    break;
                case 1100000:
                    Assert.AreEqual(
                        "02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619dfc81000",
                        paddedData);
                    break;
                case 1200000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197f4f1200",
                                           paddedData);
                    break;

                case 1300000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191fd61300",
                                           paddedData);
                    break;

                case 1400000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bf5c1500",
                                           paddedData);
                    break;

                case 1500000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195fe31600",
                                           paddedData);
                    break;

                case 1600000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619ff691800",
                                           paddedData);
                    break;

                case 1700000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199ff01900",
                                           paddedData);
                    break;

                case 1800000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193f771b00",
                                           paddedData);
                    break;

                case 1900000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619dffd1c00",
                                           paddedData);
                    break;

                case 2000000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197f841e00",
                                           paddedData);
                    break;

                case 2100000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191f0b2000",
                                           paddedData);
                    break;

                case 2200000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bf912100",
                                           paddedData);
                    break;

                case 2300000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195f182300",
                                           paddedData);
                    break;

                case 2400000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619ff9e2400",
                                           paddedData);
                    break;

                case 2500000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199f252600",
                                           paddedData);
                    break;

                case 2600000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193fac2700",
                                           paddedData);
                    break;

                case 2700000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619df322900",
                                           paddedData);
                    break;

                case 2800000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197fb92a00",
                                           paddedData);
                    break;

                case 2900000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191f402c00",
                                           paddedData);
                    break;

                case 3000000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bfc62d00",
                                           paddedData);
                    break;

                case 3100000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195f4d2f00",
                                           paddedData);
                    break;

                case 3200000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619ffd33000",
                                           paddedData);
                    break;

                case 3300000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199f5a3200",
                                           paddedData);
                    break;

                case 3400000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193fe13300",
                                           paddedData);
                    break;

                case 3500000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619df673500",
                                           paddedData);
                    break;

                case 3600000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197fee3600",
                                           paddedData);
                    break;

                case 3700000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191f753800",
                                           paddedData);
                    break;

                case 3800000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bffb3900",
                                           paddedData);
                    break;

                case 3900000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195f823b00",
                                           paddedData);
                    break;

                case 4000000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619ff083d00",
                                           paddedData);
                    break;

                case 4100000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199f8f3e00",
                                           paddedData);
                    break;

                case 4200000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193f164000",
                                           paddedData);
                    break;

                case 4300000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619df9c4100",
                                           paddedData);
                    break;

                case 4400000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197f234300",
                                           paddedData);
                    break;

                case 4500000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191faa4400",
                                           paddedData);
                    break;

                case 4600000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bf304600",
                                           paddedData);
                    break;

                case 4700000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195fb74700",
                                           paddedData);
                    break;

                case 4800000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619ff3d4900",
                                           paddedData);
                    break;

                case 4900000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199fc44a00",
                                           paddedData);
                    break;

                case 5000000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193f4b4c00",
                                           paddedData);
                    break;

                case 5100000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619dfd14d00",
                                           paddedData);
                    break;

                case 5200000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197f584f00",
                                           paddedData);
                    break;

                case 5300000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191fdf5000",
                                           paddedData);
                    break;

                case 5400000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bf655200",
                                           paddedData);
                    break;

                case 5500000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195fec5300",
                                           paddedData);
                    break;

                case 5600000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619ff725500",
                                           paddedData);
                    break;

                case 5700000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199ff95600",
                                           paddedData);
                    break;

                case 5800000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193f805800",
                                           paddedData);
                    break;

                case 5900000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619df065a00",
                                           paddedData);
                    break;

                case 6000000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197f8d5b00",
                                           paddedData);
                    break;

                case 6100000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191f145d00",
                                           paddedData);
                    break;

                case 6200000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bf9a5e00",
                                           paddedData);
                    break;

                case 6300000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195f216000",
                                           paddedData);
                    break;

                case 6400000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619ffa76100",
                                           paddedData);
                    break;

                case 6500000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016199f2e6300",
                                           paddedData);
                    break;

                case 6600000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016193fb56400",
                                           paddedData);
                    break;

                case 6700000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619df3b6600",
                                           paddedData);
                    break;

                case 6800000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016197fc26700",
                                           paddedData);
                    break;

                case 6900000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016191f496900",
                                           paddedData);
                    break;

                case 7000000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab01619bfcf6a00",
                                           paddedData);
                    break;

                case 7100000:
                    Assert.AreEqual("02000000bfc639a254587813b07b68a04b6c026b8438b155ada25e3c0000000000000000666852ad6fb58fcca973d0662eecf3d6d2dff68916a2276b72f5740f3af7e8270b535852cab016195f566c00",
                                           paddedData);
                    break;
                default:
                    Assert.AreEqual(false, true);
                    break;
            }

            return true;
        }
    }
}
