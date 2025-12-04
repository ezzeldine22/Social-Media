using API.Domain.Entites;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Validation;
using Application.DTOs.FollowingDTOs;
using Microsoft.EntityFrameworkCore;
namespace Application.Services
{
    public class FollowingServices 
    {
        //private readonly SocialMediaContext Context;
       
        //public FollowingServices(DbContext dbContext)
        //{
        //   Context = dbContext;
        //}

        //public async Task<Result> FollowAsync(FollowDto dto)
        //{
        //    if(dto.FollowerId == null || dto.FollowedId == null)
        //    {
        //        return Result.Failure(new List<string> { "wrong in id of follower or followed" });
        //    }
        //    var date = DateTime.Now;
        //    var newFollow = new Follow
        //    {
        //        FollowerId = dto.FollowerId,
        //        FollowedId = dto.FollowedId,
        //        CreatedAt = date
        //    };
        //    var resFollow = await _dbContext.Follw.Add(newFollow);

        //    if (!resFollow.Succeeded) {
        //        return Result.Failure(new List<string> { "something wrong" });
        //    }

        //    return Result.success("success following");
        //}

    }
}
