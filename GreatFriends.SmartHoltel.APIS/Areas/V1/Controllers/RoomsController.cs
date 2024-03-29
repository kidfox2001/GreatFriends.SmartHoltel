﻿using GreatFriends.SmartHoltel.APIS.Areas.V1.Models;
using GreatFriends.SmartHoltel.Models;
using GreatFriends.SmartHoltel.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatFriends.SmartHoltel.APIS.Areas.V1.Controllers
{
    //todo เติม <IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers> ที่ csproj ที่ project นี้
    //todo แก้ที่ alert

    //[ApiExplorerSettings(GroupName = "Custom Group Name")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/V1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly AppDb db;

        public RoomsController(AppDb db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetAllAsync([FromHeader(Name = "X-RoomType")] string roomType = "")
        {
            //var items =  await db.Rooms.Include(r => r.Type)
            //            .OrderBy(x => x.Id)
            //            .Select(x=> RoomResponse.FromModel(x))
            //            .ToListAsync();

            var q = db.Rooms.Include(r => r.Type)
                    .OrderBy(x => x.Id)
                    .Select(x => x);

            if (!string.IsNullOrEmpty(roomType))
            {
                q = q.Where(x => x.TypeCode == roomType);
            }

            var items = await q.ToListAsync();

            var output = items.ConvertAll(RoomResponse.FromModel);

            return output;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(RoomResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<RoomResponse> GetById(int id)
        {
            var item = db.Rooms.Find(id);

            if (item == null)
            {
                return NotFound(new ProblemDetails()
                {
                    Title = $"Room id : {id} not found"
                });
            }

            return RoomResponse.FromModel(item);
        }

        /// <summary>
        /// สร้าง
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<RoomResponse> Create(RoomRequest item)
        {

            var itemRoomType = db.RoomTypes.Find(item.RoomType);

            if (itemRoomType == null)
            {
                return NotFound(new ProblemDetails()
                {
                    Title = $"RoomType code : {item.RoomType}  not found"
                });
            }

            Room newRoom = item.ToModel();

            db.Add(newRoom);
            db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newRoom.Id }, RoomResponse.FromModel(newRoom));
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateAsync(int id, RoomRequest item)
        {
            if (id != item.Id)
            {
                return BadRequest("Invalid ID");
            }

            var itemRoom = await db.Rooms.FindAsync(id);

            if (itemRoom == null)
            {
                return NotFound(new ProblemDetails() { Title = $"Room id : {id.ToString()}  not found" });
            }


            itemRoom.AreaSquareMeters = item.AreaSquareMeters;
            itemRoom.FloorNo = item.FloorNo;
            itemRoom.TypeCode = item.RoomType;


            await db.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<RoomResponse>> DeleteAsync(int id)
        {
            var item = await db.Rooms.FindAsync(id);

            if (item == null)
            {
                return NotFound(new ProblemDetails { Title = $"Room id {id} not found" });
            }

            db.Remove(item);
            await db.SaveChangesAsync();

            return RoomResponse.FromModel(item);
        }


    }
}
