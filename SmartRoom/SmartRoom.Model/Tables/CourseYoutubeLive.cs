using SmartRoom.Database.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRoom.Database.Tables
{
    class CourseYoutubeLive {
        
        public int CourseId { get; set; }
        public virtual Course Classrooms { get; set; }
        [Required]
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

