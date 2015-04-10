using SmartRoom.Database.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRoom.Database.Tables
{
    public class YoutubeLiveDetail {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int CourseId{get;set;}
        public virtual Course Course { get; set; }

        public String BroadcastId { get; set; } // unique id assigned to broadcast
        public String BroadcastKind { get; set; } // youtube#liveBroadcast
        public String BroadcastTitle { get; set; }
        public String BroadcastDescription { get; set; }
        public DateTime BroadcastScheduledStartTime { get; set; } // Start time
        public DateTime BroadcastScheduledEndTime { get; set; } // End time
        public String BroadcastStatus { get; set; } // private, public, unlisted
        public String BroadcastchannelId { get; set; } // Id Youtube uses to identify the channel that is publishing
        public String BroadcastlifeCycleStatus { get; set; } // abandoned, complete, created, live, liveStarting, ready, reclaimed, revoked, testStarting, testing
        public String BroadcastEmbededhtml { get; set; } // Embededhtml for Youtube video player
        public String StreamId { get; set; } // unique id assigned to stream
        public String StreamKind { get; set; } // youtube#liveStream
        public String StreamName { get; set; } // RTMP stream name Youtube assigns to a video stream
        public String StreamStatus { get; set; }
        public String StreamSnippetTitle { get; set; }
        public String StreamCDNFormat { get; set; } // Format 1080p, 240p, 360p, 480p, 720p
        public String StreamCDNIngestionType { get; set; } // RTMP only option
        public String StreamCDNIngestionUrl { get; set; } // RTMPS address to stream too
        public String StreamcontentclosedCaptionsIngestionUrl { get; set; } // URL for closed captions
    }
}

