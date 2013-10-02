// Guids.cs
// MUST match guids.h
using System;

namespace NecroNet.UnReLoader
{
    static class GuidList
    {
		public const string guidUnReLoaderPkgString = "6577cb74-a30d-4f2c-aa72-389733a02ae0";
        public const string guidUnReLoaderCmdSetString = "620e06f7-12fd-43ae-883a-891f38bee1ec";
        public const string guidToolWindowPersistanceString = "c7fdfc8a-1091-4ea6-8f4e-5a89a20c3075";
	    public const string guidConsoleWindow = "{C7FDFC8A-1091-4EA6-8F4E-5A89A20C3075}";
		
        public static readonly Guid guidUnReLoaderCmdSet = new Guid(guidUnReLoaderCmdSetString);
    };
}