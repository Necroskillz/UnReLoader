// Guids.cs
// MUST match guids.h
using System;

namespace NecroNet.UnReLoader
{
    static class GuidList
    {
        public const string guidUnReLoaderPkgString = "5efeb50f-fe6c-40e8-86ca-d1dabc476b20";
        public const string guidUnReLoaderCmdSetString = "620e06f7-12fd-43ae-883a-891f38bee1ec";
        public const string guidToolWindowPersistanceString = "c7fdfc8a-1091-4ea6-8f4e-5a89a20c3075";

        public static readonly Guid guidUnReLoaderCmdSet = new Guid(guidUnReLoaderCmdSetString);
    };
}