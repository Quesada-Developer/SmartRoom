using SmartRoom.Database.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRoom.Database.Tables
{
    public class CourseYoutubeLiveDetail {
   
        [Key]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Id { get; set; }

        public int BroadcastId { get; set; }
        public String BroadcastTitle { get; set; }
        public String BroadcastDescription { get; set; }
        public DateTime BroadcastScheduledStartTime { get; set; }
        public DateTime BroadcastScheduledEndTime { get; set; }
        public String BroadcastStatus { get; set; }
        public int StreamId { get; set; }
        public String StreamTitle { get; set; }
        public String StreamKind { get; set; }
        public String StreamSnippetTitle { get; set; }
        public String StreamCDNFormat { get; set; }
        public String StreamCDNIngestionType { get; set; }

    }
}

