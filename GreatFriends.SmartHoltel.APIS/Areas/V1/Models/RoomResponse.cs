using GreatFriends.SmartHoltel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatFriends.SmartHoltel.APIS.Areas.V1.Models
{
    public class RoomResponse
    {

        public int Id { get; set; }

        public double AreaSquareMeters { get; set; } = 0.0;

        public int FloorNo { get; set; }


        // Navigation Propertys
        public RoomTypeResponse RoomType { get; set; }

        public static RoomResponse FromModel(Room m)
        {
            return new RoomResponse()
            {
                Id = m.Id,
                AreaSquareMeters = m.AreaSquareMeters,
                FloorNo = m.FloorNo,
                RoomType = RoomTypeResponse.FromModel(m.Type)
            };

        }


    }
}
