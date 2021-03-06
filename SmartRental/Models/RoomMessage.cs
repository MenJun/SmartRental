//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartRental.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoomMessage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoomMessage()
        {
            this.Order = new HashSet<Order>();
        }
    
        public int RoomID { get; set; }
        public Nullable<int> HotelID { get; set; }
        public Nullable<int> MattresID { get; set; }
        public Nullable<int> RoomTypeID { get; set; }
        public string RoomName { get; set; }
        public string RoomLayout { get; set; }
        public string RoomFacility { get; set; }
        public Nullable<int> RoomCount { get; set; }
        public Nullable<decimal> PrimeCost { get; set; }
        public Nullable<decimal> RoomPrice { get; set; }
        public Nullable<bool> Boolbreakfast { get; set; }
        public Nullable<int> RoomPhotoID { get; set; }
        public Nullable<bool> Roomstate { get; set; }
        public Nullable<int> RoomRemain { get; set; }
    
        public virtual HotelManag HotelManag { get; set; }
        public virtual Mattres Mattres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual RoomPhoto RoomPhoto { get; set; }
    }
}
