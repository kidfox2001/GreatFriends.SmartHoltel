using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GreatFriends.SmartHoltel.Models
{
    public class Room
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public double AreaSquareMeters { get; set; } = 0.0;
        public int FloorNo { get; set; }


        // Navigation Propertys
        public virtual RoomType Type { get; set; }

    }
}
