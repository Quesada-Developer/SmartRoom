using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Web
{
    public class YoutubeLiveDetail {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int CourseId{get;set;}
        public virtual Course Course { get; set; }

        public String BroadcastId { get; set; }
        public String BroadcastTitle { get; set; }
        public String BroadcastDescription { get; set; }
        public DateTime BroadcastScheduledStartTime { get; set; }
        public DateTime BroadcastScheduledEndTime { get; set; }
        public String BroadcastStatus { get; set; }
        public String StreamId { get; set; }
        public String StreamTitle { get; set; }
        public String StreamKind { get; set; }
        public String StreamSnippetTitle { get; set; }
        public String StreamCDNFormat { get; set; }
        public String StreamCDNIngestionType { get; set; }
        public String StreamCDNIngestionUrl { get; set; }
        public String Embededhtml { get; set; }
    }
}

