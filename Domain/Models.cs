using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Domain
{
    public class ShortUrl
    {
        public ShortUrl()
        {
            CreatedAt = DateTime.UtcNow;
            RequestHistorie = new List<RequestHistory>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(200)]
        public string Url { get; set; }
        public string ShortUrlString { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsExpired { get; set; }
        public bool IsDeleted { get; set; }
        public int? UserIpId { get; set; }
        public int? UserUrlId { get; set; }
        public virtual UserIp UserIp { get; set; }
        public virtual UserUrl UserUrl { get; set; }
        public virtual ICollection<RequestHistory> RequestHistorie { get; set; }
    }

    public class UserIp
    {
        public UserIp()
        {
            ShortUrls = new List<ShortUrl>();
            CreatedAt = DateTime.UtcNow;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(36)]
        public string Ip { get; set; }
        public virtual ICollection<ShortUrl> ShortUrls { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsExpired { get; set; }

    }

    public class UserUrl
    {
        public UserUrl()
        {
            ShortUrls = new List<ShortUrl>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<ShortUrl> ShortUrls { get; set; }
    }

    public class RequestHistory
    {
        public RequestHistory()
        {
            HitAt = DateTime.UtcNow;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime HitAt { get; set; }
        public int ShortUrlId { get; set; }
        public virtual ShortUrl ShortUrl { get; set; }
        public string Country { get; set; }
        public string Browser { get; set; }
        public string Platforms { get; set; }
    }

    public class URLViewModel
    {
        public string OriginalURL { get; set; }
        public int URLId { get; set; }
        public DateTime Created { get; set; }
        public string ShortURL { get; set; }
        public int AllClick { get; set; }
    }


    [DataContract]
    public class DataPoint
    {
        public DataPoint()
        {
            

        }
        
        public DateTime DateTime { get; set; }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public int HitCount { get; set; }
        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public long TimeStamp { get; set; }

        public string Label { get; set; }
        public string LegendText { get; set; }
        public double Percent { get; set; }
    }

    public enum ReportType
    {
        ReportByLast7Days = 1,
        ReportByBrowser = 2,
        ReportByCountry = 3,
        ReportByPlatforms = 4
    }
}
