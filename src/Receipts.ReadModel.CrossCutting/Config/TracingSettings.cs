﻿namespace Receipts.ReadModel.CrossCutting
{
    public class TracingSettings
    {
        public string? Environment { get; set; }
        public string? Uri { get; set; }
        public int Port { get; set; }
    }
}