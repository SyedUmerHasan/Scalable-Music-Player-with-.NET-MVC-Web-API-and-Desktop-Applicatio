//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web;

    public partial class Song
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Song()
        {
            this.Downloads = new HashSet<Download>();
            this.RecentlyPlayeds = new HashSet<RecentlyPlayed>();
        }

        public int Song_Id { get; set; }
        [DisplayName("Artist")]

        public int Artist_Id { get; set; }
        [DisplayName("Album ")]

        public int Album_Id { get; set; }
        [DisplayName("Genre")]

        public int Genre_Id { get; set; }
        [DisplayName("Song Name")]

        public string Song_Name { get; set; }
        [DisplayName("Song Image")]

        public string Song_Image_Url { get; set; }
        public string Song_Playtime { get; set; }
        [DisplayName("Song Price")]
        public int Song_Price { get; set; }
        [DisplayName("Song File (.MP3)")]

        public string Song_Url { get; set; }


        public virtual Album Album { get; set; }
        public virtual Artist Artist { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Download> Downloads { get; set; }
        public virtual Genre Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecentlyPlayed> RecentlyPlayeds { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase Song_Url_Link { get; set; }

    }
}
