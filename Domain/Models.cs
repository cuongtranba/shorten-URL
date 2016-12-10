using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ShortUrl
    {
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
    }

    public class UserIp
    {
        public UserIp()
        {
            ShortUrls = new List<ShortUrl>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(36)]
        public string Ip { get; set; }
        public virtual ICollection<ShortUrl> ShortUrls { get; set; }
    }

    public class UserUrl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<ShortUrl> ShortUrls { get; set; }
    }
}
