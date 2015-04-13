
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRoom.Web.App_Start
{
    public class YoutubeLiveDetail {


        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        /// <summary>
        /// unique id assigned to broadcast
        /// </summary>
        public String BroadcastId { get; set; }

        /// <summary>
        /// youtube#liveBroadcast
        /// </summary>
        public String BroadcastKind { get; set; } 
        public String BroadcastTitle { get; set; }
        public String BroadcastDescription { get; set; }
        
        /// <summary>
        /// Start time
        /// </summary>
        [Display(Name = "Start Time")]
        [DataType(DataType.DateTime)]
        public DateTime BroadcastScheduledStartTime { get; set; } 

        /// <summary>
        /// End Time
        /// </summary>
        [Display(Name = "End Time")]
        [DataType(DataType.DateTime)]
        public DateTime BroadcastScheduledEndTime { get; set; } 

        /// <summary>
        /// Private, Public, Unlisted
        /// </summary>
        [Display(Name = "Status")]
        public String BroadcastStatus { get; set; } 

        /// <summary>
        /// Id Youtube uses to identify the channel that is publishing
        /// </summary>
        public String BroadcastchannelId { get; set; } 

        /// <summary>
        /// abandoned, complete, created, live, liveStarting, ready, reclaimed, revoked, testStarting, testing
        /// </summary>
        [Display(Name = "Broadcast Status")]
        public String BroadcastlifeCycleStatus { get; set; } 
        
        /// <summary>
        /// Embededhtml for Youtube video player
        /// </summary>
        public String BroadcastEmbededhtml { get; set; } 

        /// <summary>
        /// unique id assigned to stream
        /// </summary>
        public String StreamId { get; set; } 

        /// <summary>
        /// youtube#liveStream
        /// </summary>
        public String StreamKind { get; set; }

        /// <summary>
        /// RTMP stream name Youtube assigns to a video stream
        /// </summary>
        [Display(Name = "Stream Name")]
        public String StreamName { get; set; } 
        public String StreamStatus { get; set; }
        public String StreamSnippetTitle { get; set; }

        /// <summary>
        /// Format 1080p, 240p, 360p, 480p, 720p
        /// </summary>
        [Display(Name = "Stream Format")]
        public String StreamCDNFormat { get; set; } 

        /// <summary>
        /// RTMP only option
        /// </summary>
        public String StreamCDNIngestionType { get; set; } 

        /// <summary>
        /// RTMPS address to stream too
        /// </summary>
        public String StreamCDNIngestionUrl { get; set; } 

        /// <summary>
        /// URL for closed captions
        /// </summary>
        public String StreamcontentclosedCaptionsIngestionUrl { get; set; } 
    }
}

