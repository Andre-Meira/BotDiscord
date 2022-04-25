using System;
using System.Collections.Generic;

namespace DataBaseApplication.Models
{
    public partial class LogCommand
    {
        public int Id { get; set; }
        public DateOnly Data { get; set; }
        public long IdServer { get; set; }
        public long IdChannel { get; set; }
        public long IdUser { get; set; }
        public string UserName { get; set; }
        public string Command { get; set; }
        public string ErrorCommands { get; set; }
    }
}
