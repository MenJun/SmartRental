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
    
    public partial class HotelPhoto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelPhoto()
        {
            this.HotelManag = new HashSet<HotelManag>();
        }
    
        public int HotelPhotoID { get; set; }
        public string Hotelphoto1 { get; set; }
        public string Hotelphoto2 { get; set; }
        public string Hotelphoto3 { get; set; }
        public string Hotelphoto4 { get; set; }
        public string Hotelphoto5 { get; set; }
        public string Hotelphoto6 { get; set; }
        public string Hotelphoto7 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelManag> HotelManag { get; set; }
    }
}
