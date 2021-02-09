using GreatFriends.SmartHoltel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatFriends.SmartHoltel.Services.Data
{
    // -s คือการบอกตำแหน่งของ startup file
    //todo  dotnet ef migrations add update01 -s ..\GreatFriends.SmartHotel.APIs\GreatFriends.SmartHotel.APIs.csproj -o Data\Migrations
    //todo  dotnet ef database update -s ..\GreatFriends.SmartHotel.APIs\GreatFriends.SmartHotel.APIs.csproj

    public class AppDb : DbContext
    {

        public AppDb(DbContextOptions<AppDb> options) :base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

    }
}
