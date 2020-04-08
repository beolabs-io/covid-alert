using System;
namespace Covid.Models.DTO
{
    public class MatchDTO
    {
        public DateTime When        { get; set; }
        public string   UserKey     { get; set; }
        public string   MatchedKey  { get; set; }
    }
}
